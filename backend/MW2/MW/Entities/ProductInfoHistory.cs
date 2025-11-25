using System;
using System.Collections.Generic;

namespace MW.Entities;

public partial class ProductInfoHistory
{
    public Guid Id { get; set; }

    public string? StoreCode { get; set; }

    public string? MallCode { get; set; }

    public string? Sku { get; set; }

    public double? Inventory { get; set; }

    public bool? IsTransfer { get; set; }

    public bool? IsNew { get; set; }

    public bool? IsPublished { get; set; }

    public string? CreateBy { get; set; }

    public string? UpdateBy { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public byte? ActiveFlag { get; set; }

    public bool? IsSyncProfit { get; set; }

    public byte? Fulfillment { get; set; }

    public string? Source { get; set; }

    public int? Action { get; set; }

    public string? TransData { get; set; }

    public int StockBuffer { get; set; }

    public bool? QuickDelivery { get; set; }
}
