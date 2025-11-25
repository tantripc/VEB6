using System;
using System.Collections.Generic;

namespace MW.Entities;

public partial class B2btax
{
    public Guid Id { get; set; }

    public string? No { get; set; }

    public string? Sku { get; set; }

    public string? ProductName { get; set; }

    public double? TaxCodeNormal { get; set; }

    public double TaxCodeB2b { get; set; }

    public string CreateBy { get; set; } = null!;

    public DateTime CreateDate { get; set; }

    public string UpdateBy { get; set; } = null!;

    public DateTime UpdateDate { get; set; }

    public byte ActiveFlag { get; set; }
}
