using System;
using System.Collections.Generic;

namespace MW.Entities;

public partial class PriceChange
{
    public Guid Id { get; set; }

    public string RecId { get; set; } = null!;

    public string ItemNo { get; set; } = null!;

    public string PrcNo { get; set; } = null!;

    public string PrcType { get; set; } = null!;

    public string PrcStartDate { get; set; } = null!;

    public string PrcEndDate { get; set; } = null!;

    public string PrcStartTime { get; set; } = null!;

    public string PrcEndTime { get; set; } = null!;

    public string PrcDiscRate { get; set; } = null!;

    public string PrcDiscAmt { get; set; } = null!;

    public string PrcSell { get; set; } = null!;

    public string StoreCode { get; set; } = null!;

    public string? Url { get; set; }

    public string? CreateBy { get; set; }

    public string? UpdateBy { get; set; }

    public DateTime CreateDate { get; set; }

    public DateTime UpdateDate { get; set; }

    public byte ActiveFlag { get; set; }

    public string StoreCodeSku { get; set; } = null!;

    public string StoreCodeSkuPrcNo { get; set; } = null!;

    public bool? IsTransferEsl { get; set; }

    public DateTime? StartDateTime { get; set; }

    public DateTime? EndDateTime { get; set; }

    public bool? IsT4vv { get; set; }

    public byte? T4vvflag { get; set; }
}
