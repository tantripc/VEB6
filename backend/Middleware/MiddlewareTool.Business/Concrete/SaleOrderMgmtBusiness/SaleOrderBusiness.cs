using AutoMapper;
using Ionic.Zip;
using MiddlewareTool.Business.Interface;
using MiddlewareTool.Common;
using MiddlewareTool.Dto;
using MiddlewareTool.Entities;
using MiddlewareTool.Repository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Reflection;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using static MiddlewareTool.Common.AppValue;
using static MiddlewareTool.Dto.ProductMgmtDto;
using static MiddlewareTool.Dto.SaleDto;
using static MiddlewareTool.Dto.StoreMgmtDto;

namespace MiddlewareTool.Business.Concrete
{
    public class SaleOrderBusiness : BaseBusiness, ISaleOrderBusiness
    {
        private readonly IProductBusiness _productBusiness;
        private readonly ISaleOrderRefundBusiness _saleOrderRefundBusiness;
        private readonly IStoreBusiness _storeBusiness;

        public SaleOrderBusiness(IUnitOfWork unitOfWork, IProductBusiness productBusiness, ISaleOrderRefundBusiness saleOrderRefundBusiness, IStoreBusiness storeBusiness) : base(unitOfWork)
        {
            _productBusiness = productBusiness;
            _saleOrderRefundBusiness = saleOrderRefundBusiness;
            _storeBusiness = storeBusiness;
        }

