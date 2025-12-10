using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MiddlewareTool.Entities.Models;

[Table("PaymentTypeMapping", Schema = "se")]
[Index("ActiveFlag", "UpdateDate", "Type", "Scope", "CustomerType", "IsMapping", "DeliveryCode", "PaymentCodeOutput", "Method", "AllowRefund", "SaleToRefund", Name = "IX_PaymentTypeMapping", IsDescending = new[] { false, true, false, false, false, false, false, false, false, false, false })]
public partial class PaymentTypeMapping
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [StringLength(10)]
    public string Type { get; set; }

    [StringLength(4000)]
    public string Description { get; set; }

    public byte? Scope { get; set; }

    [StringLength(10)]
    public string CustomerType { get; set; }

    public bool IsMapping { get; set; }

    [StringLength(100)]
    public string DeliveryCode { get; set; }

    [StringLength(10)]
    public string PaymentCodeOutput { get; set; }

    public byte? Method { get; set; }

    public bool AllowRefund { get; set; }

    [StringLength(10)]
    public string SaleToRefund { get; set; }

    [StringLength(4000)]
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
