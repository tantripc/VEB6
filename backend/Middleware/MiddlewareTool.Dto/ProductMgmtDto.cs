using MiddlewareTool.Common;

namespace MiddlewareTool.Dto
{
    public class ProductMgmtDto
    {
        public class ProductDto : BaseDto
        {
            public string CompanyCode { get; set; }
            public string StoreCode { get; set; }
            public string Code { get; set; }
            public string Name { get; set; }
            public string Sku { get; set; }
            public string VariantName { get; set; }
            public string VariantOption { get; set; }
            public string ExtendedName { get; set; }
            public string BrandName { get; set; }
            public string Ingredients { get; set; }
            public string CategoryCode { get; set; }
            public string Upc { get; set; }
            public string Barcode { get; set; }
            public string Size { get; set; }
            public string Weight { get; set; }
            public string Length { get; set; }
            public string Width { get; set; }
            public string Height { get; set; }
            public string Volume { get; set; }
            public Nullable<int> MaxCartQuantity { get; set; }
            public double UnitCount { get; set; }
            public string UnitType { get; set; }
            public string Origin { get; set; }
            public string Grade { get; set; }
            public double TaxRate { get; set; }
            public string ImageLinks { get; set; }
            public bool IsAgeGated { get; set; }
            public bool IsChilled { get; set; }
            public bool IsFrozen { get; set; }
            public Nullable<bool> IsPublished { get; set; }
            public Nullable<byte> Fulfillment { get; set; }
            public Nullable<bool> IsPerishable { get; set; }
            public Nullable<bool> IsTransfer { get; set; }
            public Nullable<bool> IsNew { get; set; }
            public Nullable<byte> Type { get; set; }
            public Nullable<bool> IsSyncProfit { get; set; }
            public string SEOTitle { get; set; }
            public string SEODescription { get; set; }
            public string SEOKeywords { get; set; }
            public string InternalNote { get; set; }
            public string VariantType { get; set; }
            public string CustomerScope { get; set; }
            public string Slug { get; set; }
            public bool ShowInProductList { get; set; }
            public bool DisplayAsOos { get; set; }
            public int Total { get; set; }
            public Nullable<double> B2BTaxRate { get; set; }
            public bool? MommyItem { get; set; }
            public bool? AeonCardItem { get; set; }
        }
        public class ProductExportDto
        {
            public string company_code { get; set; }
            public string product_id { get; set; }
            public string product_name_vi { get; set; }
            public string variant_options_vi { get; set; }
            public string sku { get; set; }
            public string variant_name_vi { get; set; }
            public string extended_name_vi { get; set; }
            public string brand_name_vi { get; set; }
            public string long_description_vi { get; set; }
            public string ingredients_vi { get; set; }
            public string category_id { get; set; }
            public string upcs { get; set; }
            public string barcode { get; set; }
            public string weight { get; set; }
            public string product_size { get; set; }
            public string length { get; set; }
            public string width { get; set; }
            public string height { get; set; }
            public string volume { get; set; }
            public Nullable<int> max_cart_quantity { get; set; }
            public double unit_count { get; set; }
            public string unit_type { get; set; }
            public string product_origin { get; set; }
            public string product_grade { get; set; }
            public double tax_rate { get; set; }
            public string image_links { get; set; }
            public string is_age_gated { get; set; }
            public string is_chilled { get; set; }
            public string is_frozen { get; set; }
            public string is_perishable { get; set; }
            //public string fulfillment_method { get; set; }
            public string is_published { get; set; }
            public string is_sync_profit { get; set; }
            public string seo_title { get; set; }
            public string seo_description { get; set; }
            public string seo_keyword { get; set; }
            public string line { get; set; }
            public string division { get; set; }
            public string group { get; set; }
            public string department { get; set; }
            public string category { get; set; }
            public string product_status { get; set; }
            public string product_type { get; set; }
            public string store { get; set; }
            public string InternalNote { get; set; }
            public string VariantType { get; set; }
            public string CustomerScope { get; set; }
            public string Slug { get; set; }
            public string ShowInProductList { get; set; }
            public string DisplayAsOos { get; set; }
            public string B2BTaxRate { get; set; }
            public string MommyItem { get; set; }
            public string AeonCardItem { get; set; }
        }
        public class ProductExportErrorDto
        {
            public string company_code { get; set; }
            public string product_id { get; set; }
            public string product_name_vi { get; set; }
            public string variant_options_vi { get; set; }
            public string sku { get; set; }
            public string variant_name_vi { get; set; }
            public string extended_name_vi { get; set; }
            public string brand_name_vi { get; set; }
            public string long_description_vi { get; set; }
            public string ingredients_vi { get; set; }
            public string category_id { get; set; }
            public string upcs { get; set; }
            public string barcode { get; set; }
            public string weight { get; set; }
            public string product_size { get; set; }
            public string length { get; set; }
            public string width { get; set; }
            public string height { get; set; }
            public string volume { get; set; }
            public string max_cart_quantity { get; set; }
            public string unit_count { get; set; }
            public string unit_type { get; set; }
            public string product_origin { get; set; }
            public string product_grade { get; set; }
            public string tax_rate { get; set; }
            public string image_links { get; set; }
            public string is_age_gated { get; set; }
            public string is_chilled { get; set; }
            public string is_frozen { get; set; }
            public string is_perishable { get; set; }
            public string fulfillment_method { get; set; }
            public string is_published { get; set; }
            public string is_sync_profit { get; set; }
            public string seo_title { get; set; }
            public string seo_description { get; set; }
            public string seo_keyword { get; set; }
            public string line { get; set; }
            public string division { get; set; }
            public string group { get; set; }
            public string department { get; set; }
            public string category { get; set; }
            public string product_status { get; set; }
            public string product_type { get; set; }
            public string store { get; set; }
            public string InternalNote { get; set; }
            public string VariantType { get; set; }
            public string CustomerScope { get; set; }
            public string Slug { get; set; }
            public string ShowInProductList { get; set; }
            public string DisplayAsOos { get; set; }
            public string B2BTaxRate { get; set; }
            public string ErrorMsg { get; set; }
        }
        public class InventoryExportDto
        {
            public string sku { get; set; }
            public string store { get; set; }
            public string variant_name_vi { get; set; }
            public string stock { get; set; }
            public string stock_buffer { get; set; }
            public string is_published { get; set; }
            public string is_sync_profit { get; set; }
            public string fulfillment_method { get; set; }
            public string QuickDelivery { get; set; }
        }
        public class ProductBoxedDto
        {
            public string Code { get; set; }
            public string Name { get; set; }
            public string VariantOption { get; set; }
            public string Sku { get; set; }
            public string VariantName { get; set; }
            public string ExtendedName { get; set; }
            public string BrandName { get; set; }
            public string Description { get; set; }
            public string Ingredients { get; set; }
            public string CategoryCode { get; set; }
            public string Is_Chill { get; set; }
            public string Is_Frozen { get; set; }
            public string Weight { get; set; }
            public string Length { get; set; }
            public string Width { get; set; }
            public string Height { get; set; }
            public string Volume { get; set; }
            public string Is_Age_Gated { get; set; }
            public Nullable<int> MaxCartQuantity { get; set; }
            public double UnitCount { get; set; }
            public string UnitType { get; set; }
            public string Is_Perishable { get; set; }
            public string Origin { get; set; }
            public string Grade { get; set; }
            public string Size { get; set; }
            public string Upc { get; set; }
            public string Barcode { get; set; }
            public double TaxRate { get; set; }
            public string ImageLinks { get; set; }
            public string seo_title { get; set; }
            public string seo_description { get; set; }
            public string seo_keyword { get; set; }
            public string VariantType { get; set; }
            public string CustomerScope { get; set; }
            public string Slug { get; set; }
            public string ShowInProductList { get; set; }
            public string DisplayAsOos { get; set; }
            public string B2BTaxRate { get; set; }

