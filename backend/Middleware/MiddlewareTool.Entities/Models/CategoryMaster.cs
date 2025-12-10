using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MiddlewareTool.Entities.Models;

[Table("CategoryMaster", Schema = "cat")]
public partial class CategoryMaster
{
    [Key]
    [StringLength(50)]
    public string Id { get; set; }

    public int DepartmentId { get; set; }

    [Required]
    public string Description { get; set; }

    public bool? AutoPA { get; set; }

    [StringLength(3)]
    public string PosFlag { get; set; }

    public bool? PwpExclusion { get; set; }

    public int? AgeStockRetenPeriod { get; set; }

    public bool? MbrDiscFlag { get; set; }

    public int? MbrDiscPerc { get; set; }

    public int? MommyDiscPerc { get; set; }

    public int? OrderNumber { get; set; }

    public string URL { get; set; }

    [StringLength(50)]
    public string CreateBy { get; set; }

    [StringLength(50)]
    public string UpdateBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreateDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdateDate { get; set; }

    public byte? ActiveFlag { get; set; }

    [ForeignKey("DepartmentId")]
    [InverseProperty("CategoryMasters")]
    public virtual DepartmentMaster Department { get; set; }
}
