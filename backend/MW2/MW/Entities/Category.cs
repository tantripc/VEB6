using System;
using System.Collections.Generic;

namespace MW.Entities;

public partial class Category
{
    public Guid Id { get; set; }

    public string Code { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Path { get; set; } = null!;

    public bool? IsTransfer { get; set; }

    public Guid? ParentId { get; set; }

    public int? OrderNumber { get; set; }

    public string Url { get; set; } = null!;

    public string? Description { get; set; }

    public string? CreateBy { get; set; }

    public string? UpdateBy { get; set; }

    public DateTime CreateDate { get; set; }

    public DateTime UpdateDate { get; set; }

    public byte ActiveFlag { get; set; }

    public bool? IsNew { get; set; }

    public virtual ICollection<Mapping> Mappings { get; set; } = new List<Mapping>();
}
