using System;
using System.Collections.Generic;

namespace MW.Entities;

public partial class LineMaster
{
    public int Id { get; set; }

    public string Description { get; set; } = null!;

    public int? OrderNumber { get; set; }

    public string? Url { get; set; }

    public string? CreateBy { get; set; }

    public string? UpdateBy { get; set; }

    public DateTime CreateDate { get; set; }

    public DateTime UpdateDate { get; set; }

    public byte ActiveFlag { get; set; }

    public virtual ICollection<DivisionMaster> DivisionMasters { get; set; } = new List<DivisionMaster>();
}
