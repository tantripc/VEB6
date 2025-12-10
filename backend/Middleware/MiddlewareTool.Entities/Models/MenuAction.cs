using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MiddlewareTool.Entities.Models;

[Table("MenuAction")]
public partial class MenuAction
{
    [Key]
    public Guid Id { get; set; }

    public byte Type { get; set; }

    public Guid MenuId { get; set; }

    [StringLength(50)]
    public string MenuController { get; set; }

    [StringLength(50)]
    public string MenuActionName { get; set; }

    [StringLength(50)]
    public string Controller { get; set; }

    [StringLength(50)]
    public string Action { get; set; }

    [StringLength(255)]
    public string ResourceID { get; set; }

    [StringLength(250)]
    public string NameVI { get; set; }

    [StringLength(250)]
    public string NameEN { get; set; }

    [StringLength(50)]
    public string Icon { get; set; }

    public int OrderNumber { get; set; }

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
}
