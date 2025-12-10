using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MiddlewareTool.Entities.Models;

[Keyless]
[Table("Province")]
public partial class Province
{
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

    [StringLength(250)]
    public string Level { get; set; }

    [StringLength(250)]
    public string EnglishName { get; set; }
}
