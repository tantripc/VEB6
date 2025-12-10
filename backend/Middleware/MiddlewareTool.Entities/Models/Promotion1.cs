using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MiddlewareTool.Entities.Models;

[Table("Promotions", Schema = "sto")]
public partial class Promotion1
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [StringLength(10)]
    public string StoreCode { get; set; }

    public bool CasePromotion { get; set; }

    [StringLength(50)]
    public string PNLAllocation { get; set; }

    [StringLength(50)]
    public string TransactionType { get; set; }

    public byte? ActiveFlag { get; set; }
}