        public async Task<bool> DeleteAsync(SaleOrderDto dto)
        {
            var result = true;
            var msg = "";
            var _appEventResult = AppSystemLog.EventResult.Fail;
            string _transData = $"ERROR! Don't Delete Order ID {dto.Id} by user: {dto.UpdateBy}.";
            using (var trans = this.UnitOfWork.BeginTransaction())
            {
                try
                {

                    var entity = await this.UnitOfWork.GetAllNoTracking<Headers1>()
                    .FirstOrDefaultAsync(x => x.Id == dto.Id && x.ActiveFlag == STATUS_ACTIVE);

                    if (entity.StatusID == (byte)SaleOrderStatuses.Rejected || entity.StatusID == (byte)SaleOrderStatuses.Updated)
                    {
                        entity.UpdateBy = dto.UpdateBy;
                        entity.UpdateDate = DateTime.Now;

                        result = await this.UnitOfWork.DeleteAsync(entity);
                        if (result)
                        {
                            dto = Mapper.Map<SaleOrderDto>(entity);
                            dto.ActionType = SaleOrderAction.Delete.ToString();
                            _appEventResult = AppSystemLog.EventResult.Success;
                            _transData = "";
                            trans.Commit();
                        }
                        else
                            this.UnitOfWork.Rollback(trans);
                    }
                }

                catch (Exception ex)
                {
                    this.UnitOfWork.Rollback(trans);
                    result = false;
                    LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
                    _transData += " " + ex.StackTrace + "---" + ex.Message;
                }
            }
            dto.Comment = _transData;
            InsertHistory(dto, (int)AppSystemLog.Action.Delete, (int)_appEventResult);
            return result;
        }
        public async Task<SaleOrderDto> GetAsync(Guid id)
        {
            try
            {
                var entity = await this.UnitOfWork.GetAllNoTracking<Headers1>()
                    .Include(x => x.Business)
                    .Include(x => x.Items1)
                    .Include(x => x.Invoices1)
                    .Include(x => x.RefundHeaders1)
                    .FirstOrDefaultAsync(x => x.Id == id && x.ActiveFlag == STATUS_ACTIVE);
                entity.Items1 = entity.Items1.OrderBy(x => x.LineNumber).ToList();
                return Mapper.Map<SaleOrderDto>(entity);
            }
            catch (Exception ex)
            {
                LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
            }
            return null;
        }
        public async Task<bool> checkRefund(Guid id)
        {
            try
            {
                var rfs = await _saleOrderRefundBusiness.GetAllBySaleOrderIdAsync(id);
                var order = await GetAsync(id);
                var itemList = order.Items.ToList();
                var refundList = rfs.Where(x => x.StatusID == SaleOrderStatuses.Invoiced).ToList();
                if (!refundList.Any()) { return true; }
                foreach (var item in itemList)
                {
                    if (item.Quantity > refundList.Where(r => r.StatusID == SaleOrderStatuses.Invoiced && r.Id != item.HeaderId).SelectMany(rf => rf.Items).Where(z => z.Sku == item.Sku).Sum(y => y.Quantity))
                    {
                        return true;
                    }
                }
                return false;

            }
            catch (Exception ex)
            {
                LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
                return false;
            }
        }
        public bool CheckView(Guid id, string userId)
        {
            try
            {
                var exist = this.UnitOfWork.GetAllNoTracking<Headers1>()
                    .Any(x => x.Id == id && x.ActiveFlag == STATUS_ACTIVE && x.CreateBy == userId);
                return exist;
            }
            catch (Exception ex)
            {
                LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
            }
            return false;
        }
        public async Task<Tuple<int, List<SaleOrderDto>>> GetPagingAsync(SaleOrderFilterDto filter, bool isAdmin, string userName)
        {
            List<SaleOrderDto> dto = new List<SaleOrderDto>();
            int totalItem = 0;
            try
            {
                var iquery = this.UnitOfWork.GetAllNoTracking<Headers1>()
                    .Where(x => x.ActiveFlag != STATUS_DELETE);
                if (!filter.HasAllPermission)
                    iquery = iquery.Where(x => x.CreateBy == filter.CreatedBy);

                if (!isAdmin)
                {
                    var userStores = this.UnitOfWork.GetAllNoTracking<UserStore>().Where(x => x.ActiveFlag == STATUS_ACTIVE && x.UserName == userName).Select(x => x.StoreCode).ToList();
                    if (!(userStores?.Count > 0))
                        userStores.Add("NotAdmin");
                    iquery = iquery.Where(x => userStores.Contains(x.StoreCode));
                }

                iquery = iquery.Include(x => x.Business)
                    .Include(x => x.Invoices1)
                    .Include(x => x.RefundHeaders1)
                    .Include(x => x.Items1);
                if (!string.IsNullOrEmpty(filter.Keyword))
                {
                    var keyword = filter.Keyword.Trim();
                    decimal.TryParse(keyword, out decimal d_keyword);
                    iquery = iquery.Where(x => x.Description.Contains(keyword)
                    || x.OrderNumber == keyword
                    || x.Items1.Any(item => item.Sku == keyword
                                         || item.Name.Contains(keyword)
                        )
                    || x.Invoices1.Any(invoice => invoice.InvoiceNumber == keyword
                                        || invoice.InvoiceReceiveNumber == keyword
                                        || invoice.InvoiceID == d_keyword)
                    );
                }
                if (!string.IsNullOrEmpty(filter.OrderNumber))
                {
                    iquery = iquery.Where(x => x.OrderNumber.Equals(filter.OrderNumber));
                }
                if (!string.IsNullOrEmpty(filter.StoreCode))
                {
                    iquery = iquery.Where(x => x.StoreCode.Equals(filter.StoreCode));
                }
                if (filter.BusinessId.HasValue)
                {
                    iquery = iquery.Where(x => x.BusinessId.Equals(filter.BusinessId.Value));
                }
                if (filter.StatusId.HasValue)
                {
                    iquery = iquery.Where(x => x.StatusID.Equals(filter.StatusId.Value));
                }
                if (filter.FromDate.HasValue && filter.ToDate.HasValue)
                {
                    var fromDate = filter.FromDate.Value.Date;
                    var toDate = filter.ToDate.Value.Date.AddDays(1).AddMilliseconds(-1);
                    iquery = iquery.Where(x => x.ReceiptDate >= fromDate && x.ReceiptDate <= toDate
                    );
                }

                else if (filter.FromDate.HasValue)
                {
                    var fromDate = filter.FromDate.Value.Date;
                    iquery = iquery.Where(x => x.ReceiptDate >= fromDate
                    );
                }
                else if (filter.ToDate.HasValue)
                {
                    var toDate = filter.ToDate.Value.AddDays(1).AddMilliseconds(-1);
                    iquery = iquery.Where(x => x.ReceiptDate <= toDate
                    );
                }
                if (filter.Refunded.HasValue)
                {
                    iquery = iquery.Where(x => x.RefundHeaders1.Any(re => re.ActiveFlag == STATUS_ACTIVE) == filter.Refunded);
                }
                totalItem = await iquery.CountAsync();
                var entities = await iquery
                    .OrderByDescending(x => x.ReceiptDate)
                    .ThenByDescending(x => x.CreateDate)
                    .ThenBy(x => x.Description)
                    .Skip((filter.PageIndex - 1) * filter.PageSize)
                    .Take(filter.PageSize)
                    .ToListAsync();
                dto = Mapper.Map<List<SaleOrderDto>>(entities);
            }
            catch (Exception ex)
            {
                LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
            }
            return new Tuple<int, List<SaleOrderDto>>(totalItem, dto);
        }
        public async Task<Tuple<int, List<SaleOrderExportDto>>> GetExportAsync(SaleOrderFilterDto filter, bool isAdmin, string userName)
        {
            List<SaleOrderExportDto> dto = new List<SaleOrderExportDto>();
            try
            {
                var iquery = this.UnitOfWork.GetAllNoTracking<Headers1>()
                    .Where(x => x.ActiveFlag != STATUS_DELETE);
                if (!filter.HasAllPermission)
                    iquery = iquery.Where(x => x.CreateBy == filter.CreatedBy);
                if (!isAdmin)
                {
                    var userStores = this.UnitOfWork.GetAllNoTracking<UserStore>().Where(x => x.ActiveFlag == STATUS_ACTIVE && x.UserName == userName).Select(x => x.StoreCode).ToList();
                    if (!(userStores?.Count > 0))
                        userStores.Add("NotAdmin");
                    iquery = iquery.Where(x => userStores.Contains(x.StoreCode));
                }

                iquery = iquery.Include(x => x.Invoices1)
                    .Include(x => x.Items1);
                if (!string.IsNullOrEmpty(filter.Keyword))
                {
                    var keyword = filter.Keyword.Trim();
                    decimal.TryParse(keyword, out decimal d_keyword);
                    iquery = iquery.Where(x => x.Description.Contains(keyword)
                    || x.OrderNumber == keyword
                    || x.Items1.Any(item => item.Sku == keyword
                                         || item.Name.Contains(keyword)
                        )
                    || x.Invoices1.Any(invoice => invoice.InvoiceNumber == keyword
                                        || invoice.InvoiceReceiveNumber == keyword
                                        || invoice.InvoiceID == d_keyword)
                    );
                }
                if (!string.IsNullOrEmpty(filter.OrderNumber))
                {
                    iquery = iquery.Where(x => x.OrderNumber.Equals(filter.OrderNumber));
                }
                if (!string.IsNullOrEmpty(filter.StoreCode))
                {
                    iquery = iquery.Where(x => x.StoreCode.Equals(filter.StoreCode));
                }
                if (!string.IsNullOrEmpty(filter.StoreCode))
                {
                    iquery = iquery.Where(x => x.StoreCode.Equals(filter.StoreCode));
                }
                if (filter.BusinessId.HasValue)
                {
                    iquery = iquery.Where(x => x.BusinessId.Equals(filter.BusinessId.Value));
                }
                if (filter.StatusId.HasValue)
                {
                    iquery = iquery.Where(x => x.StatusID.Equals(filter.StatusId.Value));
                }
                if (filter.FromDate.HasValue && filter.ToDate.HasValue)
                {
                    var fromDate = filter.FromDate.Value.Date;
                    var toDate = filter.ToDate.Value.Date.AddDays(1).AddMilliseconds(-1);
                    iquery = iquery.Where(x => x.ReceiptDate >= fromDate && x.ReceiptDate <= toDate
                    );
                }
                else if (filter.FromDate.HasValue)
                {
                    var fromDate = filter.FromDate.Value.Date;
                    iquery = iquery.Where(x => x.ReceiptDate >= fromDate
                    );
                }
                else if (filter.ToDate.HasValue)
                {
                    var toDate = filter.ToDate.Value.AddDays(1).AddMilliseconds(-1);
                    iquery = iquery.Where(x => x.ReceiptDate <= toDate
                    );
                }
                dto = iquery
                    .OrderByDescending(x => x.ReceiptDate)
                    .ThenByDescending(x => x.CreateDate)
                    .ThenBy(x => x.Description)
                    .AsEnumerable()
                    .Select(x => new SaleOrderExportDto
                    {
                        ReceiptNumber = x.OrderNumber,
                        ReceiptDate = x.ReceiptDate.ToString("dd/MM/yyyy"),
                        Store = x.StoreCode,
                        Customer = x.CustomerName,
                        TotalAmountWithVAT = x.TotalAmountWithVAT > 0 ? x.TotalAmountWithVAT.ToString() : "",
                        TotalAmountWithoutVAT = x.TotalAmountWithoutVAT > 0 ? x.TotalAmountWithoutVAT.ToString() : "",
                        TotalVATAmount = x.TotalVATAmount > 0 ? x.TotalVATAmount.ToString() : "",
                        InvoiceNumber = x.Invoices1.Any() ? x.Invoices1.FirstOrDefault().InvoiceNumber : "",
                        InvoiceIssuedDate = x.Invoices1.Any() ? x.Invoices1.FirstOrDefault().InvoiceIssuedDate : "",
                    })
                    .ToList();
                if (!dto.Any())
                    dto.Add(new SaleOrderExportDto());
            }
            catch (Exception ex)
            {
                LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
            }
            return new Tuple<int, List<SaleOrderExportDto>>(0, dto);
        }
        public async Task<List<SaleOrderCompactDto>> GetOrderNumbersAsync(SaleOrderFilterDto filter, bool isAdmin, string userName)
        {
            List<SaleOrderCompactDto> dto = new List<SaleOrderCompactDto>();
            try
            {
                var iquery = this.UnitOfWork.GetAllNoTracking<Headers1>()
                    .Include(x => x.RefundHeaders1)
                    .Where(x => x.ActiveFlag != STATUS_DELETE);
                if (!filter.HasAllPermission)
                    iquery = iquery.Where(x => x.CreateBy == filter.CreatedBy);
                if (!isAdmin)
                {
                    var userStores = this.UnitOfWork.GetAllNoTracking<UserStore>().Where(x => x.ActiveFlag == STATUS_ACTIVE && x.UserName == userName).Select(x => x.StoreCode).ToList();
                    if (!(userStores?.Count > 0))
                        userStores.Add("NotAdmin");
                    iquery = iquery.Where(x => userStores.Contains(x.StoreCode));
                }

                if (!string.IsNullOrEmpty(filter.Keyword))
                {
                    var keyword = filter.Keyword.Trim();
                    iquery = iquery.Where(x => x.OrderNumber.Contains(keyword));
                }
                if (filter.StatusId.HasValue)
                {
                    iquery = iquery.Where(x => x.StatusID == filter.StatusId);
                }
                if (filter.FromDate.HasValue && filter.ToDate.HasValue)
                {
                    var fromDate = filter.FromDate.Value.Date;
                    var toDate = filter.ToDate.Value.Date.AddDays(1).AddMilliseconds(-1);
                    iquery = iquery.Where(x => x.ReceiptDate >= fromDate && x.ReceiptDate <= toDate
                    );
                }
                else if (filter.FromDate.HasValue)
                {
                    var fromDate = filter.FromDate.Value.Date;
                    iquery = iquery.Where(x => x.ReceiptDate >= fromDate
                    );
                }
                else if (filter.ToDate.HasValue)
                {
                    var toDate = filter.ToDate.Value.AddDays(1).AddMilliseconds(-1);
                    iquery = iquery.Where(x => x.ReceiptDate <= toDate
                    );
                }
                if (filter.Refunded.HasValue)
                {
                    var headers = await iquery.ToListAsync();
                    var filteredHeaders = new List<Headers1>();

                    foreach (var item in headers)
                    {
                        var canRefund = await checkRefund(item.Id);
                        if (canRefund != filter.Refunded)
                        {
                            filteredHeaders.Add(item);
                        }
                    }

                    iquery = filteredHeaders.AsQueryable();
                }
                dto = iquery.OrderBy(x => x.OrderNumber)
                    .Select(x => new SaleOrderCompactDto()
                    {
                        Id = x.Id,
                        OrderNumber = x.StoreCode + " - " + x.OrderNumber
                    }).ToList();
            }
            catch (Exception ex)
            {
                LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
            }
            return dto;
        }
        public bool InsertUploadFile(UploadFileDto dto)
        {
            try
            {
                var entity = Mapper.Map<UploadFile>(dto);
                return this.UnitOfWork.Insert(entity) != null;
            }
            catch (Exception ex)
            {
                LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
            }
            return false;
        }
        public async Task<UploadFileDto> GetUploadFileAsync(Guid uploadId)
        {
            try
            {
                return await this.UnitOfWork.GetItemAsync<UploadFileDto, UploadFile>(x => x.Id == uploadId);
            }
            catch (Exception ex)
            {
                LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
            }
            return null;
        }
        public async Task<Tuple<bool, string>> InsertListAsync(List<SaleOrderDto> dtos)
        {
            var result = true;
            var msg = "";
            SaleOrderDto currentDto = null;

            using (var trans = this.UnitOfWork.BeginTransaction())
            {
                try
                {
                    foreach (var dto in dtos)
                    {
                        dto.SetDefaultValueInsert();
                        if (dto.Items.Any())
                        {
                            currentDto = dto;
                            dto.StatusID = (byte)SaleOrderStatuses.Updated;
                            var entity = Mapper.Map<Headers1>(dto);
                            entity = this.UnitOfWork.Insert(entity);
                            result = entity != null;
                            if (result)
                            {
                                InsertHistory(dto, (int)AppSystemLog.Action.Insert, 1);
                            }
                            else
                            {
                                msg = "Error when importing Sheet: " + dto.Description;
                                break;
                            }
                        }
                    }
                    if (result)
                        trans.Commit();
                    else
                        this.UnitOfWork.Rollback(trans);

                }
                catch (Exception ex)
                {
                    this.UnitOfWork.Rollback(trans);
                    LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
                    result = false;
                    msg = ex.StackTrace + "---" + ex.Message;
                }
            }
            return new Tuple<bool, string>(result, msg);
        }

