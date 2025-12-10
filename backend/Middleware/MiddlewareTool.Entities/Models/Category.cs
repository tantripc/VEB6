using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MiddlewareTool.Entities.Models;

[Table("Category", Schema = "cat")]
public partial class Category
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Code { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string Path { get; set; }

    public bool? IsTransfer { get; set; }

    public Guid? ParentId { get; set; }

    public int? OrderNumber { get; set; }

    [Required]
    public string URL { get; set; }

    public string Description { get; set; }

    [StringLength(50)]
    public string CreateBy { get; set; }

    [StringLength(50)]
    public string UpdateBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreateDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime UpdateDate { get; set; }

    public byte ActiveFlag { get; set; }

    public bool? IsNew { get; set; }

    [InverseProperty("Category")]
    public virtual ICollection<Mapping> Mappings { get; set; } = new List<Mapping>();
}
