using MiddlewareTool.Logs;
using System.Data;
using System.Globalization;
using System.Reflection;

namespace MiddlewareTool.Dto
{
    public class RefundDto
    {
        public class BaseRefundDto
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
        public class RefundHeaderDto : BaseRefundDto
        {
            public string MallCode { get; set; }
            public string RefundDate { get; set; }
            public string RefundTime { get; set; }
            public string OrderNumber { get; set; }
            public string SaleOrderNumber { get; set; }
            public string SalesDate { get; set; }
            public string ReasonCode { get; set; }
            public string Description { get; set; }
            public string ReceiptNumber { get; set; }
            public string CustomerID { get; set; }
            public string FoxtrotUserID { get; set; }
            public List<RefundItemDto> RefundItems { get; set; }
            public List<RefundPaymentDto> RefundPayments { get; set; }
            public List<RefundInvoiceDto> RefundInvoices { get; set; }
        }
        public class RefundItemDto : BaseRefundDto
        {
            public Guid HeaderId { get; set; }
            public string Sku { get; set; }
            public int QuantityRefunded { get; set; }
            public double SellingPrice { get; set; }
            public double ListPrice { get; set; }
            public double VATAmount { get; set; }
            public double VATCode { get; set; }
            public List<RefundPromotionDto> Promotions { get; set; }
            public string ProductName { get; set; }
        }
        public class RefundPaymentDto : BaseRefundDto
        {
            public Guid HeaderId { get; set; }
            public string PaymentType { get; set; }
            public double AmountRefund { get; set; }
            //PaymentDate
            public string TransactionID { get; set; }
            public string AuthorizationID { get; set; }
            public string UserID { get; set; }
        }
        public class RefundInvoiceDto : BaseRefundDto
        {
            public Guid HeaderId { get; set; }
            public string Code { get; set; }
            public string SerialNo { get; set; }
            public string Number { get; set; }
            public string CustomerName { get; set; }
            public string Company { get; set; }
            public string Address { get; set; }
            public string TaxCode { get; set; }
            public string CQTCode { get; set; }
        }
        public class RefundPromotionDto : BaseRefundDto
        {
            public Guid ItemId { get; set; }
            public Nullable<double> PromotionAmount { get; set; }
            public string PNLAllocation { get; set; }
            public string TransactionType { get; set; }
        }
        public class RefundByStoreDto
        {
            public string StoreCode { get; set; }
            public Guid RefundHeaderId { get; set; }
            public string OrderNumber { get; set; }
            public string RefundDate { get; set; }
        }
        public class RecordRefundFileDto : BaseDto
        {
            public string HeaderIds { get; set; }
            public string StoreCode { get; set; }
            public string Name { get; set; }
            public byte[] Content { get; set; }
            public string Ext { get; set; }
            public long Size { get; set; }
        }
        public class RecordRefundDto : BaseDto
        {
            public Guid HeaderId { get; set; }
            public string StoreCode { get; set; }
            public string SalesDate { get; set; }
            public string RefundDate { get; set; }
            public string RefundTime { get; set; }
            public string SalesOrderNumber { get; set; }
            public string FulfillmentNumber { get; set; }
            public string FulfillmentType { get; set; }
            public string ReceiptNumber { get; set; }
            public double TotalAmount { get; set; }
            public double PromotionAmount { get; set; }
            public string InvoiceNumber { get; set; }
            public double VoucherAmount { get; set; }
            public double CreditOverAmount { get; set; }
            public string InvoiceSerialNumber { get; set; }
            public string OriginalBillNumber { get; set; }
            public string ReasonCode { get; set; }
            public string CustomerName { get; set; }
            public string Address { get; set; }
            public string TaxCode { get; set; }
            public string TrackingNumber { get; set; }
            public string DeliveryCode { get; set; }
            public string PaymentType { get; set; }
            public string CustomerType { get; set; }
            public string CustomerId { get; set; }
        }
        public class RecordRefundBIDto : RecordRefundDto
        {
            public string DisplaySalesDate
            {
                get
                {
                    if (!string.IsNullOrEmpty(SalesDate))
                    {
                        var date = DateTime.ParseExact(SalesDate, "yyyyMMdd", CultureInfo.InvariantCulture);
                        return DateTime.Parse(date.ToString()).ToString("dd/MM/yyyy");
                    }
                    return string.Empty;
                }
            }
            public string DisplayRefundDate
            {
                get
                {
                    if (!string.IsNullOrEmpty(RefundDate.ToString()))
                    {
                        var date = DateTime.ParseExact(RefundDate, "yyyyMMdd", CultureInfo.InvariantCulture);
                        return DateTime.Parse(date.ToString()).ToString("dd/MM/yyyy");
                    }
                    return string.Empty;
                }
            }
            public string DisplayTotalRefundAmount
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
            public string DisplaySalesOrderNumber
            {
                get
                {
                    if (!string.IsNullOrEmpty(SalesOrderNumber) && SalesOrderNumber.Length > 8)
                    {
                        return SalesOrderNumber.Substring(8, 8);
                    }
                    return string.Empty;
                }
            }
            public string DisplayCreateDate
            {
                get
                {
                    return CreateDate.ToString("dd/MM/yyyy HH:mm:ss") ?? string.Empty;
                }
            }
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
            public bool IsCOD { get; set; }
            public Guid RecordSaleId { get; set; }
        }
        public class RefundItemsDto
        {
            public double TotalAmount { get; set; }
            public int TotalQuantityRefunded { get; set; }
            public int TotalCount { get; set; }
            public List<RefundItemDto> Items { get; set; }
        }
    }
}
