using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MiddlewareTool.Entities.Models;

[Table("LineMaster", Schema = "cat")]
public partial class LineMaster
{
    [Key]
    public int Id { get; set; }

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

    [InverseProperty("Line")]
    public virtual ICollection<DivisionMaster> DivisionMasters { get; set; } = new List<DivisionMaster>();
}
