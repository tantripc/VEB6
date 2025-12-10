using MiddlewareTool.Common;
using System.Xml.Serialization;

namespace MiddlewareTool.Dto
{
    public class SaleOrderDto : BaseDto
    {
        public System.Guid BusinessId { get; set; }
        public string StoreCode { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public byte StatusID { get; set; }
        public DateTime ReceiptDate { get; set; }
        public string OrderNumber { get; set; }
        public Nullable<System.Guid> UploadId { get; set; }
        public double TotalVATAmount { get; set; }
        public double TotalAmountWithoutVAT { get; set; }
        public double TotalAmountWithVAT { get; set; }
        public string ErrorMess { get; set; }

        public List<SaleOrderItemDto> Items { get; set; }
        public List<SaleOrderInvoiceDto> Invoices { get; set; }
        public List<SaleOrderRefundDto> RefundHeaders { get; set; }

        public BusinessDto Business { get; set; }
        public string ActionType { get; set; }
        public string Comment { get; set; }
        public bool ManualInvoice { get; set; }

        public void SetDefaultValueInsert()
        {
            this.CreateDate = DateTime.Now;
            this.UpdateDate = DateTime.Now;
            this.ActiveFlag = AppValue.ActiveFlag.Active;
        }
    }
    public class SaleCODDto : SaleOrderDto
    {
        public string CustomerID { get; set; }
        public string CustomerType { get; set; }
        public string PaymentType { get; set; }
        public string DeliveryCode { get; set; }
        public string FulfillmentNumber { get; set; }
        public string TrackingNumber { get; set; }
        public string BillNumber { get; set; }
        public Guid RecordSaleId { get; set; }
    }
    public class SaleOrderCompactDto
    {
        public Guid Id { get; set; }
        public string OrderNumber { get; set; }
        public string StoreCode { get; set; }
    }
    public class SaleOrderExportDto
    {
        public string ReceiptNumber { get; set; }
        public string ReceiptDate { get; set; }
        public string Store { get; set; }
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
    }
    public class SaleOrderItemDto : BaseDto
    {
        public System.Guid HeaderId { get; set; }
        public string Sku { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public double ListPrice { get; set; }
        public double VATAmount { get; set; }
        public double VATCode { get; set; }
        public string UnitType { get; set; }
        public string WarningMess { get; set; }
        public int LineNumber { get; set; }
        public string ErrorMess { get; set; }
        public double UnitPriceWithoutVAT { get; set; }
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
    public class SaleOrderInvoiceDto
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
        public string URL { get; set; }
        public string CreateBy { get; set; }
        public string UpdateBy { get; set; }
        public System.DateTime CreateDate { get; set; }
        public System.DateTime UpdateDate { get; set; }
        public byte ActiveFlag { get; set; }
    }

    public class SaleOrderFilterDto
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public string Keyword { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public Guid? BusinessId { get; set; }
        public string StoreCode { get; set; }
        public byte? StatusId { get; set; }
        public string CreatedBy { get; set; }
        public bool HasAllPermission { get; set; }
        public bool? Refunded { get; set; }
        public string ReasonCode { get; set; }
        public string OrderNumber { get; set; }
        public string CustomerType { get; set; }
        public List<Guid> HeaderIds { get; set; }
    }

    [XmlRoot(ElementName = "IssuedInvoicesResult", Namespace = "http://thaison.vn/inv")]
    public class IssuedInvoicesResult
    {
        [XmlElement(ElementName = "TransactionStatus", Namespace = "http://thaison.vn/inv")]
        public string TransactionStatus { get; set; }
        [XmlElement(ElementName = "InvoiceID", Namespace = "http://thaison.vn/inv")]
        public decimal InvoiceID { get; set; }
        [XmlElement(ElementName = "BranchCode", Namespace = "http://thaison.vn/inv")]
        public string BranchCode { get; set; }
        [XmlElement(ElementName = "SellerTaxCode", Namespace = "http://thaison.vn/inv")]
        public string SellerTaxCode { get; set; }
        [XmlElement(ElementName = "InvoiceTypeCode", Namespace = "http://thaison.vn/inv")]
        public string InvoiceTypeCode { get; set; }
        [XmlElement(ElementName = "InvoiceTypeName", Namespace = "http://thaison.vn/inv")]
        public string InvoiceTypeName { get; set; }
        [XmlElement(ElementName = "InvoiceTemplateCode", Namespace = "http://thaison.vn/inv")]
        public string InvoiceTemplateCode { get; set; }
        [XmlElement(ElementName = "InvoiceSeries", Namespace = "http://thaison.vn/inv")]
        public string InvoiceSeries { get; set; }
        [XmlElement(ElementName = "InvoiceNumber", Namespace = "http://thaison.vn/inv")]
        public string InvoiceNumber { get; set; }
        [XmlElement(ElementName = "InvoiceIssuedDate", Namespace = "http://thaison.vn/inv")]
        public string InvoiceIssuedDate { get; set; }
        [XmlElement(ElementName = "InvoiceKey", Namespace = "http://thaison.vn/inv")]
        public string InvoiceKey { get; set; }
        [XmlElement(ElementName = "IntegrateKey", Namespace = "http://thaison.vn/inv")]
        public string IntegrateKey { get; set; }
        [XmlElement(ElementName = "RootIntegrateKey", Namespace = "http://thaison.vn/inv")]
        public string RootIntegrateKey { get; set; }
        [XmlElement(ElementName = "RootTemplateCode", Namespace = "http://thaison.vn/inv")]
        public string RootTemplateCode { get; set; }
        [XmlElement(ElementName = "RootInvoiceSeries", Namespace = "http://thaison.vn/inv")]
        public string RootInvoiceSeries { get; set; }
        [XmlElement(ElementName = "RootInvoiceNumber", Namespace = "http://thaison.vn/inv")]
        public string RootInvoiceNumber { get; set; }
        [XmlElement(ElementName = "InvoiceReceiveNumber", Namespace = "http://thaison.vn/inv")]
        public string InvoiceReceiveNumber { get; set; }
        [XmlElement(ElementName = "AdjustmentType", Namespace = "http://thaison.vn/inv")]
        public string AdjustmentType { get; set; }
        [XmlElement(ElementName = "TransmissionStatesTax", Namespace = "http://thaison.vn/inv")]
        public string TransmissionStatesTax { get; set; }
        [XmlElement(ElementName = "TaxCodeResult", Namespace = "http://thaison.vn/inv")]
        public string TaxCodeResult { get; set; }
    }

    [XmlRoot(ElementName = "IssuedInvoicesResponse", Namespace = "http://thaison.vn/inv")]
    public class IssuedInvoicesResponse
    {
        [XmlElement(ElementName = "IssuedInvoicesResult", Namespace = "http://thaison.vn/inv")]
        public IssuedInvoicesResult IssuedInvoicesResult { get; set; }
        [XmlAttribute(AttributeName = "xmlns")]
        public string Xmlns { get; set; }
    }

    [XmlRoot(ElementName = "Body", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    public class Body
    {
        [XmlElement(ElementName = "IssuedInvoicesResponse", Namespace = "http://thaison.vn/inv")]
        public IssuedInvoicesResponse IssuedInvoicesResponse { get; set; }
    }

    [XmlRoot(ElementName = "Envelope", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    public class Envelope
    {
        [XmlElement(ElementName = "Body", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
        public Body Body { get; set; }
        [XmlAttribute(AttributeName = "soap", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Soap { get; set; }
        [XmlAttribute(AttributeName = "xsi", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Xsi { get; set; }
        [XmlAttribute(AttributeName = "xsd", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Xsd { get; set; }
    }
}