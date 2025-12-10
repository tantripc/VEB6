using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MiddlewareTool.Entities.Models;

[Table("PaymentByStore", Schema = "re")]
[Index("ActiveFlag", "HeaderId", "StoreCode", "PaymentType", Name = "IX_re_PaymentByStore")]
public partial class PaymentByStore
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

    public double AmountRefund { get; set; }

    [StringLength(30)]
    public string TransactionID { get; set; }

    [StringLength(128)]
    public string AuthorizationID { get; set; }

    [StringLength(30)]
    public string UserID { get; set; }

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
