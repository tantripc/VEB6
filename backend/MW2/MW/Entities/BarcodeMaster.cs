using System;
using System.Collections.Generic;

namespace MW.Entities;

public partial class BarcodeMaster
{
    public Guid Id { get; set; }

    public string RecId { get; set; } = null!;

    public string BarSkuNo { get; set; } = null!;

    public string BarNo { get; set; } = null!;

    public string StoreCode { get; set; } = null!;

    public string? CreateBy { get; set; }

    public string? UpdateBy { get; set; }

    public DateTime CreateDate { get; set; }

    public DateTime UpdateDate { get; set; }

    public byte ActiveFlag { get; set; }

    public bool? IsTransferM { get; set; }

    public bool? IsTransferInv { get; set; }
}
