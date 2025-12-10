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
using System.Globalization;
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
using static MiddlewareTool.Common.AppType;
using static MiddlewareTool.Common.AppValue;
using static MiddlewareTool.Dto.RefundDto;
using static MiddlewareTool.Dto.StoreMgmtDto;
using Header = MiddlewareTool.Entities.Header;

namespace MiddlewareTool.Business.Concrete
{
    public class SaleOrderRefundBusiness : BaseBusiness, ISaleOrderRefundBusiness
    {
        private readonly ISaleBusiness _saleBusiness;
        public SaleOrderRefundBusiness(IUnitOfWork unitOfWork, ISaleBusiness saleBusiness) : base(unitOfWork)
        {
            _saleBusiness = saleBusiness;
        }

        public List<RefundReasonDto> GetReasons()
        {
            List<RefundReasonDto> dto = new List<RefundReasonDto>();
            try
            {
                dto = this.UnitOfWork.GetItems<RefundReasonDto, RefundReason>().ToList();
            }
            catch (Exception ex)
            {
                LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
            }
            return dto;
        }

        public async Task<bool> DeleteAsync(SaleOrderRefundDto dto)
        {
            var result = true;
            var msg = "";
            var _appEventResult = AppSystemLog.EventResult.Fail;
            string _transData = $"ERROR! Don't Delete Order ID {dto.Id} by user: {dto.UpdateBy}.";
            using (var trans = this.UnitOfWork.BeginTransaction())
            {
                try
                {

                    var entity = await this.UnitOfWork.GetAllNoTracking<RefundHeaders1>()
                    .FirstOrDefaultAsync(x => x.Id == dto.Id && x.ActiveFlag == STATUS_ACTIVE);

                    if (entity.StatusID == (byte)SaleOrderStatuses.Rejected || entity.StatusID == (byte)SaleOrderStatuses.Updated)
                    {
                        entity.UpdateBy = dto.UpdateBy;
                        entity.UpdateDate = DateTime.Now;

                        result = await this.UnitOfWork.DeleteAsync(entity);
                        if (result)
                        {
                            dto = Mapper.Map<SaleOrderRefundDto>(entity);
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
        public async Task<bool> DeleteCODAsync(SaleOrderRefundDto dto)
        {
            var result = true;
            var msg = "";
            var _appEventResult = AppSystemLog.EventResult.Fail;
            string _transData = $"ERROR! Don't Delete Order ID {dto.Id} by user: {dto.UpdateBy}.";

            var parameters = new Dictionary<string, object>();
            this.UnitOfWork.ExecuteNonQuery(@"ALTER TABLE so.RefundHeaders NOCHECK CONSTRAINT ALL", parameters, 120, CommandType.Text);
            using (var trans = this.UnitOfWork.BeginTransaction())
            {
                try
                {

                    var entity = await this.UnitOfWork.GetAllNoTracking<RefundHeaders1>()
                    .FirstOrDefaultAsync(x => x.Id == dto.Id && x.ActiveFlag == STATUS_ACTIVE);

                    if (entity.StatusID == (byte)SaleOrderStatuses.Rejected || entity.StatusID == (byte)SaleOrderStatuses.Updated)
                    {
                        entity.UpdateBy = dto.UpdateBy;
                        entity.UpdateDate = DateTime.Now;

                        result = await this.UnitOfWork.DeleteAsync(entity);
                        if (result)
                        {
                            dto = Mapper.Map<SaleOrderRefundDto>(entity);
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
            this.UnitOfWork.ExecuteNonQuery(@"ALTER TABLE so.RefundHeaders Check CONSTRAINT ALL", parameters, 120, CommandType.Text);
            dto.Comment = _transData;
            InsertHistory(dto, (int)AppSystemLog.Action.Delete, (int)_appEventResult);
            return result;
        }
        public async Task<SaleOrderRefundDto> GetAsync(Guid id)
        {
            try
            {
                var entity = await this.UnitOfWork.GetAllNoTracking<RefundHeaders1>()
                    .Include(x => x.RefundItems1)
                    .Include(x => x.RefundInvoices1)
                    .Include(x => x.Headers1)
                    .Include(x => x.Headers1.Business)
                    .FirstOrDefaultAsync(x => x.Id == id && x.ActiveFlag == STATUS_ACTIVE);
                entity.RefundItems1 = entity.RefundItems1.OrderBy(x => x.LineNumber).ToList();
                SaleOrderRefundDto dto = Mapper.Map<SaleOrderRefundDto>(entity);
                dto.ReasonName = this.UnitOfWork.GetAllNoTracking<RefundReason>().FirstOrDefault(x => x.ReasonCode == dto.ReasonCode)?.ReasonName;
                dto.Invoices.ForEach(invoice =>
                {
                    var rootInvoice = this.UnitOfWork.GetItem<SaleOrderInvoiceDto, Invoices1>(x => x.InvoiceNumber == invoice.RootInvoiceNumber);
                    invoice.RootInvoiceIssuedDate = rootInvoice?.InvoiceIssuedDate;
                });
                return dto;
            }
            catch (Exception ex)
            {
                LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
            }
            return null;
        }
        public async Task<SaleOrderRefundDto> GetCODAsync(Guid id)
        {
            try
            {
                var entity = await this.UnitOfWork.GetAllNoTracking<RefundHeaders1>()
                    .Include(x => x.RefundItems1)
                    .Include(x => x.RefundInvoices1)
                    .FirstOrDefaultAsync(x => x.Id == id && x.ActiveFlag == STATUS_ACTIVE);
                entity.RefundItems1 = entity.RefundItems1.OrderBy(x => x.LineNumber).ToList();
                SaleOrderRefundDto dto = Mapper.Map<SaleOrderRefundDto>(entity);
                dto.ReasonName = this.UnitOfWork.GetAllNoTracking<RefundReason>().FirstOrDefault(x => x.ReasonCode == dto.ReasonCode)?.ReasonName;
                dto.Invoices.ForEach(invoice =>
                {
                    var rootInvoice = this.UnitOfWork.GetItem<SaleOrderInvoiceDto, Invoices1>(x => x.InvoiceNumber == invoice.RootInvoiceNumber);
                    invoice.RootInvoiceIssuedDate = rootInvoice?.InvoiceIssuedDate;
                });
                return dto;
            }
            catch (Exception ex)
            {
                LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
            }
            return null;
        }
        public async Task<List<SaleOrderRefundDto>> GetAllBySaleOrderIdAsync(Guid id)
        {
            try
            {
                var entity = await this.UnitOfWork.GetAllNoTracking<RefundHeaders1>()
                    .Include(x => x.RefundItems1)
                    .Include(x => x.RefundInvoices1)
                    .Include(x => x.Headers1)
                    .Include(x => x.Headers1.Business)
                    .Where(x => x.SaleOrderId == id && x.ActiveFlag == STATUS_ACTIVE)
                    .ToListAsync();
                List<SaleOrderRefundDto> listDto = new List<SaleOrderRefundDto>();
                foreach (var item in entity)
                {
                    item.RefundItems1 = item.RefundItems1.OrderBy(x => x.LineNumber).ToList();
                    SaleOrderRefundDto dto = Mapper.Map<SaleOrderRefundDto>(item);
                    dto.ReasonName = this.UnitOfWork.GetAllNoTracking<RefundReason>().FirstOrDefault(x => x.ReasonCode == dto.ReasonCode)?.ReasonName;
                    dto.Invoices.ForEach(invoice =>
                    {
                        var rootInvoice = this.UnitOfWork.GetItem<SaleOrderInvoiceDto, Invoices1>(x => x.InvoiceNumber == invoice.RootInvoiceNumber);
                        invoice.RootInvoiceIssuedDate = rootInvoice?.InvoiceIssuedDate;
                    });
                    listDto.Add(dto);
                }
                return listDto;
            }
            catch (Exception ex)
            {
                LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
            }
            return null;
        }
        //public async Task<List<SaleOrderRefundDto>> GetAllBySaleOrderIdCODAsync(Guid id)
        //{
        //    try
        //    {
        //        var entity = await this.UnitOfWork.GetAllNoTracking<RefundHeaders1>()
        //            .Include(x => x.RefundItems1)
        //            .Include(x => x.RefundInvoices1)
        //            .Where(x => x.SaleOrderId == id && x.ActiveFlag == STATUS_ACTIVE)
        //            .ToListAsync();
        //        List<SaleOrderRefundDto> listDto = new List<SaleOrderRefundDto>();
        //        foreach (var item in entity)
        //        {
        //            item.RefundItems1 = item.RefundItems1.OrderBy(x => x.LineNumber).ToList();
        //            SaleOrderRefundDto dto = Mapper.Map<SaleOrderRefundDto>(item);
        //            dto.ReasonName = this.UnitOfWork.GetAllNoTracking<RefundReason>().FirstOrDefault(x => x.ReasonCode == dto.ReasonCode)?.ReasonName;
        //            dto.Invoices.ForEach(invoice =>
        //            {
        //                var rootInvoice = this.UnitOfWork.GetItem<SaleOrderInvoiceDto, Invoices1>(x => x.InvoiceNumber == invoice.RootInvoiceNumber);
        //                invoice.RootInvoiceIssuedDate = rootInvoice?.InvoiceIssuedDate;
        //            });
        //            listDto.Add(dto);
        //        }
        //        return listDto;
        //    }
        //    catch (Exception ex)
        //    {
        //        LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
        //    }
        //    return null;
        //}
        public async Task<List<SaleOrderRefundDto>> GetAllBySaleOrderIdCODAsync(Guid id)
        {
            try
            {
                List<SaleOrderRefundDto> listDto = new List<SaleOrderRefundDto>();

                var actualOrderNumber = this.UnitOfWork.GetAllNoTracking<Header>()
                    .Where(x => x.Id == id && x.ActiveFlag == STATUS_ACTIVE)
                    .Select(x => x.ActualOrderNumber)
                    .FirstOrDefault();
                if (actualOrderNumber == null)
                {
                    return listDto;
                }
                var entity = await this.UnitOfWork.GetAllNoTracking<RefundHeader>()
                    .Include(x => x.RefundItems)
                    .Where(x => x.ActualOrderNumber == actualOrderNumber && x.ActiveFlag == STATUS_ACTIVE)
                    .ToListAsync();
                foreach (var item in entity)
                {
                    SaleOrderRefundDto dto = new SaleOrderRefundDto
                    {
                        Id = item.Id,
                        SaleOrderId = id,
                        StatusID = SaleOrderStatuses.Invoiced,
                        Items = item.RefundItems.Select(x => new SaleOrderRefundItemDto
                        {
                            Id = x.Id,
                            HeaderId = x.HeaderId,
                            Sku = x.Sku,
                            VATCode = x.VATCode,
                            VATAmount = x.VATAmount,
                            ActiveFlag = (AppValue.ActiveFlag)x.ActiveFlag,
                            Quantity = x.QuantityRefunded * -1,
                            ListPrice = x.ListPrice,
                            POPrice = x.SellingPrice,
                            Price = x.SellingPrice
                        }).ToList()
                    };
                    dto.ReasonName = this.UnitOfWork.GetAllNoTracking<RefundReason>().FirstOrDefault(x => x.ReasonCode == dto.ReasonCode)?.ReasonName;
                    //dto.Invoices.ForEach(invoice =>
                    //{
                    //    var rootInvoice = this.UnitOfWork.GetItem<SaleOrderInvoiceDto, Invoices1>(x => x.InvoiceNumber == invoice.RootInvoiceNumber
                    //    );
                    //    invoice.RootInvoiceIssuedDate = rootInvoice?.InvoiceIssuedDate;
                    //});
                    listDto.Add(dto);
                }
                return listDto;
            }
            catch (Exception ex)
            {
                LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
            }
            return null;
        }
        public bool CheckExist(Guid id)
        {
            try
            {
                var exist = this.UnitOfWork.GetAllNoTracking<RefundHeaders1>()
                    .Any(x => x.Id == id && x.ActiveFlag == STATUS_ACTIVE);
                return exist;
            }
            catch (Exception ex)
            {
                LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
            }
            return false;
        }
        public bool CheckView(Guid id, string userId)
        {
            try
            {
                var exist = this.UnitOfWork.GetAllNoTracking<RefundHeaders1>()
                    .Any(x => x.Id == id && x.ActiveFlag == STATUS_ACTIVE && x.CreateBy == userId);
                return exist;
            }
            catch (Exception ex)
            {
                LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
            }
            return false;
        }
        public bool CheckEdit(Guid id, string userId)
        {
            try
            {
                var exist = this.UnitOfWork.GetAllNoTracking<RefundHeaders1>()
                    .Any(x => x.Id == id && x.ActiveFlag == STATUS_ACTIVE && x.CreateBy == userId && !x.UploadId.HasValue);
                return exist;
            }
            catch (Exception ex)
            {
                LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
            }
            return false;
        }
        public async Task<Tuple<int, List<SaleOrderRefundDto>>> GetPagingAsync(SaleOrderFilterDto filter, bool isAdmin, string userName)
        {
            List<SaleOrderRefundDto> dto = new List<SaleOrderRefundDto>();
            int totalItem = 0;
            try
            {
                var iquery = this.UnitOfWork.GetAllNoTracking<RefundHeaders1>()
                    .Where(x => x.ActiveFlag != STATUS_DELETE
                    && x.IsCOD != true
                    );
                if (!filter.HasAllPermission)
                    iquery = iquery.Where(x => x.CreateBy == filter.CreatedBy);
                if (!isAdmin)
                {
                    var userStores = this.UnitOfWork.GetAllNoTracking<UserStore>().Where(x => x.ActiveFlag == STATUS_ACTIVE && x.UserName == userName).Select(x => x.StoreCode).ToList();
                    if (!(userStores?.Count > 0))
                        userStores.Add("NotAdmin");
                    iquery = iquery.Where(x => userStores.Contains(x.StoreCode) || x.CreateBy == filter.CreatedBy);
                }

                iquery = iquery
                    .Include(x => x.Headers1)
                    .Include(x => x.Headers1.Business)
                    .Include(x => x.RefundInvoices1)
                    .Include(x => x.RefundItems1);
                if (!string.IsNullOrEmpty(filter.Keyword))
                {
                    var keyword = filter.Keyword.Trim();
                    decimal.TryParse(keyword, out decimal d_keyword);
                    iquery = iquery.Where(x => x.Description.Contains(keyword)
                    || x.OrderNumber == keyword
                    || x.Headers1.OrderNumber == keyword
                    || x.RefundItems1.Any(item => item.Sku == keyword
                                         || item.Name.Contains(keyword)
                        )
                    || x.RefundInvoices1.Any(invoice => invoice.InvoiceNumber == keyword
                                        || invoice.InvoiceReceiveNumber == keyword
                                        || invoice.InvoiceID == d_keyword)
                    );
                }
                if (!string.IsNullOrEmpty(filter.StoreCode))
                {
                    iquery = iquery.Where(x => x.StoreCode.Equals(filter.StoreCode));
                }
                if (filter.BusinessId.HasValue)
                {
                    iquery = iquery.Where(x => x.Headers1.BusinessId.Equals(filter.BusinessId.Value));
                }
                if (filter.StatusId.HasValue)
                {
                    iquery = iquery.Where(x => x.StatusID.Equals(filter.StatusId.Value));
                }
                if (filter.FromDate.HasValue && filter.ToDate.HasValue)
                {
                    var fromDate = filter.FromDate.Value.Date;
                    var toDate = filter.ToDate.Value.Date.AddDays(1).AddMilliseconds(-1);
                    iquery = iquery.Where(x => (x.Headers1.ReceiptDate >= fromDate && x.Headers1.ReceiptDate <= toDate) || (x.RefundDate >= fromDate && x.RefundDate <= toDate)
                    );
                }
                else if (filter.FromDate.HasValue)
                {
                    var fromDate = filter.FromDate.Value.Date;
                    iquery = iquery.Where(x => x.Headers1.ReceiptDate >= fromDate || x.RefundDate >= fromDate
                    );
                }
                else if (filter.ToDate.HasValue)
                {
                    var toDate = filter.ToDate.Value.AddDays(1).AddMilliseconds(-1);
                    iquery = iquery.Where(x => x.Headers1.ReceiptDate <= toDate || x.RefundDate <= toDate
                    );
                }
                if (!string.IsNullOrEmpty(filter.ReasonCode))
                {
                    iquery = iquery.Where(x => x.ReasonCode == filter.ReasonCode);
                }
                totalItem = await iquery.CountAsync();
                var entities = await iquery
                    .OrderByDescending(x => x.RefundDate)
                    .ThenByDescending(x => x.UpdateDate)
                    .ThenBy(x => x.Description)
                    .Skip((filter.PageIndex - 1) * filter.PageSize)
                    .Take(filter.PageSize)
                    .ToListAsync();
                dto = Mapper.Map<List<SaleOrderRefundDto>>(entities);
            }
            catch (Exception ex)
            {
                LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
            }
            return new Tuple<int, List<SaleOrderRefundDto>>(totalItem, dto);
        }
        public async Task<Tuple<int, List<SaleOrderRefundCODDto>>> GetPagingCODAsync(SaleOrderFilterDto filter, bool isAdmin, string userName)
        {
            List<SaleOrderRefundCODDto> dto = new List<SaleOrderRefundCODDto>();
            int totalItem = 0;
            try
            {
                var iquery = this.UnitOfWork.GetAllNoTracking<RefundHeaders1>()
                    .Where(x => x.ActiveFlag != STATUS_DELETE
                    && x.IsCOD == true);
                var iquerySale = this.UnitOfWork.GetAllNoTracking<Header>().Where(x => x.ActiveFlag != STATUS_DELETE);
                var iqueryRecordSale = this.UnitOfWork.GetAllNoTracking<RecordSale>().Where(x => x.ActiveFlag != STATUS_DELETE);

                if (!filter.HasAllPermission)
                    iquery = iquery.Where(x => x.CreateBy == userName);
                if (!isAdmin)
                {
                    var userStores = this.UnitOfWork.GetAllNoTracking<UserStore>().Where(x => x.ActiveFlag == STATUS_ACTIVE && x.UserName == userName).Select(x => x.StoreCode).ToList();
                    if (!(userStores?.Count > 0))
                        userStores.Add("NotAdmin");
                    iquery = iquery.Where(x => userStores.Contains(x.StoreCode) || x.CreateBy == filter.CreatedBy);
                }

                iquery = iquery
                    .Include(x => x.RefundInvoices1)
                    .Include(x => x.RefundItems1);

                if (!string.IsNullOrEmpty(filter.Keyword))
                {
                    var keyword = filter.Keyword.Trim();

                    iqueryRecordSale = iqueryRecordSale.Where(x => x.BillNumber == keyword || x.ActualOrderNumber == keyword);
                    List<Guid> saleHeaderIds = iqueryRecordSale.Select(x => x.HeaderId).ToList();

                    var t = saleHeaderIds.FirstOrDefault();
                    decimal.TryParse(keyword, out decimal d_keyword);
                    iquery = iquery.Where(x =>
                        x.OrderNumber == keyword
                        || x.RefundItems1.Any(item => item.Sku == keyword
                                             || item.Name.Contains(keyword)
                            )
                        || x.RefundInvoices1.Any(invoice => invoice.InvoiceNumber == keyword
                                            || invoice.InvoiceReceiveNumber == keyword
                                            || invoice.InvoiceID == d_keyword)
                        || saleHeaderIds.Contains(x.SaleOrderId)
                        || x.Description.Contains(keyword)
                    );
                }
                if (!string.IsNullOrEmpty(filter.StoreCode))
                {
                    iquery = iquery.Where(x => x.StoreCode.Equals(filter.StoreCode));
                }
                if (filter.BusinessId.HasValue)
                {
                    iquery = iquery.Where(x => x.Headers1.BusinessId.Equals(filter.BusinessId.Value));
                }
                if (filter.StatusId.HasValue)
                {
                    iquery = iquery.Where(x => x.StatusID.Equals(filter.StatusId.Value));
                }
                if (filter.FromDate.HasValue && filter.ToDate.HasValue)
                {
                    var fromDate = filter.FromDate.Value.Date;
                    var toDate = filter.ToDate.Value.Date.AddDays(1).AddMilliseconds(-1);
                    iquery = iquery.Where(x => x.RefundDate >= fromDate && x.RefundDate <= toDate
                    );

                    //var fromDateStr = fromDate.ToString("yyyyMMdd");
                    //var toDateStr = toDate.ToString("yyyyMMdd");
                    //iquerySale = iquerySale.Where(x => x.FulfillmentDate.CompareTo(fromDateStr) >= 0 && x.FulfillmentDate.CompareTo(toDateStr) <= 0);
                }
                else if (filter.FromDate.HasValue)
                {
                    var fromDate = filter.FromDate.Value.Date;
                    iquery = iquery.Where(x => x.RefundDate >= fromDate
                    );

                    //var fromDateStr = fromDate.ToString("yyyyMMdd");
                    //iquerySale = iquerySale.Where(x => x.FulfillmentDate.CompareTo(fromDateStr) >= 0);
                }
                else if (filter.ToDate.HasValue)
                {
                    var toDate = filter.ToDate.Value.AddDays(1).AddMilliseconds(-1);
                    iquery = iquery.Where(x => x.RefundDate <= toDate
                    );

                    //var toDateStr = toDate.ToString("yyyyMMdd");
                    //iquerySale = iquerySale.Where(x => x.FulfillmentDate.CompareTo(toDateStr) <= 0);
                }
                if (!string.IsNullOrEmpty(filter.ReasonCode))
                {
                    iquery = iquery.Where(x => x.ReasonCode == filter.ReasonCode);
                }
                if (!string.IsNullOrEmpty(filter.CreatedBy))
                {
                    iquery = iquery.Where(x => x.CreateBy == filter.CreatedBy);
                }
                if (!string.IsNullOrEmpty(filter.CustomerType))
                {
                    //var saleHeaderIds = iquerySale.Where(x => x.CustomerType == filter.CustomerType).Select(x => x.Id);

                    //iquery = iquery.Where(x => saleHeaderIds.Contains(x.SaleOrderId));
                    iquery =
    from h in iquery
    join s in iquerySale
        on h.SaleOrderId equals s.Id
    where s.CustomerType == filter.CustomerType
    select h;
                }

                totalItem = iquery.Count();
                var entities = await iquery
                    .OrderByDescending(x => x.RefundDate)
                    .ThenByDescending(x => x.UpdateDate)
                    .ThenBy(x => x.Description)
                    .Skip((filter.PageIndex - 1) * filter.PageSize)
                    .Take(filter.PageSize)
                    .ToListAsync();

                dto = Mapper.Map<List<SaleOrderRefundCODDto>>(entities);

                #region Get Root for COD

                foreach (var header in dto)
                {
                    var root = await _saleBusiness.GetAsync(header.SaleOrderId, header.StoreCode);
                    header.Root = root;
                }

                #endregion
            }
            catch (Exception ex)
            {
                LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
            }
            return new Tuple<int, List<SaleOrderRefundCODDto>>(totalItem, dto);
        }
        public async Task<Tuple<int, List<RefundOrderExportDto>>> GetExportAsync(SaleOrderFilterDto filter, bool isAdmin, string userName)
        {
            List<RefundOrderExportDto> dto = new List<RefundOrderExportDto>();
            try
            {
                var iquery = this.UnitOfWork.GetAllNoTracking<RefundHeaders1>()
                    .Where(x => x.ActiveFlag != STATUS_DELETE);
                if (!filter.HasAllPermission)
                    iquery = iquery.Where(x => x.CreateBy == filter.CreatedBy);
                if (!isAdmin)
                {
                    var userStores = this.UnitOfWork.GetAllNoTracking<UserStore>().Where(x => x.ActiveFlag == STATUS_ACTIVE && x.UserName == userName).Select(x => x.StoreCode).ToList();
                    if (!(userStores?.Count > 0))
                        userStores.Add("NotAdmin");
                    iquery = iquery.Where(x => userStores.Contains(x.StoreCode) || x.CreateBy == filter.CreatedBy);
                }

                iquery = iquery
                    .Include(x => x.Headers1)
                    .Include(x => x.RefundInvoices1)
                    .Include(x => x.RefundItems1);
                if (!string.IsNullOrEmpty(filter.Keyword))
                {
                    var keyword = filter.Keyword.Trim();
                    decimal.TryParse(keyword, out decimal d_keyword);
                    iquery = iquery.Where(x => x.Description.Contains(keyword)
                    || x.OrderNumber == keyword
                    || x.Headers1.OrderNumber == keyword
                    || x.RefundItems1.Any(item => item.Sku == keyword
                                         || item.Name.Contains(keyword)
                        )
                    || x.RefundInvoices1.Any(invoice => invoice.InvoiceNumber == keyword
                                        || invoice.InvoiceReceiveNumber == keyword
                                        || invoice.InvoiceID == d_keyword)
                    );
                }
                if (!string.IsNullOrEmpty(filter.StoreCode))
                {
                    iquery = iquery.Where(x => x.StoreCode.Equals(filter.StoreCode));
                }
                if (filter.BusinessId.HasValue)
                {
                    iquery = iquery.Where(x => x.Headers1.BusinessId.Equals(filter.BusinessId.Value));
                }
                if (filter.StatusId.HasValue)
                {
                    iquery = iquery.Where(x => x.StatusID.Equals(filter.StatusId.Value));
                }
                if (filter.FromDate.HasValue && filter.ToDate.HasValue)
                {
                    var fromDate = filter.FromDate.Value.Date;
                    var toDate = filter.ToDate.Value.Date.AddDays(1).AddMilliseconds(-1);
                    iquery = iquery.Where(x => (x.Headers1.ReceiptDate >= fromDate && x.Headers1.ReceiptDate <= toDate) || (x.RefundDate >= fromDate && x.RefundDate <= toDate)
                    );
                }
                else if (filter.FromDate.HasValue)
                {
                    var fromDate = filter.FromDate.Value.Date;
                    iquery = iquery.Where(x => x.Headers1.ReceiptDate >= fromDate || x.RefundDate >= fromDate
                    );
                }
                else if (filter.ToDate.HasValue)
                {
                    var toDate = filter.ToDate.Value.AddDays(1).AddMilliseconds(-1);
                    iquery = iquery.Where(x => x.Headers1.ReceiptDate <= toDate || x.RefundDate <= toDate
                    );
                }
                if (!string.IsNullOrEmpty(filter.ReasonCode))
                {
                    iquery = iquery.Where(x => x.ReasonCode == filter.ReasonCode);
                }
                dto = iquery
                    .OrderByDescending(x => x.RefundDate)
                    .ThenByDescending(x => x.UpdateDate)
                    .ThenBy(x => x.Description).AsEnumerable()
                    .AsEnumerable()
                    .Select(x => new RefundOrderExportDto
                    {
                        RefundNumber = x.OrderNumber,
                        RefundDate = x.RefundDate.ToString("dd/MM/yyyy"),
                        Store = x.StoreCode,
                        SaleDate = x.Headers1.ReceiptDate.ToString("dd/MM/yyyy"),
                        ReceiptNumber = x.Headers1.OrderNumber,
                        Customer = x.CustomerName,
                        TotalAmountWithVAT = x.TotalAmountWithVAT > 0 ? x.TotalAmountWithVAT.ToString() : "",
                        TotalAmountWithoutVAT = x.TotalAmountWithoutVAT > 0 ? x.TotalAmountWithoutVAT.ToString() : "",
                        TotalVATAmount = x.TotalVATAmount > 0 ? x.TotalVATAmount.ToString() : "",
                        InvoiceNumber = x.RefundInvoices1.Any() ? x.RefundInvoices1.FirstOrDefault().InvoiceNumber : "",
                        InvoiceIssuedDate = x.RefundInvoices1.Any() ? x.RefundInvoices1.FirstOrDefault().InvoiceIssuedDate : "",
                        StatusID = (SaleOrderStatuses)x.StatusID
                    })
                    .ToList();
                if (!dto.Any())
                    dto.Add(new RefundOrderExportDto());
            }
            catch (Exception ex)
            {
                LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
            }
            return new Tuple<int, List<RefundOrderExportDto>>(0, dto);
        }
        public async Task<Tuple<int, List<RefundOrderExportDto>>> GetExportCODAsync(SaleOrderFilterDto filter, bool isAdmin, string userName)
        {
            List<RefundOrderExportDto> dto = new List<RefundOrderExportDto>();
            try
            {
                var iquery = this.UnitOfWork.GetAllNoTracking<RefundHeaders1>()
                    .Where(x => x.ActiveFlag != STATUS_DELETE
                    && x.IsCOD == true);
                var iquerySale = this.UnitOfWork.GetAllNoTracking<Header>().Where(x => x.ActiveFlag != STATUS_DELETE); ;
                var iqueryRecordSale = this.UnitOfWork.GetAllNoTracking<RecordSale>().Where(x => x.ActiveFlag != STATUS_DELETE);

                if (!filter.HasAllPermission)
                    iquery = iquery.Where(x => x.CreateBy == userName);
                if (!isAdmin)
                {
                    var userStores = this.UnitOfWork.GetAllNoTracking<UserStore>().Where(x => x.ActiveFlag == STATUS_ACTIVE && x.UserName == userName).Select(x => x.StoreCode).ToList();
                    if (!(userStores?.Count > 0))
                        userStores.Add("NotAdmin");
                    iquery = iquery.Where(x => userStores.Contains(x.StoreCode) || x.CreateBy == filter.CreatedBy);
                }

                iquery = iquery
                    .Include(x => x.RefundInvoices1)
                    .Include(x => x.RefundItems1);

                if (!string.IsNullOrEmpty(filter.Keyword))
                {
                    var keyword = filter.Keyword.Trim();

                    iqueryRecordSale = iqueryRecordSale.Where(x => x.BillNumber == keyword || x.ActualOrderNumber == keyword);
                    List<Guid> saleHeaderIds = iqueryRecordSale.Select(x => x.HeaderId).ToList();

                    var t = saleHeaderIds.FirstOrDefault();
                    decimal.TryParse(keyword, out decimal d_keyword);
                    iquery = iquery.Where(x =>
                        x.OrderNumber == keyword
                        || x.RefundItems1.Any(item => item.Sku == keyword
                                             || item.Name.Contains(keyword)
                            )
                        || x.RefundInvoices1.Any(invoice => invoice.InvoiceNumber == keyword
                                            || invoice.InvoiceReceiveNumber == keyword
                                            || invoice.InvoiceID == d_keyword)
                        || saleHeaderIds.Contains(x.SaleOrderId)
                        || x.Description.Contains(keyword)
                    );
                }
                if (!string.IsNullOrEmpty(filter.StoreCode))
                {
                    iquery = iquery.Where(x => x.StoreCode.Equals(filter.StoreCode));
                }
                if (filter.BusinessId.HasValue)
                {
                    iquery = iquery.Where(x => x.Headers1.BusinessId.Equals(filter.BusinessId.Value));
                }
                if (filter.StatusId.HasValue)
                {
                    iquery = iquery.Where(x => x.StatusID.Equals(filter.StatusId.Value));
                }
                if (filter.FromDate.HasValue && filter.ToDate.HasValue)
                {
                    var fromDate = filter.FromDate.Value.Date;
                    var toDate = filter.ToDate.Value.Date.AddDays(1).AddMilliseconds(-1);
                    iquery = iquery.Where(x => x.RefundDate >= fromDate && x.RefundDate <= toDate
                    );

                    //var fromDateStr = fromDate.ToString("yyyyMMdd");
                    //var toDateStr = toDate.ToString("yyyyMMdd");
                    //iquerySale = iquerySale.Where(x => x.FulfillmentDate.CompareTo(fromDateStr) >= 0 && x.FulfillmentDate.CompareTo(toDateStr) <= 0);
                }
                else if (filter.FromDate.HasValue)
                {
                    var fromDate = filter.FromDate.Value.Date;
                    iquery = iquery.Where(x => x.RefundDate >= fromDate
                    );

                    //var fromDateStr = fromDate.ToString("yyyyMMdd");
                    //iquerySale = iquerySale.Where(x => x.FulfillmentDate.CompareTo(fromDateStr) >= 0);
                }
                else if (filter.ToDate.HasValue)
                {
                    var toDate = filter.ToDate.Value.AddDays(1).AddMilliseconds(-1);
                    iquery = iquery.Where(x => x.RefundDate <= toDate
                    );

                    //var toDateStr = toDate.ToString("yyyyMMdd");
                    //iquerySale = iquerySale.Where(x => x.FulfillmentDate.CompareTo(toDateStr) <= 0);
                }
                if (!string.IsNullOrEmpty(filter.ReasonCode))
                {
                    iquery = iquery.Where(x => x.ReasonCode == filter.ReasonCode);
                }
                if (!string.IsNullOrEmpty(filter.CreatedBy))
                {
                    iquery = iquery.Where(x => x.CreateBy == filter.CreatedBy);
                }
                if (!string.IsNullOrEmpty(filter.CustomerType))
                {
                    //var saleHeaderIds = iquerySale.Where(x => x.CustomerType == filter.CustomerType).Select(x => x.Id);

                    //iquery = iquery.Where(x => saleHeaderIds.Contains(x.SaleOrderId));
                    iquery =
    from h in iquery
    join s in iquerySale
        on h.SaleOrderId equals s.Id
    where s.CustomerType == filter.CustomerType
    select h;
                }

                var entities = await iquery
                    .OrderByDescending(x => x.RefundDate)
                    .ThenByDescending(x => x.UpdateDate)
                    .ThenBy(x => x.Description)
                    .ToListAsync();

                if (entities?.Count > 0)
                {
                    var rootSaleIds = entities.Select(x => x.SaleOrderId).ToList();
                    var rootSales = this.UnitOfWork.GetAllNoTracking<Header>().Where(x => rootSaleIds.Contains(x.Id)).ToList().Select(x => new SaleCODDto()
                    {
                        Id = x.Id,
                        OrderNumber = x.ActualOrderNumber,
                        ReceiptDate = DateTime.ParseExact(x.FulfillmentDate, "yyyyMMdd", CultureInfo.InvariantCulture),
                        CustomerID = x.CustomerID,
                    }).ToList();
                    var rootRecordSales = this.UnitOfWork.GetAllNoTracking<RecordSale>().Where(x => rootSaleIds.Contains(x.HeaderId)).Select(x => new SaleCODDto()
                    {
                        Id = x.HeaderId,
                        StoreCode = x.StoreCode,
                        BillNumber = x.BillNumber
                    }).ToList();
                    var customerIDs = rootSales.Select(x => x.CustomerID).Distinct().ToList();
                    var customerDatas = this.UnitOfWork.GetAllNoTracking<CustomerData>().Where(x => customerIDs.Contains(x.CustomerID)).ToList();

                    entities.ForEach(x =>
                    {
                        var rootSale = rootSales.FirstOrDefault(sale => sale.Id == x.SaleOrderId);
                        var rootRecordSale = rootRecordSales.FirstOrDefault(sale => sale.Id == x.SaleOrderId && sale.StoreCode == x.StoreCode);
                        var customerData = customerDatas.FirstOrDefault(sale => sale.CustomerID == rootSale.CustomerID);

                        var exportDto = new RefundOrderExportDto()
                        {
                            RefundNumber = x.OrderNumber,
                            RefundDate = x.RefundDate.ToString("dd/MM/yyyy"),
                            Store = x.StoreCode,
                            SaleDate = rootSale?.ReceiptDate.ToString("dd/MM/yyyy"),
                            ReceiptNumber = rootRecordSale.BillNumber,
                            Customer = customerData?.FirstName.Trim() + " " + customerData?.LastName.Trim(),
                            TotalAmountWithVAT = x.TotalAmountWithVAT > 0 ? x.TotalAmountWithVAT.ToString() : "",
                            TotalAmountWithoutVAT = x.TotalAmountWithoutVAT > 0 ? x.TotalAmountWithoutVAT.ToString() : "",
                            TotalVATAmount = x.TotalVATAmount > 0 ? x.TotalVATAmount.ToString() : "",
                            InvoiceNumber = x.RefundInvoices1.Any() ? x.RefundInvoices1.FirstOrDefault().InvoiceNumber : "",
                            InvoiceIssuedDate = x.RefundInvoices1.Any() ? x.RefundInvoices1.FirstOrDefault().InvoiceIssuedDate : "",
                            StatusID = (SaleOrderStatuses)x.StatusID
                        };
                        dto.Add(exportDto);
                    });

                }
                if (!dto.Any())
                    dto.Add(new RefundOrderExportDto());
            }
            catch (Exception ex)
            {
                LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
            }
            return new Tuple<int, List<RefundOrderExportDto>>(0, dto);
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
        public async Task<bool> InsertAsync(SaleOrderRefundDto dto)
        {
            var result = true;
            var msg = "";
            using (var trans = this.UnitOfWork.BeginTransaction())
            {
                try
                {
                    dto.SetDefaultValueInsert();
                    dto.RefundDate = DateTime.Now;
                    if (dto.Items.Any())
                    {
                        var lineNumber = 0;
                        dto.Items.ForEach(x =>
                        {
                            x.LineNumber = ++lineNumber;
                            x.CreateDate = DateTime.Now;
                            x.UpdateDate = DateTime.Now;
                            x.CreateBy = dto.CreateBy;
                            x.UpdateBy = dto.UpdateBy;
                        });
                        dto.StatusID = (byte)SaleOrderStatuses.Updated;
                        var entity = Mapper.Map<RefundHeaders1>(dto);
                        entity = await this.UnitOfWork.InsertAsync(entity);
                        result = entity != null;
                        if (result)
                        {
                            trans.Commit();
                        }
                        else
                        {

                            this.UnitOfWork.Rollback(trans);
                            msg = $"ERROR: Refund for saleorder '{dto.SaleOrderId}' fail!";
                        }
                    }
                }
                catch (Exception ex)
                {
                    this.UnitOfWork.Rollback(trans);
                    LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
                    msg = $"ERROR: Refund for saleorder '{dto.SaleOrderId}' faill!";

                    msg += ex.StackTrace + "---" + ex.Message;
                }
            }
            dto.Comment = msg;
            InsertHistory(dto, (int)AppSystemLog.Action.Insert, result ? 1 : 0);
            return result;
        }
        public async Task<bool> InsertCODAsync(SaleOrderRefundDto dto)
        {
            var result = true;
            var msg = "";
            var parameters = new Dictionary<string, object>();
            this.UnitOfWork.ExecuteNonQuery(@"ALTER TABLE so.RefundHeaders NOCHECK CONSTRAINT ALL", parameters, 120, CommandType.Text);
            using (var trans = this.UnitOfWork.BeginTransaction())
            {
                try
                {
                    dto.SetDefaultValueInsert();
                    dto.RefundDate = DateTime.Now;
                    if (dto.Items.Any())
                    {
                        var lineNumber = 0;
                        dto.Items.ForEach(x =>
                        {
                            x.LineNumber = ++lineNumber;
                            x.CreateDate = DateTime.Now;
                            x.UpdateDate = DateTime.Now;
                            x.CreateBy = dto.CreateBy;
                            x.UpdateBy = dto.UpdateBy;
                        });
                        dto.StatusID = (byte)SaleOrderStatuses.Updated;
                        var entity = Mapper.Map<RefundHeaders1>(dto);
                        entity.IsCOD = true;
                        entity.CustomerEmail = string.Empty;
                        entity.CustomerName = string.Empty;
                        entity = await this.UnitOfWork.InsertAsync(entity);
                        result = entity != null;

                        if (result)
                        {
                            trans.Commit();
                        }
                        else
                        {

                            this.UnitOfWork.Rollback(trans);
                            msg = $"ERROR: Refund for saleorder '{dto.SaleOrderId}' fail!";
                        }
                    }
                }
                catch (Exception ex)
                {
                    this.UnitOfWork.Rollback(trans);
                    LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
                    msg = $"ERROR: Refund for saleorder '{dto.SaleOrderId}' faill!";

                    msg += ex.StackTrace + "---" + ex.Message;
                }
            }
            this.UnitOfWork.ExecuteNonQuery(@"ALTER TABLE so.RefundHeaders Check CONSTRAINT ALL", parameters, 120, CommandType.Text);

            dto.Comment = msg;
            InsertHistory(dto, (int)AppSystemLog.Action.Insert, result ? 1 : 0);
            return result;
        }
        public async Task<Tuple<bool, string>> InsertListAsync(List<SaleOrderRefundDto> dtos)
        {
            var result = true;
            var msg = "";
            SaleOrderRefundDto currentDto = null;

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
                            var entity = Mapper.Map<RefundHeaders1>(dto);
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
        public async Task<Tuple<bool, string>> InsertListCODAsync(List<SaleOrderRefundDto> dtos)
        {
            var result = true;
            var msg = "";
            SaleOrderRefundDto currentDto = null;
            var parameters = new Dictionary<string, object>();
            this.UnitOfWork.ExecuteNonQuery(@"ALTER TABLE so.RefundHeaders NOCHECK CONSTRAINT ALL", parameters, 120, CommandType.Text);
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
                            var entity = Mapper.Map<RefundHeaders1>(dto);
                            entity.IsCOD = true;
                            entity.CustomerEmail = string.Empty;
                            entity.CustomerName = string.Empty;
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
            this.UnitOfWork.ExecuteNonQuery(@"ALTER TABLE so.RefundHeaders Check CONSTRAINT ALL", parameters, 120, CommandType.Text);

            return new Tuple<bool, string>(result, msg);
        }
        #region kiểm tra số refunded của DTO có khác DB không
        public async Task<Tuple<bool, SaleOrderRefundDto>> checkRefundedItem(SaleOrderRefundDto dto)
        {
            bool check = true;
            var rf = await GetAllBySaleOrderIdAsync(dto.SaleOrderId);
            foreach (var item in dto.Items)
            {
                var rfItem = rf.Where(r => r.StatusID == SaleOrderStatuses.Invoiced && r.Id != item.HeaderId).SelectMany(x => x.Items).Where(z => z.Sku == item.Sku).Sum(y => y.Quantity);
                if (item.Refunded != rfItem)
                {
                    check = false;
                    item.Refunded = rfItem;
                    item.WarningMess = "Refunded has been updated";
                }
                else if ((item.Quantity + rfItem) > item.RootQuantity)
                {
                    check = false;
                    item.Refunded = rfItem;
                    item.WarningMess = "Cannot refund more";
                }
            }
            return new Tuple<bool, SaleOrderRefundDto>(check, dto);
        }
        #endregion

        public async Task<bool> UpdateAsync(SaleOrderRefundDto dto)
        {
            var result = false;
            AppSystemLog.Action _actionType = AppSystemLog.Action.Update;
            var trans = UnitOfWork.BeginTransaction();
            try
            {
                var entity = this.UnitOfWork.GetAllNoTracking<RefundHeaders1>()
                    .SingleOrDefault(x => x.Id == dto.Id);
                entity.UpdateDate = DateTime.Now;
                entity.UpdateBy = dto.UpdateBy;
                switch (dto.ActionType)
                {
                    case "Update":
                        _actionType = AppSystemLog.Action.Update;

                        entity.StatusID = (byte)SaleOrderStatuses.Updated;
                        entity.TotalVATAmount = dto.TotalVATAmount;
                        entity.TotalAmountWithVAT = dto.TotalAmountWithVAT;
                        entity.TotalAmountWithoutVAT = dto.TotalAmountWithoutVAT;
                        result = await this.UnitOfWork.UpdateAsync(entity, new List<Expression<Func<RefundHeaders1, object>>>() { x => x.UpdateDate, x => x.UpdateBy, x => x.StatusID, x => x.TotalVATAmount, x => x.TotalAmountWithVAT, x => x.TotalAmountWithoutVAT });
                        result = await this.UnitOfWork.UpdateToListAsync<SaleOrderRefundItemDto, RefundItems1>(dto.Items);
                        break;
                    case "SendRequest":
                        _actionType = AppSystemLog.Action.SendRequest;

                        entity.StatusID = (byte)SaleOrderStatuses.Waiting;
                        result = await this.UnitOfWork.UpdateAsync(entity, new List<Expression<Func<RefundHeaders1, object>>>() { x => x.UpdateDate, x => x.UpdateBy, x => x.StatusID });
                        break;
                    case "Approve":
                        _actionType = AppSystemLog.Action.Approve;

                        entity.StatusID = (byte)SaleOrderStatuses.Approved;
                        result = await this.UnitOfWork.UpdateAsync(entity, new List<Expression<Func<RefundHeaders1, object>>>() { x => x.UpdateDate, x => x.UpdateBy, x => x.StatusID });
                        break;
                    case "Reject":
                        _actionType = AppSystemLog.Action.Reject;
                        entity.StatusID = (byte)SaleOrderStatuses.Rejected;
                        result = await this.UnitOfWork.UpdateAsync(entity, new List<Expression<Func<RefundHeaders1, object>>>() { x => x.UpdateDate, x => x.UpdateBy, x => x.StatusID });
                        break;
                    case "Invoice":
                        _actionType = AppSystemLog.Action.Invoice;

                        #region Lấy thông tin đơn gốc
                        dto.Root = Mapper.Map<SaleOrderDto>(this.UnitOfWork.GetAllNoTracking<Headers1>()
                            .Where(x => x.Id == dto.SaleOrderId)
                            .Include(x => x.Business)
                            .Include(x => x.Invoices1)
                            .FirstOrDefault()
                        );
                        #endregion
                        var validate = await ReValidate(dto);
                        dto = validate.Item2;
                        #region Lưu lại thông tin Refunded hoặc Error (nếu có)
                        result = await this.UnitOfWork.UpdateToListAsync<SaleOrderRefundItemDto, RefundItems1>(dto.Items);
                        if (result)
                            result = await this.UnitOfWork.UpdateAsync(entity, new List<Expression<Func<RefundHeaders1, object>>>() { x => x.UpdateDate, x => x.UpdateBy });
                        trans.Commit();
                        trans = UnitOfWork.BeginTransaction();
                        #endregion

                        if (result)
                            result = validate.Item1;
                        if (result)
                        {
                            #region Gọi API TS    
                            RefundInvoices1 invoice = null;
                            if (dto.Root != null && dto.Root.Invoices != null && dto.Root.Invoices.Any())
                            {

                                if (!dto.ManualInvoice)
                                    invoice = AdjustInvoice(dto);
                                else
                                {
                                    invoice = new RefundInvoices1
                                    {
                                        InvoiceKey = Guid.NewGuid(),
                                        InvoiceID = 0,
                                        StoreCode = dto.StoreCode,
                                        VatCode = dto.Root.Business.TaxCode,
                                        InvoiceTemplateCode = "1",
                                        InvoiceSeries = "K24TAA",
                                        InvoiceNumber = "ManualInvoice",
                                        InvoiceIssuedDate = DateTime.Now.ToString("yyyy-MM-dd"),
                                        IntegrateKey = Guid.NewGuid().ToString(),
                                        InvoiceReceiveNumber = "ManualInvoice",
                                        RootIntegrateKey = dto.Invoices[0].IntegrateKey,
                                        RootInvoiceNumber = dto.Invoices[0].InvoiceNumber,
                                        RootInvoiceSeries = dto.Invoices[0].InvoiceSeries,
                                        RootInvoiceTemplateCode = dto.Invoices[0].RootInvoiceTemplateCode,
                                        HeaderId = dto.Id,
                                        CustomerName = dto.CustomerName,
                                        CompanyName = dto.Root.Business?.TaxName,
                                        Address = dto.Root.Business?.TaxAddress,
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
                                    entity.StatusID = (byte)SaleOrderStatuses.Invoiced;
                                    entity.OrderNumber = GeneralOrderNumber(dto.StoreCode);
                                    if (string.IsNullOrEmpty(entity.OrderNumber))
                                    {
                                        dto.Comment = "Error when generating OrderNumber";
                                        result = false;
                                    }
                                    else
                                        dto.OrderNumber = entity.OrderNumber;
                                    entity.RefundDate = DateTime.Now;
                                    entity.RefundTime = DateTime.Now.TimeOfDay.ToString("hhmm");
                                    result = await this.UnitOfWork.UpdateAsync(entity, new List<Expression<Func<RefundHeaders1, object>>>() { x => x.UpdateDate, x => x.UpdateBy, x => x.StatusID, x => x.RefundDate, x => x.RefundTime, x => x.OrderNumber });
                                }
                            }
                            else
                            {
                                result = false;
                                dto.Comment = "The root invoice is not exist.";
                            }

                            #endregion

                            if (result)
                            {
                                dto.RefundDate = entity.RefundDate;
                                dto.RefundTime = entity.RefundTime;
                                dto.OrderNumber = entity.OrderNumber;
                                result = CreateRefundData(dto, invoice);
                            }
                        }
                        break;
                    case "ManualAdjustInvoice":
                        _actionType = AppSystemLog.Action.Invoice;

                        #region Lấy thông tin đơn gốc
                        dto.Root = Mapper.Map<SaleOrderDto>(this.UnitOfWork.GetAllNoTracking<Headers1>()
                            .Where(x => x.Id == dto.SaleOrderId)
                            .Include(x => x.Business)
                            .Include(x => x.Invoices1)
                            .FirstOrDefault()
                        );
                        #endregion
                        result = true;
                        var manualAdjustInvoice = ManualAdjustInvoice(dto, dto.URL);

                        if (dto.Root != null && dto.Root.Invoices != null && dto.Root.Invoices.Any())
                        {
                            if (manualAdjustInvoice == null)
                            {
                                result = false;
                                dto.Comment = "Error when issuing the Invoice";
                            }
                            else
                            {
                                entity.StatusID = (byte)SaleOrderStatuses.Invoiced;
                                entity.OrderNumber = GeneralOrderNumber(dto.StoreCode);
                                if (string.IsNullOrEmpty(entity.OrderNumber))
                                {
                                    dto.Comment = "Error when generating OrderNumber";
                                    result = false;
                                }
                                else
                                    dto.OrderNumber = entity.OrderNumber;
                                entity.RefundDate = DateTime.Now;
                                entity.RefundTime = DateTime.Now.TimeOfDay.ToString("hhmm");
                                result = await this.UnitOfWork.UpdateAsync(entity, new List<Expression<Func<RefundHeaders1, object>>>() { x => x.UpdateDate, x => x.UpdateBy, x => x.StatusID, x => x.RefundDate, x => x.RefundTime, x => x.OrderNumber });
                            }
                        }
                        else
                        {
                            result = false;
                            dto.Comment = "The root invoice is not exist.";
                        }

                        if (result)
                        {
                            dto.RefundDate = entity.RefundDate;
                            dto.RefundTime = entity.RefundTime;
                            dto.OrderNumber = entity.OrderNumber;
                            result = CreateRefundData(dto, manualAdjustInvoice);
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
            }

            InsertHistory(dto, (int)_actionType, result ? 1 : 0);
            return result;
        }
        public async Task<bool> UpdateCODAsync(SaleOrderRefundDto dto)
        {
            var result = false;
            AppSystemLog.Action _actionType = AppSystemLog.Action.Update;
            var trans = UnitOfWork.BeginTransaction();
            try
            {
                var entity = this.UnitOfWork.GetAllNoTracking<RefundHeaders1>()
                    .SingleOrDefault(x => x.Id == dto.Id);
                entity.UpdateDate = DateTime.Now;
                entity.UpdateBy = dto.UpdateBy;
                switch (dto.ActionType)
                {
                    case "Update":
                        _actionType = AppSystemLog.Action.Update;

                        entity.StatusID = (byte)SaleOrderStatuses.Updated;
                        entity.TotalVATAmount = dto.TotalVATAmount;
                        entity.TotalAmountWithVAT = dto.TotalAmountWithVAT;
                        entity.TotalAmountWithoutVAT = dto.TotalAmountWithoutVAT;
                        result = await this.UnitOfWork.UpdateAsync(entity, new List<Expression<Func<RefundHeaders1, object>>>() { x => x.UpdateDate, x => x.UpdateBy, x => x.StatusID, x => x.TotalVATAmount, x => x.TotalAmountWithVAT, x => x.TotalAmountWithoutVAT });
                        result = await this.UnitOfWork.UpdateToListAsync<SaleOrderRefundItemDto, RefundItems1>(dto.Items);
                        break;
                    case "SendRequest":
                        _actionType = AppSystemLog.Action.SendRequest;

                        entity.StatusID = (byte)SaleOrderStatuses.Waiting;
                        result = await this.UnitOfWork.UpdateAsync(entity, new List<Expression<Func<RefundHeaders1, object>>>() { x => x.UpdateDate, x => x.UpdateBy, x => x.StatusID });
                        break;
                    case "Approve":
                        _actionType = AppSystemLog.Action.Approve;

                        entity.StatusID = (byte)SaleOrderStatuses.Approved;
                        result = await this.UnitOfWork.UpdateAsync(entity, new List<Expression<Func<RefundHeaders1, object>>>() { x => x.UpdateDate, x => x.UpdateBy, x => x.StatusID });
                        break;
                    case "Reject":
                        _actionType = AppSystemLog.Action.Reject;
                        entity.StatusID = (byte)SaleOrderStatuses.Rejected;
                        result = await this.UnitOfWork.UpdateAsync(entity, new List<Expression<Func<RefundHeaders1, object>>>() { x => x.UpdateDate, x => x.UpdateBy, x => x.StatusID });
                        break;
                    case "Invoice":
                    case "ManualAdjustInvoice":
                        _actionType = AppSystemLog.Action.Invoice;

                        #region Lấy thông tin đơn gốc
                        var rootSale = await _saleBusiness.GetAsync(dto.SaleOrderId, dto.StoreCode);
                        dto.Root = rootSale;
                        var customerData = this.UnitOfWork.GetAllNoTracking<CustomerData>().Where(x => x.CustomerID == rootSale.CustomerID).FirstOrDefault();
                        if (dto.Root.Invoices.Any() && customerData != null)
                        {
                            /* Change request 08-12-2025
                              MW: "Xuất hóa đơn Manual Refund (BuyerEmail, BuyerPhoneNumber)
	+ Hiện tại: MW đang dùng thông tin email, sdt khách để xuất hóa đơn refund
	+ Mong muốn: không gửi 2 thông tin này cho TS, để trống"
                             */
                            dto.CustomerName = customerData.FirstName + " " + customerData.LastName;
                            //dto.CustomerEmail = customerData.Email;
                            dto.CustomerEmail = "";
                            var taxName = dto.Root.Invoices[0].CustomerName;
                            if (string.IsNullOrEmpty(taxName))
                                taxName = dto.Root.Invoices[0].CompanyName;
                            dto.Root.Business = new BusinessDto()
                            {
                                TaxCode = "",
                                TaxName = taxName,
                                TaxAddress = dto.Root.Invoices[0].Address,
                                //Phone = customerData.PhoneNumber,
                                //Email = customerData.Email,
                                Phone = "",
                                Email = "",
                                Fax = ""
                            };
                        }
                        #endregion
                        var validate = await ReValidateCOD(dto);
                        dto = validate.Item2;
                        #region Lưu lại thông tin Refunded hoặc Error (nếu có)
                        result = await this.UnitOfWork.UpdateToListAsync<SaleOrderRefundItemDto, RefundItems1>(dto.Items);
                        if (result)
                            result = await this.UnitOfWork.UpdateAsync(entity, new List<Expression<Func<RefundHeaders1, object>>>() { x => x.UpdateDate, x => x.UpdateBy });
                        trans.Commit();
                        trans = UnitOfWork.BeginTransaction();
                        #endregion

                        if (result)
                            result = validate.Item1;
                        if (result)
                        {
                            #region Gọi API TS    
                            RefundInvoices1 invoice = null;
                            if (rootSale != null && rootSale.Invoices != null && rootSale.Invoices.Any())
                            {
                                if (!dto.ManualInvoice)
                                {
                                    if (dto.ActionType == "ManualAdjustInvoice")
                                        invoice = ManualAdjustInvoice(dto, dto.URL);
                                    else
                                        invoice = AdjustInvoice(dto);
                                }
                                else
                                {
                                    invoice = new RefundInvoices1
                                    {
                                        InvoiceKey = Guid.NewGuid(),
                                        InvoiceID = 0,
                                        StoreCode = dto.StoreCode,
                                        VatCode = dto.Root.Business.TaxCode,
                                        InvoiceTemplateCode = "1",
                                        InvoiceSeries = "K24TAA",
                                        InvoiceNumber = "ManualInvoice",
                                        InvoiceIssuedDate = DateTime.Now.ToString("yyyy-MM-dd"),
                                        IntegrateKey = Guid.NewGuid().ToString(),
                                        InvoiceReceiveNumber = "ManualInvoice",
                                        RootIntegrateKey = dto.Invoices[0].IntegrateKey,
                                        RootInvoiceNumber = dto.Invoices[0].InvoiceNumber,
                                        RootInvoiceSeries = dto.Invoices[0].InvoiceSeries,
                                        RootInvoiceTemplateCode = dto.Invoices[0].RootInvoiceTemplateCode,
                                        HeaderId = dto.Id,
                                        CustomerName = dto.CustomerName,
                                        CompanyName = dto.Root.Business?.TaxName,
                                        Address = dto.Root.Business?.TaxAddress,
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
                                    entity.StatusID = (byte)SaleOrderStatuses.Invoiced;
                                    entity.OrderNumber = GeneralOrderNumberCOD(dto.StoreCode, rootSale.CustomerType);
                                    if (string.IsNullOrEmpty(entity.OrderNumber))
                                    {
                                        dto.Comment = "Error when generating OrderNumber";
                                        result = false;
                                    }
                                    else
                                        dto.OrderNumber = entity.OrderNumber;
                                    entity.RefundDate = DateTime.Now;
                                    entity.RefundTime = DateTime.Now.TimeOfDay.ToString("hhmm");
                                    result = await this.UnitOfWork.UpdateAsync(entity, new List<Expression<Func<RefundHeaders1, object>>>() { x => x.UpdateDate, x => x.UpdateBy, x => x.StatusID, x => x.RefundDate, x => x.RefundTime, x => x.OrderNumber });
                                }
                            }
                            else
                            {
                                result = false;
                                dto.Comment = "The root invoice is not exist.";
                            }

                            #endregion

                            if (result)
                            {
                                dto.RefundDate = entity.RefundDate;
                                dto.RefundTime = entity.RefundTime;
                                dto.OrderNumber = entity.OrderNumber;
                                result = CreateRefundCODData(dto, invoice);
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
                result = false;
                this.UnitOfWork.Rollback(trans);
                LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
                dto.Comment = ex.StackTrace + "---" + ex.Message;
            }

            InsertHistory(dto, (int)_actionType, result ? 1 : 0);
            return result;
        }
        public async Task<string> WriteRefundCsvAsync(Guid headerId)
        {
            var headerDto = await this.UnitOfWork.GetAllNoTracking<RefundHeader>().Where(x => x.Id == headerId)
                .Include(x => x.RefundItems)
                .Include("RefundItems.RefundPromotions")
                .Include(x => x.RefundPayments)
                .FirstOrDefaultAsync();
            var refundInvoices = await this.UnitOfWork.GetAllNoTracking<RefundInvoice>().Where(x => x.HeaderId == headerId).ToListAsync();
            var saleHeaderOrderNumber = this.UnitOfWork.GetAllNoTracking<RefundHeaders1>()
                .Where(x => x.Id == headerId)
                .Include(x => x.Headers1)
                .FirstOrDefault()?.Headers1?.OrderNumber;

            var saleData = "";
            #region Record H
            saleData += "H";
            saleData += "|" + headerDto.MallCode;
            saleData += "|" + headerDto.RefundDate;
            saleData += "|" + headerDto.RefundTime;
            saleData += "|" + headerDto.OrderNumber;
            saleData += "|" + saleHeaderOrderNumber;
            saleData += "|" + headerDto.MallCode;
            saleData += "|" + headerDto.SalesDate;
            saleData += "|" + headerDto.ReasonCode;
            saleData += "|" + headerDto.Description;
            saleData += "|" + headerDto.OrderNumber;
            //saleData += "|" + headerDto.CustomerID;
            //saleData += "|" + headerDto.FoxtrotUserID;
            saleData += "\n";
            #endregion
            #region Record A
            if (headerDto.RefundItems != null && headerDto.RefundItems.Any())
            {
                foreach (var item in headerDto.RefundItems)
                {
                    saleData += "A";
                    saleData += "|" + item.Sku;
                    saleData += "|" + item.QuantityRefunded;
                    saleData += "|" + Math.Round(item.SellingPrice, 0);
                    saleData += "|" + (Math.Round(item.ListPrice, 0) * (-1));
                    saleData += "|" + ((item.SellingPrice * item.QuantityRefunded - Math.Round(item.SellingPrice / (1 + item.VATCode / 100) * item.QuantityRefunded, 0)) * (-1));
                    saleData += "|" + Math.Round(item.VATCode, 0);

                    if (item.RefundPromotions != null && item.RefundPromotions.Any())
                    {
                        var groupPromotions = item.RefundPromotions
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

                                saleData += "|" + Math.Round((double)promotionAmount, 0);
                                saleData += "|" + group.Key.PNLAllocation;
                                saleData += "|" + group.Key.TransactionType;
                            }
                        }
                    }
                    saleData += "\n";
                }
            }

            #endregion
            #region Record B
            if (headerDto.RefundPayments != null && headerDto.RefundPayments.Any())
            {
                foreach (var payment in headerDto.RefundPayments)
                {
                    var paymentType = (!string.IsNullOrEmpty(payment.PaymentType) && payment.PaymentType?.Length > 3)
                        ? payment.PaymentType.Substring(0, 3)
                        : payment.PaymentType;
                    saleData += "B";
                    saleData += "|" + paymentType;
                    saleData += "|" + Math.Round(payment.AmountRefund, 0);
                    saleData += "|" + payment.TransactionID;
                    saleData += "|" + payment.AuthorizationID;
                    saleData += "|" + payment.UserID;
                    saleData += "\n";
                }
            }
            #endregion
            #region Record C
            if (refundInvoices != null && refundInvoices.Any())
            {
                foreach (var invoice in refundInvoices)
                {
                    saleData += "C";
                    saleData += "|" + invoice.Code;
                    saleData += "|" + invoice.SerialNo;
                    saleData += "|" + invoice.Number;

                    string customerNameVn = UnicodeOriginToUnicodeVN1258(invoice.CustomerName.Trim());
                    byte[] bytesCustomerName = Encoding.GetEncoding(1258).GetBytes(customerNameVn);
                    string customerName = Encoding.Default.GetString(bytesCustomerName);
                    saleData += "|" + customerName;

                    string companyNameVn = UnicodeOriginToUnicodeVN1258(invoice.Company.Trim());
                    byte[] bytesCompanyName = Encoding.GetEncoding(1258).GetBytes(companyNameVn);
                    string companyName = Encoding.Default.GetString(bytesCompanyName);
                    saleData += "|" + companyName;

                    string addressVn = UnicodeOriginToUnicodeVN1258(invoice.Address.Trim());
                    byte[] bytesAddress = Encoding.GetEncoding(1258).GetBytes(addressVn);
                    string address = Encoding.Default.GetString(bytesAddress);
                    saleData += "|" + address;

                    saleData += "|" + invoice.TaxCode;
                    saleData += "|" + invoice.CQTCode;
                    saleData += "\n";
                }
            }
            #endregion
            saleData += "END";
            return saleData;
        }
        public async Task<string> WriteRefundCODCsvAsync(Guid headerId)
        {
            var headerDto = await this.UnitOfWork.GetAllNoTracking<RefundHeader>().Where(x => x.Id == headerId)
                .Include(x => x.RefundItems)
                .Include("RefundItems.RefundPromotions")
                .Include(x => x.RefundPayments)
                .FirstOrDefaultAsync();
            var refundInvoices = await this.UnitOfWork.GetAllNoTracking<RefundInvoice>().Where(x => x.HeaderId == headerId).ToListAsync();

            var refundCOD = this.UnitOfWork.GetAllNoTracking<RefundHeaders1>()
                .FirstOrDefault(x => x.Id == headerId);
            var rootRecordSale = this.UnitOfWork.GetAllNoTracking<RecordSale>().FirstOrDefault(x => x.HeaderId == refundCOD.SaleOrderId && x.StoreCode == refundCOD.StoreCode);

            var saleData = "";
            #region Record H
            saleData += "H";
            saleData += "|" + headerDto.MallCode;
            saleData += "|" + headerDto.RefundDate;
            saleData += "|" + headerDto.RefundTime;
            saleData += "|" + headerDto.OrderNumber;
            saleData += "|" + rootRecordSale.BillNumber;
            saleData += "|" + headerDto.MallCode;
            saleData += "|" + headerDto.SalesDate;
            saleData += "|" + headerDto.ReasonCode;
            saleData += "|" + headerDto.Description;
            saleData += "|" + refundCOD.OrderNumber;
            //saleData += "|" + headerDto.CustomerID;
            //saleData += "|" + headerDto.FoxtrotUserID;
            saleData += "\n";
            #endregion
            #region Record A
            if (headerDto.RefundItems != null && headerDto.RefundItems.Any())
            {
                foreach (var item in headerDto.RefundItems)
                {
                    saleData += "A";
                    saleData += "|" + item.Sku;
                    saleData += "|" + item.QuantityRefunded;
                    saleData += "|" + Math.Round(item.SellingPrice, 0);
                    saleData += "|" + (Math.Round(item.ListPrice, 0) * (-1));
                    saleData += "|" + ((item.SellingPrice * item.QuantityRefunded - Math.Round(item.SellingPrice / (1 + item.VATCode / 100) * item.QuantityRefunded, 0)) * (-1));
                    saleData += "|" + Math.Round(item.VATCode, 0);

                    if (item.RefundPromotions != null && item.RefundPromotions.Any())
                    {
                        var groupPromotions = item.RefundPromotions
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

                                saleData += "|" + Math.Round((double)promotionAmount, 0);
                                saleData += "|" + group.Key.PNLAllocation;
                                saleData += "|" + group.Key.TransactionType;
                            }
                        }
                    }
                    saleData += "\n";
                }
            }

            #endregion
            #region Record B
            if (headerDto.RefundPayments != null && headerDto.RefundPayments.Any())
            {
                headerDto.RefundPayments = headerDto.RefundPayments.OrderBy(x => x.CreateDate).ToList();
                foreach (var payment in headerDto.RefundPayments)
                {
                    var paymentType = (!string.IsNullOrEmpty(payment.PaymentType) && payment.PaymentType?.Length > 3)
                        ? payment.PaymentType.Substring(0, 3)
                        : payment.PaymentType;
                    saleData += "B";
                    saleData += "|" + paymentType;
                    saleData += "|" + Math.Round(payment.AmountRefund, 0);
                    saleData += "|" + payment.TransactionID;
                    saleData += "|" + payment.AuthorizationID;
                    saleData += "|" + payment.UserID;
                    saleData += "\n";
                }
            }
            #endregion
            #region Record C
            if (refundInvoices != null && refundInvoices.Any())
            {
                foreach (var invoice in refundInvoices)
                {
                    saleData += "C";
                    saleData += "|" + invoice.Code;
                    saleData += "|" + invoice.SerialNo;
                    saleData += "|" + invoice.Number;

                    string customerNameVn = UnicodeOriginToUnicodeVN1258(invoice.CustomerName.Trim());
                    byte[] bytesCustomerName = Encoding.GetEncoding(1258).GetBytes(customerNameVn);
                    string customerName = Encoding.Default.GetString(bytesCustomerName);
                    saleData += "|" + customerName;

                    string companyNameVn = UnicodeOriginToUnicodeVN1258(invoice.Company.Trim());
                    byte[] bytesCompanyName = Encoding.GetEncoding(1258).GetBytes(companyNameVn);
                    string companyName = Encoding.Default.GetString(bytesCompanyName);
                    saleData += "|" + companyName;

                    string addressVn = UnicodeOriginToUnicodeVN1258(invoice.Address.Trim());
                    byte[] bytesAddress = Encoding.GetEncoding(1258).GetBytes(addressVn);
                    string address = Encoding.Default.GetString(bytesAddress);
                    saleData += "|" + address;

                    saleData += "|" + invoice.TaxCode;
                    saleData += "|" + invoice.CQTCode;
                    saleData += "\n";
                }
            }
            #endregion
            saleData += "END";
            return saleData;
        }

        public async Task<RefundHeaderDto> WriteRefundCsvFileAsync(Guid headerId)
        {
            var header = await this.UnitOfWork.GetAllNoTracking<RefundHeader>().Where(x => x.Id == headerId)
                .Include(x => x.RefundItems)
                .Include("RefundItems.RefundPromotions")
                .Include(x => x.RefundPayments)
                .FirstOrDefaultAsync();
            var refundInvoices = await this.UnitOfWork.GetAllNoTracking<RefundInvoice>().Where(x => x.HeaderId == headerId).ToListAsync();
            var saleHeaderOrderNumber = this.UnitOfWork.GetAllNoTracking<RefundHeaders1>()
                .Where(x => x.Id == headerId)
                .Include(x => x.Headers1)
                .FirstOrDefault()?.Headers1?.OrderNumber;

            var headerDto = Mapper.Map<RefundHeaderDto>(header);
            headerDto.RefundInvoices = Mapper.Map<List<RefundInvoiceDto>>(refundInvoices);
            headerDto.ReceiptNumber = headerDto.OrderNumber;
            headerDto.SaleOrderNumber = saleHeaderOrderNumber;
            return headerDto;
        }
        public async Task<RefundHeaderDto> WriteRefundCODCsvFileAsync(Guid headerId)
        {
            var header = await this.UnitOfWork.GetAllNoTracking<RefundHeader>().Where(x => x.Id == headerId)
                .Include(x => x.RefundItems)
                .Include("RefundItems.RefundPromotions")
                .Include(x => x.RefundPayments)
                .FirstOrDefaultAsync();
            var refundInvoices = await this.UnitOfWork.GetAllNoTracking<RefundInvoice>().Where(x => x.HeaderId == headerId).ToListAsync();
            var refundCOD = this.UnitOfWork.GetAllNoTracking<RefundHeaders1>()
                .FirstOrDefault(x => x.Id == headerId);
            var rootRecordSale = this.UnitOfWork.GetAllNoTracking<RecordSale>().FirstOrDefault(x => x.HeaderId == refundCOD.SaleOrderId && x.StoreCode == refundCOD.StoreCode);

            if (header.RefundPayments?.Count > 0)
                header.RefundPayments = header.RefundPayments.OrderBy(x => x.CreateDate).ToList();
            var headerDto = Mapper.Map<RefundHeaderDto>(header);
            headerDto.RefundInvoices = Mapper.Map<List<RefundInvoiceDto>>(refundInvoices);
            headerDto.ReceiptNumber = refundCOD.OrderNumber;
            headerDto.SaleOrderNumber = rootRecordSale.BillNumber;
            return headerDto;
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
        private string GeneralOrderNumberCOD(string storeCode, string customerType)
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
                if (customerType == PaymentTypeCustomerTypes.B2BOnline)
                {
                    var B2B_POS_ID = this.UnitOfWork.GetAllNoTracking<SystemSetting>().Where(x => x.ActiveFlag == STATUS_ACTIVE && x.Name == "B2B_POS_ID").FirstOrDefault();
                    if (B2B_POS_ID == null)
                    {
                        POSNumber = 888;
                    }
                    else
                    {
                        POSNumber = int.Parse(B2B_POS_ID.Value);
                    }
                }

                var BillNumber = UnitOfWork.GetItem<BillNumber, BillNumber>(x => x.CurrentDate == today && x.StoreCode == storeCode && x.POSNumber == POSNumber);

                int maxOrderNumber = 0;
                if (BillNumber != null)
                    maxOrderNumber = BillNumber.Current.GetValueOrDefault();

                //if (maxOrderNumber > 9999)
                //{
                //    maxOrderNumber = 0;
                //    POSNumber = store.POSNumber2.GetValueOrDefault();
                //    BillNumber = UnitOfWork.GetItem<BillNumber, BillNumber>(x => x.CurrentDate == today && x.StoreCode == storeCode && x.POSNumber == POSNumber);
                //    if (BillNumber != null)
                //        maxOrderNumber = BillNumber.Current.GetValueOrDefault();
                //}

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
        private RefundInvoices1 AdjustInvoice(SaleOrderRefundDto dto)
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
                            <RootIntegrateKey>{dto.Root.Invoices[0].IntegrateKey}</RootIntegrateKey>
				            <RootTemplateCode>{dto.Root.Invoices[0].InvoiceTemplateCode}</RootTemplateCode>
				            <RootInvoiceSeries>{dto.Root.Invoices[0].InvoiceSeries}</RootInvoiceSeries>
				            <RootInvoiceNumber>{dto.Root.Invoices[0].InvoiceNumber}</RootInvoiceNumber>
            				<InvoiceTypeCode>{InvoiceTypeCodes.VATInvoice}</InvoiceTypeCode>
            				<InvoiceTypeName>Hóa đơn giá trị gia tăng</InvoiceTypeName>
            				<AdjustmentType>{(int)AdjustmentTypes.InvoiceAdjustment}</AdjustmentType>
            				<InvoiceIssuedDate>{DateTime.Now.ToString("yyyy/MM/dd")}</InvoiceIssuedDate>
            				<InvoiceCreateDate>{DateTime.Now.ToString("yyyy/MM/dd")}</InvoiceCreateDate>
            				<BranchCode>{dto.StoreCode}</BranchCode>
            				<SellerLegalCode>{store.MerchantTax}</SellerLegalCode>
            				<SellerTaxCode>{store.MerchantTax}</SellerTaxCode>
            				<SellerLegalName>{SecurityElement.Escape(store.TaxName)}</SellerLegalName>
            				<SellerAddressLine>{SecurityElement.Escape(store.TaxAddress)}</SellerAddressLine>
            				<BuyerCode>{dto.Root.Business.TaxCode}</BuyerCode>
            				<BuyerTaxCode>{dto.Root.Business.TaxCode}</BuyerTaxCode>
            				<BuyerLegalName>{SecurityElement.Escape(dto.Root.Business.TaxName)}</BuyerLegalName>
            				<BuyerDisplayName>{SecurityElement.Escape(dto.CustomerName)}</BuyerDisplayName>
            				<BuyerAddressLine>{SecurityElement.Escape(dto.Root.Business.TaxAddress)}</BuyerAddressLine>";
                if (!string.IsNullOrEmpty(dto.CustomerEmail))
                    xml += $@"<BuyerEmail>{dto.CustomerEmail}</BuyerEmail>";
                if (!string.IsNullOrEmpty(dto.Root.Business.Phone))
                    xml += $@"<BuyerPhoneNumber>{dto.Root.Business.Phone}</BuyerPhoneNumber>";
                if (!string.IsNullOrEmpty(dto.Root.Business.Fax))
                    xml += $@"<BuyerFaxNumber>{dto.Root.Business.Fax}</BuyerFaxNumber>";
                //xml += "<BuyerCitizenIdentityNo></BuyerCitizenIdentityNo>";
                //xml += "<TaxCodeResult></TaxCodeResult>";
                xml += $@" <CurrencyCode>VND</CurrencyCode>
            				<ExchangeRate>1</ExchangeRate>
            				<PaymentMethodCode>04</PaymentMethodCode>
            				<TotalVATAmount>{dto.TotalVATAmount.ToString().Replace(",", ".")}</TotalVATAmount>
            				<TotalAmountWithoutVAT>{dto.TotalAmountWithoutVAT.ToString().Replace(",", ".")}</TotalAmountWithoutVAT>
            				<TotalAmountWithVAT>{dto.TotalAmountWithVAT.ToString().Replace(",", ".")}</TotalAmountWithVAT>
                            <inv:dataExtension>
                            <![CDATA[
				                <EXT_VARCHAR1>{dto.OrderNumber}</EXT_VARCHAR1> <!-- Thông tin Số tham chiếu -->
				            ]]>
                            </inv:dataExtension>
            				<Goods>";

                for (int i = 0; i < dto.Items.Count; i++)
                {
                    var good = dto.Items[i];
                    var vatCode = good.IsTaxB2B && good.VATCode == 0 ? -1 : good.VATCode;
                    xml += $@"<GoodsEntity>
            				<lineNumber>{i + 1}</lineNumber>
                            <AdjustmentStatus>-1</AdjustmentStatus>
            				<ItemCode>{good.Sku}</ItemCode>
            				<ItemName>{SecurityElement.Escape(good.Name)}</ItemName>
            				<UnitCode>{SecurityElement.Escape(good.UnitType)}</UnitCode>
            				<UnitName>{SecurityElement.Escape(good.UnitType)}</UnitName>
            				<Quantity>{good.Quantity.ToString()}</Quantity>
            				<UnitPrice>{good.POPrice.ToString().Replace(",", ".")}</UnitPrice>
            				<VatPercentage>{vatCode.ToString().Replace(",", ".")}</VatPercentage>
            			</GoodsEntity>";
                }
                xml += $@"</Goods>
                            <AdjustmentContents>{dto.ReasonCode}</AdjustmentContents>
				            <AdjustmentDate>{DateTime.Now.ToString("yyyy/MM/dd")}</AdjustmentDate>
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
                LogError("Start send RefundID " + dto.Id + " to API ThaiSon!");
                SOAPReqBody.LoadXml(xml);
                using (Stream stream = request.GetRequestStream())
                {
                    SOAPReqBody.Save(stream);
                }

                #region Lưu file request
                var filePath = ConfigurationSettings.AppSettings[TempFolder_Key] ?? TempFolder_Default;
                filePath += "\\AdjustInvoices\\" + dto.Id.ToString();
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
                        LogError("Receiver result from API ThaiSon!");
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
                            var invoice = new RefundInvoices1()
                            {
                                InvoiceKey = Guid.Parse(output.InvoiceKey),
                                InvoiceID = output.InvoiceID,
                                StoreCode = output.BranchCode,
                                VatCode = dto.Root.Business.TaxCode,
                                InvoiceTemplateCode = output.InvoiceTemplateCode,
                                InvoiceSeries = output.InvoiceSeries,
                                InvoiceNumber = output.InvoiceNumber,
                                InvoiceIssuedDate = output.InvoiceIssuedDate,
                                IntegrateKey = output.IntegrateKey,
                                InvoiceReceiveNumber = output.InvoiceReceiveNumber,

                                RootIntegrateKey = output.RootIntegrateKey,
                                RootInvoiceNumber = output.RootInvoiceNumber,
                                RootInvoiceSeries = output.RootInvoiceSeries,
                                RootInvoiceTemplateCode = output.RootTemplateCode,

                                HeaderId = dto.Id,
                                CustomerName = dto.CustomerName,
                                CompanyName = dto.Root.Business.TaxName,
                                Address = dto.Root.Business.TaxAddress,
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
                            LogError("Error: Request send RefundID " + dto.Id + " to API ThaiSon: " + reqXML);
                            LogError("Error: Response from API ThaiSon: " + resXML);
                        }
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                var byteReqXML = Encoding.Unicode.GetBytes(SOAPReqBody.InnerXml);
                string reqXML = Encoding.Unicode.GetString(byteReqXML);
                LogError("Error: Request send RefundID " + dto.Id + " to API ThaiSon: " + reqXML);
                LogError("Error: Response from API ThaiSon: " + ServiceResult);
                LogError("Error: " + ex.StackTrace + "---" + ex.Message);
            }
            return null;
        }
        private RefundInvoices1 ManualAdjustInvoice(SaleOrderRefundDto dto, string responseXML)
        {
            XmlDocument SOAPReqBody = new XmlDocument();
            string ServiceResult = responseXML;
            try
            {
                var store = this.UnitOfWork.GetItem<StoreDto, Store>(x => x.Code == dto.StoreCode);
                if (store == null)
                    return null;

                var filePath = ConfigurationSettings.AppSettings[TempFolder_Key] ?? TempFolder_Default;
                filePath += "\\AdjustInvoices\\" + dto.Id.ToString();
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
                    var invoice = new RefundInvoices1()
                    {
                        InvoiceKey = Guid.Parse(output.InvoiceKey),
                        InvoiceID = output.InvoiceID,
                        StoreCode = output.BranchCode,
                        VatCode = dto.Root.Business.TaxCode,
                        InvoiceTemplateCode = output.InvoiceTemplateCode,
                        InvoiceSeries = output.InvoiceSeries,
                        InvoiceNumber = output.InvoiceNumber,
                        InvoiceIssuedDate = output.InvoiceIssuedDate,
                        IntegrateKey = output.IntegrateKey,
                        InvoiceReceiveNumber = output.InvoiceReceiveNumber,

                        RootIntegrateKey = output.RootIntegrateKey,
                        RootInvoiceNumber = output.RootInvoiceNumber,
                        RootInvoiceSeries = output.RootInvoiceSeries,
                        RootInvoiceTemplateCode = output.RootTemplateCode,

                        HeaderId = dto.Id,
                        CustomerName = dto.CustomerName,
                        CompanyName = dto.Root.Business.TaxName,
                        Address = dto.Root.Business.TaxAddress,
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
        private bool CreateRefundData(SaleOrderRefundDto dto, RefundInvoices1 invoice)
        {
            var b2bDefaultCustomerID = "avncitimart";
            var saleOrder = this.UnitOfWork.GetAllNoTracking<Headers1>()
                .FirstOrDefault(x => x.Id == dto.SaleOrderId);
            var foxtrotUserID = GuidToUUID(saleOrder.BusinessId);
            #region 1. Create refund header
            var header = new RefundHeader()
            {
                Id = dto.Id,
                MallCode = dto.StoreCode,
                RefundDate = dto.RefundDate.ToString("yyyyMMdd"),
                RefundTime = dto.RefundTime,
                OrderNumber = dto.OrderNumber,
                ReasonCode = dto.ReasonCode,
                Description = dto.ReasonName,
                SalesDate = dto.SalesDate,
                CustomerID = b2bDefaultCustomerID,
                FoxtrotUserID = foxtrotUserID,
                IsTransfer = false,
                CreateBy = dto.UpdateBy,
                UpdateBy = dto.UpdateBy,
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now,
                ActiveFlag = STATUS_ACTIVE
            };
            #endregion

            #region 2. Create sale Item
            foreach (var x in dto.Items)
            {
                RefundItem item = new RefundItem()
                {
                    Id = x.Id,
                    HeaderId = header.Id,
                    Sku = x.Sku,
                    QuantityRefunded = x.Quantity * (-1),
                    SellingPrice = x.POPrice,
                    ListPrice = x.ListPrice * (-1),
                    StoreCode = dto.StoreCode,
                    VATAmount = x.VATAmount * (-1),
                    VATCode = x.VATCode,
                    CreateBy = dto.UpdateBy,
                    UpdateBy = dto.UpdateBy,
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    ActiveFlag = STATUS_ACTIVE,
                };
                if (x.PromotionAmount != 0)
                {
                    item.RefundPromotions = new List<RefundPromotion>() {
                        new RefundPromotion {
                            Id = Guid.NewGuid(),
                            ItemId = x.Id,
                            PNLAllocation = x.PNLAllocation,
                            PromotionAmount = x.PromotionAmount,
                            TransactionType = x.TransactionType,
                            CreateBy = dto.UpdateBy,
                            UpdateBy = dto.UpdateBy,
                            CreateDate = DateTime.Now,
                            UpdateDate = DateTime.Now,
                            ActiveFlag = STATUS_ACTIVE,
                        }
                    };
                }
                header.RefundItems.Add(item);
            }
            #endregion

            #region 3. Create sale Payment
            header.RefundPayments.Add(new RefundPayment
            {
                Id = Guid.NewGuid(),
                HeaderId = header.Id,
                PaymentType = "CHQ",
                AmountRefund = (double)dto.TotalAmountWithVAT * (-1),
                //TransactionID = DateTime.Now.ToString("yyyyMMdd"),
                //UserID = dto.UpdateBy,

                CreateBy = dto.UpdateBy,
                UpdateBy = dto.UpdateBy,
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now,
                ActiveFlag = STATUS_ACTIVE
            });
            #endregion
            var rs = this.UnitOfWork.Insert(header) != null;
            #region 4. Create sale Invoice
            if (rs)
            {
                var refundinvoice = new RefundInvoice
                {
                    Id = invoice.InvoiceKey,
                    HeaderId = header.Id,
                    Code = invoice.InvoiceTemplateCode,
                    SerialNo = invoice.InvoiceSeries,
                    Number = invoice.InvoiceNumber,
                    CustomerName = invoice.CustomerName,
                    Company = invoice.CompanyName,
                    Address = invoice.Address,
                    TaxCode = invoice.VatCode,
                    StoreCode = invoice.StoreCode,
                    CQTCode = invoice.CQTCode,

                    CreateBy = dto.UpdateBy,
                    UpdateBy = dto.UpdateBy,
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    ActiveFlag = STATUS_ACTIVE
                };
                rs = this.UnitOfWork.Insert(refundinvoice) != null;
            }
            #endregion

            return rs;
        }
        private bool CreateRefundCODData(SaleOrderRefundDto dto, RefundInvoices1 invoice)
        {
            var rootSale = this.UnitOfWork.GetAllNoTracking<Header>()
                .Where(x => x.Id == dto.SaleOrderId)
                .Include(x => x.Items).Include("Items.Promotions")
                //.Include(x => x.Payments)
                .Include(x => x.Deliveries)
                .FirstOrDefault();
            rootSale.Items = rootSale.Items.Where(x => x.StoreCode == dto.StoreCode).ToList();
            #region Lấy thêm SKU delivery
            var iqueryItemForDelivery = this.UnitOfWork.GetAllNoTracking<ItemForDelivery>()
                .Where(x => x.HeaderId == dto.SaleOrderId && x.StoreCode == dto.StoreCode)
                .ToList();
            foreach (var item in iqueryItemForDelivery)
            {
                var itemTemp = new Item
                {
                    Id = item.Id,
                    HeaderId = item.HeaderId,
                    Sku = item.Sku,
                    StoreCode = dto.StoreCode,
                    QuantitySold = item.QuantitySold,
                    SellingPrice = item.SellingPrice,
                    ListPrice = item.ListPrice.GetValueOrDefault(),
                    VATAmount = item.VATAmount.GetValueOrDefault(),
                    VATCode = item.VATCode.GetValueOrDefault(),
                    Promotions = new List<Promotion> { new Promotion
                        {
                            ItemId = item.Id,
                            PNLAllocation = "MERCH",
                            PromotionAmount = item.SellingPrice - item.ListPrice,
                            TransactionType = "P",
                            CreateBy = dto.UpdateBy,
                            UpdateBy = dto.UpdateBy,
                            CreateDate = DateTime.Now,
                            UpdateDate = DateTime.Now,
                            ActiveFlag = STATUS_ACTIVE,
                        }
                    }
                };
                rootSale.Items.Add(itemTemp);
            }
            #endregion

            #region Lấy Payments của Sale gốc
            var rootSalePayments = this.UnitOfWork.GetAllNoTracking<PaymentByStore1>()
                .Where(x => x.HeaderId == dto.SaleOrderId && x.StoreCode == dto.StoreCode)
                .OrderBy(x => x.CreateDate)
                .ToList();
            #endregion
            #region Lấy PaymentTypeMapping
            var paymentTypeMappings = this.UnitOfWork.GetAllNoTracking<PaymentTypeMapping>().Where(x => x.ActiveFlag == STATUS_ACTIVE
                //&& x.Method == (byte)PaymentMethods.Original
                && (x.Scope == (byte)PaymentTypeScopes.RecordSale || !x.Scope.HasValue)
                && (string.IsNullOrEmpty(x.CustomerType) || x.CustomerType == rootSale.CustomerType)
                //&& x.AllowRefund == true
                )
                    .OrderByDescending(x => x.IsMapping)
                    .ToList();
            #endregion

            var b2bDefaultCustomerID = rootSale.CustomerID;
            var foxtrotUserID = rootSale.FoxtrotUserID;
            #region 1. Create refund header
            var header = new RefundHeader()
            {
                Id = dto.Id,
                MallCode = dto.StoreCode,
                RefundDate = dto.RefundDate.ToString("yyyyMMdd"),
                RefundTime = dto.RefundTime,
                OrderNumber = dto.RefundDate.ToString("yyyyMMdd") + rootSale.ActualOrderNumber,
                ReasonCode = dto.ReasonCode,
                Description = dto.ReasonName,
                SalesDate = dto.SalesDate,
                CustomerType = rootSale.CustomerType,
                CustomerID = b2bDefaultCustomerID,
                FoxtrotUserID = foxtrotUserID,
                IsTransfer = false,
                CreateBy = dto.UpdateBy,
                UpdateBy = dto.UpdateBy,
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now,
                ActiveFlag = STATUS_ACTIVE
            };
            #endregion

            #region 2. Create sale Item
            foreach (var x in dto.Items)
            {
                RefundItem item = new RefundItem()
                {
                    Id = x.Id,
                    HeaderId = header.Id,
                    Sku = x.Sku,
                    QuantityRefunded = x.Quantity * (-1),
                    SellingPrice = x.POPrice,
                    ListPrice = x.ListPrice * (-1),
                    StoreCode = dto.StoreCode,
                    VATAmount = x.VATAmount * (-1),
                    VATCode = x.VATCode,
                    CreateBy = dto.UpdateBy,
                    UpdateBy = dto.UpdateBy,
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    ActiveFlag = STATUS_ACTIVE,
                };
                var promotions = rootSale.Items.FirstOrDefault(rootSaleItem => rootSaleItem.StoreCode == item.StoreCode && rootSaleItem.Sku == item.Sku).Promotions;
                if (promotions?.Count > 0)
                {
                    var promotion = promotions.FirstOrDefault();
                    if (promotion != null)
                    {
                        x.PromotionAmount = promotion.PromotionAmount.GetValueOrDefault();
                        x.PNLAllocation = promotion.PNLAllocation;
                        x.TransactionType = promotion.TransactionType;
                    }
                }
                if (x.PromotionAmount != 0)
                {
                    item.RefundPromotions = new List<RefundPromotion>() {
                        new RefundPromotion {
                            Id = Guid.NewGuid(),
                            ItemId = x.Id,
                            PNLAllocation = x.PNLAllocation,
                            PromotionAmount = x.PromotionAmount,
                            TransactionType = x.TransactionType,
                            CreateBy = dto.UpdateBy,
                            UpdateBy = dto.UpdateBy,
                            CreateDate = DateTime.Now,
                            UpdateDate = DateTime.Now,
                            ActiveFlag = STATUS_ACTIVE,
                        }
                    };
                }
                header.RefundItems.Add(item);
            }
            #endregion

            #region 3. Create sale Payment
            var all_Total_Amount = rootSale.Items.Sum(x => x.SellingPrice * x.QuantitySold);
            var rate = (double)dto.TotalAmountWithVAT / all_Total_Amount;
            var datetime = DateTime.Now;
            foreach (var salePayment in rootSalePayments)
            {
                PaymentTypeMapping paymentMapping = paymentTypeMappings.FirstOrDefault(ptm => ptm.Type == salePayment.PaymentType);
                // Set cứng cho B2B - Chờ chốt phương án
                //if (paymentMapping.Method == (byte)PaymentMethods.Voucher && header.CustomerType == PaymentTypeCustomerTypes.B2BOnline)
                //    paymentMapping.SaleToRefund = "ECMRTN";
                if (salePayment.PaymentType == "CRVRTN")
                {
                    datetime = datetime.AddMilliseconds(100);
                    var payment = new RefundPayment
                    {
                        Id = Guid.NewGuid(),
                        HeaderId = header.Id,
                        PaymentType = salePayment.PaymentType,
                        AmountRefund = (double)(salePayment.TotalAmount * rate),
                        TransactionID = salePayment.TransactionID,
                        AuthorizationID = salePayment.AuthID,
                        CreateBy = dto.UpdateBy,
                        UpdateBy = dto.UpdateBy,
                        CreateDate = datetime,
                        UpdateDate = datetime,
                        ActiveFlag = STATUS_ACTIVE
                    };
                    header.RefundPayments.Add(payment);
                }
                else
                {
                    if (paymentMapping.Method != (byte)PaymentMethods.Voucher)
                    {
                        datetime = datetime.AddMilliseconds(100);
                        var payment = new RefundPayment
                        {
                            Id = Guid.NewGuid(),
                            HeaderId = header.Id,
                            PaymentType = paymentMapping?.SaleToRefund ?? salePayment.PaymentType,
                            AmountRefund = (double)(salePayment.TotalAmount * rate * (-1)),
                            TransactionID = salePayment.TransactionID,
                            AuthorizationID = salePayment.AuthID,
                            CreateBy = dto.UpdateBy,
                            UpdateBy = dto.UpdateBy,
                            CreateDate = datetime,
                            UpdateDate = datetime,
                            ActiveFlag = STATUS_ACTIVE
                        };
                        header.RefundPayments.Add(payment);
                    }
                    else
                    {
                        if (rate == 1)
                        {
                            datetime = datetime.AddMilliseconds(100);
                            var payment = new RefundPayment
                            {
                                Id = Guid.NewGuid(),
                                HeaderId = header.Id,
                                PaymentType = paymentMapping?.SaleToRefund ?? salePayment.PaymentType,
                                AmountRefund = (double)(salePayment.TotalAmount * rate * (-1)),
                                TransactionID = salePayment.TransactionID,
                                AuthorizationID = salePayment.AuthID,
                                CreateBy = dto.UpdateBy,
                                UpdateBy = dto.UpdateBy,
                                CreateDate = datetime,
                                UpdateDate = datetime,
                                ActiveFlag = STATUS_ACTIVE
                            };
                            header.RefundPayments.Add(payment);
                        }
                        else
                        {
                            datetime = datetime.AddMilliseconds(100);
                            var payment = new RefundPayment
                            {
                                Id = Guid.NewGuid(),
                                HeaderId = header.Id,
                                PaymentType = paymentMapping?.SaleToRefund ?? salePayment.PaymentType,
                                AmountRefund = (double)(salePayment.TotalAmount * (-1)),
                                TransactionID = salePayment.TransactionID,
                                AuthorizationID = salePayment.AuthID,
                                CreateBy = dto.UpdateBy,
                                UpdateBy = dto.UpdateBy,
                                CreateDate = datetime,
                                UpdateDate = datetime,
                                ActiveFlag = STATUS_ACTIVE
                            };
                            header.RefundPayments.Add(payment);
                            datetime = datetime.AddMilliseconds(100);
                            var paymentCRV = new RefundPayment
                            {
                                Id = Guid.NewGuid(),
                                HeaderId = header.Id,
                                PaymentType = "CRVRTN",
                                AmountRefund = salePayment.TotalAmount - ((double)(salePayment.TotalAmount * rate)),
                                TransactionID = salePayment.TransactionID,
                                AuthorizationID = salePayment.AuthID,
                                CreateBy = dto.UpdateBy,
                                UpdateBy = dto.UpdateBy,
                                CreateDate = datetime,
                                UpdateDate = datetime,
                                ActiveFlag = STATUS_ACTIVE
                            };
                            header.RefundPayments.Add(paymentCRV);
                        }
                    }
                }
            }
            #endregion
            var rs = this.UnitOfWork.Insert(header) != null;
            #region 4. Create sale Invoice
            if (rs)
            {
                var refundinvoice = new RefundInvoice
                {
                    Id = invoice.InvoiceKey,
                    HeaderId = header.Id,
                    Code = invoice.InvoiceTemplateCode,
                    SerialNo = invoice.InvoiceSeries,
                    Number = invoice.InvoiceNumber,
                    CustomerName = invoice.CustomerName,
                    Company = invoice.CompanyName,
                    Address = invoice.Address,
                    TaxCode = invoice.VatCode,
                    StoreCode = invoice.StoreCode,
                    CQTCode = invoice.CQTCode,

                    CreateBy = dto.UpdateBy,
                    UpdateBy = dto.UpdateBy,
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    ActiveFlag = STATUS_ACTIVE
                };
                rs = this.UnitOfWork.Insert(refundinvoice) != null;
            }
            #endregion

            return rs;
        }
        private void InsertHistory(SaleOrderRefundDto dto, int action, int sucess)
        {
            var rs = UnitOfWork.Insert(new SystemLog
            {
                LogId = Guid.NewGuid(),
                Module = AppModule.Refund.ToString(),
                UserId = dto.UpdateBy,
                UserFunction = action,
                EventResult = sucess,
                FuncDateTime = DateTime.Now,
                Source = dto.Id.ToString(),
                Transdata = dto.Comment,
                WSName = ""
            });
            if (rs != null && dto.ActionType == SaleOrderAction.Invoice.ToString())
            {
                var zipFolderPath = ConfigurationSettings.AppSettings[TempFolder_Key] ?? TempFolder_Default;
                zipFolderPath += "\\AdjustInvoices\\" + dto.Id.ToString();
                var zipFilePath = zipFolderPath + ".zip";
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

        public async Task<Tuple<bool, SaleOrderRefundDto>> ReValidate(SaleOrderRefundDto dto)
        {
            bool _valid = true;
            string _msg = string.Empty;

            #region 1. Validate Items
            var rfList = (await GetAllBySaleOrderIdAsync(dto.SaleOrderId)).Where(x => x.StatusID == SaleOrderStatuses.Invoiced && x.Id != dto.Id);
            foreach (var item in dto.Items)
            {
                item.WarningMess = string.Empty;
                var WarningMess = new List<string>();
                int sum = rfList.SelectMany(x => x.Items).Where(y => y.Sku == item.Sku).Sum(z => z.Quantity);
                item.Refunded = sum;
                if ((item.Quantity + sum) > item.RootQuantity)
                {
                    WarningMess.Add("Refund quantity is greater than sale order refunded quantity");
                    _valid = false;
                }
                if (item.IsTaxB2B)
                    WarningMess.Add("This SKU is subject to a preferential tax rate of " + $": {item.VATCode}%");

                item.WarningMess = string.Join("; ", WarningMess);
            }
            #endregion


            if (!_valid)
            {
                dto.Comment = "Refund order is wrong";
            }

            return new Tuple<bool, SaleOrderRefundDto>(_valid, dto);
        }
        public async Task<Tuple<bool, SaleOrderRefundDto>> ReValidateCOD(SaleOrderRefundDto dto)
        {
            bool _valid = true;
            string _msg = string.Empty;

            #region 1. Validate Items
            var rfList = (await GetAllBySaleOrderIdCODAsync(dto.SaleOrderId)).Where(x => x.StatusID == SaleOrderStatuses.Invoiced && x.Id != dto.Id);
            foreach (var item in dto.Items)
            {
                item.WarningMess = string.Empty;
                var WarningMess = new List<string>();
                int sum = rfList.SelectMany(x => x.Items).Where(y => y.Sku == item.Sku).Sum(z => z.Quantity);
                item.Refunded = sum;
                if ((item.Quantity + sum) > item.RootQuantity)
                {
                    WarningMess.Add("Refund quantity is greater than sale order refunded quantity");
                    _valid = false;
                }
                if (item.IsTaxB2B)
                    WarningMess.Add("This SKU is subject to a preferential tax rate of " + $": {item.VATCode}%");

                item.WarningMess = string.Join("; ", WarningMess);
            }
            #endregion


            if (!_valid)
            {
                dto.Comment = "Refund order is wrong";
            }

            return new Tuple<bool, SaleOrderRefundDto>(_valid, dto);
        }
        #endregion
    }
}
