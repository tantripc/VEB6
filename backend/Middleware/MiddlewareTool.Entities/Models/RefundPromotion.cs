using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MiddlewareTool.Entities.Models;

[Table("RefundPromotion", Schema = "re")]
[Index("ItemId", Name = "IX_reRefundPromotion")]
public partial class RefundPromotion
{
    [Key]
    public Guid Id { get; set; }

    public Guid ItemId { get; set; }

    public double? PromotionAmount { get; set; }

    [StringLength(100)]
    public string PNLAllocation { get; set; }

    [StringLength(10)]
    public string TransactionType { get; set; }

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

    [ForeignKey("ItemId")]
    [InverseProperty("RefundPromotions")]
    public virtual RefundItem Item { get; set; }
}
