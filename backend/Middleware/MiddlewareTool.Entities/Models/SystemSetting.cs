using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MiddlewareTool.Entities.Models;

[Table("SystemSetting")]
[Index("ActiveFlag", "Type", "Layout", "Code", "Name", Name = "IX_SystemSetting")]
public partial class SystemSetting
{
    [Key]
    public Guid Id { get; set; }

    public byte Layout { get; set; }

    public byte Type { get; set; }

    [StringLength(100)]
    public string Code { get; set; }

    [Required]
    [StringLength(1000)]
    public string Name { get; set; }

    [Required]
    public string Value { get; set; }

    public string Description { get; set; }

    [Required]
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
