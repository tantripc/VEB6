using System;
using System.Collections.Generic;

namespace MW.Entities;

public partial class Stock
{
    public Guid Id { get; set; }

    public string RecordFlag { get; set; } = null!;

    public string Sku { get; set; } = null!;

    public string SkuDesc { get; set; } = null!;

    public string StoreCode { get; set; } = null!;

    public double SellingPrice { get; set; }

    public double StockOnHandQty { get; set; }

    public int? OrderNumber { get; set; }

    public string? Url { get; set; }

    public string? Description { get; set; }

    public string? CreateBy { get; set; }

    public string? UpdateBy { get; set; }

    public DateTime CreateDate { get; set; }

    public DateTime UpdateDate { get; set; }

    public byte ActiveFlag { get; set; }

    public string SkuStoreCode { get; set; } = null!;

    public string? OosDate { get; set; }

    public bool IsTransfer { get; set; }
}
