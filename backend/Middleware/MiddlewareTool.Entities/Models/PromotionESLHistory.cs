using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MiddlewareTool.Entities.Models;

[Table("PromotionESLHistory", Schema = "core")]
[Index("SKU", "StoreCode", "StartDate", "EndDate", "StartTime", "EndTime", "CreateDate", "UpdateDate", "Action", "Source", Name = "IX_PromotionESLHistory", IsDescending = new[] { false, false, false, true, false, true, false, true, false, false })]
public partial class PromotionESLHistory
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

    public int Action { get; set; }

    public string TransData { get; set; }

    [StringLength(4000)]
    public string Source { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? StartDateTime { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? EndDateTime { get; set; }

    public byte EDLPFlag { get; set; }

    public bool IsTransferESL { get; set; }

    [StringLength(4000)]
    public string URL { get; set; }
}
