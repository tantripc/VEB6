using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MiddlewareTool.Entities.Models;

[Table("Location", Schema = "sku")]
public partial class Location
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    [StringLength(10)]
    public string CityCode { get; set; }

    [Required]
    [StringLength(250)]
    public string CityName { get; set; }

    [Required]
    [StringLength(10)]
    public string DistrictCode { get; set; }

    [Required]
    [StringLength(250)]
    public string DistrictName { get; set; }

    [Required]
    [StringLength(10)]
    public string WardCode { get; set; }

    [Required]
    [StringLength(250)]
    public string WardName { get; set; }

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
}
