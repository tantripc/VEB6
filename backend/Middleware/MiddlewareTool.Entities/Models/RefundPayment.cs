using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MiddlewareTool.Entities.Models;

[Table("RefundPayments", Schema = "re")]
[Index("ActiveFlag", "HeaderId", "PaymentType", Name = "IX_reRefundPayments")]
public partial class RefundPayment
{
    [Key]
    public Guid Id { get; set; }

    public Guid HeaderId { get; set; }

    [Required]
    [StringLength(10)]
    public string PaymentType { get; set; }

    public double AmountRefund { get; set; }

    [StringLength(30)]
    public string TransactionID { get; set; }

    [StringLength(128)]
    public string AuthorizationID { get; set; }

    [StringLength(30)]
    public string UserID { get; set; }

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
    [InverseProperty("RefundPayments")]
    public virtual RefundHeader Header { get; set; }
}
