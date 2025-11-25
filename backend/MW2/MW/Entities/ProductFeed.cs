using System;
using System.Collections.Generic;

namespace MW.Entities;

public partial class ProductFeed
{
    public Guid Id { get; set; }

    public string SkuId { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string ProductDetail { get; set; } = null!;

    public string GoogleProductCategory { get; set; } = null!;

    public string ProductType { get; set; } = null!;

    public string Link { get; set; } = null!;

    public string DeepLink { get; set; } = null!;

    public string ImageLink { get; set; } = null!;

    public string Condition { get; set; } = null!;

    public string Availability { get; set; } = null!;

    public string Price { get; set; } = null!;

    public string SalePrice { get; set; } = null!;

    public string Brand { get; set; } = null!;

    public string Gtin { get; set; } = null!;

    public string CustomLabel0 { get; set; } = null!;

    public string CustomLabel1 { get; set; } = null!;

    public string CustomLabel2 { get; set; } = null!;

    public string CustomLabel3 { get; set; } = null!;

    public string CustomLabel4 { get; set; } = null!;

    public string? CreateBy { get; set; }

    public string? UpdateBy { get; set; }

    public DateTime CreateDate { get; set; }

    public DateTime UpdateDate { get; set; }

    public byte ActiveFlag { get; set; }

    public bool IsTransferEsl { get; set; }
}
