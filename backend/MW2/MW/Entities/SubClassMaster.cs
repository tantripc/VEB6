using System;
using System.Collections.Generic;

namespace MW.Entities;

public partial class SubClassMaster
{
    public Guid Id { get; set; }

    public string RecId { get; set; } = null!;

    public string Cls { get; set; } = null!;

    public string SubCls { get; set; } = null!;

    public string BclsName { get; set; } = null!;

    public string AcsFlag { get; set; } = null!;

    public string Perishable { get; set; } = null!;

    public string MbrDisc { get; set; } = null!;

    public string MommyDisc { get; set; } = null!;

    public string StoreCode { get; set; } = null!;

    public string? CreateBy { get; set; }

    public string? UpdateBy { get; set; }

    public DateTime CreateDate { get; set; }

    public DateTime UpdateDate { get; set; }

    public byte ActiveFlag { get; set; }
}
