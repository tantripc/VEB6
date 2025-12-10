using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MiddlewareTool.Entities.Models;

[Table("Mapping", Schema = "cat")]
public partial class Mapping
{
    [Key]
    public Guid Id { get; set; }

    public Guid CategoryId { get; set; }

    [Required]
    [StringLength(50)]
    public string CategoryMasterId { get; set; }

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

    [ForeignKey("CategoryId")]
    [InverseProperty("Mappings")]
    public virtual Category Category { get; set; }
}
