using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MiddlewareTool.Entities.Models;

[Table("RecordSaleFiles", Schema = "se")]
public partial class RecordSaleFile
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public string HeaderIds { get; set; }

    [Required]
    [StringLength(100)]
    public string StoreCode { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public byte[] Content { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string Ext { get; set; }

    public long Size { get; set; }

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
