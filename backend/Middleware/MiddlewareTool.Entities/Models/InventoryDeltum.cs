using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MiddlewareTool.Entities.Models;

[Table("InventoryDelta", Schema = "core")]
[Index("Sku", "StoreCode", Name = "IX_InventoryDelta", IsUnique = true)]
public partial class InventoryDeltum
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

    public double QuantityDelta { get; set; }

    public bool? IsTransfer { get; set; }

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
}
