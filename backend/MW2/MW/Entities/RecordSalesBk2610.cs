using System;
using System.Collections.Generic;

namespace MW.Entities;

public partial class RecordSalesBk2610
{
    public Guid Id { get; set; }

    public Guid HeaderId { get; set; }

    public string StoreCode { get; set; } = null!;

    public string SalesDate { get; set; } = null!;

    public string SalesTime { get; set; } = null!;

    public string OrderNumber { get; set; } = null!;

    public string? BillNumber { get; set; }

    public double? TotalAmount { get; set; }

    public double? PromotionAmount { get; set; }

    public string? PaymentType { get; set; }

    public string? Url { get; set; }

    public string? CreateBy { get; set; }

    public string? UpdateBy { get; set; }

    public DateTime CreateDate { get; set; }

    public DateTime UpdateDate { get; set; }

    public byte ActiveFlag { get; set; }
}
