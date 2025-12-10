using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MiddlewareTool.Entities.Models;

[Table("B2BTax", Schema = "so")]
[Index("ActiveFlag", "SKU", Name = "IX_B2BTax")]
public partial class B2BTax
{
    [Key]
    public Guid Id { get; set; }

    [StringLength(50)]
    public string No { get; set; }

    [StringLength(13)]
    public string SKU { get; set; }

    [StringLength(4000)]
    public string ProductName { get; set; }

    public double? TaxCode_Normal { get; set; }

    public double TaxCode_B2B { get; set; }

    [Required]
    [StringLength(50)]
    public string CreateBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreateDate { get; set; }

    [Required]
    [StringLength(50)]
    public string UpdateBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime UpdateDate { get; set; }

    public byte ActiveFlag { get; set; }
}
