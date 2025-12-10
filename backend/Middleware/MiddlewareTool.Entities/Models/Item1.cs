using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MiddlewareTool.Entities.Models;

[Table("Items", Schema = "so")]
[Index("HeaderId", "Sku", Name = "IX_so_Items")]
public partial class Item1
{
    [Key]
    public Guid Id { get; set; }

    public Guid HeaderId { get; set; }

    [Required]
    [StringLength(13)]
    public string Sku { get; set; }

    public string Name { get; set; }

    public int Quantity { get; set; }

    public double Price { get; set; }

    public double VATAmount { get; set; }

    public double VATCode { get; set; }

    [StringLength(50)]
    public string UnitType { get; set; }

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

    public string WarningMess { get; set; }

    public int LineNumber { get; set; }

    public double ListPrice { get; set; }

    public string ErrorMess { get; set; }

    public double? POPrice { get; set; }

    public double? PromotionAmount { get; set; }

    [StringLength(100)]
    public string PNLAllocation { get; set; }

    [StringLength(10)]
    public string TransactionType { get; set; }

    public bool? IsTaxB2B { get; set; }

    [ForeignKey("HeaderId")]
    [InverseProperty("Item1s")]
    public virtual Header1 Header { get; set; }
}
