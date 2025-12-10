using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MiddlewareTool.Entities.Models;

[Table("DepartmentMaster", Schema = "cat")]
public partial class DepartmentMaster
{
    [Key]
    public int Id { get; set; }

    public int GroupId { get; set; }

    [Required]
    public string Description { get; set; }

    public int? OrderNumber { get; set; }

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

    [InverseProperty("Department")]
    public virtual ICollection<CategoryMaster> CategoryMasters { get; set; } = new List<CategoryMaster>();

    [ForeignKey("GroupId")]
    [InverseProperty("DepartmentMasters")]
    public virtual GroupMaster Group { get; set; }
}
