using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MiddlewareTool.Entities.Models;

[Table("RefundInvoices", Schema = "re")]
[Index("ActiveFlag", "HeaderId", "StoreCode", "SerialNo", "Number", Name = "IX_re_RefundInvoices")]
public partial class RefundInvoice
{
    [Key]
    public Guid Id { get; set; }

    public Guid HeaderId { get; set; }

    [StringLength(20)]
    public string Code { get; set; }

    [StringLength(20)]
    public string SerialNo { get; set; }

    [StringLength(20)]
    public string Number { get; set; }

    [StringLength(500)]
    public string CustomerName { get; set; }

    [StringLength(500)]
    public string Company { get; set; }

    public string Address { get; set; }

    [StringLength(100)]
    public string TaxCode { get; set; }

    [Required]
    [StringLength(4)]
    public string StoreCode { get; set; }

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
}
