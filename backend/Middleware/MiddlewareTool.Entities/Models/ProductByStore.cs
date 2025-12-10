using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MiddlewareTool.Entities.Models;

[Table("ProductByStores", Schema = "prod")]
public partial class ProductByStore
{
    [Key]
    public Guid Id { get; set; }

    [StringLength(10)]
    public string StoreCode { get; set; }

    [Required]
    [StringLength(100)]
    public string MallCode { get; set; }

    [Required]
    [StringLength(100)]
    public string Code { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    [StringLength(13)]
    public string Sku { get; set; }

    [StringLength(100)]
    public string CategoryCode { get; set; }

    [Required]
    [StringLength(100)]
    public string Upc { get; set; }

    [Required]
    [StringLength(100)]
    public string Barcode { get; set; }

    [Required]
    public string ImageLinks { get; set; }

    public bool? IsNew { get; set; }

    public byte ActiveFlag { get; set; }

    public bool? IsPublished { get; set; }
}
