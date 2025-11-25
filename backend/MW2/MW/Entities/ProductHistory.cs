using System;
using System.Collections.Generic;

namespace MW.Entities;

public partial class ProductHistory
{
    public Guid Id { get; set; }

    public string? CompanyCode { get; set; }

    public string? Code { get; set; }

    public string? Name { get; set; }

    public string? Sku { get; set; }

    public string? VariantName { get; set; }

    public string? VariantOption { get; set; }

    public string? ExtendedName { get; set; }

    public string? BrandName { get; set; }

    public string? Description { get; set; }

    public string? Ingredients { get; set; }

    public string? CategoryCode { get; set; }

    public string? Upc { get; set; }

    public string? Barcode { get; set; }

    public string? Size { get; set; }

    public string? Weight { get; set; }

    public string? Height { get; set; }

    public string? Volume { get; set; }

    public int? MaxCartQuantity { get; set; }

    public double? UnitCount { get; set; }

    public string? UnitType { get; set; }

    public string? Origin { get; set; }

    public string? Grade { get; set; }

    public double? TaxRate { get; set; }

    public string? ImageLinks { get; set; }

    public bool? IsAgeGated { get; set; }

    public bool? IsChilled { get; set; }

    public bool? IsFrozen { get; set; }

    public bool? IsPerishable { get; set; }

    public bool? IsTransfer { get; set; }

    public int? Type { get; set; }

    public string? Seotitle { get; set; }

    public string? Seodescription { get; set; }

    public string? Seokeywords { get; set; }

    public string? InternalNote { get; set; }

    public string? VariantType { get; set; }

    public string? CustomerScope { get; set; }

    public string? Slug { get; set; }

    public bool? ShowInProductList { get; set; }

    public bool? DisplayAsOos { get; set; }

    public bool? IsTransferEsl { get; set; }

    public string? CreateBy { get; set; }

    public string? UpdateBy { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public byte? ActiveFlag { get; set; }

    public string? Source { get; set; }

    public int? Action { get; set; }

    public string? TransData { get; set; }

    public double? Length { get; set; }

    public double? Width { get; set; }

    public double? B2btaxRate { get; set; }

    public bool? MommyItem { get; set; }

    public bool? AeonCardItem { get; set; }
}
