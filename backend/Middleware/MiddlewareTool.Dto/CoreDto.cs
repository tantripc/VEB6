namespace MiddlewareTool.Dto
{
    public class CoreDto
    {
        public class MasterItemDto : BaseDto
        {
            public string REC_ID { get; set; }
            public string ITEM_NO { get; set; }
            public string ITEM_SHORT_NAME { get; set; }
            public string ITEM_LONG_NAME { get; set; }
            public string ITEM_SHORT_NAME_CHINESE { get; set; }
            public string ITEM_LONG_NAME_CHINESE { get; set; }
            public string ITEM_BARCODE { get; set; }
            public string ITEM_SELL { get; set; }
            public string ITEM_MEMBER_SELL { get; set; }
            public string ITEM_UOM { get; set; }
            public string ITEM_DIV { get; set; }
            public string ITEM_DEPT { get; set; }
            public string ITEM_CLS { get; set; }
            public string ITEM_SUBCLS { get; set; }
            public string ITEM_WEIGH { get; set; }
            public string ITEM_PLU_FLAG { get; set; }
            public string ITEM_DATE { get; set; }
            public string ITEM_VAT_FLAG { get; set; }
            public string ITEM_VAT { get; set; }
            public string SEASON_ID { get; set; }
            public string SALES_TAX { get; set; }
            public string KADS1M_FLAG { get; set; }
            public string Valid_to_use_date { get; set; }
            public string CARD_FLAG { get; set; }
            public string ITEM_UOM2 { get; set; }
            public string Print_prod_flag { get; set; }
            public string TAX_CODE { get; set; }
            public string Tax_Sign { get; set; }
            public string NUTRI_FACTS { get; set; }
            public string INSTRUCT_STORAGE { get; set; }
            public string DIRECTION { get; set; }
            public string WARNING { get; set; }
            public string INGREDIENT { get; set; }
            public string EXPIRE_TIME { get; set; }
            public string EXPIRE_LABEL_FORMAT { get; set; }
        }
        public class PriceChangeDto : BaseDto
        {
            public string REC_ID { get; set; }
            public string ITEM_NO { get; set; }
            public string PRC_NO { get; set; }
            public string PRC_TYPE { get; set; }
            public string PRC_START_DATE { get; set; }
            public string PRC_END_DATE { get; set; }
            public string PRC_START_TIME { get; set; }
            public string PRC_END_TIME { get; set; }
            public string PRC_DISC_RATE { get; set; }
            public string PRC_DISC_AMT { get; set; }
            public string PRC_SELL { get; set; }
            public string StoreCode { get; set; }
        }
        public class PriceChangeCompactDto
        {
            public string ITEM_NO { get; set; }
            public string PRC_NO { get; set; }
            public string PRC_SELL { get; set; }
            public string StoreCode { get; set; }
        }
        public class GroupPriceChangeDto : BaseDto
        {
            public string REC_ID { get; set; }
            public string PRC_NO { get; set; }
            public string PRC_TYPE { get; set; }
            public string PRC_START_DATE { get; set; }
            public string PRC_END_DATE { get; set; }
            public string PRC_START_TIME { get; set; }
            public string PRC_END_TIME { get; set; }
            public string SUBCLASS { get; set; }
            public string PRC_DISC_RATE { get; set; }
            public string EXCLUDE_SSN_ID { get; set; }
            public string EndOfRecord { get; set; }
            public string StoreCode { get; set; }
        }
        public class StockDto : BaseDto
        {
            public string RecordFlag { get; set; }
            public string Sku { get; set; }
            public string SkuDesc { get; set; }
            public string StoreCode { get; set; }
            public double SellingPrice { get; set; }
            public double StockOnHandQty { get; set; }
        }
        public class PricingBoxedDto
        {
            public string MallCode { get; set; }
            public string MallName { get; set; }
            public string StoreCode { get; set; }
            public string StoreName { get; set; }
            public string Sku { get; set; }
            public double Price { get; set; }
            public Nullable<double> SalePrice { get; set; }
            public string DisplaySalePrice => (SalePrice == Price) ? string.Empty : SalePrice.ToString();
        }
        public class InventoryBoxedDto
        {
            public string MallCode { get; set; }
            public string MallName { get; set; }
            public string StoreCode { get; set; }
            public string StoreName { get; set; }
            public string Sku { get; set; }
            public double Quantity { get; set; }
            public string Is_Published { get; set; }
            public string Fulfillment_Method { get; set; }
            public string fast_delivery_eligible { get; set; }
        }
        public class InventoryDeltaBoxedDto
        {
            public string MallCode { get; set; }
            public string MallName { get; set; }
            public string StoreCode { get; set; }
            public string StoreName { get; set; }
            public string Sku { get; set; }
            public double QuantityDelta { get; set; }
            public string Is_Published { get; set; }
        }
        public class SkuMappingBoxedDto
        {
            public string MallCode { get; set; }
            public string MallName { get; set; }
            public string Sku { get; set; }
            public string StoreCode { get; set; }
            public string StoreName { get; set; }
            public string ExpressLocationGroupName { get; set; }
            public string ParcelLocationGroupName { get; set; }
            public bool Fulfillment { get; set; }
        }
    }
}
