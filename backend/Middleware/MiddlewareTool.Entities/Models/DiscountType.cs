using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MiddlewareTool.Entities.Models;

[Table("DiscountType", Schema = "se")]
[Index("UpdateDate", "TransactionType", "BOXED", "ActiveFlag", Name = "IX_DiscountType", IsDescending = new[] { true, false, false, false })]
public partial class DiscountType
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [StringLength(50)]
    public string TransactionType { get; set; }

    [Required]
    [StringLength(10)]
    public string BOXED { get; set; }

    [StringLength(10)]
    public string PROFIT { get; set; }

    public bool Remove { get; set; }

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
}
