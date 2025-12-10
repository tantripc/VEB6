using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MiddlewareTool.Entities.Models;

[Table("RefundHeaders", Schema = "re")]
[Index("ActualOrderNumber", Name = "IX_ActualOrderNumber")]
[Index("ActiveFlag", "CustomerType", "RefundDate", "IsTransfer", "OrderNumber", Name = "IX_reHeaders")]
public partial class RefundHeader
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [StringLength(100)]
    public string MallCode { get; set; }

    [Required]
    [StringLength(8)]
    public string RefundDate { get; set; }

    [Required]
    [StringLength(40)]
    public string RefundTime { get; set; }

    [Required]
    [StringLength(20)]
    public string OrderNumber { get; set; }

    [Required]
    [StringLength(8)]
    public string SalesDate { get; set; }

    [Required]
    [StringLength(10)]
    public string ReasonCode { get; set; }

    public string Description { get; set; }

    public bool? IsTransfer { get; set; }

    public string URL { get; set; }

    [Required]
    [StringLength(50)]
    public string CreateBy { get; set; }

    [StringLength(50)]
    public string UpdateBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreateDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime UpdateDate { get; set; }

    public byte ActiveFlag { get; set; }

    [StringLength(1)]
    public string CustomerType { get; set; }

    [StringLength(23)]
    public string ActualOrderNumber { get; set; }

    [Required]
    [StringLength(50)]
    public string CustomerID { get; set; }

    [StringLength(32)]
    public string FoxtrotUserID { get; set; }

    [InverseProperty("Header")]
    public virtual ICollection<RefundItem> RefundItems { get; set; } = new List<RefundItem>();

    [InverseProperty("Header")]
    public virtual ICollection<RefundPayment> RefundPayments { get; set; } = new List<RefundPayment>();
}
