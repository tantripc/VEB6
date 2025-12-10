using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MiddlewareTool.Entities.Models;

public partial class Mailbox
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [StringLength(250)]
    public string Subject { get; set; }

    [Required]
    public string Body { get; set; }

    [Required]
    public string MailTo { get; set; }

    public string MailCc { get; set; }

    public int NumSend { get; set; }

    public bool? Sent { get; set; }

    public bool IsSeen { get; set; }

    public string URL { get; set; }

    public string Description { get; set; }

    [StringLength(50)]
    public string CreateBy { get; set; }

    [StringLength(50)]
    public string UpdateBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreateDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdateDate { get; set; }

    public byte? ActiveFlag { get; set; }
}
