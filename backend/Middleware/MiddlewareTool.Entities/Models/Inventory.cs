using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MiddlewareTool.Entities.Models;

[Table("Inventory", Schema = "core")]
[Index("ActiveFlag", Name = "IX_ActiveFlag")]
[Index("Sku", "StoreCode", "ActiveFlag", Name = "IX_Inventory", IsUnique = true)]
[Index("StoreCodeSku", Name = "IX_SkuStoreCode")]
[Index("URL", Name = "IX_URL")]
public partial class Inventory
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [StringLength(10)]
    public string StoreCode { get; set; }

    [Required]
    public string StoreName { get; set; }

    [Required]
    [StringLength(13)]
    public string Sku { get; set; }

    public double Quantity { get; set; }

    public bool? IsTransfer { get; set; }

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
