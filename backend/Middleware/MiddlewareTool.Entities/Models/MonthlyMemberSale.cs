using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MiddlewareTool.Entities.Models;

[Table("MonthlyMemberSales", Schema = "se")]
[Index("ActiveFlag", "YearMonth", "Membercode", "UpdateDate", Name = "IX_MonthlyMemberSales", IsDescending = new[] { false, true, false, true })]
public partial class MonthlyMemberSale
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [StringLength(60)]
    public string Membercode { get; set; }

    [Required]
    [StringLength(10)]
    public string Companycode { get; set; }

    [Required]
    [StringLength(2)]
    public string Transactionmonth { get; set; }

    [Required]
    [StringLength(4)]
    public string Transactionyear { get; set; }

    public double Monthlysalesamount { get; set; }

    [Required]
    [StringLength(10)]
    public string Currency { get; set; }

    [Required]
    [StringLength(27)]
    public string Memberlevel { get; set; }

    public double PercentageRedemption { get; set; }

    public int Point { get; set; }

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

    [Required]
    [StringLength(6)]
    public string YearMonth { get; set; }
}
