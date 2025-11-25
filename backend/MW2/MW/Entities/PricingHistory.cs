using System;
using System.Collections.Generic;

namespace MW.Entities;

public partial class PricingHistory
{
    public Guid Id { get; set; }

    public string? StoreCode { get; set; }

    public string? StoreName { get; set; }

    public string? Sku { get; set; }

    public double? Price { get; set; }

    public double? SalePrice { get; set; }

    public string? ExpiredDate { get; set; }

    public bool? IsTransfer { get; set; }

    public string? CreateBy { get; set; }

    public string? UpdateBy { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public byte? ActiveFlag { get; set; }

    public string? Source { get; set; }

    public int? Action { get; set; }

    public string? TransData { get; set; }
}
