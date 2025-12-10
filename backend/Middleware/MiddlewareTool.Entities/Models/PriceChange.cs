using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MiddlewareTool.Entities.Models;

[Table("PriceChange", Schema = "core")]
[Index("ActiveFlag", Name = "IX_ActiveFlag")]
[Index("ITEM_NO", Name = "IX_ITEM_NO")]
[Index("IsTransferESL", Name = "IX_IsTransferESL")]
[Index("PRC_NO", Name = "IX_PRC_NO")]
[Index("T4VVFlag", Name = "IX_PriceChange_T4VVFlag")]
[Index("StoreCodeSku", Name = "IX_SkuStoreCode")]
[Index("StoreCode", Name = "IX_StoreCode")]
[Index("StoreCodeSkuPRC_NO", Name = "IX_StoreCodeSkuPRC_NO")]
[Index("IsT4VV", "T4VVFlag", "StartDateTime", "EndDateTime", Name = "IX_T4VV", IsDescending = new[] { true, false, true, true })]
public partial class PriceChange
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

    public bool? IsTransferESL { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? StartDateTime { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? EndDateTime { get; set; }

    public bool? IsT4VV { get; set; }

    public byte? T4VVFlag { get; set; }
}
