using System;
using System.Collections.Generic;

namespace MW.Entities;

public partial class Mapping
{
    public Guid Id { get; set; }

    public Guid CategoryId { get; set; }

    public string CategoryMasterId { get; set; } = null!;

    public int? OrderNumber { get; set; }

    public string Url { get; set; } = null!;

    public string? Description { get; set; }

    public string? CreateBy { get; set; }

    public string? UpdateBy { get; set; }

    public DateTime CreateDate { get; set; }

    public DateTime UpdateDate { get; set; }

    public byte ActiveFlag { get; set; }

    public virtual Category Category { get; set; } = null!;
}
