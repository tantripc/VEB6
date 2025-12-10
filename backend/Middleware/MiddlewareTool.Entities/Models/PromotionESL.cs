using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MiddlewareTool.Entities.Models;

[Table("PromotionESL", Schema = "core")]
[Index("UpdateDate", "ActiveFlag", "SKU", "StoreCode", "IsTransferESL", "EDLPFlag", "StartDate", "EndDate", "StartTime", "EndTime", "CreateBy", "UpdateBy", Name = "IX_PromotionESL", IsDescending = new[] { true, false, false, false, false, false, false, true, false, true, false, false })]
public partial class PromotionESL
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [StringLength(13)]
    public string SKU { get; set; }

    [Required]
    [StringLength(10)]
    public string StoreCode { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public TimeOnly StartTime { get; set; }

    public TimeOnly EndTime { get; set; }

    [Required]
    [StringLength(50)]
    public string CreateBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreateDate { get; set; }

    [Required]
    [StringLength(50)]
    public string UpdateBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime UpdateDate { get; set; }

    public byte ActiveFlag { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? StartDateTime { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? EndDateTime { get; set; }

    public byte EDLPFlag { get; set; }

    public bool IsTransferESL { get; set; }
}
