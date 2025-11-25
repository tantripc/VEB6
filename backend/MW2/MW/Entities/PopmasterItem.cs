using System;
using System.Collections.Generic;

namespace MW.Entities;

public partial class PopmasterItem
{
    public Guid Id { get; set; }

    public string Sku { get; set; } = null!;

    public string DateCreate { get; set; } = null!;

    public string CreatedBy { get; set; } = null!;

    public string ItemDescEng { get; set; } = null!;

    public string ItemDescVnm { get; set; } = null!;

    public string PluDescEng { get; set; } = null!;

    public string PluDescVnm { get; set; } = null!;

    public string FocDescEng { get; set; } = null!;

    public string FocDescVnm { get; set; } = null!;

    public string Ticket1DescEng { get; set; } = null!;

    public string Ticket1DescVnm { get; set; } = null!;

    public string Ticket2DescEng { get; set; } = null!;

    public string Ticket2DescVnm { get; set; } = null!;

    public string Pop1DescEng { get; set; } = null!;

    public string Pop1DescVnm { get; set; } = null!;

    public string Pop2DescEng { get; set; } = null!;

    public string Pop2DescVnm { get; set; } = null!;

    public string DeptId { get; set; } = null!;

    public string SupplierId { get; set; } = null!;

    public string CategoryId { get; set; } = null!;

    public string SupplierContract { get; set; } = null!;

    public string Brand { get; set; } = null!;

    public string DivisionId { get; set; } = null!;

    public string LineId { get; set; } = null!;

    public string GroupId { get; set; } = null!;

    public string Style { get; set; } = null!;

    public string ColourSizeGrid { get; set; } = null!;

    public string Colour { get; set; } = null!;

    public string SizeId { get; set; } = null!;

    public string PurchaseMethod { get; set; } = null!;

    public string ItemSource { get; set; } = null!;

    public string Returnable { get; set; } = null!;

    public string Kads1mFlag { get; set; } = null!;

    public string ItemType { get; set; } = null!;

    public string IngredientType { get; set; } = null!;

    public string MerchandisePlan { get; set; } = null!;

    public string SeasonId { get; set; } = null!;

    public string PackItem { get; set; } = null!;

    public string PerishItem { get; set; } = null!;

    public string NonInventory { get; set; } = null!;

    public string NonInventoryCode { get; set; } = null!;

    public string NonPlu { get; set; } = null!;

    public string MommyItem { get; set; } = null!;

    public string FoodItem { get; set; } = null!;

    public string MemberDiscItem { get; set; } = null!;

    public string SuperSaverItem { get; set; } = null!;

    public string AddAutoDiscItem { get; set; } = null!;

    public string AutoReplenishItem { get; set; } = null!;

    public string DaisoDocSku { get; set; } = null!;

    public string Actived { get; set; } = null!;

    public string DateActived { get; set; } = null!;

    public string HoldOrder { get; set; } = null!;

    public string HoldOrderStartDate { get; set; } = null!;

    public string HoldOrderEndDate { get; set; } = null!;

    public string Discontinue { get; set; } = null!;

    public string DateDiscontinue { get; set; } = null!;

    public string Deleted { get; set; } = null!;

    public string DateDeleted { get; set; } = null!;

    public string SubCategory { get; set; } = null!;

    public string RetailVatCode { get; set; } = null!;

    public string RetailVatRate { get; set; } = null!;

    public string SugUnitRetailWvat { get; set; } = null!;

    public string RetailUom { get; set; } = null!;

    public string SugUnitRetailWovat { get; set; } = null!;

    public string SalesTaxRate { get; set; } = null!;

    public string CostVatRate { get; set; } = null!;

    public string StdCostUom { get; set; } = null!;

    public string OrderUom { get; set; } = null!;

    public string ParentSku { get; set; } = null!;

    public string TicketSku { get; set; } = null!;

    public string TicketType { get; set; } = null!;

    public string AutoOrderStartDate { get; set; } = null!;

    public string AutoOrderEndDate { get; set; } = null!;

    public string HsCode { get; set; } = null!;

    public string MsdsCode { get; set; } = null!;

    public string NetWeightKg { get; set; } = null!;

    public string GrossWeightKg { get; set; } = null!;

    public string CubicMeterM3 { get; set; } = null!;

    public string NeirePerc { get; set; } = null!;

    public string ExtraField1 { get; set; } = null!;

    public string ExtraField2 { get; set; } = null!;

    public string ExtraField3 { get; set; } = null!;

    public string ExtraField4 { get; set; } = null!;

    public string ModifiedDate { get; set; } = null!;

    public string Pop3DescVnm { get; set; } = null!;

    public string SellingPoint1 { get; set; } = null!;

    public string SellingPoint2 { get; set; } = null!;

    public string SellingPoint3 { get; set; } = null!;

    public string SellingPoint4 { get; set; } = null!;

    public string SellingPoint5 { get; set; } = null!;

    public string Url { get; set; } = null!;

    public string? CreateBy { get; set; }

    public string? UpdateBy { get; set; }

    public DateTime CreateDate { get; set; }

    public DateTime UpdateDate { get; set; }

    public byte ActiveFlag { get; set; }

    public bool IsTransferEsl { get; set; }
}
