using System;
using System.Collections.Generic;

namespace MW.Entities;

public partial class Promotion1
{
    public Guid Id { get; set; }

    public string StoreCode { get; set; } = null!;

    public bool CasePromotion { get; set; }

    public string? Pnlallocation { get; set; }

    public string? TransactionType { get; set; }

    public byte? ActiveFlag { get; set; }
}
