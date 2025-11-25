using System;
using System.Collections.Generic;

namespace MW.Entities;

public partial class ProductInfo
{
    public Guid Id { get; set; }

    public string StoreCode { get; set; } = null!;

    public string? MallCode { get; set; }

    public string Sku { get; set; } = null!;

    public string ProductName { get; set; } = null!;

    public double? Inventory { get; set; }

    public double? Pricing { get; set; }

    public bool? IsTransfer { get; set; }

    public bool IsNew { get; set; }

    public bool IsPublished { get; set; }

    public int? OrderNumber { get; set; }

    public string? Url { get; set; }

    public string? CreateBy { get; set; }

    public string? UpdateBy { get; set; }

    public DateTime CreateDate { get; set; }

    public DateTime UpdateDate { get; set; }

    public byte ActiveFlag { get; set; }

    public bool? IsSyncProfit { get; set; }

    public string StoreCodeSku { get; set; } = null!;

    public byte? Fulfillment { get; set; }

    public int StockBuffer { get; set; }

    public bool? QuickDelivery { get; set; }
}
