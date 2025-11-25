using System;
using System.Collections.Generic;

namespace MW.Entities;

public partial class SkuUploadMonitor
{
    public Guid Id { get; set; }

    public string FileName { get; set; } = null!;

    public byte[] FileContent { get; set; } = null!;

    public string FileExt { get; set; } = null!;

    public int TotalRow { get; set; }

    public string Curent { get; set; } = null!;

    public int? OrderNumber { get; set; }

    public string Url { get; set; } = null!;

    public string? Description { get; set; }

    public string? CreateBy { get; set; }

    public string? UpdateBy { get; set; }

    public DateTime CreateDate { get; set; }

    public DateTime UpdateDate { get; set; }

    public byte ActiveFlag { get; set; }

    public virtual ICollection<SkuUploadError> SkuUploadErrors { get; set; } = new List<SkuUploadError>();
}
