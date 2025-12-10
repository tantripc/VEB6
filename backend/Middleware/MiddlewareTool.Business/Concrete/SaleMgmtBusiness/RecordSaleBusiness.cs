using AutoMapper;
using MiddlewareTool.Business.Interface;
using MiddlewareTool.Common;
using MiddlewareTool.Entities;
using MiddlewareTool.OpenXML;
using MiddlewareTool.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using static MiddlewareTool.Dto.ProductMgmtDto;
using System.Text;
using System.Threading.Tasks;
using static MiddlewareTool.Dto.SaleDto;

namespace MiddlewareTool.Business.Concrete
{
    public class RecordSaleBusiness : BaseBusiness, IRecordSaleBusiness
    {
        private readonly ISaleBusiness _saleBusiness;
        public RecordSaleBusiness(IUnitOfWork unitOfWork) : base(unitOfWork) { }
        public RecordSaleBusiness(IUnitOfWork unitOfWork, ISaleBusiness saleBusiness) : base(unitOfWork)
        {
            _saleBusiness = saleBusiness;
        }
        public bool Insert(RecordSaleFileDto dto)
        {
            bool result = false;
            try
            {
                var entity = Mapper.Map<RecordSaleFile>(dto);
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
        public bool Update(RecordSaleFileDto dto)
        {
            bool result = false;
            try
            {
                var entity = this.UnitOfWork.GetSingle<RecordSaleFile>(
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
                        .GetSingle<RecordSaleFile>(x => x.StoreCode.ToUpper() == storeCode.ToUpper()
                            && x.Name.ToUpper() == name.ToUpper()
                            && x.ActiveFlag == STATUS_ACTIVE);
                    if (iquery == null) { result = false; }
                }
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return result;
        }
        public Tuple<int, List<RecordSaleDto>> GetPaging(string keyWord, string dateFrom, string dateTo, int pageIndex, int pageSize, bool B2B = false, List<string> storeCodes = null, string customerType = null, List<string> paymentType = null, List<string> deliveryCode = null)
        {
            int total = 0;
            var lstDto = new List<RecordSaleDto>();
            Tuple<int, List<RecordSaleDto>> lstResult = new Tuple<int, List<RecordSaleDto>>(total, lstDto);
            try
            {
                if (storeCodes?.Count > 0)
                {
                    storeCodes = storeCodes.Where(x => !string.IsNullOrEmpty(x)).ToList();
                    storeCodes.ForEach(item =>
                    {
                        item = item.Trim();
                    });
                }
                if (paymentType?.Count > 0)
                {
                    paymentType = paymentType.Where(x => !string.IsNullOrEmpty(x)).ToList();
                    paymentType.ForEach(item =>
                    {
                        item = item.Trim();
                    });
                }
                if (deliveryCode?.Count > 0)
                {
                    deliveryCode = deliveryCode.Where(x => !string.IsNullOrEmpty(x)).ToList();
                    deliveryCode.ForEach(item =>
                    {
                        item = item.Trim();
                    });
                }
                Dictionary<string, object> m_Param = new Dictionary<string, object>()
                {
                    {"@keyWord", keyWord},
                    {"@dateFrom", dateFrom},
                    {"@dateTo", dateTo},
                    {"@pageIndex", pageIndex},
                    {"@pageSize", pageSize},
                    {"@storeCodes", storeCodes !=null && storeCodes.Count >0 ? string.Join(",", storeCodes) : null },
                    {"@customerType", customerType },
                    {"@paymentType", paymentType !=null && paymentType.Count>0 ? string.Join(",", paymentType) : null },
                    {"@deliveryCode", deliveryCode !=null && deliveryCode.Count > 0 ? string.Join(",", deliveryCode) : null},
                };
                var ds = this.UnitOfWork.ExecuteQuery(BaseBusiness.SP_RecordSale_GetPaging, m_Param);
                if (ds != null)
                {
                    total = int.Parse(ds.Tables[1].Rows[0][0].ToString());
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        var dto = new RecordSaleDto();
                        if (ds.Tables[0].Rows[i] != null && dto.ParseData(ds.Tables[0].Rows[i]))
                        {
                            lstDto.Add(dto);
                        }
                    }
                    lstResult = new Tuple<int, List<RecordSaleDto>>(total, lstDto);
                }
            }
            catch (Exception ex)
            {
                this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
                lstResult = null;
            }
            return lstResult;
        }
        public List<RecordSaleDto> Export(string keyWord, string dateFrom, string dateTo, bool B2B = false, string storeCodes = null, string customerType = null, string paymentType = null, string deliveryCode = null)
        {
            var lstDto = new List<RecordSaleDto>();
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
                var ds = this.UnitOfWork.ExecuteQuery(BaseBusiness.SP_RecordSale_Export, m_Param);
                if (ds != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        var result = new RecordSaleDto();
                        if (ds.Tables[0].Rows[i] != null && result.ParseData(ds.Tables[0].Rows[i]))
                        {
                            result.TotalAmount = Math.Round(result.TotalAmount);
                            result.PromotionAmount = Math.Round(result.PromotionAmount);
                            result.VoucherAmount = Math.Round(result.VoucherAmount);
                            result.CreditOverAmount = Math.Round(result.CreditOverAmount);
                            result.OrderTotal = Math.Round(result.OrderTotal);
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
            var lstDto = new List<RecordSaleBIDto>();
            string dateFrom = dateTime.ToString("yyyy-MM-dd 00:00:00");
            string dateTo = dateTime.ToString("yyyy-MM-dd 23:59:59");
            try
            {
                Dictionary<string, object> m_Param = new Dictionary<string, object>()
                {
                    {"@dateFrom", dateFrom},
                    {"@dateTo", dateTo}
                };
                var ds = this.UnitOfWork.ExecuteQuery(BaseBusiness.SP_RecordSale_GetTransfer, m_Param);
                if (ds != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        var result = new RecordSaleBIDto();
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
                return this.UnitOfWork.ExecuteNonQuery(BaseBusiness.SP_RecordSale_UpdateTransferred, m_Param);
            }
            catch (Exception ex)
            {
                this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
            }
            return false;
        }

        public RecordSaleBIDto GetById(Guid id)
        {
            try
            {
                Dictionary<string, object> m_Param = new Dictionary<string, object>()
                {
                    {"@id", id.ToString()},
                };
                var ds = this.UnitOfWork.ExecuteQuery(BaseBusiness.SP_RecordSale_GetDetail, m_Param);
                if (ds != null)
                {
                    var dto = new RecordSaleBIDto();
                    if (ds.Tables[0].Rows[0] != null && dto.ParseData(ds.Tables[0].Rows[0]))
                    {
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

        public ItemsDto GetPagingItems(int pageIndex, int pageSize, string headerId, string storeCode)
        {
            try
            {
                var itemsDto = new ItemsDto();
                Dictionary<string, object> m_Param = new Dictionary<string, object>()
                {
                    {"@pageIndex", pageIndex},
                    {"@pageSize", pageSize},
                    {"@headerId", headerId},
                    {"@storeCode", storeCode }
                };
                var ds = this.UnitOfWork.ExecuteQuery(BaseBusiness.SP_RecordSale_GetItemsPaging, m_Param);
                if (ds != null && ds.Tables.Count > 0)
                {
                    var lstDto = new List<ItemDto>();
                    int totalItem = int.Parse(ds.Tables[1].Rows[0][0].ToString());
                    int totalQuantitySold = int.Parse(ds.Tables[2].Rows[0][0].ToString());
                    int totalAmount = int.Parse(ds.Tables[3].Rows[0][0].ToString());
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        var dto = new ItemDto();
                        if (ds.Tables[0].Rows[i] != null && dto.ParseData(ds.Tables[0].Rows[i]))
                        {
                            lstDto.Add(dto);
                        }
                    }
                    itemsDto.Items = lstDto;
                    itemsDto.TotalQuantitySold = totalQuantitySold;
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
        public async Task<string> WriteBoxedSaleCsvAsync(Guid recordSaleId)
        {
            var recordSale = this.UnitOfWork
                        .GetSingle<RecordSale>(x => x.Id == recordSaleId);
            var headerDto = await this.UnitOfWork.GetAllNoTracking<Header>().Where(x => x.Id == recordSale.HeaderId)
                .Include(x => x.Items)
                .Include(x => x.Payments)
                .Include(x => x.Invoices)
                .Include(x => x.Deliveries)
                .FirstOrDefaultAsync();
            var customerData = await this.UnitOfWork.GetAllNoTracking<CustomerData>().Where(x => x.FoxtrotUserID == headerDto.FoxtrotUserID).FirstOrDefaultAsync();
            var itemForDeliveries = this.UnitOfWork.GetAllNoTracking<ItemForDelivery>()
                .Where(x => x.HeaderId == recordSale.HeaderId).ToList();
            var itemForRefunds = this.UnitOfWork.GetAllNoTracking<ItemForRefund>()
                .Where(x => x.HeaderId == recordSale.HeaderId).ToList();

            var saleData = "";
            #region Record H
            saleData += "H";
            saleData += "|" + headerDto.StoreCode;
            saleData += "|" + headerDto.FulfillmentDate;
            saleData += "|" + headerDto.SettlementTime;
            saleData += "|" + headerDto.OrderNumber;
            saleData += "|" + headerDto.CustomerType;
            saleData += "|" + headerDto.CustomerID;
            saleData += "|" + headerDto.FoxtrotUserID;
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
                    saleData += "|" + item.StoreCode;
                    saleData += "|" + Math.Round(item.ListPrice, 0);
                    saleData += "|" + (item.SellingPrice - Math.Round(item.SellingPrice / (1 + item.VATCode / 100) * item.QuantitySold, 0));
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
                    saleData += "A";
                    saleData += "|" + item.Sku;
                    saleData += "|" + item.QuantitySold;
                    saleData += "|" + Math.Round(item.SellingPrice, 0);
                    saleData += "|" + item.StoreCode;
                    saleData += "|" + Math.Round(item.TotalAmount, 0);
                    saleData += "|" + (item.SellingPrice - Math.Round(item.SellingPrice / (1 + (item.VATCode ?? 0.0) / 100) * item.QuantitySold, 0));
                    saleData += "|" + Math.Round(item.VATCode.Value, 0);
                    saleData += "\n";
                }
            }
            if (itemForRefunds != null && itemForRefunds.Any())
            {
                foreach (var item in itemForRefunds)
                {
                    saleData += "A";
                    saleData += "|" + item.Sku;
                    saleData += "|" + item.QuantitySold;
                    saleData += "|" + Math.Round(item.TotalAmount, 0);
                    saleData += "\n";
                }
            }
            #endregion
            #region Record B
            if (headerDto.Payments != null && headerDto.Payments.Any())
            {
                headerDto.Payments = headerDto.Payments.OrderBy(p => p.CreateDate).ToList();
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
                    if (!string.IsNullOrEmpty(payment.SubOrderID)) saleData += "|" + payment.SubOrderID;
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
                    saleData += "|" + invoice.CustomerName.Trim();
                    saleData += "|" + invoice.CompanyName.Trim();
                    saleData += "|" + invoice.Address.Trim();
                    saleData += "|" + invoice.VatCode;
                    saleData += "|" + invoice.StoreCode;
                    saleData += "|" + invoice.CQTCode;
                    saleData += "\n";
                }
            }
            #endregion
            #region Record D
            if (headerDto.Deliveries != null && headerDto.Deliveries.Any())
            {
                foreach (var delivery in headerDto.Deliveries)
                {
                    saleData += "D";
                    saleData += "|" + delivery.SubOrderNumber;
                    saleData += "|" + delivery.DeliveryCode;
                    saleData += "|" + delivery.TrackingNumber;
                    saleData += "\n";
                }
            }
            #endregion
            #region Record E
            if (customerData != null)
            {
                saleData += "E";
                saleData += "|" + customerData.Email;
                saleData += "|" + customerData.FirstName;
                saleData += "|" + customerData.LastName;
                saleData += "|" + customerData.PhoneNumber;
                saleData += "|" + customerData.Ward;
                saleData += "|" + customerData.District;
                saleData += "|" + customerData.City;
                saleData += "\n";
            }
            #endregion
            saleData += "END";
            return saleData;
        }

        public async Task<string> WriteProfitSaleCsvAsync(Guid recordSaleId)
        {
            var recordSale = this.UnitOfWork
                        .GetSingle<RecordSale>(x => x.Id == recordSaleId);
            var header = await this.UnitOfWork.GetAllNoTracking<Header>().Where(x => x.Id == recordSale.HeaderId && x.ActualOrderNumber == recordSale.ActualOrderNumber)
                .FirstOrDefaultAsync();
            var headerDto = _saleBusiness.GetTransfer(recordSale.StoreCode, recordSale.HeaderId, true);
            var totalAmount = headerDto.Items.Sum(item => item.SellingPrice * item.QuantitySold);
            var totalAmountByStore = headerDto.Items.Sum(item => item.SellingPrice * item.QuantitySold);
            var customerData = await this.UnitOfWork.GetAllNoTracking<CustomerData>().Where(x => x.FoxtrotUserID == headerDto.FoxtrotUserID).FirstOrDefaultAsync();
            var lstPaymentType = this.UnitOfWork.GetAllNoTracking<PaymentType>().Where(s => s.IsMethod == false).ToList();
            var lstDeliveries = this.UnitOfWork.GetAllNoTracking<DeliveryCode>().Where(s => s.IsMapping == true).ToList();
            var itemForDeliveries = this.UnitOfWork.GetAllNoTracking<ItemForDelivery>()
                .Where(x => x.HeaderId == recordSale.HeaderId).ToList();
            double totalAmountDelivery = 0;
            double totalAmountDeliveryByStore = 0;
            var saleData = "";
            #region Record H
            saleData += "H";
            saleData += "|" + recordSale.StoreCode;
            saleData += "|" + header.FulfillmentDate;
            saleData += "|" + header.SettlementTime;
            saleData += "|" + headerDto.OrderNumber;
            saleData += "|" + recordSale.BillNumber;
            saleData += "\n";
            #endregion
            #region Record A
            if (headerDto.Items != null && headerDto.Items.Any())
            {
                foreach (var item in headerDto.Items)
                {
                    if (item.QuantitySold <= 0) continue;
                    saleData += "A";
                    saleData += "|" + item.Sku;
                    saleData += "|" + item.QuantitySold;
                    saleData += "|" + Math.Round(item.SellingPrice, 0);
                    saleData += "|" + Math.Round(item.ListPrice, 0);
                    saleData += "|" + (item.SellingPrice * item.QuantitySold - Math.Round(item.SellingPrice / (1 + item.VATCode / 100) * item.QuantitySold, 0));
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
            #endregion
            #region Record B
            totalAmount = totalAmount + totalAmountDelivery;
            totalAmountByStore = totalAmountByStore + totalAmountDeliveryByStore;
            var ratio = totalAmount == 0 ? 0 : totalAmountByStore / totalAmount;
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
        public Tuple<string, InvoiceDto> ManualInvoice(InvoiceDto dto)
        {
            var errMsg = "";
            try
            {
                var exist = this.UnitOfWork.GetAllNoTracking<Invoice>()
                    .Any(x => x.ActiveFlag == STATUS_ACTIVE
                        && x.HeaderId == dto.HeaderId
                    );
                if (exist)
                {
                    errMsg = "Invoice is exist";
                }
                else
                {
                    dto.Id = Guid.NewGuid();
                    dto.ActiveFlag = STATUS_ACTIVE;
                    dto.CreateDate = DateTime.Now;
                    dto.UpdateDate = DateTime.Now;
                    dto.CompanyName = string.IsNullOrEmpty(dto.CompanyName) ? "" : dto.CompanyName;
                    dto.CustomerName = string.IsNullOrEmpty(dto.CustomerName) ? "" : dto.CustomerName;
                    dto.VatCode = string.IsNullOrEmpty(dto.VatCode) ? "" : dto.VatCode;
                    dto.URL = "Manual Invoice";

                    var entity = Mapper.Map<Invoice>(dto);
                    var trans = this.UnitOfWork.BeginTransaction();
                    this.UnitOfWork.Insert(entity);
                    this.UnitOfWork.Commit(trans);
                }
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return new Tuple<string, InvoiceDto>(errMsg, dto);
        }
    }
}
