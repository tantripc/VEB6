using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MiddlewareTool.Entities.Models;

[Table("DeliveryCodes", Schema = "se")]
public partial class DeliveryCode
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(20)]
    public string Code { get; set; }

    public bool? IsMapping { get; set; }

    public string Description { get; set; }
}
