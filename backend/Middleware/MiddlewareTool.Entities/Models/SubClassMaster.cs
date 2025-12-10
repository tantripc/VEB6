using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MiddlewareTool.Entities.Models;

[Table("SubClassMaster", Schema = "core")]
public partial class SubClassMaster
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [StringLength(1)]
    public string REC_ID { get; set; }

    [Required]
    [StringLength(6)]
    public string CLS { get; set; }

    [Required]
    [StringLength(9)]
    public string SUB_CLS { get; set; }

    [Required]
    [StringLength(30)]
    public string BCLS_NAME { get; set; }

    [Required]
    [StringLength(1)]
    public string ACS_FLAG { get; set; }

    [Required]
    [StringLength(1)]
    [Unicode(false)]
    public string Perishable { get; set; }

    [Required]
    [StringLength(3)]
    public string MBR_DISC { get; set; }

    [Required]
    [StringLength(3)]
    public string MOMMY_DISC { get; set; }

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
