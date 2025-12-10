using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MiddlewareTool.Entities.Models;

[Table("POPMasterItems", Schema = "core")]
[Index("ACTIVED", Name = "IX_ACTIVED")]
[Index("ActiveFlag", Name = "IX_ActiveFlag")]
[Index("DELETED", Name = "IX_DELETED")]
[Index("IsTransferESL", Name = "IX_IsTransferESL")]
[Index("SKU", Name = "IX_SKU")]
[Index("ActiveFlag", "ACTIVED", "DELETED", "Id", "SKU", "IsTransferESL", Name = "IX_core_POPMasterItems")]
public partial class POPMasterItem
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [StringLength(13)]
    public string SKU { get; set; }

    [Required]
    [StringLength(8)]
    public string DATE_CREATE { get; set; }

    [Required]
    [StringLength(8)]
    public string CREATED_BY { get; set; }

    [Required]
    [StringLength(4000)]
    public string ITEM_DESC_ENG { get; set; }

    [Required]
    [StringLength(4000)]
    public string ITEM_DESC_VNM { get; set; }

    [Required]
    [StringLength(1200)]
    public string PLU_DESC_ENG { get; set; }

    [Required]
    [StringLength(1200)]
    public string PLU_DESC_VNM { get; set; }

    [Required]
    [StringLength(4000)]
    public string FOC_DESC_ENG { get; set; }

    [Required]
    [StringLength(4000)]
    public string FOC_DESC_VNM { get; set; }

    [Required]
    [StringLength(4000)]
    public string TICKET1_DESC_ENG { get; set; }

    [Required]
    [StringLength(4000)]
    public string TICKET1_DESC_VNM { get; set; }

    [Required]
    [StringLength(4000)]
    public string TICKET2_DESC_ENG { get; set; }

    [Required]
    [StringLength(4000)]
    public string TICKET2_DESC_VNM { get; set; }

    [Required]
    [StringLength(4000)]
    public string POP1_DESC_ENG { get; set; }

    [Required]
    [StringLength(4000)]
    public string POP1_DESC_VNM { get; set; }

    [Required]
    [StringLength(4000)]
    public string POP2_DESC_ENG { get; set; }

    [Required]
    [StringLength(4000)]
    public string POP2_DESC_VNM { get; set; }

    [Required]
    [StringLength(6)]
    public string DEPT_ID { get; set; }

    [Required]
    [StringLength(10)]
    public string SUPPLIER_ID { get; set; }

    [Required]
    [StringLength(9)]
    public string CATEGORY_ID { get; set; }

    [Required]
    [StringLength(10)]
    public string SUPPLIER_CONTRACT { get; set; }

    [Required]
    [StringLength(10)]
    public string BRAND { get; set; }

    [Required]
    [StringLength(3)]
    public string DIVISION_ID { get; set; }

    [Required]
    [StringLength(3)]
    public string LINE_ID { get; set; }

    [Required]
    [StringLength(3)]
    public string GROUP_ID { get; set; }

    [Required]
    [StringLength(100)]
    public string STYLE { get; set; }

    [Required]
    [StringLength(1)]
    public string COLOUR_SIZE_GRID { get; set; }

    [Required]
    [StringLength(3)]
    public string COLOUR { get; set; }

    [Required]
    [StringLength(5)]
    public string SIZE_ID { get; set; }

    [Required]
    [StringLength(1)]
    public string PURCHASE_METHOD { get; set; }

    [Required]
    [StringLength(1)]
    public string ITEM_SOURCE { get; set; }

    [Required]
    [StringLength(1)]
    public string RETURNABLE { get; set; }

    [Required]
    [StringLength(1)]
    public string KADS1M_FLAG { get; set; }

    [Required]
    [StringLength(1)]
    public string ITEM_TYPE { get; set; }

    [Required]
    [StringLength(1)]
    public string INGREDIENT_TYPE { get; set; }

    [Required]
    [StringLength(1)]
    public string MERCHANDISE_PLAN { get; set; }

    [Required]
    [StringLength(6)]
    public string SEASON_ID { get; set; }

    [Required]
    [StringLength(1)]
    public string PACK_ITEM { get; set; }

    [Required]
    [StringLength(1)]
    public string PERISH_ITEM { get; set; }

    [Required]
    [StringLength(1)]
    public string NON_INVENTORY { get; set; }

    [Required]
    [StringLength(3)]
    public string NON_INVENTORY_CODE { get; set; }

    [Required]
    [StringLength(1)]
    public string NON_PLU { get; set; }

    [Required]
    [StringLength(1)]
    public string MOMMY_ITEM { get; set; }

    [Required]
    [StringLength(1)]
    public string FOOD_ITEM { get; set; }

    [Required]
    [StringLength(1)]
    public string MEMBER_DISC_ITEM { get; set; }

    [Required]
    [StringLength(1)]
    public string SUPER_SAVER_ITEM { get; set; }

    [Required]
    [StringLength(1)]
    public string ADD_AUTO_DISC_ITEM { get; set; }

    [Required]
    [StringLength(1)]
    public string AUTO_REPLENISH_ITEM { get; set; }

    [Required]
    [StringLength(1)]
    public string DAISO_DOC_SKU { get; set; }

    [Required]
    [StringLength(1)]
    public string ACTIVED { get; set; }

    [Required]
    [StringLength(8)]
    public string DATE_ACTIVED { get; set; }

    [Required]
    [StringLength(1)]
    public string HOLD_ORDER { get; set; }

    [Required]
    [StringLength(8)]
    public string HOLD_ORDER_START_DATE { get; set; }

    [Required]
    [StringLength(8)]
    public string HOLD_ORDER_END_DATE { get; set; }

    [Required]
    [StringLength(1)]
    public string DISCONTINUE { get; set; }

    [Required]
    [StringLength(8)]
    public string DATE_DISCONTINUE { get; set; }

    [Required]
    [StringLength(1)]
    public string DELETED { get; set; }

    [Required]
    [StringLength(8)]
    public string DATE_DELETED { get; set; }

    [Required]
    [StringLength(2)]
    public string SUB_CATEGORY { get; set; }

    [Required]
    [StringLength(2)]
    public string RETAIL_VAT_CODE { get; set; }

    [Required]
    [StringLength(22)]
    public string RETAIL_VAT_RATE { get; set; }

    [Required]
    [StringLength(22)]
    public string SUG_UNIT_RETAIL_WVAT { get; set; }

    [Required]
    [StringLength(5)]
    public string RETAIL_UOM { get; set; }

    [Required]
    [StringLength(22)]
    public string SUG_UNIT_RETAIL_WOVAT { get; set; }

    [Required]
    [StringLength(2)]
    public string SALES_TAX_RATE { get; set; }

    [Required]
    [StringLength(2)]
    public string COST_VAT_RATE { get; set; }

    [Required]
    [StringLength(100)]
    public string STD_COST_UOM { get; set; }

    [Required]
    [StringLength(5)]
    public string ORDER_UOM { get; set; }

    [Required]
    [StringLength(13)]
    public string PARENT_SKU { get; set; }

    [Required]
    [StringLength(13)]
    public string TICKET_SKU { get; set; }

    [Required]
    [StringLength(3)]
    public string TICKET_TYPE { get; set; }

    [Required]
    [StringLength(8)]
    public string AUTO_ORDER_START_DATE { get; set; }

    [Required]
    [StringLength(8)]
    public string AUTO_ORDER_END_DATE { get; set; }

    [Required]
    [StringLength(50)]
    public string HS_CODE { get; set; }

    [Required]
    [StringLength(50)]
    public string MSDS_CODE { get; set; }

    [Required]
    [StringLength(22)]
    public string NET_WEIGHT_KG { get; set; }

    [Required]
    [StringLength(22)]
    public string GROSS_WEIGHT_KG { get; set; }

    [Required]
    [StringLength(22)]
    public string CUBIC_METER_M3 { get; set; }

    [Required]
    [StringLength(22)]
    public string NEIRE_PERC { get; set; }

    [Required]
    [StringLength(22)]
    public string EXTRA_FIELD1 { get; set; }

    [Required]
    [StringLength(1)]
    public string EXTRA_FIELD2 { get; set; }

    [Required]
    [StringLength(1)]
    public string EXTRA_FIELD3 { get; set; }

    [Required]
    [StringLength(1)]
    public string EXTRA_FIELD4 { get; set; }

    [Required]
    [StringLength(8)]
    public string MODIFIED_DATE { get; set; }

    [Required]
    [StringLength(50)]
    public string POP3_DESC_VNM { get; set; }

    [Required]
    [StringLength(150)]
    public string SELLING_POINT1 { get; set; }

    [Required]
    [StringLength(150)]
    public string SELLING_POINT2 { get; set; }

    [Required]
    [StringLength(150)]
    public string SELLING_POINT3 { get; set; }

    [Required]
    [StringLength(150)]
    public string SELLING_POINT4 { get; set; }

    [Required]
    [StringLength(150)]
    public string SELLING_POINT5 { get; set; }

    [Required]
    [StringLength(1000)]
    public string URL { get; set; }

    [StringLength(50)]
    public string CreateBy { get; set; }

    [StringLength(50)]
    public string UpdateBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreateDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime UpdateDate { get; set; }

    public byte ActiveFlag { get; set; }

    public bool IsTransferESL { get; set; }
}
