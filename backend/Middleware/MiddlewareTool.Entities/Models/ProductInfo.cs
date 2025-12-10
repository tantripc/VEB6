using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MiddlewareTool.Entities.Models;

[Table("ProductInfo", Schema = "prod")]
[Index("ActiveFlag", Name = "IX_ActiveFlag")]
[Index("Sku", "StoreCode", "ActiveFlag", Name = "IX_ProductInfo", IsUnique = true)]
[Index("Sku", Name = "IX_Sku")]
[Index("StoreCode", "MallCode", Name = "IX_StoreCode")]
[Index("StoreCodeSku", Name = "IX_StoreCode_Sku")]
[Index("URL", Name = "IX_URL")]
[Index("ActiveFlag", "UpdateDate", "StoreCode", "Sku", "IsTransfer", "IsNew", "IsPublished", "IsSyncProfit", "Fulfillment", "StoreCodeSku", "QuickDelivery", Name = "IX__ProductInfo", IsDescending = new[] { false, true, false, false, false, false, false, false, false, false, false })]
[Index("Id", Name = "NonClusteredIndex-20240611-143502")]
public partial class ProductInfo
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [StringLength(10)]
    public string StoreCode { get; set; }

    [StringLength(100)]
    public string MallCode { get; set; }

    [Required]
    [StringLength(13)]
    public string Sku { get; set; }

    [Required]
    public string ProductName { get; set; }

    public double? Inventory { get; set; }

    public double? Pricing { get; set; }

    public bool? IsTransfer { get; set; }

    public bool IsNew { get; set; }

    public bool IsPublished { get; set; }

    public int? OrderNumber { get; set; }

    [StringLength(400)]
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

    public bool? IsSyncProfit { get; set; }

    [Required]
    [StringLength(23)]
    public string StoreCodeSku { get; set; }

    public byte? Fulfillment { get; set; }

    public int StockBuffer { get; set; }

    public bool? QuickDelivery { get; set; }
}
