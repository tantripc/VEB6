using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MiddlewareTool.Entities.Models;

[Table("Promotion", Schema = "se")]
[Index("ItemId", "ActiveFlag", Name = "IX_Promotion")]
public partial class Promotion
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
    [InverseProperty("Promotions")]
    public virtual Item Item { get; set; }
}
