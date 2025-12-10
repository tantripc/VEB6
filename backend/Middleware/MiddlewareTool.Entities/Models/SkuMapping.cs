using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MiddlewareTool.Entities.Models;

[Table("SkuMapping", Schema = "core")]
public partial class SkuMapping
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [StringLength(100)]
    public string MallCode { get; set; }

    [Required]
    public string MallName { get; set; }

    public string ExpressLocationGroupName { get; set; }

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

    public string ParcelLocationGroupName { get; set; }
}
