using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MiddlewareTool.Entities.Models;

[Table("UploadErrors", Schema = "prod")]
public partial class UploadError
{
    [Key]
    public Guid Id { get; set; }

    public Guid UploadId { get; set; }

    public Guid? ProductId { get; set; }

    public string Infor { get; set; }

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

    public int? Current { get; set; }

    [ForeignKey("UploadId")]
    [InverseProperty("UploadErrors")]
    public virtual UploadMonitor Upload { get; set; }
}
