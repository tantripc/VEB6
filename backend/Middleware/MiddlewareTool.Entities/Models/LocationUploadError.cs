using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MiddlewareTool.Entities.Models;

[Table("LocationUploadErrors", Schema = "sku")]
public partial class LocationUploadError
{
    [Key]
    public Guid Id { get; set; }

    public Guid UploadId { get; set; }

    [StringLength(100)]
    public string CityCode { get; set; }

    [StringLength(1000)]
    public string CityName { get; set; }

    [StringLength(100)]
    public string DistrictCode { get; set; }

    [StringLength(1000)]
    public string DistrictName { get; set; }

    [StringLength(100)]
    public string WardCode { get; set; }

    [StringLength(1000)]
    public string WardName { get; set; }

    public string LocationGroupName { get; set; }

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

    [ForeignKey("UploadId")]
    [InverseProperty("LocationUploadErrors")]
    public virtual LocationUploadMonitor Upload { get; set; }
}
