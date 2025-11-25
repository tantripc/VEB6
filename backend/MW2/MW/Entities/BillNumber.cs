using System;
using System.Collections.Generic;

namespace MW.Entities;

public partial class BillNumber
{
    public Guid Id { get; set; }

    public string StoreCode { get; set; } = null!;

    public int Posnumber { get; set; }

    public int? Current { get; set; }

    public string CurrentDate { get; set; } = null!;

    public string Url { get; set; } = null!;

    public string? CreateBy { get; set; }

    public string? UpdateBy { get; set; }

    public DateTime CreateDate { get; set; }

    public DateTime UpdateDate { get; set; }

    public byte ActiveFlag { get; set; }
}
