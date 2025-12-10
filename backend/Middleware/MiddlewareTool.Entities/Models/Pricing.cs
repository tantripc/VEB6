using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MiddlewareTool.Entities.Models;

[Table("Pricing", Schema = "core")]
[Index("ActiveFlag", Name = "IX_ActiveFlag")]
[Index("Sku", "StoreCode", "ActiveFlag", Name = "IX_Pricing", IsUnique = true)]
[Index("StoreCodeSku", Name = "IX_SkuStoreCode")]
[Index("URL", Name = "IX_URL")]
public partial class Pricing
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [StringLength(13)]
    public string Sku { get; set; }

    [Required]
    [StringLength(10)]
    public string StoreCode { get; set; }

    [Required]
    public string StoreName { get; set; }

    public double Price { get; set; }

    public double? SalePrice { get; set; }

    public bool? IsTransfer { get; set; }

    public bool? IsChange { get; set; }

    [StringLength(12)]
    public string EffectDate { get; set; }

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

    [Required]
    [StringLength(23)]
    public string StoreCodeSku { get; set; }
}
