using Microsoft.EntityFrameworkCore;
using MW.Data;
using MW.DTO;
using MW.Entities;

namespace MW.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _db;

        public ProductRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<List<Product>> GetAll()
            => await _db.Products.AsNoTracking().ToListAsync();
        public async Task<List<Product>> GetPaging()
        {
            var iquery = await _db.Products.AsNoTracking().Skip(0).Take(10).ToListAsync();

            return iquery;
        }

        public async Task<Product?> GetById(Guid id)
            => await _db.Products.FirstOrDefaultAsync(x => x.Id == id);

        public async Task<Product> Add(Product product)
        {
            _db.Products.Add(product);
            await _db.SaveChangesAsync();
            return product;
        }

        public async Task Update(Product product)
        {
            _db.Products.Update(product);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var entity = await _db.Products.FindAsync(id);
            if (entity != null)
            {
                _db.Products.Remove(entity);
                await _db.SaveChangesAsync();
            }
        }

        public async Task<List<SaleOrderCompactDto>> GetSaleOrderNumbersAsync(SaleOrderFilterDto filter, bool isAdmin, string userName)
        {
            List<SaleOrderCompactDto> dto = new List<SaleOrderCompactDto>();
            try
            {
                var iquery = _db.Headers.AsNoTracking()
                    .Include(x => x.Items)
                    .Include(x => x.Payments)
                    .Include(x => x.Deliveries)
                    .Include(x => x.Invoices)
                    .Where(x => x.ActiveFlag != 2
                    && x.IsTransfer == true
                    && x.Invoices.Any(i => i.ActiveFlag == 0)
                    );

                // Anh Hiển: em support bổ sung 1 cái cờ ở DB hay webconfig/ Stored procedure gì đó để anh có thể cho/ không cho refund đơn hàng của B2B có sử dụng Voucher
                var flag_RefundCOD_AllowB2B = _db.SystemSettings.AsNoTracking()
                    .Where(x => x.Code == "Setting_RefundCOD_AllowB2B" && x.Value == "1").Any();
                if (!flag_RefundCOD_AllowB2B)
                {
                    iquery = iquery.Where(x => x.CustomerType == "C");
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
                    var userStores = _db.UserStores.AsNoTracking().Where(x => x.ActiveFlag == 0 && x.UserName == userName).Select(x => x.StoreCode).ToList();
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
                //var paymentTypeMappings = this.UnitOfWork.GetAllNoTracking<PaymentTypeMapping>().Where(x => x.ActiveFlag == STATUS_ACTIVE
                ////&& x.Method == (byte)PaymentMethods.Original
                //&& (x.Scope == (byte)PaymentTypeScopes.RecordSale || !x.Scope.HasValue)
                //&& x.AllowRefund == true)
                //    .OrderByDescending(x => x.IsMapping)
                //    .ToList();
                //var paymentCodes = new List<string>();
                //if (paymentTypeMappings?.Count > 0)
                //{
                //    foreach (var item in paymentTypeMappings)
                //    {
                //        if (!string.IsNullOrEmpty(item.PaymentCodeOutput))
                //            paymentCodes.Add(item.PaymentCodeOutput);
                //        if (!string.IsNullOrEmpty(item.Type))
                //            paymentCodes.Add(item.Type);
                //    }

                //    paymentCodes.Distinct().ToList();
                //    iquery = iquery.Where(x => x.Payments.Any(p => paymentCodes.Contains(p.PaymentType))
                //    );
                //}
                //else
                //{
                //    iquery = iquery.Where(x => false);
                //}


                #endregion

                if (filter.Refunded.HasValue)
                {
                    var headers = iquery.ToList();
                    var filteredHeaders = new List<Header>();
                    var headerIds = headers.Select(x => x.Id).ToList();
                    var iquerySalePayment = _db.PaymentByStores1.AsNoTracking()
                        .Where(x => x.ActiveFlag == 0
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
                            //foreach (var paymentType in paymentTypeMappings)
                            //{
                            //    if (paymentType.IsMapping)
                            //    {
                            //        List<string> paymentDeliveries = new List<string>();
                            //        if (!string.IsNullOrEmpty(paymentType.DeliveryCode))
                            //            paymentDeliveries = paymentType.DeliveryCode.Replace(" ", "").Split(';').ToList();
                            //        if (item.Deliveries.Any(x => paymentDeliveries.Contains(x.DeliveryCode)))
                            //        {
                            //            added = salePayments.Any(p => p.PaymentType.Equals(paymentType.PaymentCodeOutput));
                            //        }
                            //        else
                            //        {
                            //            added = salePayments.Any(p => p.PaymentType.Equals(paymentType.Type));
                            //        }
                            //    }
                            //    else
                            //    {
                            //        if (string.IsNullOrEmpty(paymentType.PaymentCodeOutput))
                            //            added = salePayments.Any(p => p.PaymentType.Equals(paymentType.Type));
                            //        else
                            //            added = salePayments.Any(p => p.PaymentType.Equals(paymentType.PaymentCodeOutput));
                            //    }

                            //    if (added)
                            //        break;
                            //}
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
                //LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
            }
            return dto;
        }

        public async Task<bool> CheckRefundAsync(Guid saleId, string storeCode)
        {
            try
            {
                var rfs = await GetAllBySaleOrderIdCODAsync(saleId);
                if (rfs.Count == 0)
                    return true;
                var order = _db.Headers.AsNoTracking()
                    .Include(x => x.Items)
                    .FirstOrDefault(x => x.Id == saleId
                    && x.ActiveFlag == 0);

                #region Lấy thêm SKU delivery
                var iqueryItemForDelivery = _db.ItemForDeliveries.AsNoTracking()
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
                        Vatamount = item.Vatamount.GetValueOrDefault(),
                        Vatcode = item.Vatcode.GetValueOrDefault()
                    });
                }
                #endregion
                var itemList = order.Items.Where(x => x.StoreCode == storeCode).OrderBy(x => x.Sku).ToList();
                var refundList = rfs.ToList();

                foreach (var item in itemList)
                {
                    if (item.QuantitySold > refundList.Where(r => r.Id != item.HeaderId).SelectMany(rf => rf.RefundItems).Where(z => z.Sku == item.Sku).Sum(y => y.QuantityRefunded))
                    {
                        return true;
                    }
                }
                return false;

            }
            catch (Exception ex)
            {
                //LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
                return false;
            }
        }

        public async Task<List<RefundHeader>> GetAllBySaleOrderIdCODAsync(Guid id)
        {
            try
            {
                //List<SaleOrderRefundDto> listDto = new List<SaleOrderRefundDto>();

                var actualOrderNumber = _db.Headers.AsNoTracking()
                    .Where(x => x.Id == id && x.ActiveFlag == 0)
                    .Select(x => x.ActualOrderNumber)
                    .FirstOrDefault();
                if (actualOrderNumber == null)
                {
                    return new List<RefundHeader>();
                }
                var entity = await _db.RefundHeaders.AsNoTracking()
                    .Include(x => x.RefundItems)
                    .Where(x => x.ActualOrderNumber == actualOrderNumber && x.ActiveFlag == 0)
                    .ToListAsync();
                foreach (var item in entity)
                {
                    var ReasonName = _db.RefundReasons.AsNoTracking().FirstOrDefault(x => x.ReasonCode == item.ReasonCode)?.ReasonName;
                    //dto.Invoices.ForEach(invoice =>
                    //{
                    //    var rootInvoice = this.UnitOfWork.GetItem<SaleOrderInvoiceDto, Invoices1>(x => x.InvoiceNumber == invoice.RootInvoiceNumber
                    //    );
                    //    invoice.RootInvoiceIssuedDate = rootInvoice?.InvoiceIssuedDate;
                    //});
                    //listDto.Add(dto);
                }
                return entity;
            }
            catch (Exception ex)
            {
                //LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
            }
            return null;
        }
    }
}
