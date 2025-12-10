using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MiddlewareTool.Entities.Models;

[Table("PricingHistory", Schema = "core")]
[Index("UpdateDate", "StoreCode", "Sku", "Action", "CreateBy", "UpdateBy", "CreateDate", Name = "IX-PricingHistory", IsDescending = new[] { true, false, false, false, false, false, true })]
public partial class PricingHistory
{
    [Key]
    public Guid Id { get; set; }

    [StringLength(10)]
    public string StoreCode { get; set; }

    public string StoreName { get; set; }

    [StringLength(13)]
    public string Sku { get; set; }

    public double? Price { get; set; }

    public double? SalePrice { get; set; }

    [StringLength(12)]
    public string ExpiredDate { get; set; }

    public bool? IsTransfer { get; set; }

    [StringLength(50)]
    public string CreateBy { get; set; }

    [StringLength(50)]
    public string UpdateBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreateDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdateDate { get; set; }

    public byte? ActiveFlag { get; set; }

    [StringLength(4000)]
    public string Source { get; set; }

    public int? Action { get; set; }

    public string TransData { get; set; }
}
