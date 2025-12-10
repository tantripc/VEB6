using AutoMapper;
using Ionic.Zlib;
using MiddlewareTool.Business.Interface;
using MiddlewareTool.Common;
using MiddlewareTool.Dto;
using MiddlewareTool.Dto.S4;
using MiddlewareTool.Entities;
using MiddlewareTool.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using static MiddlewareTool.Common.AppType;
using static MiddlewareTool.Dto.SaleDto;

namespace MiddlewareTool.Business.Concrete
{
    public class SaleBusiness : BaseBusiness, ISaleBusiness
    {

        public SaleBusiness(IUnitOfWork unitOfWork) : base(unitOfWork) { }
        public bool Import(DataTable dtHeader, DataTable dtItem, DataTable dtPayment, DataTable dtInvoice, DataTable dtDelivery, DataTable dtPromotion, DataTable dtItemForDelivery, DataTable dtItemForRefund, DataTable dtCustomerData)
        {
            try
            {
                if (dtHeader != null && dtItem != null && dtPayment != null && dtInvoice != null)
                {
                    Dictionary<string, object> m_Param = new Dictionary<string, object>()
                    {
                        {"@dtHeader", dtHeader},
                        {"@dtItem", dtItem},
                        {"@dtPayment", dtPayment},
                        {"@dtInvoice", dtInvoice},
                        {"@dtDelivery", dtDelivery},
                        {"@dtPromotion", dtPromotion},
                        {"@dtItemForDelivery", dtItemForDelivery},
                        {"@dtItemForRefund", dtItemForRefund},
                        {"@dtCustomerData", dtCustomerData}
                    };
                    return this.UnitOfWork.ExecuteNonQuery(BaseBusiness.SP_Sale_Import, m_Param, 120 * 10);
                }
            }
            catch (Exception ex)
            {
                this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
            }
            return false;
        }
        public HeaderByStoreDto GetTransfer(string storeCode, Guid headerId, bool isSAP = false)
        {
            var headerDto = new HeaderByStoreDto();
            try
            {
                if (!string.IsNullOrEmpty(storeCode) && headerId != Guid.Empty)
                {
                    Dictionary<string, object> m_Param = new Dictionary<string, object>()
                    {
                        {"@storeCode", storeCode},
                        {"@headerId", headerId},
                        {"@isSAP", isSAP},
                    };

                    var ds = this.UnitOfWork.ExecuteQuery(BaseBusiness.SP_SaleByStore_GetTransfer, m_Param);
                    if (ds != null)
                    {
                        //Headers
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            if (ds.Tables[0].Rows[0] != null)
                            {
                                headerDto.ParseData(ds.Tables[0].Rows[0]);
                            }
                        }
                        //Promotion
                        var lstPromotions = new List<PromotionByStoreDto>();
                        for (int i = 0; i < ds.Tables[4].Rows.Count; i++)
                        {
                            var promotionDto = new PromotionByStoreDto();
                            if (ds.Tables[4].Rows[i] != null && promotionDto.ParseData(ds.Tables[4].Rows[i]))
                            {
                                lstPromotions.Add(promotionDto);
                            }
                        }
                        //Items
                        headerDto.Items = new List<ItemByStoreDto>();
                        for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                        {
                            var itemDto = new ItemByStoreDto();
                            if (ds.Tables[1].Rows[i] != null && itemDto.ParseData(ds.Tables[1].Rows[i]))
                            {
                                var promotionAdd = lstPromotions?.Where(x => x.ItemId == itemDto.Id);
                                if (promotionAdd != null && promotionAdd.Count() > 0)
                                {
                                    itemDto.Promotions = new List<PromotionByStoreDto>();
                                    itemDto.Promotions.AddRange(promotionAdd);
                                }
                                headerDto.Items.Add(itemDto);
                            }
                        }
                        //Payments
                        headerDto.Payments = new List<PaymentByStoreDto>();
                        for (int i = 0; i < ds.Tables[2].Rows.Count; i++)
                        {
                            var paymentDto = new PaymentByStoreDto();
                            if (ds.Tables[2].Rows[i] != null && paymentDto.ParseData(ds.Tables[2].Rows[i]))
                            {
                                headerDto.Payments.Add(paymentDto);
                            }
                        }
                        //Invoices
                        headerDto.Invoices = new List<InvoiceByStoreDto>();
                        for (int i = 0; i < ds.Tables[3].Rows.Count; i++)
                        {
                            var invoiceDto = new InvoiceByStoreDto();
                            if (ds.Tables[3].Rows[i] != null && invoiceDto.ParseData(ds.Tables[3].Rows[i]))
                            {
                                headerDto.Invoices.Add(invoiceDto);
                            }
                        }
                        return headerDto;
                    }
                }
            }
            catch (Exception ex)
            {
                LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
            }
            return null;
        }
        public List<SaleByStoreDto> GetStores()
        {
            var lstResult = new List<SaleByStoreDto>();
            try
            {
                var ds = this.UnitOfWork.ExecuteQuery(BaseBusiness.SP_Sale_GetStores, null);
                if (ds != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        var dr = ds.Tables[0].Rows[i];
                        if (dr != null)
                        {
                            var itemDto = new SaleByStoreDto();
                            itemDto.StoreCode = dr[dr.Table.Columns[0]].ToString();
                            itemDto.HeaderId = Guid.Parse(dr[dr.Table.Columns[1]].ToString());
                            itemDto.OrderNumber = dr[dr.Table.Columns[2]].ToString();
                            itemDto.FulfillmentDate = dr[dr.Table.Columns[3]].ToString();
                            itemDto.QuantitySold = int.Parse(dr[dr.Table.Columns[5]].ToString());
                            lstResult.Add(itemDto);
                        }
                    }
                }
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return lstResult;
        }
        public List<SaleByStoreDto> GetSAPStores()
        {
            var lstResult = new List<SaleByStoreDto>();
            try
            {
                var ds = this.UnitOfWork.ExecuteQuery(SP_SAP_Sale_GetStores, null);
                if (ds != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        var dr = ds.Tables[0].Rows[i];
                        if (dr != null)
                        {
                            var itemDto = new SaleByStoreDto();
                            itemDto.StoreCode = dr[dr.Table.Columns[0]].ToString();
                            itemDto.HeaderId = Guid.Parse(dr[dr.Table.Columns[1]].ToString());
                            itemDto.OrderNumber = dr[dr.Table.Columns[2]].ToString();
                            itemDto.FulfillmentDate = dr[dr.Table.Columns[3]].ToString();
                            lstResult.Add(itemDto);
                        }
                    }
                }
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return lstResult;
        }
        public List<SaleByStoreDto> GetS4Stores()
        {
            var lstResult = new List<SaleByStoreDto>();
            try
            {
                var ds = this.UnitOfWork.ExecuteQuery(SP_S4_Sale_GetStores, null);
                if (ds != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        var dr = ds.Tables[0].Rows[i];
                        if (dr != null)
                        {
                            var itemDto = new SaleByStoreDto();
                            itemDto.StoreCode = dr[dr.Table.Columns[0]].ToString();
                            itemDto.HeaderId = Guid.Parse(dr[dr.Table.Columns[1]].ToString());
                            itemDto.OrderNumber = dr[dr.Table.Columns[2]].ToString();
                            itemDto.FulfillmentDate = dr[dr.Table.Columns[3]].ToString();
                            lstResult.Add(itemDto);
                        }
                    }
                }
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return lstResult;
        }
        public bool UpdateTransferred(string storeCode, Guid headerId, bool success = true)
        {
            try
            {
                var sql = BaseBusiness.SP_Sale_UpdateTransferred;
                var parameters = new Dictionary<string, object>()
                {
                    {"@storeCode", storeCode},
                    {"@headerId", headerId},
                    {"@success", success},

                };
                return this.UnitOfWork.ExecuteNonQuery(sql, parameters);
            }
            catch (Exception ex)
            {
                LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
            }
            return false;
        }
        public bool UpdateSAPTransferred(string storeCode, Guid headerId)
        {
            try
            {
                var sql = BaseBusiness.SP_SAP_Sale_UpdateTransferred;
                var parameters = new Dictionary<string, object>()
                {
                    {"@storeCode", storeCode},
                    {"@headerId", headerId}
                };
                return this.UnitOfWork.ExecuteNonQuery(sql, parameters);
            }
            catch (Exception ex)
            {
                LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
            }
            return false;
        }
        public bool UpdateS4Transferred(string storeCode, Guid headerId, bool success = true)
        {
            try
            {
                var sql = BaseBusiness.SP_S4_Sale_UpdateTransferred;
                var parameters = new Dictionary<string, object>()
                {
                    {"@storeCode", storeCode},
                    {"@headerId", headerId},
                    {"@success", success}
                };
                return this.UnitOfWork.ExecuteNonQuery(sql, parameters);
            }
            catch (Exception ex)
            {
                LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex, true);
                throw (ex);
            }
        }
        public List<SaleByStoreDto> GetHotFixPaymentByStore()
        {
            var lstResult = new List<SaleByStoreDto>();
            try
            {
                var sql = @"select distinct h.Id as HeaderId, h.OrderNumber, i.StoreCode, h.FulfillmentDate from se.headers h
	inner join se.Items i on i.HeaderId = h.Id
	where h.ActiveFlag = 0
	and not exists (select 1 from se.PaymentByStore pbs where pbs.HeaderId = h.Id and pbs.StoreCode = i.StoreCode and pbs.ActiveFlag = 0)
	order by h.FulfillmentDate desc, i.StoreCode, h.OrderNumber;";
                var ds = this.UnitOfWork.SqlQuery<SaleByStoreDto>(sql, 10 * 60).ToList();
                return ds;
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return lstResult;
        }
        public async Task<SaleCODDto> GetAsync(Guid id, string storeCode)
        {
            try
            {
                var entity = await this.UnitOfWork.GetAllNoTracking<Header>()
                    .Include(x => x.Items)
                    .Include(x => x.Payments)
                    .Include(x => x.Invoices)
                    .Include(x => x.Deliveries)
                    .FirstOrDefaultAsync(x => x.Id == id
                    && x.ActiveFlag == STATUS_ACTIVE
                    && x.Items.Any(i => i.StoreCode == storeCode)
                    && x.Invoices.Any(i => i.ActiveFlag == STATUS_ACTIVE)
                    );
                if (entity == null)
                    return null;
                entity.Items = entity.Items.Where(i => i.StoreCode == storeCode).OrderBy(x => x.Sku).ToList();

                #region Lấy thêm SKU delivery
                var iqueryItemForDelivery = this.UnitOfWork.GetAllNoTracking<ItemForDelivery>()
                    .Where(x => x.HeaderId == id && x.StoreCode == storeCode)
                    .ToList();
                foreach (var item in iqueryItemForDelivery)
                {
                    entity.Items.Add(new Item
                    {
                        Id = item.Id,
                        HeaderId = item.HeaderId,
                        Sku = item.Sku,
                        QuantitySold = item.QuantitySold,
                        SellingPrice = item.SellingPrice,
                        ListPrice = item.ListPrice.GetValueOrDefault(),
                        VATAmount = item.VATAmount.GetValueOrDefault(),
                        VATCode = item.VATCode.GetValueOrDefault()
                    });
                }
                #endregion

                #region Lấy PaymentByStore
                var salePayments = this.UnitOfWork.GetAllNoTracking<PaymentByStore1>().Where(x => x.HeaderId == id && x.StoreCode == storeCode).ToList();
                #endregion

                #region Map Dto
                var skus = entity.Items.Select(x => x.Sku).ToList();
                var products = this.UnitOfWork.GetAllNoTracking<Product>().Where(x => skus.Contains(x.Sku) && x.ActiveFlag == STATUS_ACTIVE).ToList();
                //var masters = this.UnitOfWork.GetAllNoTracking<MasterItem>().Where(x => skus.Contains(x.ITEM_NO) && x.ActiveFlag == STATUS_ACTIVE).ToList();

                var fulfillmentDate = DateTime.ParseExact(entity.FulfillmentDate, "yyyyMMdd", CultureInfo.InvariantCulture);

                var paymentTypeMappings = this.UnitOfWork.GetAllNoTracking<PaymentTypeMapping>().Where(x => x.ActiveFlag == STATUS_ACTIVE
                //&& x.Method == (byte)PaymentMethods.Original
                && (x.Scope == (byte)PaymentTypeScopes.RecordSale || !x.Scope.HasValue)
                && x.AllowRefund == true)
                    .ToList();
                if (paymentTypeMappings?.Count > 0)
                {
                    foreach (var paymentItem in salePayments)
                    {
                        if (paymentTypeMappings.Any(x => x.Type == paymentItem.PaymentType))
                        {
                            paymentItem.URL = ((byte)paymentTypeMappings.FirstOrDefault(x => x.Type == paymentItem.PaymentType).Method.GetValueOrDefault()).ToString();
                        }
                    }
                }
                var paymentType = salePayments.OrderBy(x => x.URL).FirstOrDefault().PaymentType;
                var recordSale = this.UnitOfWork.GetAllNoTracking<RecordSale>().Where(x => x.HeaderId == entity.Id
                && x.ActiveFlag == STATUS_ACTIVE
                && x.StoreCode == storeCode
                ).FirstOrDefault();

                var dto = new SaleCODDto()
                {
                    Id = entity.Id,
                    StoreCode = storeCode,
                    BusinessId = Guid.Empty,
                    OrderNumber = entity.ActualOrderNumber,
                    BillNumber = recordSale?.BillNumber,
                    RecordSaleId = recordSale?.Id ?? Guid.Empty,
                    ReceiptDate = fulfillmentDate,
                    CustomerID = entity.CustomerID,
                    CustomerType = entity.CustomerType,
                    PaymentType = paymentType,
                    DeliveryCode = entity.Deliveries.FirstOrDefault()?.DeliveryCode,
                    FulfillmentNumber = entity.Deliveries.FirstOrDefault()?.SubOrderNumber,
                    TrackingNumber = entity.Deliveries.FirstOrDefault()?.TrackingNumber,

                    CreateBy = entity.CreateBy,
                    CreateDate = entity.CreateDate,
                    UpdateBy = entity.UpdateBy,
                    UpdateDate = entity.UpdateDate,
                    ActiveFlag = (AppValue.ActiveFlag)entity.ActiveFlag,

                    Items = new List<SaleOrderItemDto>(),
                    Invoices = entity.Invoices.Select(x => new SaleOrderInvoiceDto
                    {
                        InvoiceID = 0,
                        HeaderId = x.HeaderId,
                        IntegrateKey = "",
                        InvoiceTemplateCode = x.Code,
                        InvoiceSeries = x.SerialNo,
                        InvoiceNumber = x.Number,
                        CustomerName = x.CustomerName,
                        CompanyName = x.CompanyName,
                        Address = x.Address
                    }).ToList()
                };
                foreach (var item in entity.Items)
                {
                    var product = products.FirstOrDefault(x => x.Sku == item.Sku);
                    //var master = masters.FirstOrDefault(x => x.ITEM_NO == item.Sku);
                    var itemDto = new SaleOrderItemDto
                    {
                        Id = item.Id,
                        HeaderId = item.HeaderId,
                        Sku = item.Sku,
                        Name = product?.Name, //?? master?.ITEM_LONG_NAME_CHINESE,
                        Quantity = item.QuantitySold,
                        Price = item.SellingPrice,
                        ListPrice = item.ListPrice,
                        POPrice = item.SellingPrice,
                        VATAmount = item.VATAmount,
                        VATCode = item.VATCode,
                        IsTaxB2B = false,
                        UnitType = product?.UnitType, //?? master?.ITEM_UOM2,

                        CreateBy = item.CreateBy,
                        CreateDate = item.CreateDate,
                        UpdateBy = item.UpdateBy,
                        UpdateDate = item.UpdateDate,
                        ActiveFlag = (AppValue.ActiveFlag)item.ActiveFlag
                    };
                    if (string.IsNullOrEmpty(itemDto.Name) || string.IsNullOrEmpty(itemDto.UnitType))
                    {
                        itemDto.ErrorMess = "SKU Name and UnitType is required;";
                    }
                    dto.Items.Add(itemDto);
                }

                #endregion

                return dto;
            }
            catch (Exception ex)
            {
                LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
            }
            return null;
        }
    }
}
