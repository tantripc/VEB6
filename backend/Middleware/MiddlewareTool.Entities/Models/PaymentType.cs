using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MiddlewareTool.Entities.Models;

[Table("PaymentTypes", Schema = "se")]
public partial class PaymentType
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(10)]
    public string Type { get; set; }

    public string Description { get; set; }

    public bool? IsMethod { get; set; }

    public int? Scope { get; set; }
}
