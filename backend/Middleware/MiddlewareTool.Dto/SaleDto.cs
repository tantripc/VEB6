using MiddlewareTool.Common;
using MiddlewareTool.Logs;
using System.Data;
using System.Globalization;
using System.Reflection;

namespace MiddlewareTool.Dto
{
    public class SaleDto
    {
        public class BaseSaleDto
        {
            public Guid Id { get; set; }
            public bool ParseData(DataRow dr)
            {
                try
                {
                    for (int i = 0; i < dr.Table.Columns.Count; i++)
                    {
                        string _colName = dr.Table.Columns[i].ColumnName;
                        PropertyInfo _prop = this.GetType().GetProperty(_colName);
                        if (!(dr[_colName] is DBNull) && _prop != null)
                        {
                            _prop.SetValue(this, dr[_colName], null);
                        }
                    }
                    return true;
                }
                catch (Exception ex)
                {
                    Logging.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
                    return false;
                }
            }
        }
        public class HeaderDto : BaseDto
        {
            public string StoreCode { get; set; }
            public string FulfillmentDate { get; set; }
            public string SettlementTime { get; set; }
            public string CustomerID { get; set; }
            public string FoxtrotUserID { get; set; }
            public Nullable<bool> IsTransfer { get; set; }
            public List<DeliveryDto> Deliveries { get; set; }
            public List<InvoiceDto> Invoices { get; set; }
            public List<ItemDto> Items { get; set; }
            public List<PaymentDto> Payments { get; set; }
            public List<ItemForDeliveryDto> ItemForDeliveries { get; set; }
        }
        public class ItemDto : BaseDto
        {
            public Guid HeaderId { get; set; }
            public string Sku { get; set; }
            public int QuantitySold { get; set; }
            public double SellingPrice { get; set; }
            public string StoreCode { get; set; }
            public double ListPrice { get; set; }
            public double VATAmount { get; set; }
            public double VATCode { get; set; }
            public double POPrice { get; set; }
            public double PromotionAmount { get; set; }
            public string PNLAllocation { get; set; }
            public string TransactionType { get; set; }
            public List<PromotionDto> Promotions { get; set; }
            public string DisplayPrice
            {
                get
                {
                    return ListPrice != 0 ? string.Format("{0:0,0}", ListPrice) : ListPrice.ToString();
                }
            }
            public string DisplaySellingPrice
            {
                get
                {
                    return SellingPrice != 0 ? string.Format("{0:0,0}", SellingPrice) : SellingPrice.ToString();
                }
            }
            public string ProductName { get; set; }
        }
        public class PaymentDto : BaseDto
        {
            public Guid HeaderId { get; set; }
            public string PaymentType { get; set; }
            public double TotalAmount { get; set; }
            public string TransactionID { get; set; }
            public string AuthID { get; set; }
            public double TotalAmountWithoutVATForTaxableItems { get; set; }
            public double TotalAmountForNonTaxableItems { get; set; }
            public double TotalTaxAmount { get; set; }
        }
        public class InvoiceDto : BaseDto
        {
            public Guid HeaderId { get; set; }
            public string Code { get; set; }
            public string SerialNo { get; set; }
            public string Number { get; set; }
            public string CustomerName { get; set; }
            public string CompanyName { get; set; }
            public string Address { get; set; }
            public string VatCode { get; set; }
            public string CQTCode { get; set; }
            public string StoreCode { get; set; }
        }
        public class DeliveryDto : BaseDto
        {
            public Guid HeaderId { get; set; }
            public string SubOrderNumber { get; set; }
            public string DeliveryCode { get; set; }
            public string TrackingNumber { get; set; }
        }
        public class PromotionDto : BaseDto
        {
            public Guid ItemId { get; set; }
            public Nullable<double> PromotionAmount { get; set; }
            public string PNLAllocation { get; set; }
            public string TransactionType { get; set; }
        }
        public class PaymentTypeDto
        {
            public int Id { get; set; }
            public string Type { get; set; }
            public string Description { get; set; }
            public string Remark { get; set; }
        }
        public class SaleByStoreDto
        {
            public string StoreCode { get; set; }
            public Guid HeaderId { get; set; }
            public string OrderNumber { get; set; }
            public string FulfillmentDate { get; set; }
            public int QuantitySold { get; set; }
        }
        public class HeaderByStoreDto : BaseSaleDto
        {
            public string StoreCode { get; set; }
            public string SalesDate { get; set; }
            public string SalesTime { get; set; }
            public string OrderNumber { get; set; }
            public string BillNumber { get; set; }
            public string CustomerID { get; set; }
            public string FoxtrotUserID { get; set; }
            public List<InvoiceByStoreDto> Invoices { get; set; }
            public List<ItemByStoreDto> Items { get; set; }
            public List<PaymentByStoreDto> Payments { get; set; }
            public double TotalNetSales => Items.Sum(x => x.QuantitySold * x.SellingPrice);
            public double TotalTender => Payments.Sum(x => x.TotalAmount);
            public double ActualTotalTender => Payments.Sum(x => x.ActualTotalTender);
            public bool Difference
            {
                get
                {
                    return TotalNetSales != TotalTender
                        || TotalNetSales != ActualTotalTender
                        ;
                }
                set { }
            }
        }
        public class ItemByStoreDto : BaseSaleDto
        {
            public Guid HeaderId { get; set; }
            public string Sku { get; set; }
            public int QuantitySold { get; set; }
            public double SellingPrice { get; set; }
            public double ListPrice { get; set; }
            public double VATAmount { get; set; }
            public double VATCode { get; set; }
            public List<PromotionByStoreDto> Promotions { get; set; }
        }
        public class PaymentByStoreDto : BaseSaleDto
        {
            public Guid HeaderId { get; set; }
            public string PaymentType { get; set; }
            public double TotalAmount { get; set; }
            public double TotalAmountOriginal { get; set; }
            //PaymentDate
            public string TransactionID { get; set; }
            //AuthorizationID
            public string AuthID { get; set; }
            public double TotalAmountWithoutVATForTaxableItems { get; set; }
            public double TotalAmountForNonTaxableItems { get; set; }
            public double TotalTaxAmount { get; set; }
            public string SubOrderID { get; set; }
            public double ActualTotalTender { get; set; }
        }
        public class InvoiceByStoreDto : BaseSaleDto
        {
            public Guid HeaderId { get; set; }
            public string Code { get; set; }
            public string SerialNo { get; set; }
            public string Number { get; set; }
            public string CustomerName { get; set; }
            public string CompanyName { get; set; }
            public string Address { get; set; }
            public string VatCode { get; set; }
            public string CQTCode { get; set; }
        }
        public class PromotionByStoreDto : BaseSaleDto
        {
            public Guid ItemId { get; set; }
            public Nullable<double> PromotionAmount { get; set; }
            public string PNLAllocation { get; set; }
            public string TransactionType { get; set; }
        }
        public class ItemForRefundDto : BaseDto
        {
            public Guid HeaderId { get; set; }
            public string Sku { get; set; }
            public int QuantitySold { get; set; }
            public double TotalAmount { get; set; }
        }
        public class ItemForDeliveryDto : BaseDto
        {
            public Guid HeaderId { get; set; }
            public string Sku { get; set; }
            public int QuantitySold { get; set; }
            public double SellingPrice { get; set; }
            public double TotalAmount { get; set; }
            public string StoreCode { get; set; }
            public double TaxRate { get; set; }
            public double ListPrice { get; set; }
        }
        public class DeliverySkuDto : BaseDto
        {
            public string Sku { get; set; }
        }
        public class RefundSkuDto : BaseDto
        {
            public string Sku { get; set; }
        }
        public class RecordSaleFileDto : BaseDto
        {
            public string HeaderIds { get; set; }
            public string StoreCode { get; set; }
            public string Name { get; set; }
            public byte[] Content { get; set; }
            public string Ext { get; set; }
            public long Size { get; set; }
        }
        public class RecordSaleDto : BaseDto
        {
            public Guid HeaderId { get; set; }
            public string StoreCode { get; set; }
            public string SalesDate { get; set; }
            public string SalesTime { get; set; }
            public string SalesOrderNumber { get; set; }
            public string FulfillmentNumber { get; set; }
            public string FulfillmentType { get; set; }
            public string BillNumber { get; set; }
            public double TotalAmount { get; set; }
            public double OrderTotal { get; set; }
            public double PromotionAmount { get; set; }
            public string PaymentType { get; set; }
            public string DeliveryCode { get; set; }
            public string TrackingNumber { get; set; }
            public string InvoiceNumber { get; set; }
            public double VoucherAmount { get; set; }
            public double CreditOverAmount { get; set; }
            public string InvoiceSerialNumber { get; set; }
            public string ActualOrderNumber { get; set; }
            public bool IsTransferSAP { get; set; }
            public bool IsTransferS4 { get; set; }
            public string CustomerType { get; set; }
            public string CustomerId { get; set; }
            public bool CheckRefund { get; set; }
            public bool CheckInvoice { get; set; }
        }
        public class RecordSaleBIDto : RecordSaleDto
        {
            public string DisplaySalesDate
            {
                get
                {
                    if (!string.IsNullOrEmpty(SalesDate.ToString()))
                    {
                        var date = DateTime.ParseExact(SalesDate, "yyyyMMdd", CultureInfo.InvariantCulture);
                        return DateTime.Parse(date.ToString()).ToString("dd/MM/yyyy");
                    }
                    return string.Empty;
                }
            }
            public string DisplayPaymentType
            {
                get
                {
                    string result = string.Empty;
                    if (!string.IsNullOrEmpty(PaymentType))
                    {
                        var types = new List<string>();
                        if (PaymentType.Contains(","))
                        {
                            types = PaymentType.Split(',').ToList();
                        }
                        else
                        {
                            types.Add(PaymentType);
                        }
                        foreach (var item in types)
                        {
                            string type = item.Trim().Replace(" ", "");
                            if (type == AppType.PaymentType.ECDSLS.ToString())
                            {
                                string span = string.Format(@"<span class='label label-sm label-success'>{0}</span>", type);
                                if (!result.Contains(span))
                                {
                                    result += span;
                                }
                            }
                            else if (type == AppType.PaymentType.ECMRTN.ToString())
                            {
                                string span = string.Format(@"<span class='label label-sm label-info'>{0}</span>", type);
                                if (!result.Contains(span))
                                {
                                    result += span;
                                }
                            }
                            else if (type == AppType.PaymentType.ECMSLS.ToString())
                            {
                                string span = string.Format(@"<span class='label label-sm label-primary'>{0}</span>", type);
                                if (!result.Contains(span))
                                {
                                    result += span;
                                }
                            }
                            else if (type == AppType.PaymentType.ECDRTN.ToString())
                            {
                                string span = string.Format(@"<span class='label label-sm label-warning'>{0}</span>", type);
                                if (!result.Contains(span))
                                {
                                    result += span;
                                }
                            }
                            else if (type == string.Empty)
                            {
                                string span = string.Format(@"<span class='label label-sm label-secondary'>{0}</span>", "EVCSLS");
                                if (!result.Contains(span))
                                {
                                    result += span;
                                }
                            }
                            else
                            {
                                string span = string.Format(@"<span class='label label-sm label-danger'>{0}</span>", type);
                                if (!result.Contains(span))
                                {
                                    result += span;
                                }
                            }
                        }
                    }
                    return result;
                }
            }
            public string GroupPaymentType
            {
                get
                {
                    string result = string.Empty;
                    if (!string.IsNullOrEmpty(PaymentType))
                    {
                        var types = new List<string>();
                        if (PaymentType.Contains(","))
                        {
                            types = PaymentType.Split(',').ToList();
                        }
                        else
                        {
                            types.Add(PaymentType);
                        }
                        foreach (var item in types)
                        {
                            string type = item.Trim().Replace(" ", "");
                            if (type == AppType.PaymentType.ECDSLS.ToString() && !result.Contains(type))
                            {
                                result += type;
                            }
                            else if (type == AppType.PaymentType.ECMRTN.ToString() && !result.Contains(type))
                            {
                                result += type;
                            }
                            else if (type == AppType.PaymentType.ECMSLS.ToString() && !result.Contains(type))
                            {
                                result += type;
                            }
                            else if (type == AppType.PaymentType.ECDRTN.ToString() && !result.Contains(type))
                            {
                                result += type;
                            }
                            else
                            {
                                if (!result.Contains(type))
                                {
                                    result += type;
                                }
                            }
                        }
                    }
                    return result;
                }
            }
            public string DisplayDeliveryCode
            {
                get
                {
                    if (!string.IsNullOrEmpty(DeliveryCode))
                    {

                        if (DeliveryCode == AppType.DeliveryCode.INHOUSE.ToString())
                        {
                            return string.Format(@"<span class='label label-sm label-success'>{0}</span>", DeliveryCode);
                        }
                        else if (DeliveryCode == AppType.DeliveryCode.NINJAVAN.ToString())
                        {
                            return string.Format(@"<span class='label label-sm label-info'>{0}</span>", DeliveryCode);
                        }
                        else if (DeliveryCode == AppType.DeliveryCode.AHAMOVE.ToString())
                        {
                            return string.Format(@"<span class='label label-sm label-primary'>{0}</span>", DeliveryCode);
                        }
                        else if (DeliveryCode == AppType.DeliveryCode.BOPIS.ToString())
                        {
                            return string.Format(@"<span class='label label-sm label-warning'>{0}</span>", DeliveryCode);
                        }
                        else if (DeliveryCode == AppType.DeliveryCode.CARRIER.ToString())
                        {
                            return string.Format(@"<span class='label label-sm label-warning'>{0}</span>", DeliveryCode);
                        }
                        else
                        {
                            return string.Format(@"<span class='label label-sm label-danger'>{0}</span>", DeliveryCode);
                        }
                    }
                    return string.Empty;
                }
            }
            public string DisplayTotalAmount
            {
                get
                {
                    return TotalAmount != 0 ? string.Format("{0:0,0}", TotalAmount) : TotalAmount.ToString();
                }
            }
            public string DisplayPromotionAmount
            {
                get
                {
                    return PromotionAmount != 0 ? string.Format("{0:0,0}", PromotionAmount) : PromotionAmount.ToString();
                }
            }
            public string DisplayOrderTotal
            {
                get
                {
                    return OrderTotal != 0 ? string.Format("{0:0,0}", OrderTotal) : OrderTotal.ToString();
                }
            }
            public string DisplaySalesOrderNumber
            {
                get
                {
                    if (!string.IsNullOrEmpty(SalesOrderNumber))
                    {
                        if (SalesOrderNumber.Length > 8)
                            return SalesOrderNumber.Substring(8, 8);
                        else
                            return SalesOrderNumber;
                    }
                    return string.Empty;
                }
            }
            public string CustomerName { get; set; }
            public string BusinessName { get; set; }
            public string VatCode { get; set; }
            public string Address { get; set; }
            public string PhoneNumber { get; set; }
            public string CustomerAddress { get; set; }
            public string DisplayCustomerType
            {
                get
                {
                    if (!string.IsNullOrEmpty(CustomerType))
                    {

                        if (CustomerType == "B")
                        {
                            return "Business"; // Xanh lá
                        }
                        else if (CustomerType == "C")
                        {
                            return "E-commerce"; // Tím
                        }
                    }
                    return string.Empty;
                }
            }
        }

        public class ItemsDto
        {
            public double TotalAmount { get; set; }
            public int TotalQuantitySold { get; set; }
            public int TotalCount { get; set; }
            public List<ItemDto> Items { get; set; }
        }
    }
}
