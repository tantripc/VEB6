using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MiddlewareTool.Entities.Models;

[Table("HPriceChange", Schema = "core")]
[Index("PRC_NO", "StoreCode", "REC_ID", Name = "IX_HPriceChange")]
public partial class HPriceChange
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [StringLength(1)]
    public string REC_ID { get; set; }

    [Required]
    [StringLength(10)]
    public string PRC_NO { get; set; }

    [Required]
    [StringLength(30)]
    public string PRC_DESC { get; set; }

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
    [StringLength(5)]
    public string APPLICABLE_TO { get; set; }

    [Required]
    [StringLength(7)]
    public string REWARD_EVENT_DAY { get; set; }

    [Required]
    [StringLength(17)]
    public string MIN_ENTITLEMENT_AMOUNT { get; set; }

    [Required]
    [StringLength(1)]
    public string GST { get; set; }

    [Required]
    [StringLength(8)]
    public string MAX_ENTITLEMENT_PWP_QUANTITY { get; set; }

    [Required]
    [StringLength(8)]
    public string MAX_RECEIPT_PWP_QUANTITY { get; set; }

    [Required]
    public string INCLUSION_DIVISION { get; set; }

    [Required]
    public string INCLUSION_DEPARTMENT { get; set; }

    [Required]
    public string INCLUSION_CATEGORY { get; set; }

    [Required]
    public string INCLUSION_SKU { get; set; }

    [Required]
    public string EXCLUSION_DEPARTMENT { get; set; }

    [Required]
    public string EXCLUSION_CATEGORY { get; set; }

    [Required]
    public string EXCLUSION_SKU { get; set; }

    [Required]
    public string SHORT_SKU_CODE { get; set; }

    [Required]
    public string NEW_PWP_SELLING_PRICE { get; set; }

    [Required]
    [StringLength(1)]
    public string AUTO_SCAN_FOC { get; set; }

    [Required]
    [StringLength(8)]
    public string FOC_SHORT_SKU { get; set; }

    [Required]
    [StringLength(2)]
    public string FOC_QTY { get; set; }

    [Required]
    [StringLength(1)]
    public string FOC_ITEM_FOR_MEMBER_ONLY { get; set; }

    [Required]
    [StringLength(2)]
    public string MAX_FOC_QTY_FOR_MEMBER { get; set; }

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
