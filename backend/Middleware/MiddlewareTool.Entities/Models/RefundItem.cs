using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MiddlewareTool.Entities.Models;

[Table("RefundItems", Schema = "re")]
[Index("ActiveFlag", "HeaderId", "StoreCode", Name = "IX_reRefundItems")]
public partial class RefundItem
{
    [Key]
    public Guid Id { get; set; }

    public Guid HeaderId { get; set; }

    [Required]
    [StringLength(13)]
    public string Sku { get; set; }

    public int QuantityRefunded { get; set; }

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
    [InverseProperty("RefundItems")]
    public virtual RefundHeader Header { get; set; }

    [InverseProperty("Item")]
    public virtual ICollection<RefundPromotion> RefundPromotions { get; set; } = new List<RefundPromotion>();
}
