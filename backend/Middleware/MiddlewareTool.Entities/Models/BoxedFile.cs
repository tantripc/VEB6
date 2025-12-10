using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MiddlewareTool.Entities.Models;

[Table("BoxedFiles", Schema = "core")]
public partial class BoxedFile
{
    [Key]
    public Guid Id { get; set; }

    public byte Type { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public byte[] Content { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string Ext { get; set; }

    public long Size { get; set; }

    public Guid? Transfer { get; set; }

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
