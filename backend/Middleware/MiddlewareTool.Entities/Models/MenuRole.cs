using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MiddlewareTool.Entities.Models;

[Table("MenuRole", Schema = "acc")]
public partial class MenuRole
{
    [Key]
    public Guid Id { get; set; }

    public byte Type { get; set; }

    public Guid RoleId { get; set; }

    public Guid MenuId { get; set; }

    public Guid? MenuActionId { get; set; }

    [StringLength(50)]
    public string UserName { get; set; }

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

    [ForeignKey("RoleId")]
    [InverseProperty("MenuRoles")]
    public virtual Role Role { get; set; }
}
