using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MiddlewareTool.Entities.Models;

[Table("RefundHeaders", Schema = "so")]
[Index("SaleOrderId", Name = "IX_RefundHeaders_SaleOrderId")]
[Index("ActiveFlag", "StoreCode", "IsCOD", "RefundDate", "OrderNumber", "StatusID", "ReasonCode", Name = "IX_so_RefundHeaders", IsDescending = new[] { false, false, false, true, false, false, false })]
public partial class RefundHeader1
{
    [Key]
    public Guid Id { get; set; }

    public Guid SaleOrderId { get; set; }

    [Required]
    [StringLength(100)]
    public string StoreCode { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime RefundDate { get; set; }

    [StringLength(4)]
    public string RefundTime { get; set; }

    [StringLength(20)]
    public string OrderNumber { get; set; }

    public byte StatusID { get; set; }

    [Required]
    [StringLength(8)]
    public string SalesDate { get; set; }

    [Required]
    [StringLength(10)]
    public string ReasonCode { get; set; }

    public Guid? UploadId { get; set; }

    [Column(TypeName = "decimal(18, 0)")]
    public decimal TotalVATAmount { get; set; }

    [Column(TypeName = "decimal(18, 0)")]
    public decimal TotalAmountWithoutVAT { get; set; }

    [Column(TypeName = "decimal(18, 0)")]
    public decimal TotalAmountWithVAT { get; set; }

    public string Description { get; set; }

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

    [Required]
    [StringLength(255)]
    public string CustomerName { get; set; }

    [Required]
    public string CustomerEmail { get; set; }

    public bool? IsCOD { get; set; }

    [InverseProperty("Header")]
    public virtual ICollection<RefundInvoice1> RefundInvoice1s { get; set; } = new List<RefundInvoice1>();

    [InverseProperty("Header")]
    public virtual ICollection<RefundItem1> RefundItem1s { get; set; } = new List<RefundItem1>();

    [ForeignKey("SaleOrderId")]
    [InverseProperty("RefundHeader1s")]
    public virtual Header1 SaleOrder { get; set; }
}
