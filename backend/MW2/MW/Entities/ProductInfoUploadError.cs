using System;
using System.Collections.Generic;

namespace MW.Entities;

public partial class ProductInfoUploadError
{
    public Guid Id { get; set; }

    public Guid UploadId { get; set; }

    public string? Sku { get; set; }

    public string? Infor { get; set; }

    public int? OrderNumber { get; set; }

    public string Url { get; set; } = null!;

    public string? Description { get; set; }

    public string? CreateBy { get; set; }

    public string? UpdateBy { get; set; }

    public DateTime CreateDate { get; set; }

    public DateTime UpdateDate { get; set; }

    public byte ActiveFlag { get; set; }

    public string? StockBuffer { get; set; }

    public string? IsPublished { get; set; }

    public string? IsSyncProfit { get; set; }

    public string? Fulfillment { get; set; }

    public bool? QuickDelivery { get; set; }

    public virtual ProductInfoUploadMonitor Upload { get; set; } = null!;
}
