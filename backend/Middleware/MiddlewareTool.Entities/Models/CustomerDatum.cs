using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MiddlewareTool.Entities.Models;

[Table("CustomerData", Schema = "se")]
[Index("CustomerID", "FoxtrotUserID", "ActiveFlag", Name = "IX_seCustomerData")]
public partial class CustomerDatum
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [StringLength(255)]
    public string Email { get; set; }

    [Required]
    [StringLength(255)]
    public string FirstName { get; set; }

    [Required]
    [StringLength(255)]
    public string LastName { get; set; }

    [Required]
    [StringLength(255)]
    public string PhoneNumber { get; set; }

    [Required]
    [StringLength(255)]
    public string Ward { get; set; }

    [Required]
    [StringLength(255)]
    public string District { get; set; }

    [Required]
    [StringLength(255)]
    public string City { get; set; }

    [Required]
    [StringLength(255)]
    public string CustomerID { get; set; }

    [StringLength(32)]
    public string FoxtrotUserID { get; set; }

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

    [StringLength(1)]
    public string CustomerType { get; set; }
}
