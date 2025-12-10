using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MiddlewareTool.Entities.Models;

public partial class Department
{
    [Key]
    public Guid Id { get; set; }

    public long Index { get; set; }

    public Guid? ParentId { get; set; }

    public int Level { get; set; }

    [Required]
    [StringLength(500)]
    public string Name { get; set; }

    [StringLength(100)]
    public string Code { get; set; }

    public int OrderNumber { get; set; }

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