        #region Không Có cột giá trong file template
        public async Task<Tuple<bool, SaleOrderDto>> UpdateAsync(SaleOrderDto dto)
        {
            var result = false;
            var _valid = true;
            AppSystemLog.Action _actionType = AppSystemLog.Action.Update;
            var trans = UnitOfWork.BeginTransaction();
            try
            {
                var entity = this.UnitOfWork.GetAllNoTracking<Headers1>()
                    .Include(x => x.Business)
                    .SingleOrDefault(x => x.Id == dto.Id);
                entity.UpdateDate = DateTime.Now;
                entity.UpdateBy = dto.UpdateBy;
                switch (dto.ActionType)
                {
                    case "SendRequest":
                        _actionType = AppSystemLog.Action.SendRequest;

                        entity.StatusID = (byte)SaleOrderStatuses.Waiting;
                        result = await this.UnitOfWork.UpdateAsync(entity, new List<System.Linq.Expressions.Expression<Func<Headers1, object>>>() { x => x.UpdateDate, x => x.StatusID });
                        break;
                    case "Approve":
                        _actionType = AppSystemLog.Action.Approve;

                        entity.StatusID = (byte)SaleOrderStatuses.Approved;
                        result = await this.UnitOfWork.UpdateAsync(entity, new List<System.Linq.Expressions.Expression<Func<Headers1, object>>>() { x => x.UpdateDate, x => x.UpdateBy, x => x.StatusID });
                        break;
                    case "Reject":
                        _actionType = AppSystemLog.Action.Reject;

                        entity.StatusID = (byte)SaleOrderStatuses.Rejected;
                        result = await this.UnitOfWork.UpdateAsync(entity, new List<System.Linq.Expressions.Expression<Func<Headers1, object>>>() { x => x.UpdateDate, x => x.UpdateBy, x => x.StatusID });
                        break;
                    case "Invoice":
                        _actionType = AppSystemLog.Action.Invoice;

                        #region Re-validate
                        _valid = ReValidate(ref dto);

                        entity.TotalVATAmount = (decimal)dto.TotalVATAmount;
                        entity.TotalAmountWithVAT = (decimal)dto.TotalAmountWithVAT;
                        entity.TotalAmountWithoutVAT = (decimal)dto.TotalAmountWithoutVAT;
                        entity.ErrorMess = dto.ErrorMess;
                        result = await this.UnitOfWork.UpdateToListAsync<SaleOrderItemDto, Items1>(dto.Items);
                        if (result)
                            result = await this.UnitOfWork.UpdateAsync(entity, new List<Expression<Func<Headers1, object>>>() { x => x.UpdateDate, x => x.UpdateBy, x => x.OrderNumber, x => x.TotalVATAmount, x => x.TotalAmountWithVAT, x => x.TotalAmountWithoutVAT, x => x.ErrorMess });
                        this.UnitOfWork.Commit(trans);
                        trans = this.UnitOfWork.BeginTransaction();
                        #endregion

                        entity.StatusID = (byte)SaleOrderStatuses.Invoiced;
                        entity.OrderNumber = GeneralOrderNumber(dto.StoreCode);
                        if (string.IsNullOrEmpty(entity.OrderNumber))
                        {
                            dto.Comment = "Error when generating OrderNumber";
                            result = false;
                        }
                        else
                        {
                            dto.OrderNumber = entity.OrderNumber;
                            if (_valid)
                            {
                                #region Gọi API TS    
                                dto.Business = Mapper.Map<BusinessDto>(entity.Business);
                                Invoices1 invoice = null;
                                if (!dto.ManualInvoice)
                                    invoice = IssuedInvoice(dto);
                                else
                                {
                                    invoice = new Invoices1
                                    {
                                        InvoiceKey = Guid.NewGuid(),
                                        InvoiceID = 0,
                                        StoreCode = dto.StoreCode,
                                        VatCode = dto.Business.TaxCode,
                                        InvoiceTemplateCode = "1",
                                        InvoiceSeries = "K24TAA",
                                        InvoiceNumber = "ManualInvoice",
                                        InvoiceIssuedDate = DateTime.Now.ToString("yyyy-MM-dd"),
                                        IntegrateKey = Guid.NewGuid().ToString(),
                                        InvoiceReceiveNumber = "ManualInvoice",
                                        HeaderId = dto.Id,
                                        CustomerName = dto.CustomerName,
                                        CompanyName = dto.Business.TaxName,
                                        Address = dto.Business.TaxAddress,
                                        CQTCode = "",
                                        CreateBy = dto.CreateBy,
                                        UpdateBy = dto.UpdateBy,
                                        CreateDate = dto.CreateDate,
                                        UpdateDate = dto.UpdateDate,
                                        ActiveFlag = STATUS_ACTIVE
                                    };
                                    this.UnitOfWork.Insert(invoice);
                                }
                                if (invoice == null)
                                {
                                    result = false;
                                    dto.Comment = "Error when issuing the Invoice";
                                }
                                else
                                {
                                    entity.ReceiptDate = DateTime.Now;

                                    if (result)
                                        result = await this.UnitOfWork.UpdateAsync(entity, new List<Expression<Func<Headers1, object>>>() { x => x.UpdateDate, x => x.UpdateBy, x => x.StatusID, x => x.ReceiptDate, x => x.OrderNumber });
                                }
                                #endregion

                                if (result)
                                {
                                    dto.ReceiptDate = entity.ReceiptDate;
                                    dto.OrderNumber = entity.OrderNumber;
                                    result = CreateSaleData(dto, invoice);
                                }
                            }
                            else
                            {
                                result = false;
                            }
                        }
                        break;
                    case "ManualIssuedInvoice":
                        _actionType = AppSystemLog.Action.Invoice;

                        entity.StatusID = (byte)SaleOrderStatuses.Invoiced;
                        entity.OrderNumber = GeneralOrderNumber(dto.StoreCode);
                        if (string.IsNullOrEmpty(entity.OrderNumber))
                        {
                            dto.Comment = "Error when generating OrderNumber";
                            result = false;
                        }
                        else
                        {
                            dto.OrderNumber = entity.OrderNumber;

                            #region lấy chuỗi xml   
                            dto.Business = Mapper.Map<BusinessDto>(entity.Business);
                            Invoices1 invoice = ManualIssuedInvoice(dto, dto.URL);
                            if (invoice == null)
                            {
                                result = false;
                                dto.Comment = "Error when issuing the Invoice";
                            }
                            else
                            {
                                result = true;
                                dto.Comment = @"Manual Issued Invoice";
                                entity.ReceiptDate = DateTime.Now;

                                if (result)
                                    result = await this.UnitOfWork.UpdateAsync(entity, new List<Expression<Func<Headers1, object>>>() { x => x.UpdateDate, x => x.UpdateBy, x => x.StatusID, x => x.ReceiptDate, x => x.OrderNumber });
                            }
                            #endregion

                            if (result)
                            {
                                dto.ReceiptDate = entity.ReceiptDate;
                                dto.OrderNumber = entity.OrderNumber;
                                result = CreateSaleData(dto, invoice);
                            }
                        }
                        break;
                    default:
                        break;

                }
                if (result)
                {
                    this.UnitOfWork.Commit(trans);
                }
                else
                    this.UnitOfWork.Rollback(trans);
            }
            catch (Exception ex)
            {
                this.UnitOfWork.Rollback(trans);
                LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
                dto.Comment = ex.StackTrace + "---" + ex.Message;
                result = false;
            }

            InsertHistory(dto, (int)_actionType, result ? 1 : 0);
            return new Tuple<bool, SaleOrderDto>(result, dto);
        }
        #endregion

        #region Có cột giá trong file template
        //public async Task<Tuple<bool, SaleOrderDto>> UpdateAsync(SaleOrderDto dto)
        //{
        //    var result = false;
        //    var _valid = true;
        //    AppSystemLog.Action _actionType = AppSystemLog.Action.Update;
        //    var trans = UnitOfWork.BeginTransaction();
        //    try
        //    {
        //        var entity = this.UnitOfWork.GetAllNoTracking<Headers1>()
        //            .Include(x => x.Business)
        //            .SingleOrDefault(x => x.Id == dto.Id);
        //        entity.UpdateDate = DateTime.Now;
        //        entity.UpdateBy = dto.UpdateBy;
        //        switch (dto.ActionType)
        //        {
        //            case "SendRequest":
        //                _actionType = AppSystemLog.Action.SendRequest;

        //                #region Re-validate
        //                _valid = ReValidate(ref dto);

        //                entity.ErrorMess = dto.ErrorMess;
        //                result = await this.UnitOfWork.UpdateToListAsync<SaleOrderItemDto, Items1>(dto.Items);
        //                if (result)
        //                    result = await this.UnitOfWork.UpdateAsync(entity, new List<Expression<Func<Headers1, object>>>() { x => x.UpdateDate, x => x.UpdateBy, x => x.OrderNumber, x => x.ErrorMess });
        //                this.UnitOfWork.Commit(trans);
        //                trans = UnitOfWork.BeginTransaction();
        //                #endregion

