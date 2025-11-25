using System;
using System.Collections.Generic;

namespace MW.Entities;

public partial class RefundInvoice
{
    public Guid Id { get; set; }

    public Guid HeaderId { get; set; }

    public string? Code { get; set; }

    public string? SerialNo { get; set; }

    public string? Number { get; set; }

    public string? CustomerName { get; set; }

    public string? Company { get; set; }

    public string? Address { get; set; }

    public string? TaxCode { get; set; }

    public string StoreCode { get; set; } = null!;

    public string? Url { get; set; }

    public string? CreateBy { get; set; }

    public string? UpdateBy { get; set; }

    public DateTime CreateDate { get; set; }

    public DateTime UpdateDate { get; set; }

    public byte ActiveFlag { get; set; }

    public string? Cqtcode { get; set; }
}
