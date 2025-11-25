using System;
using System.Collections.Generic;

namespace MW.Entities;

public partial class Store
{
    public Guid Id { get; set; }

    public Guid? MallId { get; set; }

    public string Code { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string MerchantTax { get; set; } = null!;

    public string Url { get; set; } = null!;

    public string? Description { get; set; }

    public string? CreateBy { get; set; }

    public string? UpdateBy { get; set; }

    public DateTime CreateDate { get; set; }

    public DateTime UpdateDate { get; set; }

    public byte ActiveFlag { get; set; }

    public string? TaxName { get; set; }

    public string? TaxAddress { get; set; }

    public int? Posnumber1 { get; set; }

    public int? Posnumber2 { get; set; }

    public string? MallCode { get; set; }

    public int StoreType { get; set; }

    public bool? ApplyPromotion { get; set; }
}
