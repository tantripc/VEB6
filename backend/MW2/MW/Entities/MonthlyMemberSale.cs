using System;
using System.Collections.Generic;

namespace MW.Entities;

public partial class MonthlyMemberSale
{
    public Guid Id { get; set; }

    public string Membercode { get; set; } = null!;

    public string Companycode { get; set; } = null!;

    public string Transactionmonth { get; set; } = null!;

    public string Transactionyear { get; set; } = null!;

    public double Monthlysalesamount { get; set; }

    public string Currency { get; set; } = null!;

    public string Memberlevel { get; set; } = null!;

    public double PercentageRedemption { get; set; }

    public int Point { get; set; }

    public string? Url { get; set; }

    public string? CreateBy { get; set; }

    public string? UpdateBy { get; set; }

    public DateTime CreateDate { get; set; }

    public DateTime UpdateDate { get; set; }

    public byte ActiveFlag { get; set; }

    public string YearMonth { get; set; } = null!;
}
