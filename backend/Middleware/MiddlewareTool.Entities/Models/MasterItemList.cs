using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MiddlewareTool.Entities.Models;

[Keyless]
[Table("MasterItemList", Schema = "core")]
public partial class MasterItemList
{
    [Required]
    [StringLength(1)]
    public string REC_ID { get; set; }

    [Required]
    [StringLength(13)]
    public string ITEM_NO { get; set; }

    [Required]
    [StringLength(15)]
    public string ITEM_SHORT_NAME { get; set; }

    [Required]
    [StringLength(30)]
    public string ITEM_LONG_NAME { get; set; }

    [Required]
    [StringLength(20)]
    public string ITEM_SHORT_NAME_CHINESE { get; set; }

    [Required]
    [StringLength(40)]
    public string ITEM_LONG_NAME_CHINESE { get; set; }

    [Required]
    [StringLength(18)]
    public string ITEM_BARCODE { get; set; }

    [Required]
    [StringLength(17)]
    public string ITEM_SELL { get; set; }

    [Required]
    [StringLength(17)]
    public string ITEM_MEMBER_SELL { get; set; }

    [Required]
    [StringLength(5)]
    public string ITEM_UOM { get; set; }

    [Required]
    [StringLength(3)]
    public string ITEM_DIV { get; set; }

    [Required]
    [StringLength(3)]
    public string ITEM_DEPT { get; set; }

    [Required]
    [StringLength(6)]
    public string ITEM_CLS { get; set; }

    [Required]
    [StringLength(9)]
    public string ITEM_SUBCLS { get; set; }

    [Required]
    [StringLength(1)]
    public string ITEM_WEIGH { get; set; }

    [Required]
    [StringLength(1)]
    public string ITEM_PLU_FLAG { get; set; }

    [Required]
    [StringLength(8)]
    public string ITEM_DATE { get; set; }

    [Required]
    [StringLength(1)]
    public string ITEM_VAT_FLAG { get; set; }

    [Required]
    [StringLength(3)]
    public string ITEM_VAT { get; set; }

    [Required]
    [StringLength(6)]
    public string SEASON_ID { get; set; }

    [Required]
    [StringLength(3)]
    public string SALES_TAX { get; set; }

    [Required]
    [StringLength(1)]
    public string KADS1M_FLAG { get; set; }

    [Required]
    [StringLength(3)]
    public string Valid_to_use_date { get; set; }

    [Required]
    [StringLength(1)]
    public string CARD_FLAG { get; set; }

    [Required]
    [StringLength(8)]
    public string ITEM_UOM2 { get; set; }

    [Required]
    [StringLength(3)]
    public string Print_prod_flag { get; set; }

    [Required]
    [StringLength(5)]
    public string TAX_CODE { get; set; }

    [Required]
    [StringLength(5)]
    public string Tax_Sign { get; set; }

    [StringLength(100)]
    public string NUTRI_FACTS { get; set; }

    [StringLength(66)]
    public string INSTRUCT_STORAGE { get; set; }

    [StringLength(300)]
    public string DIRECTION { get; set; }

    [StringLength(45)]
    public string WARNING { get; set; }

    [StringLength(475)]
    public string INGREDIENT { get; set; }

    [StringLength(4)]
    public string EXPIRE_TIME { get; set; }

    [StringLength(1)]
    public string EXPIRE_LABEL_FORMAT { get; set; }
}
