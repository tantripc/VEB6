using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MiddlewareTool.Entities.Models;

[Table("Headers", Schema = "se")]
[Index("ActualOrderNumber", Name = "IX_ActualOrderNumber")]
[Index("UpdateDate", Name = "IX_UpdateDate", AllDescending = true)]
[Index("ActiveFlag", "FulfillmentDate", "CustomerType", Name = "IX_seHeaders", IsDescending = new[] { false, true, false })]
public partial class Header
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [StringLength(100)]
    public string StoreCode { get; set; }

    [Required]
    [StringLength(8)]
    public string FulfillmentDate { get; set; }

    [Required]
    [StringLength(4)]
    public string SettlementTime { get; set; }

    [Required]
    [StringLength(20)]
    public string OrderNumber { get; set; }

    public bool? IsTransfer { get; set; }

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
    public virtual ICollection<Delivery> Deliveries { get; set; } = new List<Delivery>();

    [InverseProperty("Header")]
    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();

    [InverseProperty("Header")]
    public virtual ICollection<Item> Items { get; set; } = new List<Item>();

    [InverseProperty("Header")]
    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
}