        //                if (_valid)
        //                {
        //                    entity.StatusID = (byte)SaleOrderStatuses.Waiting;
        //                    result = await this.UnitOfWork.UpdateAsync(entity, new List<System.Linq.Expressions.Expression<Func<Headers1, object>>>() { x => x.UpdateDate, x => x.StatusID });
        //                }
        //                else
        //                {
        //                    result = false;
        //                }

        //                break;
        //            case "Approve":
        //                _actionType = AppSystemLog.Action.Approve;

        //                #region Re-validate
        //                _valid = ReValidate(ref dto);

        //                entity.ErrorMess = dto.ErrorMess;
        //                result = await this.UnitOfWork.UpdateToListAsync<SaleOrderItemDto, Items1>(dto.Items);
        //                if (result)
        //                    result = await this.UnitOfWork.UpdateAsync(entity, new List<Expression<Func<Headers1, object>>>() { x => x.UpdateDate, x => x.UpdateBy, x => x.OrderNumber, x => x.ErrorMess });
        //                this.UnitOfWork.Commit(trans);
        //                trans = UnitOfWork.BeginTransaction();
        //                #endregion

        //                if (_valid)
        //                {
        //                    entity.StatusID = (byte)SaleOrderStatuses.Approved;
        //                    result = await this.UnitOfWork.UpdateAsync(entity, new List<System.Linq.Expressions.Expression<Func<Headers1, object>>>() { x => x.UpdateDate, x => x.UpdateBy, x => x.StatusID });
        //                }
        //                else
        //                {
        //                    result = false;
        //                }
        //                break;
        //            case "Reject":
        //                _actionType = AppSystemLog.Action.Reject;

        //                entity.StatusID = (byte)SaleOrderStatuses.Rejected;
        //                result = await this.UnitOfWork.UpdateAsync(entity, new List<System.Linq.Expressions.Expression<Func<Headers1, object>>>() { x => x.UpdateDate, x => x.UpdateBy, x => x.StatusID });
        //                break;
        //            case "Invoice":
        //                _actionType = AppSystemLog.Action.Invoice;

        //                #region Re-validate
        //                _valid = ReValidate(ref dto);

        //                //entity.TotalVATAmount = (decimal)dto.TotalVATAmount;
        //                //entity.TotalAmountWithVAT = (decimal)dto.TotalAmountWithVAT;
        //                //entity.TotalAmountWithoutVAT = (decimal)dto.TotalAmountWithoutVAT;
        //                entity.ErrorMess = dto.ErrorMess;
        //                result = await this.UnitOfWork.UpdateToListAsync<SaleOrderItemDto, Items1>(dto.Items);
        //                if (result)
        //                    result = await this.UnitOfWork.UpdateAsync(entity, new List<Expression<Func<Headers1, object>>>() { x => x.UpdateDate, x => x.UpdateBy, x => x.OrderNumber, x => x.ErrorMess });
        //                this.UnitOfWork.Commit(trans);
        //                trans = UnitOfWork.BeginTransaction();
        //                #endregion

        //                entity.StatusID = (byte)SaleOrderStatuses.Invoiced;
        //                entity.OrderNumber = GeneralOrderNumber(dto.StoreCode);
        //                if (string.IsNullOrEmpty(entity.OrderNumber))
        //                {
        //                    dto.Comment = "Error when generating OrderNumber";
        //                    result = false;
        //                }
        //                else
        //                {
        //                    dto.OrderNumber = entity.OrderNumber;
        //                    if (_valid)
        //                    {
        //                        #region Gọi API TS    
        //                        dto.Business = Mapper.Map<BusinessDto>(entity.Business);
        //                        Invoices1 invoice = null;
        //                        if (!dto.ManualInvoice)
        //                            invoice = IssuedInvoice(dto);
        //                        else
        //                        {
        //                            invoice = new Invoices1
        //                            {
        //                                InvoiceKey = Guid.NewGuid(),
        //                                InvoiceID = 0,
        //                                StoreCode = dto.StoreCode,
        //                                VatCode = dto.Business.TaxCode,
        //                                InvoiceTemplateCode = "1",
        //                                InvoiceSeries = "K24TAA",
        //                                InvoiceNumber = "",
        //                                InvoiceIssuedDate = DateTime.Now.ToString("yyyy-MM-dd"),
        //                                IntegrateKey = Guid.NewGuid().ToString(),
        //                                InvoiceReceiveNumber = "",
        //                                HeaderId = dto.Id,
        //                                CustomerName = dto.CustomerName,
        //                                CompanyName = dto.Business.TaxName,
        //                                Address = dto.Business.TaxAddress,
        //                                CreateBy = dto.CreateBy,
        //                                UpdateBy = dto.UpdateBy,
        //                                CreateDate = dto.CreateDate,
        //                                UpdateDate = dto.UpdateDate,
        //                                ActiveFlag = STATUS_ACTIVE
        //                            };
        //                        }
        //                        if (invoice == null)
        //                        {
        //                            result = false;
        //                            dto.Comment = "Error when issuing the Invoice";
        //                        }
        //                        else
        //                        {
        //                            entity.ReceiptDate = DateTime.Now;

        //                            if (result)
        //                                result = await this.UnitOfWork.UpdateAsync(entity, new List<Expression<Func<Headers1, object>>>() { x => x.UpdateDate, x => x.UpdateBy, x => x.StatusID, x => x.ReceiptDate, x => x.OrderNumber });
        //                        }
        //                        #endregion

        //                        if (result)
        //                        {
        //                            dto.ReceiptDate = entity.ReceiptDate;
        //                            dto.OrderNumber = entity.OrderNumber;
        //                            result = CreateSaleData(dto, invoice);
        //                        }
        //                    }
        //                    else
        //                    {
        //                        result = false;
        //                    }
        //                }
        //                break;
        //            default:
        //                break;

        //        }
        //        if (result)
        //        {
        //            trans.Commit();
        //        }
        //        else
        //            this.UnitOfWork.Rollback(trans);
        //    }
        //    catch (Exception ex)
        //    {
        //        this.UnitOfWork.Rollback(trans);
        //        LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
        //        dto.Comment = ex.StackTrace + "---" + ex.Message;
        //        result = false;
        //    }

        //    InsertHistory(dto, (int)_actionType, result ? 1 : 0);
        //    return new Tuple<bool, SaleOrderDto>(result, dto);
        //}
        #endregion

