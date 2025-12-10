using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MiddlewareTool.Entities.Models;

[Table("InventoryHistory", Schema = "core")]
[Index("UpdateDate", "StoreCode", "Sku", "Action", "UpdateBy", "CreateBy", "CreateDate", Name = "IX_Inventory_History", IsDescending = new[] { true, false, false, false, true, false, true })]
public partial class InventoryHistory
{
    [Key]
    public Guid Id { get; set; }

    [StringLength(10)]
    public string StoreCode { get; set; }

    public string StoreName { get; set; }

    [StringLength(13)]
    public string Sku { get; set; }

    public double? Quantity { get; set; }

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
