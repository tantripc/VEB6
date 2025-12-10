using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MiddlewareTool.Entities.Models;

[Table("GroupPriceChange", Schema = "core")]
public partial class GroupPriceChange
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
    [StringLength(9)]
    public string SUBCLASS { get; set; }

    [Required]
    [StringLength(6)]
    public string PRC_DISC_RATE { get; set; }

    [Required]
    [StringLength(6)]
    public string EXCLUDE_SSN_ID { get; set; }

    [StringLength(1)]
    public string EndOfRecord { get; set; }

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
}
