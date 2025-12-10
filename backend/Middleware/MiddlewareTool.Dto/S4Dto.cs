using MiddlewareTool.Common;
using System.Xml.Serialization;
using static MiddlewareTool.Dto.RefundDto;
using static MiddlewareTool.Dto.SaleDto;

namespace MiddlewareTool.Dto.S4
{
    public class PaymentType
    {
        public string lineNo { get; set; }
        public string amount { get; set; }
        public string pmtMethod { get; set; } // Payment Type
        public string netAmount { get; set; }
        public string assignment { get; set; }
        public string Description { get; set; }
    }

    public class S4Dto
    {
        public S4Dto(HeaderByStoreDto header)
        {
            orderId = header.OrderNumber;
            receiptNo = header.BillNumber;
            originalReceiptNo = header.OrderNumber;
            invoiceNo = string.Format("{0}{1}-{2}", header.Invoices[0].Code, header.Invoices[0].SerialNo, header.Invoices[0].Number);
            extCustomerNo = header.CustomerID;
            postingDate = header.SalesDate;
            type = AppType.S4HANATypes.Sale;
            storeId = header.StoreCode;
            items = new List<PaymentType>();
            if (header.Payments != null)
            {
                TotalAmount = Math.Round(header.Payments.Sum(x => x.TotalAmount)).ToString();
                for (int i = 0; i < header.Payments.Count; i++)
                {
                    var payment = header.Payments[i];
                    items.Add(new PaymentType()
                    {
                        lineNo = (i + 1).ToString(),
                        amount = payment.TotalAmount.ToString(),
                        pmtMethod = payment.PaymentType,
                        netAmount = payment.TotalAmount.ToString(),
                        assignment = AppType.S4HANAAssignments.Sale,
                        Description = ""
                    });
                }
            }
        }
        public S4Dto(RefundHeaderDto header)
        {
            orderId = header.OrderNumber;
            receiptNo = header.ReceiptNumber;
            originalReceiptNo = header.SaleOrderNumber;
            invoiceNo = invoiceNo = invoiceNo = string.Format("{0}{1}-{2}", header.RefundInvoices[0].Code, header.RefundInvoices[0].SerialNo, header.RefundInvoices[0].Number);
            extCustomerNo = header.CustomerID;
            postingDate = header.RefundDate;
            type = AppType.S4HANATypes.Refund;
            storeId = header.MallCode;
            items = new List<PaymentType>();
            if (header.RefundPayments != null)
            {
                TotalAmount = Math.Round(header.RefundPayments.Sum(x => x.AmountRefund)).ToString();
                for (int i = 0; i < header.RefundPayments.Count; i++)
                {
                    var payment = header.RefundPayments[i];
                    items.Add(new PaymentType()
                    {
                        lineNo = (i + 1).ToString(),
                        amount = payment.AmountRefund.ToString(),
                        pmtMethod = payment.PaymentType,
                        netAmount = payment.AmountRefund.ToString(),
                        assignment = AppType.S4HANAAssignments.Refund,
                        Description = ""
                    });
                }
            }
        }
        public void SetUUID(Guid uuid)
        {
            this.uuid = AppValue.GuidToUUID(uuid);
        }
        public string uuid { get; set; }
        public string orderId { get; set; }
        public string receiptNo { get; set; }
        public string originalReceiptNo { get; set; }
        public string invoiceNo { get; set; }
        public string extCustomerNo { get; set; }
        public string postingDate { get; set; }
        public string type { get; set; }
        public string TotalAmount { get; set; }
        public string storeId { get; set; }
        public List<PaymentType> items { get; set; }

        public static Properties Deserialize(string responseData)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Entry));
            Properties headerResponse;
            using (var sreader = new StringReader(responseData))
            {
                var entry = (Entry)serializer.Deserialize(sreader);
                // Dữ liệu Header
                headerResponse = entry.Content.Properties;
            }
            return headerResponse;
        }
    }
    #region API Model
    [XmlRoot(ElementName = "entry", Namespace = "http://www.w3.org/2005/Atom")]
    public class Entry
    {
        [XmlElement(ElementName = "id", Namespace = "http://www.w3.org/2005/Atom")]
        public string Id { get; set; }

        [XmlElement(ElementName = "title", Namespace = "http://www.w3.org/2005/Atom")]
        public string Title { get; set; }

        [XmlElement(ElementName = "updated", Namespace = "http://www.w3.org/2005/Atom")]
        public string Updated { get; set; }

        [XmlElement(ElementName = "content", Namespace = "http://www.w3.org/2005/Atom")]
        public Content Content { get; set; }

        [XmlElement(ElementName = "link", Namespace = "http://www.w3.org/2005/Atom")]
        public List<Link> Links { get; set; }
    }

    public class Content
    {
        [XmlElement(ElementName = "properties", Namespace = "http://schemas.microsoft.com/ado/2007/08/dataservices/metadata")]
        public Properties Properties { get; set; }
    }

    public class Properties
    {
        [XmlElement(ElementName = "uuid", Namespace = "http://schemas.microsoft.com/ado/2007/08/dataservices")]
        public string UUID { get; set; }

        [XmlElement(ElementName = "orderId", Namespace = "http://schemas.microsoft.com/ado/2007/08/dataservices")]
        public string OrderId { get; set; }

        [XmlElement(ElementName = "receiptNo", Namespace = "http://schemas.microsoft.com/ado/2007/08/dataservices")]
        public string ReceiptNo { get; set; }

        [XmlElement(ElementName = "postingDate", Namespace = "http://schemas.microsoft.com/ado/2007/08/dataservices")]
        public string PostingDate { get; set; }

        [XmlElement(ElementName = "invoiceNo", Namespace = "http://schemas.microsoft.com/ado/2007/08/dataservices")]
        public string InvoiceNo { get; set; }

        [XmlElement(ElementName = "storeId", Namespace = "http://schemas.microsoft.com/ado/2007/08/dataservices")]
        public string StoreId { get; set; }

        [XmlElement(ElementName = "TotalAmount", Namespace = "http://schemas.microsoft.com/ado/2007/08/dataservices")]
        public decimal TotalAmount { get; set; }

        [XmlElement(ElementName = "status", Namespace = "http://schemas.microsoft.com/ado/2007/08/dataservices")]
        public string Status { get; set; }

        [XmlElement(ElementName = "message", Namespace = "http://schemas.microsoft.com/ado/2007/08/dataservices")]
        public string Message { get; set; }
    }

    public class Link
    {
        [XmlAttribute(AttributeName = "href")]
        public string Href { get; set; }

        [XmlAttribute(AttributeName = "rel")]
        public string Rel { get; set; }

        [XmlAttribute(AttributeName = "title")]
        public string Title { get; set; }
    }
    #endregion

}
