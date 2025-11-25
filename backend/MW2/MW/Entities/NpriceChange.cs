using System;
using System.Collections.Generic;

namespace MW.Entities;

public partial class NpriceChange
{
    public Guid Id { get; set; }

    public string PrcNo { get; set; } = null!;

    public string PrcType { get; set; } = null!;

    public string PrcStartDate { get; set; } = null!;

    public string PrcEndDate { get; set; } = null!;

    public string PrcStartTime { get; set; } = null!;

    public string PrcEndTime { get; set; } = null!;

    public string PluCount { get; set; } = null!;

    public string MmPromoQty { get; set; } = null!;

    public string MmPromoQty1 { get; set; } = null!;

    public string MmPromoQty2 { get; set; } = null!;

    public string MmPromoQty3 { get; set; } = null!;

    public string MmPromoQty4 { get; set; } = null!;

    public string MmPromoQty5 { get; set; } = null!;

    public string MmPromoQty6 { get; set; } = null!;

    public string ShortSku1 { get; set; } = null!;

    public string ShortSku2 { get; set; } = null!;

    public string ShortSku3 { get; set; } = null!;

    public string ShortSku4 { get; set; } = null!;

    public string ShortSku5 { get; set; } = null!;

    public string ShortSku6 { get; set; } = null!;

    public string MmPromoPrice1 { get; set; } = null!;

    public string MmPromoPrice2 { get; set; } = null!;

    public string MmPromoPrice3 { get; set; } = null!;

    public string MmPromoPrice4 { get; set; } = null!;

    public string MmPromoPrice5 { get; set; } = null!;

    public string MmPromoPrice6 { get; set; } = null!;

    public string MmTtPromoPrice { get; set; } = null!;

    public string PromotionDesc { get; set; } = null!;

    public string RecId { get; set; } = null!;

    public string StoreCode { get; set; } = null!;

    public string? CreateBy { get; set; }

    public string? UpdateBy { get; set; }

    public DateTime CreateDate { get; set; }

    public DateTime UpdateDate { get; set; }

    public byte ActiveFlag { get; set; }
}
