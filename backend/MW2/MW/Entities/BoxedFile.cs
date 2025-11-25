using System;
using System.Collections.Generic;

namespace MW.Entities;

public partial class BoxedFile
{
    public Guid Id { get; set; }

    public byte Type { get; set; }

    public string Name { get; set; } = null!;

    public byte[] Content { get; set; } = null!;

    public string? Ext { get; set; }

    public long Size { get; set; }

    public Guid? Transfer { get; set; }

    public string? Url { get; set; }

    public string? CreateBy { get; set; }

    public string? UpdateBy { get; set; }

    public DateTime CreateDate { get; set; }

    public DateTime UpdateDate { get; set; }

    public byte ActiveFlag { get; set; }
}
