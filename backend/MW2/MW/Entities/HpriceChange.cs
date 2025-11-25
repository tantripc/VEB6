using System;
using System.Collections.Generic;

namespace MW.Entities;

public partial class HpriceChange
{
    public Guid Id { get; set; }

    public string RecId { get; set; } = null!;

    public string PrcNo { get; set; } = null!;

    public string PrcDesc { get; set; } = null!;

    public string PrcStartDate { get; set; } = null!;

    public string PrcEndDate { get; set; } = null!;

    public string PrcStartTime { get; set; } = null!;

    public string PrcEndTime { get; set; } = null!;

    public string ApplicableTo { get; set; } = null!;

    public string RewardEventDay { get; set; } = null!;

    public string MinEntitlementAmount { get; set; } = null!;

    public string Gst { get; set; } = null!;

    public string MaxEntitlementPwpQuantity { get; set; } = null!;

    public string MaxReceiptPwpQuantity { get; set; } = null!;

    public string InclusionDivision { get; set; } = null!;

    public string InclusionDepartment { get; set; } = null!;

    public string InclusionCategory { get; set; } = null!;

    public string InclusionSku { get; set; } = null!;

    public string ExclusionDepartment { get; set; } = null!;

    public string ExclusionCategory { get; set; } = null!;

    public string ExclusionSku { get; set; } = null!;

    public string ShortSkuCode { get; set; } = null!;

    public string NewPwpSellingPrice { get; set; } = null!;

    public string AutoScanFoc { get; set; } = null!;

    public string FocShortSku { get; set; } = null!;

    public string FocQty { get; set; } = null!;

    public string FocItemForMemberOnly { get; set; } = null!;

    public string MaxFocQtyForMember { get; set; } = null!;

    public string StoreCode { get; set; } = null!;

    public string? CreateBy { get; set; }

    public string? UpdateBy { get; set; }

    public DateTime CreateDate { get; set; }

    public DateTime UpdateDate { get; set; }

    public byte ActiveFlag { get; set; }
}
