using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MiddlewareTool.Entities.Models;

[Table("RecordSales", Schema = "se")]
[Index("ActualOrderNumber", Name = "IX_ActualOrderNumber")]
[Index("IsTransferSAP", "IsTransferS4", "HeaderId", "StoreCode", "BillNumber", "ActualOrderNumber", Name = "IX_RecordSales")]
public partial class RecordSale
{
    [Key]
    public Guid Id { get; set; }

    public Guid HeaderId { get; set; }

    [Required]
    [StringLength(100)]
    public string StoreCode { get; set; }

    [Required]
    [StringLength(8)]
    public string SalesDate { get; set; }

    [Required]
    [StringLength(4)]
    public string SalesTime { get; set; }

    [Required]
    [StringLength(20)]
    public string OrderNumber { get; set; }

    [StringLength(20)]
    public string BillNumber { get; set; }

    public double? TotalAmount { get; set; }

    public double? PromotionAmount { get; set; }

    [StringLength(100)]
    public string PaymentType { get; set; }

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

    [StringLength(23)]
    public string ActualOrderNumber { get; set; }

    public bool IsTransferSAP { get; set; }

    public bool? IsTransferS4 { get; set; }
}
