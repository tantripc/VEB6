using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MiddlewareTool.Entities.Models;

[Table("Headers", Schema = "so")]
[Index("ActiveFlag", "CreateBy", "StoreCode", "BusinessId", "OrderNumber", "StatusID", "ReceiptDate", "CreateDate", "UpdateDate", Name = "IX_so_Headers", IsDescending = new[] { false, false, false, false, false, false, true, true, true })]
public partial class Header1
{
    [Key]
    public Guid Id { get; set; }

    public Guid BusinessId { get; set; }

    [Required]
    [StringLength(100)]
    public string StoreCode { get; set; }

    [Required]
    [StringLength(255)]
    public string CustomerName { get; set; }

    [Required]
    public string CustomerEmail { get; set; }

    [StringLength(20)]
    public string OrderNumber { get; set; }

    public byte StatusID { get; set; }

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

    public string Description { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime ReceiptDate { get; set; }

    public Guid? UploadId { get; set; }

    [Column(TypeName = "decimal(18, 0)")]
    public decimal TotalVATAmount { get; set; }

    [Column(TypeName = "decimal(18, 0)")]
    public decimal TotalAmountWithoutVAT { get; set; }

    [Column(TypeName = "decimal(18, 0)")]
    public decimal TotalAmountWithVAT { get; set; }

    public string ErrorMess { get; set; }

    [ForeignKey("BusinessId")]
    [InverseProperty("Header1s")]
    public virtual Business Business { get; set; }

    [InverseProperty("Header")]
    public virtual ICollection<History> Histories { get; set; } = new List<History>();

    [InverseProperty("Header")]
    public virtual ICollection<Invoice1> Invoice1s { get; set; } = new List<Invoice1>();

    [InverseProperty("Header")]
    public virtual ICollection<Item1> Item1s { get; set; } = new List<Item1>();

    [InverseProperty("SaleOrder")]
    public virtual ICollection<RefundHeader1> RefundHeader1s { get; set; } = new List<RefundHeader1>();
}
