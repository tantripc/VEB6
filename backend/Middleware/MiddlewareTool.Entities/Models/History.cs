using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MiddlewareTool.Entities.Models;

[Table("History", Schema = "so")]
[Index("HeaderId", "CreatedDate", "UserId", Name = "IX_so_History")]
public partial class History
{
    [Key]
    public Guid Id { get; set; }

    public Guid HeaderId { get; set; }

    public string ActionCode { get; set; }

    public string Comment { get; set; }

    public string Description { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedDate { get; set; }

    [Required]
    [StringLength(50)]
    public string UserId { get; set; }

    [StringLength(1000)]
    public string Log { get; set; }

    [ForeignKey("HeaderId")]
    [InverseProperty("Histories")]
    public virtual Header1 Header { get; set; }
}
