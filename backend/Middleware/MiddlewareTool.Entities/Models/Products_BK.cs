using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MiddlewareTool.Entities.Models;

[Table("Products_BK", Schema = "prod")]
public partial class Products_BK
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [StringLength(100)]
    public string CompanyCode { get; set; }

    [StringLength(10)]
    public string StoreCode { get; set; }

    [Required]
    [StringLength(100)]
    public string Code { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    [StringLength(13)]
    public string Sku { get; set; }

    [Required]
    public string VariantName { get; set; }

    public string VariantOption { get; set; }

    public string ExtendedName { get; set; }

    public string BrandName { get; set; }

    public string Description { get; set; }

    public string Ingredients { get; set; }

    [StringLength(100)]
    public string CategoryCode { get; set; }

    [Required]
    [StringLength(100)]
    public string Upc { get; set; }

    [Required]
    [StringLength(100)]
    public string Barcode { get; set; }

    [StringLength(250)]
    public string Size { get; set; }

    [StringLength(250)]
    public string Weight { get; set; }

    [StringLength(250)]
    public string Length { get; set; }

    [StringLength(250)]
    public string Width { get; set; }

    [StringLength(250)]
    public string Height { get; set; }

    [StringLength(250)]
    public string Volume { get; set; }

    public int? MaxCartQuantity { get; set; }

    public double UnitCount { get; set; }

    [Required]
    [StringLength(250)]
    public string UnitType { get; set; }

    [StringLength(250)]
    public string Origin { get; set; }

    [StringLength(250)]
    public string Grade { get; set; }

    public double TaxRate { get; set; }

    [Required]
    public string ImageLinks { get; set; }

    public bool IsAgeGated { get; set; }

    public bool IsChilled { get; set; }

    public bool IsFrozen { get; set; }

    public bool? IsPerishable { get; set; }

    public bool? IsTransfer { get; set; }

    public int? OrderNumber { get; set; }

    [Required]
    public string URL { get; set; }

    [StringLength(50)]
    public string CreateBy { get; set; }

    [StringLength(50)]
    public string UpdateBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreateDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime UpdateDate { get; set; }

    public byte ActiveFlag { get; set; }

    public byte? Fulfillment { get; set; }

    public bool? IsPublished { get; set; }

    public bool? IsNew { get; set; }

    public bool? IsSyncProfit { get; set; }

    public byte? Type { get; set; }

    public string SEOTitle { get; set; }

    public string SEODescription { get; set; }

    public string SEOKeywords { get; set; }

    public string InternalNote { get; set; }

    [StringLength(50)]
    public string VariantType { get; set; }

    [StringLength(500)]
    public string CustomerScope { get; set; }

    [StringLength(4000)]
    public string Slug { get; set; }

    public bool ShowInProductList { get; set; }

    public bool DisplayAsOos { get; set; }

    public bool IsTransferESL { get; set; }
}
