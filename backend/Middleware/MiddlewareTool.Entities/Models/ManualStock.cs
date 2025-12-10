using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MiddlewareTool.Entities.Models;

[Table("ManualStock", Schema = "prod")]
public partial class ManualStock
{
    [Key]
    public Guid Id { get; set; }

    [StringLength(1)]
    public string RecordFlag { get; set; }

    [Required]
    [StringLength(13)]
    public string Sku { get; set; }

    [Required]
    public string SkuDesc { get; set; }

    [StringLength(10)]
    public string StoreCode { get; set; }

    public double? SellingPrice { get; set; }

    public double StockOnHandQty { get; set; }

    public int OrderNumber { get; set; }

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
}
