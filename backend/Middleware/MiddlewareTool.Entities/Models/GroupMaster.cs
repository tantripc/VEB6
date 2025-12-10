using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MiddlewareTool.Entities.Models;

[Table("GroupMaster", Schema = "cat")]
[Index("DivisionId", Name = "IX_GroupMaster")]
public partial class GroupMaster
{
    [Key]
    public int Id { get; set; }

    public int DivisionId { get; set; }

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

    [InverseProperty("Group")]
    public virtual ICollection<DepartmentMaster> DepartmentMasters { get; set; } = new List<DepartmentMaster>();

    [ForeignKey("DivisionId")]
    [InverseProperty("GroupMasters")]
    public virtual DivisionMaster Division { get; set; }
}
