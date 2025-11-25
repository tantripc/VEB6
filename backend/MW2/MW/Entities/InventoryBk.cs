using System;
using System.Collections.Generic;

namespace MW.Entities;

public partial class InventoryBk
{
    public Guid Id { get; set; }

    public string StoreCode { get; set; } = null!;

    public string StoreName { get; set; } = null!;

    public string Sku { get; set; } = null!;

    public double Quantity { get; set; }

    public bool? IsTransfer { get; set; }

    public string? Url { get; set; }

    public string? CreateBy { get; set; }

    public string? UpdateBy { get; set; }

    public DateTime CreateDate { get; set; }

    public DateTime UpdateDate { get; set; }

    public byte ActiveFlag { get; set; }

    public string StoreCodeSku { get; set; } = null!;
}
