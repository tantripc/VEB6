using AutoMapper;
using MiddlewareTool.Business.Interface;
using MiddlewareTool.Entities;
using MiddlewareTool.OpenXML;
using MiddlewareTool.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using static MiddlewareTool.Dto.ProductMgmtDto;
using System.Text;
using System.Threading.Tasks;
using static MiddlewareTool.Dto.RefundDto;
using static MiddlewareTool.Dto.SaleDto;
using System.Runtime.Remoting.Messaging;

namespace MiddlewareTool.Business.Concrete
{
    public class RecordRefundBusiness : BaseBusiness, IRecordRefundBusiness
    {
        private readonly IRefundBusiness _refundBusiness;
        public RecordRefundBusiness(IUnitOfWork unitOfWork) : base(unitOfWork) { }
        public RecordRefundBusiness(IUnitOfWork unitOfWork, IRefundBusiness refundBusiness) : base(unitOfWork)
        {
            _refundBusiness = refundBusiness;
        }
        public bool Insert(RecordRefundFileDto dto)
        {
            bool result = false;
            try
            {
                var entity = Mapper.Map<RecordRefundFile>(dto);
                entity.Id = Guid.NewGuid();
                entity.CreateDate = dto.CreateDate;
                entity.CreateBy = dto.CreateBy;
                entity.UpdateBy = dto.CreateBy;
                entity.UpdateDate = DateTime.Now;
                var add = this.UnitOfWork.Insert(entity);
                if (add != null) { result = true; }
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return result;
        }
        public bool Update(RecordRefundFileDto dto)
        {
            bool result = false;
            try
            {
                var entity = this.UnitOfWork.GetSingle<RecordRefundFile>(
                    x => x.StoreCode.ToUpper() == dto.StoreCode.ToUpper()
                    && x.Name.ToUpper() == dto.Name.ToUpper()
                    && x.ActiveFlag == STATUS_ACTIVE);
                if (entity != null)
                {
                    entity.UpdateBy = dto.CreateBy;
                    entity.UpdateDate = DateTime.Now;
                    entity.Content = dto.Content;
                    entity.Ext = dto.Ext;
                    entity.Size = dto.Size;
                    result = this.UnitOfWork.Update(entity);
                }
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return result;
        }
        public bool IsExist(string storeCode, string name)
        {
            bool result = true;
            try
            {
                if (!string.IsNullOrEmpty(storeCode) && !string.IsNullOrEmpty(name))
                {
                    var iquery = this.UnitOfWork
                        .GetSingle<RecordRefundFile>(x => x.StoreCode.ToUpper() == storeCode.ToUpper()
                            && x.Name.ToUpper() == name.ToUpper()
                            && x.ActiveFlag == STATUS_ACTIVE);
                    if (iquery == null) { result = false; }
                }
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return result;
        }
        public Tuple<int, List<RecordRefundDto>> GetPaging(string keyWord, string dateFrom, string dateTo, int pageIndex, int pageSize, List<string> storeCodes = null, string customerType = null, List<string> paymentType = null, List<string> deliveryCode = null)
        {
            int total = 0;
            var lstDto = new List<RecordRefundDto>();
            Tuple<int, List<RecordRefundDto>> lstResult = new Tuple<int, List<RecordRefundDto>>(total, lstDto);
            try
            {
                if (storeCodes?.Count > 0)
                {
                    storeCodes = storeCodes.Where(x => !string.IsNullOrEmpty(x)).ToList();
                }
                if (paymentType?.Count > 0)
                {
                    paymentType = paymentType.Where(x => !string.IsNullOrEmpty(x)).ToList();
                }
                if (deliveryCode?.Count > 0)
                {
                    deliveryCode = deliveryCode.Where(x => !string.IsNullOrEmpty(x)).ToList();
                }
                Dictionary<string, object> m_Param = new Dictionary<string, object>()
                {
                    {"@keyWord", keyWord},
                    {"@dateFrom", dateFrom},
                    {"@dateTo", dateTo},
                    {"@pageIndex", pageIndex},
                    {"@pageSize", pageSize},
                    {"@storeCodes", storeCodes !=null ? string.Join(",", storeCodes) : null },
                    {"@customerType", customerType },
                    {"@paymentType", paymentType !=null ? string.Join(",", paymentType) : null },
                    {"@deliveryCode", deliveryCode !=null ? string.Join(",", deliveryCode) : null},
                };
                var ds = this.UnitOfWork.ExecuteQuery(BaseBusiness.SP_RecordRefund_GetPaging, m_Param);
                if (ds != null)
                {
                    total = int.Parse(ds.Tables[1].Rows[0][0].ToString());
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        var dto = new RecordRefundDto();
                        if (ds.Tables[0].Rows[i] != null && dto.ParseData(ds.Tables[0].Rows[i]))
                        {
                            lstDto.Add(dto);
                        }
                    }
                    lstResult = new Tuple<int, List<RecordRefundDto>>(total, lstDto);
                }
            }
            catch (Exception ex)
            {
                this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
                lstResult = null;
            }
            return lstResult;
        }
        public List<RecordRefundDto> Export(string keyWord, string dateFrom, string dateTo, string storeCodes = null, string customerType = null, string paymentType = null, string deliveryCode = null)
        {
            var lstDto = new List<RecordRefundDto>();
            try
            {
                Dictionary<string, object> m_Param = new Dictionary<string, object>()
                {
                    {"@keyWord", keyWord},
                    {"@dateFrom", dateFrom},
                    {"@dateTo", dateTo},
                    {"@storeCodes", storeCodes},
                    {"@customerType", customerType},
                    {"@paymentType", paymentType},
                    {"@deliveryCode", deliveryCode},
                };
                var ds = this.UnitOfWork.ExecuteQuery(BaseBusiness.SP_RecordRefund_Export, m_Param);
                if (ds != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        var result = new RecordRefundDto();
                        if (ds.Tables[0].Rows[i] != null && result.ParseData(ds.Tables[0].Rows[i]))
                        {
                            result.TotalAmount = Math.Round(result.TotalAmount);
                            result.PromotionAmount = Math.Round(result.PromotionAmount);
                            result.VoucherAmount = Math.Round(result.VoucherAmount);
                            result.CreditOverAmount = Math.Round(result.CreditOverAmount);
                            lstDto.Add(result);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
            }
            return lstDto;
        }
        public byte[] GetTransfer(byte[] template, DateTime dateTime)
        {
            var lstDto = new List<RecordRefundBIDto>();
            string dateFrom = dateTime.ToString("yyyy-MM-dd 00:00:00");
            string dateTo = dateTime.ToString("yyyy-MM-dd 23:59:59");
            try
            {
                Dictionary<string, object> m_Param = new Dictionary<string, object>()
                {
                    {"@dateFrom", dateFrom},
                    {"@dateTo", dateTo}
                };
                var ds = this.UnitOfWork.ExecuteQuery(BaseBusiness.SP_RecordRefund_GetTransfer, m_Param);
                if (ds != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        var result = new RecordRefundBIDto();
                        if (ds.Tables[0].Rows[i] != null && result.ParseData(ds.Tables[0].Rows[i]))
                        {
                            lstDto.Add(result);
                        }
                    }
                    if (lstDto.Count <= 0)
                        return null;
                    Excel m_Excel = new Excel();
                    m_Excel.TemplateFileData = template;
                    var data = m_Excel.Export(lstDto);
                    return data;
                }
            }
            catch (Exception ex)
            {
                this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
            }
            return null;
        }
        public bool UpdateTransferred(DateTime dateTime)
        {
            string dateFrom = dateTime.ToString("yyyy-MM-dd 00:00:00");
            string dateTo = dateTime.ToString("yyyy-MM-dd 23:59:59");
            try
            {
                Dictionary<string, object> m_Param = new Dictionary<string, object>()
                {
                    {"@dateFrom", dateFrom},
                    {"@dateTo", dateTo}
                };
                return this.UnitOfWork.ExecuteNonQuery(BaseBusiness.SP_RecordRefund_UpdateTransferred, m_Param);
            }
            catch (Exception ex)
            {
                this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
            }
            return false;
        }

        public List<RecordRefundBIDto> GetPagingRefundHistories(int pageIndex, int pageSize, string actualOrderNumber)
        {
            int total = 0;
            var lstDto = new List<RecordRefundBIDto>();
            try
            {
                Dictionary<string, object> m_Param = new Dictionary<string, object>()
                {

                    {"@pageIndex", pageIndex},
                    {"@pageSize", pageSize},
                    {"@actualOrderNumber", actualOrderNumber }
                };
                var ds = this.UnitOfWork.ExecuteQuery(BaseBusiness.SP_RecordRefund_GetRefundHistories, m_Param);
                if (ds != null)
                {
                    total = int.Parse(ds.Tables[1].Rows[0][0].ToString());
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        var dto = new RecordRefundBIDto();
                        if (ds.Tables[0].Rows[i] != null && dto.ParseData(ds.Tables[0].Rows[i]))
                        {
                            lstDto.Add(dto);
                        }
                    }
                }
                return lstDto;
            }
            catch (Exception ex)
            {
                this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
                return null;
            }
        }
        public RecordRefundBIDto GetById(Guid id)
        {
            try
            {
                Dictionary<string, object> m_Param = new Dictionary<string, object>()
                {
                    {"@id", id.ToString()},
                };
                var ds = this.UnitOfWork.ExecuteQuery(BaseBusiness.SP_RecordRefund_GetDetail, m_Param);
                if (ds != null)
                {
                    var dto = new RecordRefundBIDto();
                    if (ds.Tables[0].Rows[0] != null && dto.ParseData(ds.Tables[0].Rows[0]))
                    {
                        #region Check refund COD thì không hiện nút download File đầu vào
                        dto.IsCOD = this.UnitOfWork.GetAllNoTracking<RefundHeaders1>().Any(x => x.Id == dto.HeaderId && x.IsCOD == true);
                        #endregion

                        return dto;
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
                return null;
            }
        }
        public RefundItemsDto GetPagingItems(int pageIndex, int pageSize, string headerId, string storeCode)
        {
            try
            {
                var itemsDto = new RefundItemsDto();
                Dictionary<string, object> m_Param = new Dictionary<string, object>()
                {
                    {"@pageIndex", pageIndex},
                    {"@pageSize", pageSize},
                    {"@headerId", headerId},
                    {"@storeCode", storeCode }
                };
                var ds = this.UnitOfWork.ExecuteQuery(BaseBusiness.SP_RecordRefund_GetItemsPaging, m_Param);
                if (ds != null)
                {
                    var lstDto = new List<RefundItemDto>();
                    int totalItem = int.Parse(ds.Tables[1].Rows[0][0].ToString());
                    int totalQuantitySold = int.Parse(ds.Tables[2].Rows[0][0].ToString());
                    int totalAmount = int.Parse(ds.Tables[3].Rows[0][0].ToString());
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        var dto = new RefundItemDto();
                        if (ds.Tables[0].Rows[i] != null && dto.ParseData(ds.Tables[0].Rows[i]))
                        {
                            lstDto.Add(dto);
                        }
                    }
                    itemsDto.Items = lstDto;
                    itemsDto.TotalQuantityRefunded = totalQuantitySold;
                    itemsDto.TotalAmount = totalAmount;
                    itemsDto.TotalCount = totalItem;
                }
                return itemsDto;
            }
            catch (Exception ex)
            {
                this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
                return null;
            }
        }
        public async Task<string> WriteBoxedSaleCsvAsync(Guid recordRefundId)
        {
            var recordRefund = this.UnitOfWork
                        .GetSingle<RecordRefund>(x => x.Id == recordRefundId);
            var headerDto = await this.UnitOfWork.GetAllNoTracking<RefundHeader>().Where(x => x.Id == recordRefund.HeaderId)
                .Include(x => x.RefundItems)
                .Include("RefundItems.RefundPromotions")
                .Include(x => x.RefundPayments)
                .FirstOrDefaultAsync();
            var refundInvoices = await this.UnitOfWork.GetAllNoTracking<RefundInvoice>().Where(x => x.HeaderId == recordRefund.HeaderId).ToListAsync();
            var saleHeaderOrderNumber = this.UnitOfWork.GetAllNoTracking<RefundHeaders1>()
                .Where(x => x.Id == recordRefund.HeaderId)
                .Include(x => x.Headers1)
                .FirstOrDefault()?.Headers1?.OrderNumber;

            var saleData = "";
            #region Record H
            saleData += "H";
            saleData += "|" + headerDto.MallCode;
            saleData += "|" + headerDto.RefundDate;
            saleData += "|" + headerDto.RefundTime;
            saleData += "|" + headerDto.OrderNumber;
            saleData += "|" + headerDto.MallCode;
            saleData += "|" + headerDto.SalesDate;
            saleData += "|" + headerDto.ReasonCode;
            saleData += "|" + headerDto.Description;
            saleData += "|" + headerDto.CustomerType;
            saleData += "|" + headerDto.CustomerID;
            saleData += "|" + headerDto.FoxtrotUserID;
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
                    saleData += "|" + item.StoreCode;
                    saleData += "|" + (Math.Round(item.ListPrice, 0));
                    saleData += "|" + item.VATAmount;
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

                headerDto.RefundPayments = headerDto.RefundPayments.OrderBy(p => p.CreateDate).ToList();
                foreach (var payment in headerDto.RefundPayments)
                {
                    //var paymentType = (!string.IsNullOrEmpty(payment.PaymentType) && payment.PaymentType?.Length > 3)
                    //    ? payment.PaymentType.Substring(0, 3)
                    //    : payment.PaymentType;
                    var paymentType = headerDto.CustomerType == "B" && payment.PaymentType == "COC" ? "BOC" : payment.PaymentType;
                    saleData += "B";
                    saleData += "|" + paymentType;
                    saleData += "|" + Math.Round(payment.AmountRefund, 0);
                    saleData += "|" + payment.TransactionID;
                    saleData += "|" + payment.AuthorizationID;
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
                    saleData += "|" + invoice.CustomerName.Trim();
                    saleData += "|" + invoice.Company.Trim();
                    saleData += "|" + invoice.Address.Trim();

                    saleData += "|" + invoice.TaxCode;
                    saleData += "|" + invoice.StoreCode;
                    saleData += "|" + invoice.CQTCode;
                    saleData += "\n";
                }
            }
            #endregion
            saleData += "END";
            return saleData;
        }

        public async Task<string> WriteProfitSaleCsvAsync(Guid recordRefundId)
        {
            var recordRefund = this.UnitOfWork
                        .GetSingle<RecordRefund>(x => x.Id == recordRefundId);
            var headerDto = _refundBusiness.GetTransfer(recordRefund.StoreCode, recordRefund.HeaderId, true);
            var header = await this.UnitOfWork.GetAllNoTracking<RefundHeader>().Where(x => x.Id == recordRefund.HeaderId)
                .FirstOrDefaultAsync();
            var refundInvoices = await this.UnitOfWork.GetAllNoTracking<RefundInvoice>().Where(x => x.HeaderId == recordRefund.HeaderId).ToListAsync();
            //var saleHeaderOrderNumber = this.UnitOfWork.GetAllNoTracking<RefundHeaders1>()
            //    .Where(x => x.Id == recordRefund.HeaderId)
            //    .Include(x => x.Headers1)
            //    .FirstOrDefault()?.Headers1?.OrderNumber;
            var recordSale = this.UnitOfWork.GetAllNoTracking<RecordSale>().Where(s => s.ActualOrderNumber == header.ActualOrderNumber && s.StoreCode == recordRefund.StoreCode).FirstOrDefault();

            var saleData = "";
            #region Record H
            saleData += "H";
            saleData += "|" + recordRefund.StoreCode;
            saleData += "|" + headerDto.RefundDate;
            saleData += "|" + headerDto.RefundTime;
            saleData += "|" + headerDto.OrderNumber;
            saleData += "|" + recordSale.BillNumber ?? "";
            saleData += "|" + headerDto.MallCode;
            saleData += "|" + headerDto.SalesDate;
            saleData += "|" + headerDto.ReasonCode;
            saleData += "|" + headerDto.Description;
            saleData += "|" + recordRefund.ReceiptNumber;
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
                    saleData += "|" + (Math.Round(item.ListPrice, 0) * -1);
                    saleData += "|" + ((item.SellingPrice * item.QuantityRefunded - Math.Round(item.SellingPrice / (1 + item.VATCode / 100) * item.QuantityRefunded, 0)) * -1);
                    saleData += "|" + Math.Round(item.VATCode, 0);

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
    }
}
