using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MiddlewareTool.Entities.Models;

[Table("Business", Schema = "so")]
[Index("Name", "TaxName", "TaxCode", "TaxAddress", "Email", "Phone", "Fax", "CustomerName", Name = "NonClusteredIndex-20240407-173850")]
public partial class Business
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [StringLength(1000)]
    public string Name { get; set; }

    [Required]
    [StringLength(1000)]
    public string TaxName { get; set; }

    [Required]
    [StringLength(255)]
    public string TaxCode { get; set; }

    [Required]
    [StringLength(1000)]
    public string TaxAddress { get; set; }

    [Required]
    [StringLength(1000)]
    public string Email { get; set; }

    [StringLength(1000)]
    public string Phone { get; set; }

    [StringLength(1000)]
    public string Fax { get; set; }

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

    [StringLength(10)]
    public string PayMethodCode { get; set; }

    [StringLength(255)]
    public string CustomerName { get; set; }

    [StringLength(255)]
    public string NoStreet { get; set; }

    [StringLength(255)]
    public string City { get; set; }

    [StringLength(255)]
    public string District { get; set; }

    [StringLength(255)]
    public string Ward { get; set; }

    [InverseProperty("Business")]
    public virtual ICollection<Header1> Header1s { get; set; } = new List<Header1>();
}
