using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MiddlewareTool.Entities.Models;

[Table("Stores", Schema = "sto")]
public partial class Store
{
    [Key]
    public Guid Id { get; set; }

    public Guid? MallId { get; set; }

    [Required]
    [StringLength(10)]
    public string Code { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    [StringLength(250)]
    public string MerchantTax { get; set; }

    [Required]
    public string URL { get; set; }

    public string Description { get; set; }

    [StringLength(50)]
    public string CreateBy { get; set; }

    [StringLength(50)]
    public string UpdateBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreateDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime UpdateDate { get; set; }

    public byte ActiveFlag { get; set; }

    [StringLength(255)]
    public string TaxName { get; set; }

    public string TaxAddress { get; set; }

    public int? POSNumber1 { get; set; }

    public int? POSNumber2 { get; set; }

    [StringLength(50)]
    public string MallCode { get; set; }

    public int StoreType { get; set; }

    public bool? ApplyPromotion { get; set; }
}
