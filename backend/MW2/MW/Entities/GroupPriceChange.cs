using System;
using System.Collections.Generic;

namespace MW.Entities;

public partial class GroupPriceChange
{
    public Guid Id { get; set; }

    public string RecId { get; set; } = null!;

    public string PrcNo { get; set; } = null!;

    public string PrcType { get; set; } = null!;

    public string PrcStartDate { get; set; } = null!;

    public string PrcEndDate { get; set; } = null!;

    public string PrcStartTime { get; set; } = null!;

    public string PrcEndTime { get; set; } = null!;

    public string Subclass { get; set; } = null!;

    public string PrcDiscRate { get; set; } = null!;

    public string ExcludeSsnId { get; set; } = null!;

    public string? EndOfRecord { get; set; }

    public string StoreCode { get; set; } = null!;

    public string? Url { get; set; }

    public string? CreateBy { get; set; }

    public string? UpdateBy { get; set; }

    public DateTime CreateDate { get; set; }

    public DateTime UpdateDate { get; set; }

    public byte ActiveFlag { get; set; }
}
