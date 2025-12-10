using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MiddlewareTool.Entities.Models;

[PrimaryKey("Sku", "StoreCode")]
[Table("Stock", Schema = "core")]
[Index("ActiveFlag", Name = "IX_ActiveFlag")]
[Index("SkuStoreCode", Name = "IX_Stock")]
public partial class Stock
{
    public Guid Id { get; set; }

    [Required]
    [StringLength(1)]
    public string RecordFlag { get; set; }

    [Key]
    [StringLength(13)]
    public string Sku { get; set; }

    [Required]
    public string SkuDesc { get; set; }

    [Key]
    [StringLength(10)]
    public string StoreCode { get; set; }

    public double SellingPrice { get; set; }

    public double StockOnHandQty { get; set; }

    public int? OrderNumber { get; set; }

    public string URL { get; set; }

    public string Description { get; set; }

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
    public string SkuStoreCode { get; set; }

    [StringLength(8)]
    public string OOS_Date { get; set; }

    public bool IsTransfer { get; set; }
}
