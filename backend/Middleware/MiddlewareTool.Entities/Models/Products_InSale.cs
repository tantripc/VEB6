using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MiddlewareTool.Entities.Models;

[Keyless]
[Table("Products_InSale")]
public partial class Products_InSale
{
    [StringLength(13)]
    public string SKU { get; set; }
}
