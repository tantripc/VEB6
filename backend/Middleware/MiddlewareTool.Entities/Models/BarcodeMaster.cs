using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MiddlewareTool.Entities.Models;

[Table("BarcodeMaster", Schema = "core")]
[Index("ActiveFlag", Name = "IX_ActiveFlag")]
[Index("BAR_NO", Name = "IX_BAR_NO")]
[Index("BAR_SKU_NO", Name = "IX_BAR_SKU_NO")]
[Index("ActiveFlag", "Id", "BAR_SKU_NO", "StoreCode", "IsTransferM", "IsTransferINV", Name = "IX_BarcodeMaster")]
[Index("StoreCode", Name = "IX_StoreCode")]
[Index("ActiveFlag", "Id", "BAR_SKU_NO", "StoreCode", "IsTransferM", "IsTransferINV", Name = "IX_core_BarcodeMaster")]
public partial class BarcodeMaster
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [StringLength(1)]
    public string REC_ID { get; set; }

    [Required]
    [StringLength(13)]
    public string BAR_SKU_NO { get; set; }

    [Required]
    [StringLength(18)]
    public string BAR_NO { get; set; }

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

    public bool? IsTransferM { get; set; }

    public bool? IsTransferINV { get; set; }
}
