using System;
using System.Collections.Generic;

namespace MW.Entities;

public partial class GroupMaster
{
    public int Id { get; set; }

    public int DivisionId { get; set; }

    public string Description { get; set; } = null!;

    public int? OrderNumber { get; set; }

    public string? Url { get; set; }

    public string? CreateBy { get; set; }

    public string? UpdateBy { get; set; }

    public DateTime CreateDate { get; set; }

    public DateTime UpdateDate { get; set; }

    public byte ActiveFlag { get; set; }

    public virtual ICollection<DepartmentMaster> DepartmentMasters { get; set; } = new List<DepartmentMaster>();

    public virtual DivisionMaster Division { get; set; } = null!;
}
