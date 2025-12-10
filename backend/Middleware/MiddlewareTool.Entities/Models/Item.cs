using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MiddlewareTool.Entities.Models;

[Table("Items", Schema = "se")]
[Index("HeaderId", "StoreCode", "VATAmount", "ActiveFlag", "QuantitySold", Name = "IX_Items")]
[Index("ActiveFlag", "HeaderId", "StoreCode", "Sku", Name = "IX_se_Items")]
public partial class Item
{
    [Key]
    public Guid Id { get; set; }

    public Guid HeaderId { get; set; }

    [Required]
    [StringLength(13)]
    public string Sku { get; set; }

    public int QuantitySold { get; set; }

    public double SellingPrice { get; set; }

    [Required]
    [StringLength(4)]
    public string StoreCode { get; set; }

    public double ListPrice { get; set; }

    public double VATAmount { get; set; }

    public double VATCode { get; set; }

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

    [ForeignKey("HeaderId")]
    [InverseProperty("Items")]
    public virtual Header Header { get; set; }

    [InverseProperty("Item")]
    public virtual ICollection<Promotion> Promotions { get; set; } = new List<Promotion>();
}
