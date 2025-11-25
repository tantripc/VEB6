using System;
using System.Collections.Generic;

namespace MW.Entities;

public partial class CategoryMaster
{
    public string Id { get; set; } = null!;

    public int DepartmentId { get; set; }

    public string Description { get; set; } = null!;

    public bool? AutoPa { get; set; }

    public string? PosFlag { get; set; }

    public bool? PwpExclusion { get; set; }

    public int? AgeStockRetenPeriod { get; set; }

    public bool? MbrDiscFlag { get; set; }

    public int? MbrDiscPerc { get; set; }

    public int? MommyDiscPerc { get; set; }

    public int? OrderNumber { get; set; }

    public string? Url { get; set; }

    public string? CreateBy { get; set; }

    public string? UpdateBy { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public byte? ActiveFlag { get; set; }

    public virtual DepartmentMaster Department { get; set; } = null!;
}
