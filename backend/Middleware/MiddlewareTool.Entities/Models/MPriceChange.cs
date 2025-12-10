using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MiddlewareTool.Entities.Models;

[Table("MPriceChange", Schema = "core")]
public partial class MPriceChange
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
    [StringLength(13)]
    public string ITEM_NO { get; set; }

    [Required]
    [StringLength(8)]
    public string QUANTITY_DISCOUNT { get; set; }

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
    [StringLength(2)]
    public string QUANTITY { get; set; }

    [Required]
    [StringLength(17)]
    public string PRC_SELL { get; set; }

    [Required]
    [StringLength(17)]
    public string PRC_DESC { get; set; }

    [Required]
    [StringLength(1)]
    public string REC_ID { get; set; }

    [Required]
    [StringLength(1)]
    public string OTHER_FOC_ITEM_FLAG { get; set; }

    [StringLength(13)]
    public string OTHER_FOC_SKU_NUMBER { get; set; }

    [StringLength(2)]
    public string MINIMUM_QUANTITY { get; set; }

    [Required]
    [StringLength(2)]
    public string FOC_QUANTITY { get; set; }

    [Required]
    [StringLength(1)]
    public string FOC_FOR_MEMBER_ONLY { get; set; }

    [Required]
    [StringLength(2)]
    public string MAXIMUM_FOC_QTY_FOR_MEMBER { get; set; }

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
