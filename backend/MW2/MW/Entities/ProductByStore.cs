using System;
using System.Collections.Generic;

namespace MW.Entities;

public partial class ProductByStore
{
    public Guid Id { get; set; }

    public string? StoreCode { get; set; }

    public string MallCode { get; set; } = null!;

    public string Code { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Sku { get; set; } = null!;

    public string? CategoryCode { get; set; }

    public string Upc { get; set; } = null!;

    public string Barcode { get; set; } = null!;

    public string ImageLinks { get; set; } = null!;

    public bool? IsNew { get; set; }

    public byte ActiveFlag { get; set; }

    public bool? IsPublished { get; set; }
}
