using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MiddlewareTool.Entities.Models;

[Table("Menu")]
public partial class Menu
{
    [Key]
    public Guid Id { get; set; }

    public Guid? ParentId { get; set; }

    public byte MenuStatus { get; set; }

    [StringLength(50)]
    public string Controller { get; set; }

    [StringLength(50)]
    public string Action { get; set; }

    [StringLength(500)]
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

    [StringLength(250)]
    public string Method { get; set; }

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