            public string is_dry_good => VariantType == AppType.VariantTypes.dry_good ? "1" : "0";
            public string is_fresh_food => VariantType == AppType.VariantTypes.fresh_food ? "1" : "0";
        }
        public class ProductInventoryAndPricingDto
        {
            public string MallCode { get; set; }
            public string MallName { get; set; }
            public string StoreID { get; set; }
            public string StoreName { get; set; }
            public double StockOnHandQty { get; set; }
            public int StockBuffer { get; set; }
            public double ManualQuantity { get; set; }
            public double Price { get; set; }
            public double SellingPrice { get; set; }
            public bool IsPublished { get; set; }
            public bool IsSyncProfit { get; set; }
            public Nullable<byte> Fulfillment { get; set; }
            public bool? QuickDelivery { get; set; }
        }
        public class ProductUploadErrorDto : BaseDto
        {
            public Guid UploadId { get; set; }
            public Guid ProductId { get; set; }
            public string Infor { get; set; }
            public int? Current { get; set; }
        }
        public class ProductUploadMonitorDto : BaseDto
        {
            public string FileName { get; set; }
            public byte[] FileContent { get; set; }
            public string FileExt { get; set; }
            public int TotalRow { get; set; }
            public string Curent { get; set; }
        }
        public class ProductInfoUploadMonitorDto : BaseDto
        {
            public string FileName { get; set; }
            public byte[] FileContent { get; set; }
            public string FileExt { get; set; }
            public int TotalRow { get; set; }
            public string Curent { get; set; }
        }
        public class CategoriesDto
        {
            public string Code { get; set; }
            public string Name { get; set; }
        }
        public class ProductInfoUploadErrorDto : BaseDto
        {
            public Guid UploadId { get; set; }
            public string Sku { get; set; }
            public string Infor { get; set; }
        }
        public class ProductInfoDto : BaseDto
        {
            public string StoreCode { get; set; }
            public string StoreName { get; set; }
            public string MallCode { get; set; }
            public string Sku { get; set; }
            public string ProductName { get; set; }
            public Nullable<double> InventoryQuantity { get; set; }
            public Nullable<double> Inventory { get; set; }
            public Nullable<double> Pricing { get; set; }
            public bool IsSyncProfit { get; set; }
            public Nullable<bool> IsTransfer { get; set; }
            public bool IsNew { get; set; }
            public bool IsPublished { get; set; }
            public Nullable<byte> Fulfillment { get; set; }
            public int StockBuffer { get; set; }
            public bool? QuickDelivery { get; set; }
        }
        public class ProductTypeExportDto
        {
            public string product_name_vi { get; set; }
            public string sku { get; set; }
            public string product_type { get; set; }
            public string description { get; set; }
        }

