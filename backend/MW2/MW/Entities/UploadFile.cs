using System;
using System.Collections.Generic;

namespace MW.Entities;

public partial class UploadFile
{
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public string? FileName { get; set; }

    public byte[]? FileContent { get; set; }

    public int? OrderNumber { get; set; }

    public string Url { get; set; } = null!;

    public string? Description { get; set; }

    public string? CreateBy { get; set; }

    public string? UpdateBy { get; set; }

    public DateTime CreateDate { get; set; }

    public DateTime UpdateDate { get; set; }

    public byte ActiveFlag { get; set; }
}
