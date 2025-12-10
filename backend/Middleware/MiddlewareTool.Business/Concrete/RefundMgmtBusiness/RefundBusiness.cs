using MiddlewareTool.Business.Interface;
using MiddlewareTool.Common;
using MiddlewareTool.Dto;
using MiddlewareTool.Entities;
using MiddlewareTool.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using static MiddlewareTool.Common.AppType;
using static MiddlewareTool.Common.AppValue;
using static MiddlewareTool.Dto.RefundDto;

namespace MiddlewareTool.Business.Concrete
{
    public class RefundBusiness : BaseBusiness, IRefundBusiness
    {
        private readonly ISaleOrderRefundBusiness _saleOrderRefundBusiness;
        public RefundBusiness(IUnitOfWork unitOfWork, ISaleOrderRefundBusiness saleOrderRefundBusiness) : base(unitOfWork)
        {
            _saleOrderRefundBusiness = saleOrderRefundBusiness;
        }
        public bool Import(DataTable dtHeader, DataTable dtItem, DataTable dtPayment, DataTable dtInvoice, DataTable dtPromotion)
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
                        {"@dtPromotion", dtPromotion}
                    };
                    return this.UnitOfWork.ExecuteNonQuery(BaseBusiness.SP_Refund_Import, m_Param);
                }
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return false;
        }
        public RefundHeaderDto GetTransfer(string storeCode, Guid refundHeaderId, bool isSAP = false)
        {
            var headerDto = new RefundHeaderDto();
            try
            {
                if (!string.IsNullOrEmpty(storeCode) && refundHeaderId != Guid.Empty)
                {
                    Dictionary<string, object> m_Param = new Dictionary<string, object>()
                    {
                        {"@storeCode", storeCode},
                        {"@refundHeaderId", refundHeaderId},
                        {"@isSAP", isSAP}
                    };
                    var ds = this.UnitOfWork.ExecuteQuery(BaseBusiness.SP_RefundByStore_GetTransfer, m_Param);
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
                        if (string.IsNullOrEmpty(headerDto.ReceiptNumber))
                        {
                            return null;
                        }
                        //Promotion
                        var lstPromotions = new List<RefundPromotionDto>();
                        for (int i = 0; i < ds.Tables[4].Rows.Count; i++)
                        {
                            var promotionDto = new RefundPromotionDto();
                            if (ds.Tables[4].Rows[i] != null && promotionDto.ParseData(ds.Tables[4].Rows[i]))
                            {
                                lstPromotions.Add(promotionDto);
                            }
                        }
                        //Items
                        headerDto.RefundItems = new List<RefundItemDto>();
                        for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                        {
                            var itemDto = new RefundItemDto();
                            if (ds.Tables[1].Rows[i] != null && itemDto.ParseData(ds.Tables[1].Rows[i]))
                            {
                                var promotionAdd = lstPromotions?.Where(x => x.ItemId == itemDto.Id);
                                if (promotionAdd != null && promotionAdd.Count() > 0)
                                {
                                    itemDto.Promotions = new List<RefundPromotionDto>();
                                    itemDto.Promotions.AddRange(promotionAdd);
                                }
                                headerDto.RefundItems.Add(itemDto);
                            }
                        }
                        //Payments
                        headerDto.RefundPayments = new List<RefundPaymentDto>();
                        for (int i = 0; i < ds.Tables[2].Rows.Count; i++)
                        {
                            var paymentDto = new RefundPaymentDto();
                            if (ds.Tables[2].Rows[i] != null && paymentDto.ParseData(ds.Tables[2].Rows[i]))
                            {
                                headerDto.RefundPayments.Add(paymentDto);
                            }
                        }
                        //Invoices
                        headerDto.RefundInvoices = new List<RefundInvoiceDto>();
                        for (int i = 0; i < ds.Tables[3].Rows.Count; i++)
                        {
                            var invoiceDto = new RefundInvoiceDto();
                            if (ds.Tables[3].Rows[i] != null && invoiceDto.ParseData(ds.Tables[3].Rows[i]))
                            {
                                headerDto.RefundInvoices.Add(invoiceDto);
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
        public List<RefundByStoreDto> GetStores()
        {
            var lstResult = new List<RefundByStoreDto>();
            try
            {
                var ds = this.UnitOfWork.ExecuteQuery(BaseBusiness.SP_Refund_GetStores, null);
                if (ds != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        var dr = ds.Tables[0].Rows[i];
                        if (dr != null)
                        {
                            var itemDto = new RefundByStoreDto();
                            itemDto.StoreCode = dr[dr.Table.Columns[0]].ToString();
                            itemDto.RefundHeaderId = Guid.Parse(dr[dr.Table.Columns[1]].ToString());
                            itemDto.OrderNumber = dr[dr.Table.Columns[2]].ToString();
                            itemDto.RefundDate = dr[dr.Table.Columns[3]].ToString();
                            lstResult.Add(itemDto);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
            }
            return lstResult;
        }
        public List<RefundByStoreDto> GetSAPStores()
        {
            var lstResult = new List<RefundByStoreDto>();
            try
            {
                var ds = this.UnitOfWork.ExecuteQuery(BaseBusiness.SP_SAP_Refund_GetStores, null);
                if (ds != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        var dr = ds.Tables[0].Rows[i];
                        if (dr != null)
                        {
                            var itemDto = new RefundByStoreDto();
                            itemDto.StoreCode = dr[dr.Table.Columns[0]].ToString();
                            itemDto.RefundHeaderId = Guid.Parse(dr[dr.Table.Columns[1]].ToString());
                            itemDto.OrderNumber = dr[dr.Table.Columns[2]].ToString();
                            itemDto.RefundDate = dr[dr.Table.Columns[3]].ToString();
                            lstResult.Add(itemDto);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
            }
            return lstResult;
        }
        public List<RefundByStoreDto> GetS4Stores()
        {
            var lstResult = new List<RefundByStoreDto>();
            try
            {
                var ds = this.UnitOfWork.ExecuteQuery(BaseBusiness.SP_S4_Refund_GetStores, null);
                if (ds != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        var dr = ds.Tables[0].Rows[i];
                        if (dr != null)
                        {
                            var itemDto = new RefundByStoreDto();
                            itemDto.StoreCode = dr[dr.Table.Columns[0]].ToString();
                            itemDto.RefundHeaderId = Guid.Parse(dr[dr.Table.Columns[1]].ToString());
                            itemDto.OrderNumber = dr[dr.Table.Columns[2]].ToString();
                            itemDto.RefundDate = dr[dr.Table.Columns[3]].ToString();
                            lstResult.Add(itemDto);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
            }
            return lstResult;
        }
        public bool UpdateTransferred(string storeCode, Guid refundHeaderId)
        {
            try
            {
                var sql = BaseBusiness.SP_Refund_UpdateTransferred;
                var parameters = new Dictionary<string, object>()
                {
                    {"@storeCode", storeCode},
                    {"@refundHeaderId", refundHeaderId}
                };
                return this.UnitOfWork.ExecuteNonQuery(sql, parameters);
            }
            catch (Exception ex)
            {
                LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
            }
            return false;
        }
        public bool UpdateSAPTransferred(string storeCode, Guid refundHeaderId)
        {
            try
            {
                var sql = BaseBusiness.SP_SAP_Refund_UpdateTransferred;
                var parameters = new Dictionary<string, object>()
                {
                    {"@storeCode", storeCode},
                    {"@headerId", refundHeaderId}
                };
                return this.UnitOfWork.ExecuteNonQuery(sql, parameters);
            }
            catch (Exception ex)
            {
                LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
            }
            return false;
        }
        public bool UpdateS4Transferred(string storeCode, Guid refundHeaderId, bool success = true)
        {
            try
            {
                var sql = BaseBusiness.SP_S4_Refund_UpdateTransferred;
                var parameters = new Dictionary<string, object>()
                {
                    {"@storeCode", storeCode},
                    {"@headerId", refundHeaderId},
                    {"@success", success}
                };
                return this.UnitOfWork.ExecuteNonQuery(sql, parameters);
            }
            catch (Exception ex)
            {
                LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
            }
            return false;
        }
        public List<RefundByStoreDto> GetHotFixPaymentByStore()
        {
            var lstResult = new List<RefundByStoreDto>();
            try
            {
                var sql = @"select distinct h.Id as RefundHeaderId, h.OrderNumber, i.StoreCode, h.RefundDate from re.RefundHeaders h
inner join re.RefundItems i on i.HeaderId = h.Id
where h.ActiveFlag = 0
and not exists (select 1 from re.PaymentByStore pbs where pbs.HeaderId = h.Id and pbs.StoreCode = i.StoreCode and pbs.ActiveFlag = 0)
order by h.RefundDate desc, i.StoreCode, h.OrderNumber;";
                var ds = this.UnitOfWork.SqlQuery<RefundByStoreDto>(sql, 10 * 60).ToList();
                return ds;
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return lstResult;
        }

        public async Task<bool> CheckRefundAsync(Guid saleId, string storeCode)
        {
            try
            {
                var rfs = await _saleOrderRefundBusiness.GetAllBySaleOrderIdCODAsync(saleId);
                if (rfs.Count == 0)
                    return true;
                var order = this.UnitOfWork.GetAllNoTracking<Header>()
                    .Include(x => x.Items)
                    .FirstOrDefault(x => x.Id == saleId
                    && x.ActiveFlag == STATUS_ACTIVE);

                #region Lấy thêm SKU delivery
                var iqueryItemForDelivery = this.UnitOfWork.GetAllNoTracking<ItemForDelivery>()
                    .Where(x => x.HeaderId == saleId && x.StoreCode == storeCode)
                    .ToList();
                foreach (var item in iqueryItemForDelivery)
                {
                    order.Items.Add(new Item
                    {
                        Id = item.Id,
                        HeaderId = item.HeaderId,
                        Sku = item.Sku,
                        StoreCode = storeCode,
                        QuantitySold = item.QuantitySold,
                        SellingPrice = item.SellingPrice,
                        ListPrice = item.ListPrice.GetValueOrDefault(),
                        VATAmount = item.VATAmount.GetValueOrDefault(),
                        VATCode = item.VATCode.GetValueOrDefault()
                    });
                }
                #endregion
                var itemList = order.Items.Where(x => x.StoreCode == storeCode).OrderBy(x => x.Sku).ToList();
                var refundList = rfs.Where(x => x.StatusID == SaleOrderStatuses.Invoiced).ToList();

                foreach (var item in itemList)
                {
                    if (item.QuantitySold > refundList.Where(r => r.StatusID == SaleOrderStatuses.Invoiced && r.Id != item.HeaderId).SelectMany(rf => rf.Items).Where(z => z.Sku == item.Sku).Sum(y => y.Quantity))
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

        public async Task<List<SaleOrderCompactDto>> GetSaleOrderNumbersAsync(SaleOrderFilterDto filter, bool isAdmin, string userName)
        {
            List<SaleOrderCompactDto> dto = new List<SaleOrderCompactDto>();
            try
            {
                var iquery = this.UnitOfWork.GetAllNoTracking<Header>()
                    .Include(x => x.Items)
                    .Include(x => x.Payments)
                    .Include(x => x.Deliveries)
                    .Include(x => x.Invoices)
                    .Where(x => x.ActiveFlag != STATUS_DELETE
                    && x.IsTransfer == true
                    && x.Invoices.Any(i => i.ActiveFlag == STATUS_ACTIVE)
                    );

                // Anh Hiển: em support bổ sung 1 cái cờ ở DB hay webconfig/ Stored procedure gì đó để anh có thể cho/ không cho refund đơn hàng của B2B có sử dụng Voucher
                var flag_RefundCOD_AllowB2B = this.UnitOfWork.GetAllNoTracking<SystemSetting>()
                    .Where(x => x.Code == AppValue.Key_Setting_RefundCOD_AllowB2B && x.Value == "1").Any();
                if (!flag_RefundCOD_AllowB2B)
                {
                    iquery = iquery.Where(x => x.CustomerType == PaymentTypeCustomerTypes.EC);
                }

                if (filter.HeaderIds?.Count > 0)
                {
                    iquery = iquery.Where(x => filter.HeaderIds.Contains(x.Id));
                }
                if (!string.IsNullOrEmpty(filter.StoreCode))
                {
                    iquery = iquery.Where(x => x.Items.Any(i => i.StoreCode == filter.StoreCode));
                }
                if (!filter.HasAllPermission)
                    iquery = iquery.Where(x => x.CreateBy == filter.CreatedBy);
                if (!isAdmin)
                {
                    var userStores = this.UnitOfWork.GetAllNoTracking<UserStore>().Where(x => x.ActiveFlag == STATUS_ACTIVE && x.UserName == userName).Select(x => x.StoreCode).ToList();
                    if (!(userStores?.Count > 0))
                        userStores.Add("NotAdmin");
                    iquery = iquery.Where(x => x.Items.Any(item => userStores.Contains(item.StoreCode)));
                }

                if (!string.IsNullOrEmpty(filter.Keyword))
                {
                    var keyword = filter.Keyword.Trim();
                    iquery = iquery.Where(x => x.OrderNumber.Contains(keyword));
                }
                if (!string.IsNullOrEmpty(filter.OrderNumber))
                {
                    iquery = iquery.Where(x => x.ActualOrderNumber == filter.OrderNumber);
                }
                if (filter.FromDate.HasValue && filter.ToDate.HasValue)
                {
                    var fromDate = filter.FromDate.Value.Date.ToString("yyyyMMdd");
                    var toDate = filter.ToDate.Value.Date.AddDays(1).AddMilliseconds(-1);
                    iquery = iquery.Where(x => x.FulfillmentDate == fromDate);
                }

                #region Chỉ lấy các đơn có PaymentMethod "Original" và cho phép Refund
                var paymentTypeMappings = this.UnitOfWork.GetAllNoTracking<PaymentTypeMapping>().Where(x => x.ActiveFlag == STATUS_ACTIVE
                //&& x.Method == (byte)PaymentMethods.Original
                && (x.Scope == (byte)PaymentTypeScopes.RecordSale || !x.Scope.HasValue)
                && x.AllowRefund == true)
                    .OrderByDescending(x => x.IsMapping)
                    .ToList();
                var paymentCodes = new List<string>();
                if (paymentTypeMappings?.Count > 0)
                {
                    foreach (var item in paymentTypeMappings)
                    {
                        if (!string.IsNullOrEmpty(item.PaymentCodeOutput))
                            paymentCodes.Add(item.PaymentCodeOutput);
                        if (!string.IsNullOrEmpty(item.Type))
                            paymentCodes.Add(item.Type);
                    }

                    paymentCodes.Distinct().ToList();
                    iquery = iquery.Where(x => x.Payments.Any(p => paymentCodes.Contains(p.PaymentType))
                    );
                }
                else
                {
                    iquery = iquery.Where(x => false);
                }


                #endregion

                if (filter.Refunded.HasValue)
                {
                    var headers = iquery.ToList();
                    var filteredHeaders = new List<Header>();
                    var headerIds = headers.Select(x => x.Id).ToList();
                    var iquerySalePayment = this.UnitOfWork.GetAllNoTracking<PaymentByStore1>()
                        .Where(x => x.ActiveFlag == STATUS_ACTIVE
                            && headerIds.Contains(x.HeaderId)
                            && x.StoreCode == filter.StoreCode
                        )
                        .ToList();

                    foreach (var item in headers)
                    {
                        var added = true;

                        var canRefund = await CheckRefundAsync(item.Id, filter.StoreCode);
                        if (canRefund == filter.Refunded)
                        {
                            added = false;
                        }

                        #region Kiểm tra PaymentTypeMapping
                        if (added)
                        {
                            var salePayments = iquerySalePayment.Where(x => x.HeaderId == item.Id && x.StoreCode == filter.StoreCode).ToList();
                            foreach (var paymentType in paymentTypeMappings)
                            {
                                if (paymentType.IsMapping)
                                {
                                    List<string> paymentDeliveries = new List<string>();
                                    if (!string.IsNullOrEmpty(paymentType.DeliveryCode))
                                        paymentDeliveries = paymentType.DeliveryCode.Replace(" ", "").Split(';').ToList();
                                    if (item.Deliveries.Any(x => paymentDeliveries.Contains(x.DeliveryCode)))
                                    {
                                        added = salePayments.Any(p => p.PaymentType.Equals(paymentType.PaymentCodeOutput));
                                    }
                                    else
                                    {
                                        added = salePayments.Any(p => p.PaymentType.Equals(paymentType.Type));
                                    }
                                }
                                else
                                {
                                    if (string.IsNullOrEmpty(paymentType.PaymentCodeOutput))
                                        added = salePayments.Any(p => p.PaymentType.Equals(paymentType.Type));
                                    else
                                        added = salePayments.Any(p => p.PaymentType.Equals(paymentType.PaymentCodeOutput));
                                }

                                if (added)
                                    break;
                            }
                        }
                        #endregion
                        if (added)
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
                        OrderNumber = x.ActualOrderNumber,
                        StoreCode = x.StoreCode
                    }).ToList();
            }
            catch (Exception ex)
            {
                LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
            }
            return dto;
        }

        public List<SaleOrderCompactDto> GetSaleOrderNumbers(SaleOrderFilterDto filter, bool isAdmin, string userName)
        {
            return GetSaleOrderNumbersAsync(filter, isAdmin, userName).GetAwaiter().GetResult();
        }
    }
}
