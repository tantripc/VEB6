using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MiddlewareTool.Entities.Models;

[Table("Invoices", Schema = "so")]
[Index("HeaderId", "InvoiceID", "InvoiceNumber", "InvoiceReceiveNumber", Name = "IX_so_Invoices")]
public partial class Invoice1
{
    [Key]
    public Guid InvoiceKey { get; set; }

    [Column(TypeName = "decimal(18, 0)")]
    public decimal InvoiceID { get; set; }

    [Required]
    [StringLength(4)]
    public string StoreCode { get; set; }

    [Required]
    [StringLength(100)]
    public string VatCode { get; set; }

    [Required]
    [StringLength(50)]
    public string InvoiceTemplateCode { get; set; }

    [Required]
    [StringLength(20)]
    public string InvoiceSeries { get; set; }

    [Required]
    [StringLength(20)]
    public string InvoiceNumber { get; set; }

    [Required]
    [StringLength(10)]
    public string InvoiceIssuedDate { get; set; }

    [Required]
    [StringLength(255)]
    public string IntegrateKey { get; set; }

    [Required]
    [StringLength(50)]
    public string InvoiceReceiveNumber { get; set; }

    public Guid HeaderId { get; set; }

    [Required]
    [StringLength(500)]
    public string CustomerName { get; set; }

    [Required]
    [StringLength(500)]
    public string CompanyName { get; set; }

    public string Address { get; set; }

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

    [StringLength(250)]
    public string CQTCode { get; set; }

    [ForeignKey("HeaderId")]
    [InverseProperty("Invoice1s")]
    public virtual Header1 Header { get; set; }
}
