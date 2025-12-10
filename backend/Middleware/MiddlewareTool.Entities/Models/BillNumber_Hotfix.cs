using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MiddlewareTool.Entities.Models;

[Keyless]
[Table("BillNumber_Hotfix")]
public partial class BillNumber_Hotfix
{
    [StringLength(8)]
    public string SalesDate { get; set; }

    [StringLength(100)]
    public string StoreCode { get; set; }

    [StringLength(100)]
    public string OrderNumber { get; set; }

    [StringLength(100)]
    public string BillNumber { get; set; }
}
