using MiddlewareTool.Common;
using static MiddlewareTool.Common.AppValue;

namespace MiddlewareTool.Dto
{
    public class SaleOrderRefundDto : BaseDto
    {
        public System.Guid SaleOrderId { get; set; }
        public string StoreCode { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public DateTime RefundDate { get; set; }
        public string RefundTime { get; set; }
        public string OrderNumber { get; set; }
        public AppValue.SaleOrderStatuses StatusID { get; set; }
        public string SalesDate { get; set; }
        public string ReasonCode { get; set; }
        public Nullable<System.Guid> UploadId { get; set; }
        public decimal TotalVATAmount { get; set; }
        public decimal TotalAmountWithoutVAT { get; set; }
        public decimal TotalAmountWithVAT { get; set; }

        public List<SaleOrderRefundItemDto> Items { get; set; }
        public SaleOrderDto Root { get; set; }
        public List<SaleOrderRefundInvoiceDto> Invoices { get; set; }
        public string ActionType { get; set; }
        public string Comment { get; set; }
        public bool ManualInvoice { get; set; }
        public string ReasonName { get; set; }

        public void SetDefaultValueInsert()
        {
            this.CreateDate = DateTime.Now;
            this.UpdateDate = DateTime.Now;
            this.ActiveFlag = AppValue.ActiveFlag.Active;
        }
    }
    public class SaleOrderRefundCODDto : SaleOrderRefundDto
    {
        public string SaleOrderNumber { get; set; }
        public new SaleCODDto Root { get; set; }
    }
    public class RefundOrderExportDto : SaleOrderExportDto
    {
        public string RefundNumber { get; set; }
        public string RefundDate { get; set; }
        public string Store { get; set; }
        public string SaleDate { get; set; }
        public string ReceiptDate { get; set; }
        public string Customer { get; set; }
        public string TotalAmountWithVAT { get; set; }
        public string TotalAmountWithoutVAT { get; set; }
        public string TotalVATAmount { get; set; }
        public string InvoiceNumber { get; set; }
        public string InvoiceIssuedDate { get; set; }
        public string InvoiceDate
        {
            get
            {
                if (!string.IsNullOrEmpty(InvoiceIssuedDate))
                {
                    var display = InvoiceIssuedDate.Split('-').Reverse();
                    return string.Join("/", display);
                }
                return string.Empty;
            }
        }
        public SaleOrderStatuses StatusID { get; set; }
        public string StatusName
        {
            get
            {
                switch (StatusID)
                {
                    case SaleOrderStatuses.Updated:
                        return "Draft";
                    case SaleOrderStatuses.Waiting:
                        return "Waiting";
                    case SaleOrderStatuses.Approved:
                        return "Error";
                    case SaleOrderStatuses.Rejected:
                        return "Rejected";
                    case SaleOrderStatuses.Invoiced:
                        return "Invoiced";
                    default:
                        break;
                }
                return "";
            }
        }
    }
    public class SaleOrderRefundItemDto : BaseDto
    {
        public System.Guid HeaderId { get; set; }
        public string Sku { get; set; }
        public string Name { get; set; }
        public int RootQuantity { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public double VATAmount { get; set; }
        public double VATCode { get; set; }
        public string UnitType { get; set; }
        public int LineNumber { get; set; }
        public double ListPrice { get; set; }
        public string WarningMess { get; set; }
        public int Refunded { get; set; }
        public double POPrice { get; set; }
        public double PromotionAmount { get; set; }
        public string PNLAllocation { get; set; }
        public string TransactionType { get; set; }
        public bool IsTaxB2B { get; set; }

        public void SetDefaultValueInsert()
        {
            this.CreateDate = DateTime.Now;
            this.UpdateDate = DateTime.Now;
            this.ActiveFlag = AppValue.ActiveFlag.Active;
        }
    }
    public class SaleOrderRefundInvoiceDto
    {
        public System.Guid InvoiceKey { get; set; }
        public decimal InvoiceID { get; set; }
        public string StoreCode { get; set; }
        public string VatCode { get; set; }
        public string InvoiceTemplateCode { get; set; }
        public string InvoiceSeries { get; set; }
        public string InvoiceNumber { get; set; }
        public string InvoiceIssuedDate { get; set; }
        public string IntegrateKey { get; set; }
        public string InvoiceReceiveNumber { get; set; }
        public System.Guid HeaderId { get; set; }
        public string CustomerName { get; set; }
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public string CQTCode { get; set; }
        public string RootIntegrateKey { get; set; }
        public string RootInvoiceTemplateCode { get; set; }
        public string RootInvoiceSeries { get; set; }
        public string RootInvoiceNumber { get; set; }
        public string RootInvoiceIssuedDate { get; set; }
        public string URL { get; set; }
        public string CreateBy { get; set; }
        public string UpdateBy { get; set; }
        public System.DateTime CreateDate { get; set; }
        public System.DateTime UpdateDate { get; set; }
        public byte ActiveFlag { get; set; }
    }
    public class RefundReasonDto
    {
        public string ReasonCode { get; set; }
        public string ReasonName { get; set; }
    }
}