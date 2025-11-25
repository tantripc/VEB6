using System;
using System.Collections.Generic;

namespace MW.Entities;

public partial class MpriceChange
{
    public Guid Id { get; set; }

    public string PrcNo { get; set; } = null!;

    public string PrcType { get; set; } = null!;

    public string ItemNo { get; set; } = null!;

    public string QuantityDiscount { get; set; } = null!;

    public string PrcStartDate { get; set; } = null!;

    public string PrcEndDate { get; set; } = null!;

    public string PrcStartTime { get; set; } = null!;

    public string PrcEndTime { get; set; } = null!;

    public string Quantity { get; set; } = null!;

    public string PrcSell { get; set; } = null!;

    public string PrcDesc { get; set; } = null!;

    public string RecId { get; set; } = null!;

    public string OtherFocItemFlag { get; set; } = null!;

    public string? OtherFocSkuNumber { get; set; }

    public string? MinimumQuantity { get; set; }

    public string FocQuantity { get; set; } = null!;

    public string FocForMemberOnly { get; set; } = null!;

    public string MaximumFocQtyForMember { get; set; } = null!;

    public string StoreCode { get; set; } = null!;

    public string? CreateBy { get; set; }

    public string? UpdateBy { get; set; }

    public DateTime CreateDate { get; set; }

    public DateTime UpdateDate { get; set; }

    public byte ActiveFlag { get; set; }
}
