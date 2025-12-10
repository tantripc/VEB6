using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MiddlewareTool.Entities.Models;

[Table("PaymentByStore", Schema = "se")]
[Index("ActiveFlag", "HeaderId", "StoreCode", "PaymentType", "UpdateDate", Name = "IX_PaymentByStore", IsDescending = new[] { false, false, false, false, true })]
public partial class PaymentByStore1
{
    [Key]
    public Guid Id { get; set; }

    public Guid HeaderId { get; set; }

    [Required]
    [StringLength(13)]
    public string StoreCode { get; set; }

    [Required]
    [StringLength(10)]
    public string PaymentType { get; set; }

    public double TotalAmount { get; set; }

    public double TotalAmountOriginal { get; set; }

    [StringLength(30)]
    public string TransactionID { get; set; }

    [StringLength(128)]
    public string AuthID { get; set; }

    public double TotalAmountWithoutVATForTaxableItems { get; set; }

    public double TotalAmountForNonTaxableItems { get; set; }

    public double TotalTaxAmount { get; set; }

    [StringLength(30)]
    public string SubOrderID { get; set; }

    public double ActualTotalTender { get; set; }

    [StringLength(500)]
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
