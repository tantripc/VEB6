using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MiddlewareTool.Entities.Models;

[Table("ProductInfoHistory", Schema = "prod")]
[Index("UpdateDate", "StoreCode", "Sku", "Action", "CreateBy", "UpdateBy", "CreateDate", Name = "IX-ProductInfoHistory", IsDescending = new[] { true, false, false, false, false, false, true })]
public partial class ProductInfoHistory
{
    [Key]
    public Guid Id { get; set; }

    [StringLength(10)]
    public string StoreCode { get; set; }

    [StringLength(100)]
    public string MallCode { get; set; }

    [StringLength(13)]
    public string Sku { get; set; }

    public double? Inventory { get; set; }

    public bool? IsTransfer { get; set; }

    public bool? IsNew { get; set; }

    public bool? IsPublished { get; set; }

    [StringLength(50)]
    public string CreateBy { get; set; }

    [StringLength(50)]
    public string UpdateBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreateDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdateDate { get; set; }

    public byte? ActiveFlag { get; set; }

    public bool? IsSyncProfit { get; set; }

    public byte? Fulfillment { get; set; }

    [StringLength(4000)]
    public string Source { get; set; }

    public int? Action { get; set; }

    public string TransData { get; set; }

    public int StockBuffer { get; set; }

    public bool? QuickDelivery { get; set; }
}
