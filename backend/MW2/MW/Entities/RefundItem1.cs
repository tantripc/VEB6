using System;
using System.Collections.Generic;

namespace MW.Entities;

public partial class RefundItem1
{
    public Guid Id { get; set; }

    public Guid HeaderId { get; set; }

    public string Sku { get; set; } = null!;

    public string? Name { get; set; }

    public int RootQuantity { get; set; }

    public int Quantity { get; set; }

    public double Price { get; set; }

    public double Vatamount { get; set; }

    public double Vatcode { get; set; }

    public string? UnitType { get; set; }

    public int LineNumber { get; set; }

    public double ListPrice { get; set; }

    public string? WarningMess { get; set; }

    public string? Url { get; set; }

    public string? CreateBy { get; set; }

    public string? UpdateBy { get; set; }

    public DateTime CreateDate { get; set; }

    public DateTime UpdateDate { get; set; }

    public byte ActiveFlag { get; set; }

    public int? Refunded { get; set; }

    public double? Poprice { get; set; }

    public double? PromotionAmount { get; set; }

    public string? Pnlallocation { get; set; }

    public string? TransactionType { get; set; }

    public bool? IsTaxB2b { get; set; }

    public virtual RefundHeader1 Header { get; set; } = null!;
}
