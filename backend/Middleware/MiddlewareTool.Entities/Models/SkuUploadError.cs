using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MiddlewareTool.Entities.Models;

[Table("SkuUploadErrors", Schema = "sku")]
public partial class SkuUploadError
{
    [Key]
    public Guid Id { get; set; }

    public Guid UploadId { get; set; }

    [StringLength(100)]
    public string MallCode { get; set; }

    [StringLength(10)]
    public string StoreCode { get; set; }

    public string ExpressLocationGroupName { get; set; }

    [StringLength(13)]
    public string Sku { get; set; }

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

    public string ParcelLocationGroupName { get; set; }

    [ForeignKey("UploadId")]
    [InverseProperty("SkuUploadErrors")]
    public virtual SkuUploadMonitor Upload { get; set; }
}
