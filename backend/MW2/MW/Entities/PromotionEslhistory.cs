using System;
using System.Collections.Generic;

namespace MW.Entities;

public partial class PromotionEslhistory
{
    public Guid Id { get; set; }

    public string Sku { get; set; } = null!;

    public string StoreCode { get; set; } = null!;

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public TimeOnly StartTime { get; set; }

    public TimeOnly EndTime { get; set; }

    public string CreateBy { get; set; } = null!;

    public DateTime CreateDate { get; set; }

    public string UpdateBy { get; set; } = null!;

    public DateTime UpdateDate { get; set; }

    public int Action { get; set; }

    public string? TransData { get; set; }

    public string? Source { get; set; }

    public DateTime? StartDateTime { get; set; }

    public DateTime? EndDateTime { get; set; }

    public byte Edlpflag { get; set; }

    public bool IsTransferEsl { get; set; }

    public string? Url { get; set; }
}
