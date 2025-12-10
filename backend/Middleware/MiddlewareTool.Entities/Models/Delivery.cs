using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MiddlewareTool.Entities.Models;

[Table("Delivery", Schema = "se")]
[Index("HeaderId", "ActiveFlag", Name = "IX_Delivery")]
[Index("ActiveFlag", "HeaderId", "DeliveryCode", "TrackingNumber", Name = "IX_se_Delivery")]
public partial class Delivery
{
    [Key]
    public Guid Id { get; set; }

    public Guid HeaderId { get; set; }

    [Required]
    [StringLength(20)]
    public string SubOrderNumber { get; set; }

    [Required]
    [StringLength(20)]
    public string DeliveryCode { get; set; }

    [StringLength(256)]
    public string TrackingNumber { get; set; }

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

    [ForeignKey("HeaderId")]
    [InverseProperty("Deliveries")]
    public virtual Header Header { get; set; }
}
