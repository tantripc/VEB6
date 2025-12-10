using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MiddlewareTool.Entities.Models;

[Table("ItemForDelivery", Schema = "se")]
[Index("HeaderId", "StoreCode", "ActiveFlag", Name = "IX_ItemForDelivery")]
public partial class ItemForDelivery
{
    [Key]
    public Guid Id { get; set; }

    public Guid HeaderId { get; set; }

    [Required]
    [StringLength(13)]
    public string Sku { get; set; }

    public int QuantitySold { get; set; }

    public double SellingPrice { get; set; }

    public double TotalAmount { get; set; }

    [Required]
    [StringLength(4)]
    public string StoreCode { get; set; }

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

    public double? ListPrice { get; set; }

    public double? VATAmount { get; set; }

    public double? VATCode { get; set; }
}
