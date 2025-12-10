using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MiddlewareTool.Entities.Models;

[Table("RefundReason", Schema = "so")]
public partial class RefundReason
{
    [Key]
    [StringLength(10)]
    public string ReasonCode { get; set; }

    [Required]
    [StringLength(155)]
    public string ReasonName { get; set; }
}
