using System;
using System.Collections.Generic;

namespace MW.Entities;

public partial class Invoice1
{
    public Guid InvoiceKey { get; set; }

    public decimal InvoiceId { get; set; }

    public string StoreCode { get; set; } = null!;

    public string VatCode { get; set; } = null!;

    public string InvoiceTemplateCode { get; set; } = null!;

    public string InvoiceSeries { get; set; } = null!;

    public string InvoiceNumber { get; set; } = null!;

    public string InvoiceIssuedDate { get; set; } = null!;

    public string IntegrateKey { get; set; } = null!;

    public string InvoiceReceiveNumber { get; set; } = null!;

    public Guid HeaderId { get; set; }

    public string CustomerName { get; set; } = null!;

    public string CompanyName { get; set; } = null!;

    public string? Address { get; set; }

    public string? Url { get; set; }

    public string? CreateBy { get; set; }

    public string? UpdateBy { get; set; }

    public DateTime CreateDate { get; set; }

    public DateTime UpdateDate { get; set; }

    public byte ActiveFlag { get; set; }

    public string? Cqtcode { get; set; }

    public virtual Header1 Header { get; set; } = null!;
}
