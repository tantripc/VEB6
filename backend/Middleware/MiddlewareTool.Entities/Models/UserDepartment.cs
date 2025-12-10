using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MiddlewareTool.Entities.Models;

[PrimaryKey("UserId", "DeptId")]
[Table("UserDepartments", Schema = "acc")]
public partial class UserDepartment
{
    [Key]
    public Guid UserId { get; set; }

    [Key]
    public Guid DeptId { get; set; }

    [StringLength(150)]
    public string JobDescription { get; set; }

    public int? OrderNumber { get; set; }

    public bool? IsManager { get; set; }

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
