using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MiddlewareTool.Entities.Models;

[Table("Mall", Schema = "sto")]
public partial class Mall
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Code { get; set; }

    [Required]
    public string Name { get; set; }

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

    [StringLength(100)]
    public string Phone { get; set; }

    [StringLength(255)]
    public string Email { get; set; }

    public string AddressLine { get; set; }

    public string CityName { get; set; }

    [StringLength(50)]
    public string CityCode { get; set; }

    public string DistrictName { get; set; }

    [StringLength(50)]
    public string DistrictCode { get; set; }

    public string WardName { get; set; }

    [StringLength(50)]
    public string WardCode { get; set; }

    [StringLength(255)]
    public string MerchantId { get; set; }
}
