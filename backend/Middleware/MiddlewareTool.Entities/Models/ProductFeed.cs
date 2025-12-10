using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MiddlewareTool.Entities.Models;

[Table("ProductFeed", Schema = "prod")]
[Index("ActiveFlag", Name = "IX_ActiveFlag")]
[Index("GTIN", Name = "IX_GTIN")]
[Index("Id", Name = "IX_Id")]
[Index("IsTransferESL", Name = "IX_IsTransferESL")]
public partial class ProductFeed
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [StringLength(10)]
    public string SKU_ID { get; set; }

    [Required]
    [StringLength(2000)]
    public string TITLE { get; set; }

    [Required]
    public string DESCRIPTION { get; set; }

    [Required]
    [StringLength(2000)]
    public string PRODUCT_DETAIL { get; set; }

    [Required]
    [StringLength(1000)]
    public string GOOGLE_PRODUCT_CATEGORY { get; set; }

    [Required]
    [StringLength(1000)]
    public string PRODUCT_TYPE { get; set; }

    [Required]
    [StringLength(1000)]
    public string LINK { get; set; }

    [Required]
    [StringLength(1000)]
    public string DEEP_LINK { get; set; }

    [Required]
    public string IMAGE_LINK { get; set; }

    [Required]
    [StringLength(50)]
    public string CONDITION { get; set; }

    [Required]
    [StringLength(50)]
    public string AVAILABILITY { get; set; }

    [Required]
    [StringLength(30)]
    public string PRICE { get; set; }

    [Required]
    [StringLength(30)]
    public string SALE_PRICE { get; set; }

    [Required]
    [StringLength(1000)]
    public string BRAND { get; set; }

    [Required]
    [StringLength(100)]
    public string GTIN { get; set; }

    [Required]
    [StringLength(1000)]
    public string CUSTOM_LABEL_0 { get; set; }

    [Required]
    [StringLength(1000)]
    public string CUSTOM_LABEL_1 { get; set; }

    [Required]
    [StringLength(1000)]
    public string CUSTOM_LABEL_2 { get; set; }

    [Required]
    [StringLength(1000)]
    public string CUSTOM_LABEL_3 { get; set; }

    [Required]
    [StringLength(1000)]
    public string CUSTOM_LABEL_4 { get; set; }

    [StringLength(50)]
    public string CreateBy { get; set; }

    [StringLength(50)]
    public string UpdateBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreateDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime UpdateDate { get; set; }

    public byte ActiveFlag { get; set; }

    public bool IsTransferESL { get; set; }
}
