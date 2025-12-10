using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MiddlewareTool.Entities.Models;

[Table("Payments", Schema = "se")]
[Index("HeaderId", "PaymentType", "ActiveFlag", Name = "IX_Payments")]
[Index("HeaderId", "PaymentType", Name = "IX_sePayments")]
public partial class Payment
{
    [Key]
    public Guid Id { get; set; }

    public Guid HeaderId { get; set; }

    [Required]
    [StringLength(10)]
    public string PaymentType { get; set; }

    public double TotalAmount { get; set; }

    [StringLength(30)]
    public string TransactionID { get; set; }

    [StringLength(128)]
    public string AuthID { get; set; }

    public double TotalAmountWithoutVATForTaxableItems { get; set; }

    public double TotalAmountForNonTaxableItems { get; set; }

    public double TotalTaxAmount { get; set; }

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

    [StringLength(30)]
    public string SubOrderID { get; set; }

    [ForeignKey("HeaderId")]
    [InverseProperty("Payments")]
    public virtual Header Header { get; set; }
}
