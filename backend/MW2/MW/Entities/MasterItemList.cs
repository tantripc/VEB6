using System;
using System.Collections.Generic;

namespace MW.Entities;

public partial class MasterItemList
{
    public string RecId { get; set; } = null!;

    public string ItemNo { get; set; } = null!;

    public string ItemShortName { get; set; } = null!;

    public string ItemLongName { get; set; } = null!;

    public string ItemShortNameChinese { get; set; } = null!;

    public string ItemLongNameChinese { get; set; } = null!;

    public string ItemBarcode { get; set; } = null!;

    public string ItemSell { get; set; } = null!;

    public string ItemMemberSell { get; set; } = null!;

    public string ItemUom { get; set; } = null!;

    public string ItemDiv { get; set; } = null!;

    public string ItemDept { get; set; } = null!;

    public string ItemCls { get; set; } = null!;

    public string ItemSubcls { get; set; } = null!;

    public string ItemWeigh { get; set; } = null!;

    public string ItemPluFlag { get; set; } = null!;

    public string ItemDate { get; set; } = null!;

    public string ItemVatFlag { get; set; } = null!;

    public string ItemVat { get; set; } = null!;

    public string SeasonId { get; set; } = null!;

    public string SalesTax { get; set; } = null!;

    public string Kads1mFlag { get; set; } = null!;

    public string ValidToUseDate { get; set; } = null!;

    public string CardFlag { get; set; } = null!;

    public string ItemUom2 { get; set; } = null!;

    public string PrintProdFlag { get; set; } = null!;

    public string TaxCode { get; set; } = null!;

    public string TaxSign { get; set; } = null!;

    public string? NutriFacts { get; set; }

    public string? InstructStorage { get; set; }

    public string? Direction { get; set; }

    public string? Warning { get; set; }

    public string? Ingredient { get; set; }

    public string? ExpireTime { get; set; }

    public string? ExpireLabelFormat { get; set; }
}
