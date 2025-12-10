using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MiddlewareTool.Entities.Models;

[Table("RecordRefund", Schema = "re")]
[Index("HeaderId", "StoreCode", "IsTransferSAP", "IsTransferS4", Name = "IX_RecordRefund")]
public partial class RecordRefund
{
    [Key]
    public Guid Id { get; set; }

    public Guid HeaderId { get; set; }

    [Required]
    [StringLength(100)]
    public string StoreCode { get; set; }

    [Required]
    [StringLength(20)]
    public string ReceiptNumber { get; set; }

    public double TotalAmount { get; set; }

    public double PromotionAmount { get; set; }

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

    public bool IsTransfer { get; set; }

    public bool IsTransferSAP { get; set; }

    public bool? IsTransferS4 { get; set; }
}
