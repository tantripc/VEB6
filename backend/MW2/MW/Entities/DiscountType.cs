using System;
using System.Collections.Generic;

namespace MW.Entities;

public partial class DiscountType
{
    public Guid Id { get; set; }

    public string TransactionType { get; set; } = null!;

    public string Boxed { get; set; } = null!;

    public string? Profit { get; set; }

    public bool Remove { get; set; }

    public string? Url { get; set; }

    public string? CreateBy { get; set; }

    public string? UpdateBy { get; set; }

    public DateTime CreateDate { get; set; }

    public DateTime UpdateDate { get; set; }

    public byte ActiveFlag { get; set; }
}
