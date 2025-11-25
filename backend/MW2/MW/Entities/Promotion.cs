using System;
using System.Collections.Generic;

namespace MW.Entities;

public partial class Promotion
{
    public Guid Id { get; set; }

    public Guid ItemId { get; set; }

    public double? PromotionAmount { get; set; }

    public string? Pnlallocation { get; set; }

    public string? TransactionType { get; set; }

    public string? Url { get; set; }

    public string? CreateBy { get; set; }

    public string? UpdateBy { get; set; }

    public DateTime CreateDate { get; set; }

    public DateTime UpdateDate { get; set; }

    public byte ActiveFlag { get; set; }

    public virtual Item Item { get; set; } = null!;
}
