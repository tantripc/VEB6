using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MiddlewareTool.Entities.Models;

[Table("ProductHistoryAction")]
[Index("Value", Name = "IX_ProductHistoryAction")]
public partial class ProductHistoryAction
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [StringLength(250)]
    public string Action { get; set; }

    public byte Value { get; set; }

    [StringLength(30)]
    public string Name { get; set; }
}
