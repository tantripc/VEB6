using System;
using System.Collections.Generic;

namespace MW.Entities;

public partial class RefundSku
{
    public Guid Id { get; set; }

    public string Sku { get; set; } = null!;

    public string? Url { get; set; }

    public string? CreateBy { get; set; }

    public string? UpdateBy { get; set; }

    public DateTime CreateDate { get; set; }

    public DateTime UpdateDate { get; set; }

    public byte ActiveFlag { get; set; }
}
