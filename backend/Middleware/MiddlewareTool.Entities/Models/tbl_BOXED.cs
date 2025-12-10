using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MiddlewareTool.Entities.Models;

[Keyless]
[Table("tbl_BOXED")]
public partial class tbl_BOXED
{
    [StringLength(50)]
    public string SKU { get; set; }

    [StringLength(10)]
    public string StoreCode { get; set; }

    public bool? Status { get; set; }
}
