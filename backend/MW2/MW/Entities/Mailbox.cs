using System;
using System.Collections.Generic;

namespace MW.Entities;

public partial class Mailbox
{
    public Guid Id { get; set; }

    public string Subject { get; set; } = null!;

    public string Body { get; set; } = null!;

    public string MailTo { get; set; } = null!;

    public string? MailCc { get; set; }

    public int NumSend { get; set; }

    public bool? Sent { get; set; }

    public bool IsSeen { get; set; }

    public string? Url { get; set; }

    public string? Description { get; set; }

    public string? CreateBy { get; set; }

    public string? UpdateBy { get; set; }

    public DateTime CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public byte? ActiveFlag { get; set; }
}