        public async Task<string> WriteSaleCsvAsync(Guid headerId)
        {
            var headerDto = await this.UnitOfWork.GetAllNoTracking<Header>().Where(x => x.Id == headerId)
                .Include(x => x.Items)
                .Include(x => x.Payments)
                .Include(x => x.Invoices)
                .FirstOrDefaultAsync();

            var itemForDeliveries = this.UnitOfWork.GetAllNoTracking<ItemForDelivery>()
                .Where(x => x.HeaderId == headerId).ToList();

            var saleData = "";
            #region Record H
            saleData += "H";
            saleData += "|" + headerDto.StoreCode;
            saleData += "|" + headerDto.FulfillmentDate;
            saleData += "|" + headerDto.SettlementTime;
            saleData += "|" + headerDto.OrderNumber;
            saleData += "|" + headerDto.OrderNumber;
            //saleData += "|" + headerDto.CustomerID;
            //saleData += "|" + headerDto.FoxtrotUserID;
            saleData += "\n";
            #endregion
            #region Record A
            if (headerDto.Items != null && headerDto.Items.Any())
            {
                foreach (var item in headerDto.Items)
                {
                    saleData += "A";
                    saleData += "|" + item.Sku;
                    saleData += "|" + item.QuantitySold;
                    saleData += "|" + Math.Round(item.SellingPrice, 0);
                    saleData += "|" + Math.Round(item.ListPrice, 0);
                    saleData += "|" + (item.SellingPrice * item.QuantitySold - Math.Round(item.SellingPrice / (1 + item.VATCode / 100) * item.QuantitySold, 0));
                    saleData += "|" + Math.Round(item.VATCode, 0);
                    item.Promotions = this.UnitOfWork.GetAllNoTracking<Promotion>().Where(x => x.ItemId == item.Id).ToList();
                    if (item.Promotions != null && item.Promotions.Any())
                    {
                        var groupPromotions = item.Promotions
                            .GroupBy(g => new { g.PNLAllocation, g.TransactionType });
                        if (groupPromotions != null && groupPromotions.Any())
                        {
                            foreach (var group in groupPromotions)
                            {
                                double promotionAmount = 0;
                                foreach (var itemGroup in group)
                                {
                                    promotionAmount += (double)itemGroup.PromotionAmount;
                                }
                                if (group.Key.PNLAllocation != null || group.Key.TransactionType != null)
                                {
                                    saleData += "|" + Math.Round((double)promotionAmount, 0);
                                    saleData += "|" + group.Key.PNLAllocation;
                                    saleData += "|" + group.Key.TransactionType;
                                }
                            }
                        }
                    }
                    saleData += "\n";
                }
            }
            if (itemForDeliveries != null && itemForDeliveries.Any())
            {
                foreach (var item in itemForDeliveries)
                {
                    var sql = $@"Exec {BaseBusiness.SP_B2B_GET_PRODUCT_BY_STORE}
                @storeCode = '{headerDto.StoreCode}',                
                @sku = '{item.Sku}'";
                    var deliverySku = this.UnitOfWork.SqlQuery<ProductInventoryPricingTaxDto>(sql, 120).FirstOrDefault();
                    saleData += "A";
                    saleData += "|" + item.Sku;
                    saleData += "|" + item.QuantitySold;
                    saleData += "|" + Math.Round(item.SellingPrice, 0);
                    saleData += "|" + Math.Round(deliverySku.ListPrice.GetValueOrDefault(), 0);
                    saleData += "|" + (item.SellingPrice * item.QuantitySold - Math.Round(item.SellingPrice / (1 + deliverySku.TaxRate / 100) * item.QuantitySold, 0));
                    saleData += "|" + Math.Round(deliverySku.TaxRate, 0);
                    saleData += "|" + Math.Round(item.SellingPrice - deliverySku.ListPrice.GetValueOrDefault(), 0);
                    saleData += "|MERCH";
                    saleData += "|P";
                    saleData += "\n";
                }
            }
            #endregion
            #region Record B
            if (headerDto.Payments != null && headerDto.Payments.Any())
            {
                foreach (var payment in headerDto.Payments)
                {
                    saleData += "B";
                    saleData += "|" + payment.PaymentType;
                    saleData += "|" + Math.Round(payment.TotalAmount, 0);
                    saleData += "|" + payment.TransactionID;
                    saleData += "|" + payment.AuthID;
                    saleData += "|" + Math.Round(payment.TotalAmountWithoutVATForTaxableItems, 0);
                    saleData += "|" + Math.Round(payment.TotalAmountForNonTaxableItems, 0);
                    saleData += "|" + Math.Round(payment.TotalTaxAmount, 0);
                    saleData += "\n";
                }
            }
            #endregion
            #region Record C
            if (headerDto.Invoices != null && headerDto.Invoices.Any())
            {
                foreach (var invoice in headerDto.Invoices)
                {
                    saleData += "C";
                    saleData += "|" + invoice.Code;
                    saleData += "|" + invoice.SerialNo;
                    saleData += "|" + invoice.Number;

                    string customerNameVn = UnicodeOriginToUnicodeVN1258(invoice.CustomerName.Trim());
                    byte[] bytesCustomerName = Encoding.GetEncoding(1258).GetBytes(customerNameVn);
                    string customerName = Encoding.Default.GetString(bytesCustomerName);
                    saleData += "|" + customerName;

                    string companyNameVn = UnicodeOriginToUnicodeVN1258(invoice.CompanyName.Trim());
                    byte[] bytesCompanyName = Encoding.GetEncoding(1258).GetBytes(companyNameVn);
                    string companyName = Encoding.Default.GetString(bytesCompanyName);
                    saleData += "|" + companyName;

                    string addressVn = UnicodeOriginToUnicodeVN1258(invoice.Address.Trim());
                    byte[] bytesAddress = Encoding.GetEncoding(1258).GetBytes(addressVn);
                    string address = Encoding.Default.GetString(bytesAddress);
                    saleData += "|" + address;

                    saleData += "|" + invoice.VatCode;
                    saleData += "|" + invoice.CQTCode;
                    saleData += "\n";
                }
            }
            #endregion
            saleData += "END";
            return saleData;
        }
        public async Task<HeaderDto> WriteSaleCsvFileAsync(Guid headerId)
        {
            var headerDto = await this.UnitOfWork.GetAllNoTracking<Header>().Where(x => x.Id == headerId)
                .Include(x => x.Items)
                .Include(x => x.Payments)
                .Include(x => x.Invoices)
                .FirstOrDefaultAsync();
            var ItemForDeliveries = this.UnitOfWork.GetAllNoTracking<ItemForDelivery>().Where(x => x.HeaderId == headerId).ToList();

            var saleDto = Mapper.Map<HeaderDto>(headerDto);
            saleDto.ItemForDeliveries = Mapper.Map<List<ItemForDeliveryDto>>(ItemForDeliveries);
            if (ItemForDeliveries != null && ItemForDeliveries.Any())
            {
                foreach (var item in saleDto.ItemForDeliveries)
                {
                    var sql = $@"Exec {BaseBusiness.SP_B2B_GET_PRODUCT_BY_STORE}
                @storeCode = '{headerDto.StoreCode}',                
                @sku = '{item.Sku}'";
                    var deliverySku = this.UnitOfWork.SqlQuery<ProductInventoryPricingTaxDto>(sql, 120).FirstOrDefault();
                    item.TaxRate = deliverySku.TaxRate;
                    item.ListPrice = deliverySku.ListPrice.GetValueOrDefault();
                }
            }
            return saleDto;
        }
        #region Private method
        private string GeneralOrderNumber(string storeCode)
        {
            try
            {
                var today = DateTime.Now.AddDays(1).ToString("yyyyMMdd");

                var store = this.UnitOfWork.GetItem<StoreDto, Store>(x => x.Code == storeCode && x.ActiveFlag == STATUS_ACTIVE);
                if (store == null)
                {
                    return "";
                }

                var POSNumber = store.POSNumber1.GetValueOrDefault();
                var BillNumber = UnitOfWork.GetItem<BillNumber, BillNumber>(x => x.CurrentDate == today && x.StoreCode == storeCode && x.POSNumber == POSNumber);

                int maxOrderNumber = 0;
                if (BillNumber != null)
                    maxOrderNumber = BillNumber.Current.GetValueOrDefault();

                if (maxOrderNumber > 9999)
                {
                    maxOrderNumber = 0;
                    POSNumber = store.POSNumber2.GetValueOrDefault();
                    BillNumber = UnitOfWork.GetItem<BillNumber, BillNumber>(x => x.CurrentDate == today && x.StoreCode == storeCode && x.POSNumber == POSNumber);
                    if (BillNumber != null)
                        maxOrderNumber = BillNumber.Current.GetValueOrDefault();
                }

                maxOrderNumber++;
                string orderNumber = POSNumber + maxOrderNumber.ToString("0###");

                if (BillNumber == null)
                {
                    BillNumber = new BillNumber()
                    {
                        Id = Guid.NewGuid(),
                        StoreCode = store.Code,
                        POSNumber = POSNumber,
                        Current = 1,
                        CurrentDate = today,

                        URL = "",
                        CreateDate = DateTime.Now,
                        UpdateDate = DateTime.Now,
                        ActiveFlag = STATUS_ACTIVE
                    };
                    this.UnitOfWork.Insert(BillNumber);
                }
                else
                {
                    BillNumber.Current = maxOrderNumber;
                    this.UnitOfWork.Update(BillNumber, new List<Expression<Func<BillNumber, object>>>() { x => x.Current });
                }


                return orderNumber;
            }
            catch (Exception ex)
            {
                LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
            }
            return "";
        }
        private Invoices1 IssuedInvoice(SaleOrderDto dto)
        {
            XmlDocument SOAPReqBody = new XmlDocument();
            string ServiceResult = "";
            try
            {
                var store = this.UnitOfWork.GetItem<StoreDto, Store>(x => x.Code == dto.StoreCode);
                if (store == null)
                    return null;

                var einvoiceInfos = this.UnitOfWork.GetAllNoTracking<SystemSetting>().Where(x => x.Name.StartsWith("EInvoice")).ToList();
                if (einvoiceInfos.Count < 3)
                {
                    return null;
                }

                var einvoiceURL = einvoiceInfos.FirstOrDefault(x => x.Name == "EInvoiceURL").Value;
                var einvoiceUser = einvoiceInfos.FirstOrDefault(x => x.Name == "EInvoiceUser").Value;
                var einvoicePassword = einvoiceInfos.FirstOrDefault(x => x.Name == "EInvoicePassword").Value;
                #region Khởi tạo request đến TS  

                #region thực tế
                var xml = $@"<?xml version=""1.0"" encoding=""utf-8""?>
            <soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:inv=""http://thaison.vn/inv"">
            	<soapenv:Header>
            		<Authentication xmlns=""http://thaison.vn/inv"">
            			<userName>{einvoiceUser}</userName>
            			<password>{einvoicePassword}</password>
            		</Authentication>
            	</soapenv:Header>
            	<soapenv:Body>
            		<IssuedInvoices xmlns=""http://thaison.vn/inv"">
            			<invoiceEntity>
            				<IntegrateKey>{dto.Id.ToString()}</IntegrateKey>
            				<InvoiceTypeCode>{InvoiceTypeCodes.VATInvoice}</InvoiceTypeCode>
            				<InvoiceTypeName>Hóa đơn giá trị gia tăng</InvoiceTypeName>
            				<AdjustmentType>{(int)AdjustmentTypes.OriginalBill}</AdjustmentType>
            				<InvoiceIssuedDate>{DateTime.Now.ToString("yyyy/MM/dd")}</InvoiceIssuedDate>
            				<InvoiceCreateDate>{DateTime.Now.ToString("yyyy/MM/dd")}</InvoiceCreateDate>
            				<BranchCode>{dto.StoreCode}</BranchCode>
            				<SellerLegalCode>{store.MerchantTax}</SellerLegalCode>
            				<SellerTaxCode>{store.MerchantTax}</SellerTaxCode>
            				<SellerLegalName>{SecurityElement.Escape(store.TaxName)}</SellerLegalName>
            				<SellerAddressLine>{SecurityElement.Escape(store.TaxAddress)}</SellerAddressLine>
            				<BuyerCode>{dto.Business.TaxCode}</BuyerCode>
            				<BuyerTaxCode>{dto.Business.TaxCode}</BuyerTaxCode>
            				<BuyerLegalName>{SecurityElement.Escape(dto.Business.TaxName)}</BuyerLegalName>
            				<BuyerDisplayName>{SecurityElement.Escape(dto.CustomerName)}</BuyerDisplayName>
            				<BuyerAddressLine>{SecurityElement.Escape(dto.Business.TaxAddress)}</BuyerAddressLine>            			<BuyerEmail>{dto.CustomerEmail}</BuyerEmail>";

                if (!string.IsNullOrEmpty(dto.Business.Phone))
                    xml += $@"<BuyerPhoneNumber>{dto.Business.Phone}</BuyerPhoneNumber>";
                if (!string.IsNullOrEmpty(dto.Business.Fax))
                    xml += $@"<BuyerFaxNumber>{dto.Business.Fax}</BuyerFaxNumber>";
                //xml += "<BuyerCitizenIdentityNo></BuyerCitizenIdentityNo>";
                //xml += "<TaxCodeResult></TaxCodeResult>";
                xml += $@" <CurrencyCode>VND</CurrencyCode>
            				<ExchangeRate>1</ExchangeRate>
            				<PaymentMethodCode>04</PaymentMethodCode>
                    <inv:dataExtension>
                    <![CDATA[
				        <EXT_VARCHAR1>{dto.OrderNumber}</EXT_VARCHAR1> <!-- Thông tin Số tham chiếu -->
				    ]]>
                    </inv:dataExtension>

            				<TotalVATAmount>{dto.TotalVATAmount.ToString().Replace(",", ".")}</TotalVATAmount>
            				<TotalAmountWithoutVAT>{dto.TotalAmountWithoutVAT.ToString().Replace(",", ".")}</TotalAmountWithoutVAT>
            				<TotalAmountWithVAT>{dto.TotalAmountWithVAT.ToString().Replace(",", ".")}</TotalAmountWithVAT>
            				<Goods>";

                for (int i = 0; i < dto.Items.Count; i++)
                {
                    var good = dto.Items[i];
                    var vatCode = good.IsTaxB2B && good.VATCode == 0 ? -1 : good.VATCode;
                    xml += $@"<GoodsEntity>
            				<lineNumber>{i + 1}</lineNumber>						
            				<ItemCode>{good.Sku}</ItemCode>
            				<ItemName>{SecurityElement.Escape(good.Name)}</ItemName>
            				<UnitCode>{SecurityElement.Escape(good.UnitType)}</UnitCode>
            				<UnitName>{SecurityElement.Escape(good.UnitType)}</UnitName>
            				<Quantity>{good.Quantity.ToString()}</Quantity>
            				<UnitPrice>{good.POPrice.ToString().Replace(",", ".")}</UnitPrice>
            				<VatPercentage>{vatCode.ToString().Replace(",", ".")}</VatPercentage>
            			</GoodsEntity>";
                }
                xml += @"</Goods>
            			</invoiceEntity>
            		</IssuedInvoices>
            	</soapenv:Body>
            </soapenv:Envelope>";
                #endregion

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(einvoiceURL);
                request.ContentType = "text/xml";
                request.MediaType = "text/xml";
                request.Accept = "text/xml";
                request.Method = "POST";
                LogInfo("Start send SaleOrderID " + dto.Id + " to API ThaiSon!");
                SOAPReqBody.LoadXml(xml);
                using (Stream stream = request.GetRequestStream())
                {
                    SOAPReqBody.Save(stream);
                }
                #region Lưu file request
                var filePath = ConfigurationSettings.AppSettings[TempFolder_Key] ?? TempFolder_Default;
                filePath += "\\IssuedInvoices\\" + dto.Id.ToString();
                if (!Directory.Exists(filePath))
                    Directory.CreateDirectory(filePath);
                var fileName = string.Concat(filePath, "\\request.xml");
                if (File.Exists(fileName))
                    File.Delete(fileName);
                var byteReqXML = Encoding.Unicode.GetBytes(SOAPReqBody.InnerXml);
                string reqXML = Encoding.Unicode.GetString(byteReqXML);
                File.WriteAllText(fileName, reqXML);
                #endregion
                using (WebResponse Serviceres = request.GetResponse())
                {
                    using (StreamReader rd = new StreamReader(Serviceres.GetResponseStream()))
                    {
                        ServiceResult = rd.ReadToEnd();
                        LogInfo("Receiver result from API ThaiSon!");
                        #region Lưu file response
                        fileName = string.Concat(filePath, "\\response.xml");
                        if (File.Exists(fileName))
                            File.Delete(fileName);
                        var byteResXML = Encoding.Unicode.GetBytes(ServiceResult);
                        string resXML = Encoding.Unicode.GetString(byteResXML);
                        File.WriteAllText(fileName, resXML);
                        #endregion

                        XDocument doc = XDocument.Parse(ServiceResult);
                        XNamespace ns = @"http://schemas.xmlsoap.org/soap/envelope/";
                        var unwrappedResponse = doc.Descendants((XNamespace)"http://schemas.xmlsoap.org/soap/envelope/" + "Body").First().FirstNode;

                        XmlSerializer oXmlSerializer = new XmlSerializer(typeof(IssuedInvoicesResponse));
                        var responseObj = (IssuedInvoicesResponse)oXmlSerializer.Deserialize(unwrappedResponse.CreateReader());

                        var output = responseObj.IssuedInvoicesResult;
                        if (output != null && output.InvoiceID != 0)
                        {
                            var invoice = new Invoices1()
                            {
                                InvoiceKey = Guid.Parse(output.InvoiceKey),
                                InvoiceID = output.InvoiceID,
                                StoreCode = output.BranchCode,
                                VatCode = dto.Business.TaxCode,
                                InvoiceTemplateCode = output.InvoiceTemplateCode,
                                InvoiceSeries = output.InvoiceSeries,
                                InvoiceNumber = output.InvoiceNumber,
                                InvoiceIssuedDate = output.InvoiceIssuedDate,
                                IntegrateKey = output.IntegrateKey,
                                InvoiceReceiveNumber = output.InvoiceReceiveNumber,
                                HeaderId = dto.Id,
                                CustomerName = dto.CustomerName,
                                CompanyName = dto.Business.TaxName,
                                Address = dto.Business.TaxAddress,
                                CQTCode = output.TaxCodeResult ?? string.Empty,
                                CreateBy = dto.CreateBy,
                                UpdateBy = dto.UpdateBy,
                                CreateDate = dto.CreateDate,
                                UpdateDate = dto.UpdateDate,
                                ActiveFlag = STATUS_ACTIVE
                            };
                            this.UnitOfWork.Insert(invoice);
                            return invoice;
                        }
                        else
                        {
                            LogError("Error: Request send SaleOrderID " + dto.Id + " to API ThaiSon: " + reqXML);
                            LogError("Error: Response from API ThaiSon: " + ServiceResult);
                        }
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                var byteReqXML = Encoding.Unicode.GetBytes(SOAPReqBody.InnerXml);
                string reqXML = Encoding.Unicode.GetString(byteReqXML);
                LogError("Error: Request send SaleOrderID " + dto.Id + " to API ThaiSon: " + reqXML);
                LogError("Error: Response from API ThaiSon: " + ServiceResult);
                LogError("Error: " + ex.StackTrace + "---" + ex.Message);
                throw (ex);
            }
            return null;
        }
        private Invoices1 ManualIssuedInvoice(SaleOrderDto dto, string responseXML)
        {
            XmlDocument SOAPReqBody = new XmlDocument();
            string ServiceResult = responseXML;
            try
            {
                var store = this.UnitOfWork.GetItem<StoreDto, Store>(x => x.Code == dto.StoreCode);
                if (store == null)
                    return null;

                var filePath = ConfigurationSettings.AppSettings[TempFolder_Key] ?? TempFolder_Default;
                filePath += "\\IssuedInvoices\\" + dto.Id.ToString();
                if (!Directory.Exists(filePath))
                    Directory.CreateDirectory(filePath);
                #region Lưu file response
                var fileName = string.Concat(filePath, "\\response.xml");
                if (File.Exists(fileName))
                    File.Delete(fileName);
                var byteResXML = Encoding.Unicode.GetBytes(ServiceResult);
                string resXML = Encoding.Unicode.GetString(byteResXML);
                File.WriteAllText(fileName, resXML);
                #endregion

                XDocument doc = XDocument.Parse(ServiceResult);
                XNamespace ns = @"http://schemas.xmlsoap.org/soap/envelope/";
                var unwrappedResponse = doc.Descendants((XNamespace)"http://schemas.xmlsoap.org/soap/envelope/" + "Body").First().FirstNode;

                XmlSerializer oXmlSerializer = new XmlSerializer(typeof(IssuedInvoicesResponse));
                var responseObj = (IssuedInvoicesResponse)oXmlSerializer.Deserialize(unwrappedResponse.CreateReader());

                var output = responseObj.IssuedInvoicesResult;
                if (output != null && output.InvoiceID != 0)
                {
                    var invoice = new Invoices1()
                    {
                        InvoiceKey = Guid.Parse(output.InvoiceKey),
                        InvoiceID = output.InvoiceID,
                        StoreCode = output.BranchCode,
                        VatCode = dto.Business.TaxCode,
                        InvoiceTemplateCode = output.InvoiceTemplateCode,
                        InvoiceSeries = output.InvoiceSeries,
                        InvoiceNumber = output.InvoiceNumber,
                        InvoiceIssuedDate = output.InvoiceIssuedDate,
                        IntegrateKey = output.IntegrateKey,
                        InvoiceReceiveNumber = output.InvoiceReceiveNumber,
                        HeaderId = dto.Id,
                        CustomerName = dto.CustomerName,
                        CompanyName = dto.Business.TaxName,
                        Address = dto.Business.TaxAddress,
                        CQTCode = output.TaxCodeResult ?? string.Empty,
                        CreateBy = dto.CreateBy,
                        UpdateBy = dto.UpdateBy,
                        CreateDate = dto.CreateDate,
                        UpdateDate = dto.UpdateDate,
                        ActiveFlag = STATUS_ACTIVE
                    };
                    this.UnitOfWork.Insert(invoice);
                    return invoice;
                }
            }
            catch (Exception ex)
            {
                LogError("Error: Request send SaleOrderID " + dto.Id + " to API ThaiSon");
                LogError("Error: Response from API ThaiSon: " + responseXML);
                LogError("Error: " + ex.StackTrace + "---" + ex.Message);
            }
            return null;
        }
        private bool CreateSaleData(SaleOrderDto dto, Invoices1 invoice)
        {
            var TotalAmountForNonTaxableItems = dto.Items.Where(x => x.VATCode == 0).Sum(x => x.POPrice * x.Quantity);
            var TotalAmountWithVATForTaxableItems = dto.Items.Where(x => x.VATCode > 0).Sum(x => x.POPrice * x.Quantity);

            var TotalAmountWithoutVATForTaxableItems = dto.Items.Where(x => x.VATCode > 0).Sum(x => Math.Round((x.POPrice / (1 + (x.VATCode * 0.01))) * x.Quantity, 0));
            var TotalAmountVATForTaxableItems = TotalAmountWithVATForTaxableItems - TotalAmountWithoutVATForTaxableItems;

            var deliveriySkus = this.UnitOfWork.GetAllNoTracking<DeliverySku>().Where(x => x.ActiveFlag == STATUS_ACTIVE).Select(x => x.Sku).ToList();

            var b2bDefaultCustomerID = "avncitimart";
            #region 1. Create sale header
            var header = new Header()
            {
                Id = dto.Id,
                StoreCode = dto.StoreCode,
                FulfillmentDate = dto.ReceiptDate.ToString("yyyyMMdd"),
                SettlementTime = DateTime.Now.TimeOfDay.ToString("hhmm"),
                OrderNumber = dto.OrderNumber,
                CustomerID = b2bDefaultCustomerID,
                FoxtrotUserID = AppValue.GuidToUUID(dto.BusinessId),
                IsTransfer = false,
                CreateBy = dto.UpdateBy,
                UpdateBy = dto.UpdateBy,
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now,
                ActiveFlag = STATUS_ACTIVE
            };
            #endregion

            #region 2. Create sale Item
            foreach (var x in dto.Items.Where(x => !deliveriySkus.Contains(x.Sku)))
            {
                var saleItem = new Item()
                {
                    Id = x.Id,
                    HeaderId = header.Id,
                    Sku = x.Sku,
                    QuantitySold = x.Quantity,
                    SellingPrice = x.POPrice,
                    ListPrice = x.ListPrice,
                    StoreCode = dto.StoreCode,
                    VATAmount = x.VATAmount,
                    VATCode = x.VATCode,
                    CreateBy = dto.UpdateBy,
                    UpdateBy = dto.UpdateBy,
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    ActiveFlag = STATUS_ACTIVE
                };

                if (x.PromotionAmount != 0)
                {
                    saleItem.Promotions = new List<Promotion>() { new Promotion()
                        {
                            Id = Guid.NewGuid(),
                            ItemId = x.Id,
                            PromotionAmount = x.PromotionAmount,
                            TransactionType = x.TransactionType,
                            PNLAllocation= x.PNLAllocation,
                            CreateBy = dto.UpdateBy,
                            UpdateBy = dto.UpdateBy,
                            CreateDate = DateTime.Now,
                            UpdateDate = DateTime.Now,
                        }
                    };
                }
                header.Items.Add(saleItem);
            }
            #endregion

            #region 3. Create sale Payment
            header.Payments.Add(new Payment
            {
                Id = Guid.NewGuid(),
                HeaderId = header.Id,
                PaymentType = "CHQ",
                TotalAmount = dto.TotalAmountWithVAT,
                TransactionID = DateTime.Now.ToString("yyyyMMdd"),
                TotalTaxAmount = TotalAmountVATForTaxableItems,
                TotalAmountForNonTaxableItems = TotalAmountForNonTaxableItems,
                TotalAmountWithoutVATForTaxableItems = TotalAmountWithoutVATForTaxableItems,

                CreateBy = dto.UpdateBy,
                UpdateBy = dto.UpdateBy,
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now,
                ActiveFlag = STATUS_ACTIVE
            });
            #endregion

            #region 4. Create sale Invoice
            header.Invoices.Add(new Invoice
            {
                Id = invoice.InvoiceKey,
                HeaderId = header.Id,
                Code = invoice.InvoiceTemplateCode,
                SerialNo = invoice.InvoiceSeries,
                Number = invoice.InvoiceNumber,
                CustomerName = invoice.CustomerName,
                CompanyName = invoice.CompanyName,
                Address = invoice.Address,
                VatCode = invoice.VatCode,
                StoreCode = invoice.StoreCode,
                CQTCode = invoice.CQTCode,

                CreateBy = dto.CreateBy,
                UpdateBy = dto.UpdateBy,
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now,
                ActiveFlag = STATUS_ACTIVE
            });
            #endregion

            var rs = this.UnitOfWork.Insert(header) != null;
            #region 5. Create sale Delivery Item
            if (rs)
            {
                var Deliveries = dto.Items.Where(x => deliveriySkus.Contains(x.Sku)).Select(x => new ItemForDelivery()
                {
                    Id = x.Id,
                    HeaderId = header.Id,
                    Sku = x.Sku,
                    QuantitySold = x.Quantity,
                    SellingPrice = x.POPrice,
                    StoreCode = dto.StoreCode,
                    TotalAmount = x.Price,
                    ListPrice = x.ListPrice,
                    VATAmount = x.VATAmount,
                    VATCode = x.VATCode,

                    CreateBy = dto.UpdateBy,
                    UpdateBy = dto.UpdateBy,
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    ActiveFlag = STATUS_ACTIVE
                }).ToList();
                if (Deliveries?.Count > 0)
                    rs = this.UnitOfWork.InsertToList(Deliveries).Count > 0;
            }
            #endregion

            #region 6. Create Record E
            var foxtrotUserID = GuidToUUID(dto.Business.Id);
            var e = this.UnitOfWork.GetAll<CustomerData>()
                .FirstOrDefault(x => x.FoxtrotUserID == foxtrotUserID
                                || x.Id == dto.BusinessId
                                );
            if (e != null)
            {
                this.UnitOfWork.Delete(e, true);
            }
            this.UnitOfWork.Insert(new CustomerData
            {
                Id = dto.Business.Id,
                Email = dto.Business.Email,
                FirstName = dto.Business.CustomerName,
                LastName = "",
                PhoneNumber = dto.Business.Phone ?? string.Empty,
                Ward = dto.Business.Ward ?? string.Empty,
                District = dto.Business.District ?? string.Empty,
                City = dto.Business.City ?? string.Empty,
                CustomerID = b2bDefaultCustomerID,
                FoxtrotUserID = foxtrotUserID,
                CustomerType = "",
                URL = dto.Business.URL,
                CreateBy = dto.Business.CreateBy,
                UpdateBy = dto.Business.UpdateBy,
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now,
                ActiveFlag = STATUS_ACTIVE,
            });
            #endregion
            return rs;
        }
        private void InsertHistory(SaleOrderDto dto, int action, int sucess)
        {
            var rs = this.UnitOfWork.Insert(new SystemLog
            {
                LogId = Guid.NewGuid(),
                Module = AppModule.SaleOrder.ToString(),
                UserId = dto.UpdateBy,
                UserFunction = action,
                EventResult = sucess,
                FuncDateTime = DateTime.Now,
                Source = dto.Id.ToString(),
                Transdata = dto.Comment,
                WSName = ""
            });
            if (rs != null && (dto.ActionType == SaleOrderAction.Invoice.ToString() || dto.ActionType == "ManualIssuedInvoice"))
            {
                var zipFolderPath = ConfigurationSettings.AppSettings[TempFolder_Key] ?? TempFolder_Default;
                zipFolderPath += "\\IssuedInvoices\\" + dto.Id.ToString();
                var zipFilePath = zipFolderPath + ".zip";
                if (Directory.Exists(zipFolderPath))
                {
                    var zipFile = new ZipFile(zipFilePath);
                    zipFile.AddDirectory(zipFolderPath);
                    zipFile.Save();
                    zipFile.Dispose();
                    byte[] fileContent = File.ReadAllBytes(zipFilePath);
                    var dir = new DirectoryInfo(zipFolderPath);
                    foreach (var item in dir.GetFiles())
                    {
                        item.Delete();
                    }
                    Directory.Delete(zipFolderPath);
                    var sysAtt = new SystemLogAttachment
                    {
                        Id = Guid.NewGuid(),
                        LogId = rs.LogId,
                        Name = dto.Id.ToString() + ".zip",
                        FileName = dto.Id.ToString() + ".zip",
                        FileContent = fileContent,

                        URL = "",
                        CreateBy = dto.UpdateBy,
                        CreateDate = DateTime.Now,
                        UpdateBy = dto.UpdateBy,
                        UpdateDate = DateTime.Now,
                        ActiveFlag = STATUS_ACTIVE
                    };
                    sysAtt = this.UnitOfWork.Insert(sysAtt);
                    if (sysAtt != null)
                        File.Delete(zipFilePath);
                }
            }
        }

        #region Không Có cột giá trong file template
        private bool ReValidate(ref SaleOrderDto dto)
        {
            bool _valid = true;
            string _msg = string.Empty;

            Guid BusinessId = dto.BusinessId;
            string storeCode = dto.StoreCode;

            #region 1. Check Business
            _valid = this.UnitOfWork.GetAllNoTracking<Entities.Business>().Any(x => x.Id == BusinessId && x.ActiveFlag == STATUS_ACTIVE);
            if (!_valid)
                dto.ErrorMess = "Business is deleted or deactived;";
            #endregion

            #region 2. Validate Items
            var productInfos = _productBusiness.GetProductInfoByStoreCode(storeCode);
            var storeDto = _storeBusiness.Get(storeCode);
            var promotionStores = _storeBusiness.GetPromotionByStoreCode(storeDto.Code);
            foreach (var item in dto.Items)
            {
                var productInfo = productInfos.FirstOrDefault(x => x.Sku == item.Sku);
                item.ErrorMess = string.Empty;
                item.WarningMess = string.Empty;
                var WarningMess = new List<string>();

                item.UpdateDate = DateTime.Now;
                item.UpdateBy = dto.UpdateBy;
                if (productInfo == null) //2.1. Check exist
                {
                    _valid = false;
                    item.ErrorMess += $"Sku does not exist in store {storeCode};";
                    item.Price = 0;
                    item.ListPrice = 0;
                }
                else
                {
                    if (item.Quantity > productInfo.Inventory) //2.2. Check exist
                    {
                        WarningMess.Add($"Quantity greater than inventory (Inventory: {productInfo.Inventory})");
                    }
                    item.Name = productInfo.ProductName;
                    item.Price = productInfo.Pricing.GetValueOrDefault();
                    item.ListPrice = productInfo.ListPrice.GetValueOrDefault();
                    item.UnitType = productInfo.UnitType;
                    item.VATCode = productInfo.TaxRate;

                    item.UnitPriceWithoutVAT = item.POPrice / (double)(1 + (item.VATCode * 0.01));
                    item.VATAmount = (item.POPrice * item.Quantity) - Math.Round(item.UnitPriceWithoutVAT * item.Quantity, 0);

                    if (item.POPrice != item.Price)
                    {
                        WarningMess.Add($"PO price is different with system price. System price is {productInfo.Pricing.GetValueOrDefault().ToString("#,###0").Replace(".", ",")}; ");

                        if (storeDto.ApplyPromotion)
                        {
                            item.PromotionAmount = item.POPrice - item.Price;
                            StoreMgmtDto.PromotionStoreDto promotion;
                            if (item.PromotionAmount < 0)
                                promotion = promotionStores.FirstOrDefault(x => x.CasePromotion);
                            else
                                promotion = promotionStores.FirstOrDefault(x => !x.CasePromotion);

                            item.PNLAllocation = promotion.PNLAllocation;
                            item.TransactionType = promotion.TransactionType;
                        }
                    }

                    item.IsTaxB2B = productInfo.IsTaxB2B;
                    if (productInfo.IsTaxB2B)
                    {
                        WarningMess.Add($"This SKU is subject to a preferential tax rate of: {item.VATCode}%;");
                    }
                }
                item.WarningMess = string.Join("; ", WarningMess);
            }
            #endregion

            #region 3. Caculator TotalAmount
            dto.TotalAmountWithVAT = dto.Items.Sum(x => x.POPrice * x.Quantity);
            dto.TotalAmountWithoutVAT = dto.Items.Sum(item => Math.Round(item.UnitPriceWithoutVAT * item.Quantity, 0));

            dto.TotalVATAmount = dto.TotalAmountWithVAT - dto.TotalAmountWithoutVAT;
            #endregion

            if (!_valid)
            {
                dto.Comment = "Sale order is wrong";
            }

            return _valid;
        }
        #endregion
        #region Có cột giá trong file template
        //private bool ReValidate(ref SaleOrderDto dto)
        //{
        //    bool _valid = true;
        //    string _msg = string.Empty;

        //    Guid BusinessId = dto.BusinessId;
        //    string storeCode = dto.StoreCode;

        //    #region 1. Check Business
        //    _valid = this.UnitOfWork.GetAllNoTracking<Entities.Business>().Any(x => x.Id == BusinessId && x.ActiveFlag == STATUS_ACTIVE);
        //    if (!_valid)
        //        dto.ErrorMess = "Business is deleted or deactived;";
        //    #endregion

        //    #region 2. Validate Items
        //    var productInfos = _productBusiness.GetProductInfoByStoreCode(storeCode);
        //    foreach (var item in dto.Items)
        //    {
        //        var productInfo = productInfos.FirstOrDefault(x => x.Sku == item.Sku);
        //        item.ErrorMess = string.Empty;
        //        item.WarningMess = string.Empty;

        //        item.UpdateDate = DateTime.Now;
        //        item.UpdateBy = dto.UpdateBy;
        //        if (productInfo == null) //2.1. Check exist
        //        {
        //            _valid = false;
        //            item.ErrorMess += $"Sku does not exist in store {storeCode};";
        //            //item.Price = 0;
        //            //item.ListPrice = 0;
        //        }
        //        else
        //        {
        //            item.Name = productInfo.ProductName;
        //            if (item.Quantity > productInfo.Inventory) //2.2. Check exist
        //            {
        //                item.WarningMess += "Quantity greater than inventory;";
        //            }

        //            if (item.Price != productInfo.Pricing.GetValueOrDefault())
        //            {
        //                _valid = false;
        //                item.ErrorMess += $"Unit price is different with system price. System price is {productInfo.Pricing.GetValueOrDefault().ToString("#,###0").Replace(".", ",")};";
        //            }

        //            //item.Price = productInfo.Pricing.GetValueOrDefault();
        //            //item.ListPrice = productInfo.ListPrice.GetValueOrDefault();

        //            //item.UnitType = productInfo.UnitType;
        //            //item.VATCode = productInfo.TaxRate;

        //            //item.UnitPriceWithoutVAT = item.Price / (double)(1 + (item.VATCode * 0.01));
        //            //item.VATAmount = (item.Price * item.Quantity) - Math.Round(item.UnitPriceWithoutVAT * item.Quantity, 0);
        //        }
        //    }
        //    #endregion

        //    #region 3. Caculator TotalAmount
        //    //dto.TotalAmountWithVAT = dto.Items.Sum(x => x.Price * x.Quantity);
        //    //dto.TotalAmountWithoutVAT = dto.Items.Sum(item => Math.Round(item.UnitPriceWithoutVAT * item.Quantity, 0));

        //    //dto.TotalVATAmount = dto.TotalAmountWithVAT - dto.TotalAmountWithoutVAT;
        //    #endregion

        //    if (!_valid)
        //    {
        //        dto.Comment = "Sale order is wrong";
        //    }

        //    return _valid;
        //}
        #endregion

        #endregion

        public List<PromotionDto> GetPromotionByItemId(Guid id)
        {
            try
            {
                var iquery = this.UnitOfWork.GetAllNoTracking<Promotion>().Where(x => x.ItemId == id).ToList();
                return Mapper.Map<List<PromotionDto>>(iquery);
            }
            catch (Exception ex)
            {
                LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
            }
            return null;
        }
    }
}
