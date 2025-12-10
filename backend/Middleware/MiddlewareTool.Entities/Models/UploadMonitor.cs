using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MiddlewareTool.Entities.Models;

[Table("UploadMonitors", Schema = "prod")]
public partial class UploadMonitor
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public string FileName { get; set; }

    [Required]
    public byte[] FileContent { get; set; }

    [Required]
    [StringLength(10)]
    [Unicode(false)]
    public string FileExt { get; set; }

    public int TotalRow { get; set; }

    [Required]
    [StringLength(10)]
    [Unicode(false)]
    public string Curent { get; set; }

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

    [InverseProperty("Upload")]
    public virtual ICollection<UploadError> UploadErrors { get; set; } = new List<UploadError>();
}