        public class InventoryAndPublishDto : BaseDto
        {
            public string StoreCode { get; set; }
            public string MallCode { get; set; }
            public string Sku { get; set; }
            public string ProductName { get; set; }
            public Nullable<double> Inventory { get; set; }
            public Nullable<double> Pricing { get; set; }
            public Nullable<bool> IsTransfer { get; set; }
            public bool IsNew { get; set; }
            public bool IsPublished { get; set; }
            public bool IsSyncProfit { get; set; }
            public Nullable<byte> Fulfillment { get; set; }
            public int StockBuffer { get; set; }
            public Nullable<bool> QuickDelivery { get; set; }
        }
        public class ProductInventoryPricingTaxDto
        {
            public string StoreCode { get; set; }
            public string Sku { get; set; }
            public string ProductName { get; set; }
            public Nullable<double> Inventory { get; set; }
            public Nullable<double> Pricing { get; set; }
            public Nullable<double> ListPrice { get; set; }
            public double TaxRate { get; set; }
            public string UnitType { get; set; }
            public bool IsTaxB2B { get; set; }
        }
        public class MasterItemCompactDto
        {
            public string ITEM_NO { get; set; }
            public string ITEM_LONG_NAME_CHINESE { get; set; }
            public string ITEM_UOM2 { get; set; }
        }
        public class ProductCompactDto
        {
            public string Sku { get; set; }
            public double TaxRate { get; set; }
            public string Name { get; set; }
            public string VariantName { get; set; }
            public bool? IsSyncProfit { get; set; }
            public string UnitType { get; set; }
        }

        public class ManualStockExportDto
        {
            public string sku { get; set; }
            public string store_code { get; set; }
            public string product_name_vi { get; set; }
            public string inventory { get; set; }
            public string description { get; set; }
            public string stock_buffer { get; set; }
            public string published { get; set; }
            public string sync_profit { get; set; }
            public string fulfillment { get; set; }
            public string QuickDelivery { get; set; }

        }
        public class ESLDto
        {
            public string POP1_DESC_ENG { get; set; }
            public string POP1_DESC_VNM { get; set; }
            public string POP2_DESC_ENG { get; set; }
            public string POP3_DESC_VNM { get; set; }
            public string SELLING_POINT1 { get; set; }
            public string SELLING_POINT2 { get; set; }
            public string SELLING_POINT3 { get; set; }
            public string SELLING_POINT4 { get; set; }
            public string SELLING_POINT5 { get; set; }
            public string LINK { get; set; }
        }
    }
}
