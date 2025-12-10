using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MiddlewareTool.Entities.Models;

[Keyless]
public partial class Products_RealDatum
{
    [StringLength(13)]
    public string SKU { get; set; }

    [StringLength(10)]
    public string StoreCode { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? StartPublishDate { get; set; }
}
