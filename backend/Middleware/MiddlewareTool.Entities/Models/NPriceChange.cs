using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MiddlewareTool.Entities.Models;

[Table("NPriceChange", Schema = "core")]
[Index("PRC_NO", "StoreCode", "PRC_TYPE", Name = "IX_NPriceChange")]
public partial class NPriceChange
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [StringLength(10)]
    public string PRC_NO { get; set; }

    [Required]
    [StringLength(1)]
    public string PRC_TYPE { get; set; }

    [Required]
    [StringLength(8)]
    public string PRC_START_DATE { get; set; }

    [Required]
    [StringLength(8)]
    public string PRC_END_DATE { get; set; }

    [Required]
    [StringLength(4)]
    public string PRC_START_TIME { get; set; }

    [Required]
    [StringLength(4)]
    public string PRC_END_TIME { get; set; }

    [Required]
    [StringLength(8)]
    public string PLU_COUNT { get; set; }

    [Required]
    [StringLength(8)]
    public string MM_PROMO_QTY { get; set; }

    [Required]
    [StringLength(6)]
    public string MM_PROMO_QTY1 { get; set; }

    [Required]
    [StringLength(6)]
    public string MM_PROMO_QTY2 { get; set; }

    [Required]
    [StringLength(6)]
    public string MM_PROMO_QTY3 { get; set; }

    [Required]
    [StringLength(6)]
    public string MM_PROMO_QTY4 { get; set; }

    [Required]
    [StringLength(6)]
    public string MM_PROMO_QTY5 { get; set; }

    [Required]
    [StringLength(6)]
    public string MM_PROMO_QTY6 { get; set; }

    [Required]
    [StringLength(13)]
    public string SHORT_SKU1 { get; set; }

    [Required]
    [StringLength(13)]
    public string SHORT_SKU2 { get; set; }

    [Required]
    [StringLength(13)]
    public string SHORT_SKU3 { get; set; }

    [Required]
    [StringLength(13)]
    public string SHORT_SKU4 { get; set; }

    [Required]
    [StringLength(13)]
    public string SHORT_SKU5 { get; set; }

    [Required]
    [StringLength(13)]
    public string SHORT_SKU6 { get; set; }

    [Required]
    [StringLength(17)]
    public string MM_PROMO_PRICE1 { get; set; }

    [Required]
    [StringLength(17)]
    public string MM_PROMO_PRICE2 { get; set; }

    [Required]
    [StringLength(17)]
    public string MM_PROMO_PRICE3 { get; set; }

    [Required]
    [StringLength(17)]
    public string MM_PROMO_PRICE4 { get; set; }

    [Required]
    [StringLength(17)]
    public string MM_PROMO_PRICE5 { get; set; }

    [Required]
    [StringLength(17)]
    public string MM_PROMO_PRICE6 { get; set; }

    [Required]
    [StringLength(17)]
    public string MM_TT_PROMO_PRICE { get; set; }

    [Required]
    [StringLength(13)]
    public string PROMOTION_DESC { get; set; }

    [Required]
    [StringLength(1)]
    public string REC_ID { get; set; }

    [Required]
    [StringLength(4)]
    public string StoreCode { get; set; }

    [StringLength(50)]
    public string CreateBy { get; set; }

    [StringLength(50)]
    public string UpdateBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreateDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime UpdateDate { get; set; }

    public byte ActiveFlag { get; set; }
}
