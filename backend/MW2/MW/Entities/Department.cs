using System;
using System.Collections.Generic;

namespace MW.Entities;

public partial class Department
{
    public Guid Id { get; set; }

    public long Index { get; set; }

    public Guid? ParentId { get; set; }

    public int Level { get; set; }

    public string Name { get; set; } = null!;

    public string? Code { get; set; }

    public int OrderNumber { get; set; }

    public string? Url { get; set; }

    public string? CreateBy { get; set; }

    public string? UpdateBy { get; set; }

    public DateTime CreateDate { get; set; }

    public DateTime UpdateDate { get; set; }

    public byte ActiveFlag { get; set; }
}
