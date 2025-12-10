using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MiddlewareTool.Entities.Models;

[Table("PriceChangeHistory", Schema = "core")]
[Index("ITEM_NO", "StoreCode", "StartDateTime", "EndDateTime", "UpdateDate", Name = "IX_PriceChangeHistory", IsDescending = new[] { false, false, true, true, true })]
public partial class PriceChangeHistory
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [StringLength(1)]
    public string REC_ID { get; set; }

    [Required]
    [StringLength(13)]
    public string ITEM_NO { get; set; }

    [Required]
    [StringLength(10)]
    public string PRC_NO { get; set; }

    [Required]
    [StringLength(6)]
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
    [StringLength(6)]
    public string PRC_DISC_RATE { get; set; }

    [Required]
    [StringLength(12)]
    public string PRC_DISC_AMT { get; set; }

    [Required]
    [StringLength(17)]
    public string PRC_SELL { get; set; }

    [Required]
    [StringLength(10)]
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

    [Required]
    [StringLength(23)]
    public string StoreCodeSku { get; set; }

    [Required]
    [StringLength(33)]
    public string StoreCodeSkuPRC_NO { get; set; }

    public int Action { get; set; }

    [StringLength(4000)]
    public string Source { get; set; }

    public string TransData { get; set; }

    public bool? IsT4VV { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? StartDateTime { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? EndDateTime { get; set; }

    public byte? T4VVFlag { get; set; }

    public bool? IsTransferESL { get; set; }
}
