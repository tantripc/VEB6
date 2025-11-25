using System;
using System.Collections.Generic;

namespace MW.Entities;

public partial class SystemSetting
{
    public Guid Id { get; set; }

    public byte Layout { get; set; }

    public byte Type { get; set; }

    public string? Code { get; set; }

    public string Name { get; set; } = null!;

    public string Value { get; set; } = null!;

    public string? Description { get; set; }

    public string Url { get; set; } = null!;

    public string? CreateBy { get; set; }

    public string? UpdateBy { get; set; }

    public DateTime CreateDate { get; set; }

    public DateTime UpdateDate { get; set; }

    public byte ActiveFlag { get; set; }
}
