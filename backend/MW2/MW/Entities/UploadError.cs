using System;
using System.Collections.Generic;

namespace MW.Entities;

public partial class UploadError
{
    public Guid Id { get; set; }

    public Guid UploadId { get; set; }

    public Guid? ProductId { get; set; }

    public string? Infor { get; set; }

    public int? OrderNumber { get; set; }

    public string Url { get; set; } = null!;

    public string? Description { get; set; }

    public string? CreateBy { get; set; }

    public string? UpdateBy { get; set; }

    public DateTime CreateDate { get; set; }

    public DateTime UpdateDate { get; set; }

    public byte ActiveFlag { get; set; }

    public int? Current { get; set; }

    public virtual UploadMonitor Upload { get; set; } = null!;
}
