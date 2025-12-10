using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MiddlewareTool.Entities.Models;

[Table("ReceiptNumbers", Schema = "re")]
public partial class ReceiptNumber
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [StringLength(10)]
    public string StoreCode { get; set; }

    public int POSNumber { get; set; }

    public int? Current { get; set; }

    [Required]
    [StringLength(8)]
    public string CurrentDate { get; set; }

    [Required]
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
}
