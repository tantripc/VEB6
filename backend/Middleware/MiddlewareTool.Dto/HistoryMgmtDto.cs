using MiddlewareTool.Common;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace MiddlewareTool.Dto
{
    public class ProductInfoHistoryDto
    {
        public System.Guid Id { get; set; }
        public string StoreCode { get; set; }
        public string MallCode { get; set; }
        public string Sku { get; set; }
        public Nullable<double> Inventory { get; set; }
        public Nullable<bool> IsTransfer { get; set; }
        public Nullable<bool> IsNew { get; set; }
        public Nullable<bool> IsPublished { get; set; }
        public string CreateBy { get; set; }
        public string UpdateBy { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }
        public Nullable<AppValue.ActiveFlag> ActiveFlag { get; set; }
        public string ActiveFlagDisplay => ActiveFlag?.ToString();
        public Nullable<bool> IsSyncProfit { get; set; }
        public Nullable<byte> Fulfillment { get; set; }
        public int StockBuffer { get; set; }
        public bool? QuickDelivery { get; set; }
        public string Source { get; set; }
        public Nullable<int> Action { get; set; }
        public string TransData { get; set; }
        [NotMapped]
        public int Total { get; set; }
        [NotMapped]
        public string ActionText { get; set; }
        [NotMapped]
        public string UpdateByFullName { get; set; }
        [NotMapped]
        public string CreateByFullName { get; set; }
    }
    public class ProductInfoHistoryExportDto
    {
        public string StoreCode { get; set; }
        public string MallCode { get; set; }
        public string Sku { get; set; }
        public Nullable<double> Inventory { get; set; }
        public int IsTransfer { get; set; }
        public int IsNew { get; set; }
        public int IsPublished { get; set; }
        public string CreateBy { get; set; }
        public string UpdateBy { get; set; }
        public string CreateDate { get; set; }
        public string UpdateDate { get; set; }
        public Nullable<AppValue.ActiveFlag> ActiveFlag { get; set; }
        public string ActiveFlagDisplay => ActiveFlag?.ToString();
        public int IsSyncProfit { get; set; }
        public int Fulfillment { get; set; }
        public int StockBuffer { get; set; }
        public int QuickDelivery { get; set; }
        public string Source { get; set; }
        public string Action { get; set; }
        public string TransData { get; set; }
        public string ActionText { get; set; }
        public string UpdateByFullName { get; set; }
        public string CreateByFullName { get; set; }
    }
    public class ProductHistoryDto
    {
        public System.Guid Id { get; set; }
        public string CompanyCode { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Sku { get; set; }
        public string VariantName { get; set; }
        public string VariantOption { get; set; }
        public string ExtendedName { get; set; }
        public string BrandName { get; set; }
        public string Description { get; set; }
        public string Ingredients { get; set; }
        public string CategoryCode { get; set; }
        public string Upc { get; set; }
        public string Barcode { get; set; }
        public string Size { get; set; }
        public string Weight { get; set; }
        public string Height { get; set; }
        public string Volume { get; set; }
        public Nullable<int> MaxCartQuantity { get; set; }
        public Nullable<double> UnitCount { get; set; }
        public string UnitType { get; set; }
        public string Origin { get; set; }
        public string Grade { get; set; }
        public Nullable<double> TaxRate { get; set; }
        public string ImageLinks { get; set; }
        public Nullable<bool> IsAgeGated { get; set; }
        public Nullable<bool> IsChilled { get; set; }
        public Nullable<bool> IsFrozen { get; set; }
        public Nullable<bool> IsPerishable { get; set; }
        public Nullable<bool> IsTransfer { get; set; }
        public Nullable<int> Type { get; set; }
        public string SEOTitle { get; set; }
        public string SEODescription { get; set; }
        public string SEOKeywords { get; set; }
        public string InternalNote { get; set; }
        public string VariantType { get; set; }
        public string CustomerScope { get; set; }
        public string Slug { get; set; }
        public Nullable<bool> ShowInProductList { get; set; }
        public Nullable<bool> DisplayAsOos { get; set; }
        public Nullable<bool> IsTransferESL { get; set; }
        public string CreateBy { get; set; }
        public string UpdateBy { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }
        public AppValue.ActiveFlag ActiveFlag { get; set; }
        public string ActiveFlagDisplay => ActiveFlag.ToString();
        public string Source { get; set; }
        public Nullable<int> Action { get; set; }
        public string TransData { get; set; }
        [NotMapped]
        public int Total { get; set; }
        [NotMapped]
        public string ActionText { get; set; }
        [NotMapped]
        public string UpdateByFullName { get; set; }
        [NotMapped]
        public string CreateByFullName { get; set; }
        public Nullable<double> B2BTaxRate { get; set; }
    }
    public class ProductHistoryExportDto
    {
        public string CompanyCode { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Sku { get; set; }
        public string VariantName { get; set; }
        public string VariantOption { get; set; }
        public string ExtendedName { get; set; }
        public string BrandName { get; set; }
        public string Description { get; set; }
        public string Ingredients { get; set; }
        public string CategoryCode { get; set; }
        public string Upc { get; set; }
        public string Barcode { get; set; }
        public string Size { get; set; }
        public string Weight { get; set; }
        public string Height { get; set; }
        public string Volume { get; set; }
        public Nullable<int> MaxCartQuantity { get; set; }
        public Nullable<double> UnitCount { get; set; }
        public string UnitType { get; set; }
        public string Origin { get; set; }
        public string Grade { get; set; }
        public Nullable<double> TaxRate { get; set; }
        public string ImageLinks { get; set; }
        public string IsAgeGated { get; set; }
        public string IsChilled { get; set; }
        public string IsFrozen { get; set; }
        public string IsPerishable { get; set; }
        public string IsTransfer { get; set; }
        public string Type { get; set; }
        public string SEOTitle { get; set; }
        public string SEODescription { get; set; }
        public string SEOKeywords { get; set; }
        public string InternalNote { get; set; }
        public string VariantType { get; set; }
        public string CustomerScope { get; set; }
        public string Slug { get; set; }
        public string ShowInProductList { get; set; }
        public string DisplayAsOos { get; set; }
        public string IsTransferESL { get; set; }
        public string CreateBy { get; set; }
        public string UpdateBy { get; set; }
        public string CreateDate { get; set; }
        public string UpdateDate { get; set; }
        public string ActiveFlag { get; set; }
        public string Source { get; set; }
        public Nullable<int> Action { get; set; }
        public string TransData { get; set; }

        public string ActionText { get; set; }
        public string UpdateByFullName { get; set; }
        public string CreateByFullName { get; set; }
        public Nullable<double> B2BTaxRate { get; set; }
    }
    public class InventoryHistoryDto : BaseDto
    {
        public string StoreCode { get; set; }
        public string StoreName { get; set; }
        public string Sku { get; set; }
        public Nullable<double> Quantity { get; set; }
        public Nullable<bool> IsTransfer { get; set; }
        public string Source { get; set; }
        public Nullable<int> Action { get; set; }
        public string TransData { get; set; }
        [NotMapped]
        public int Total { get; set; }
        [NotMapped]
        public string ActionText { get; set; }
        [NotMapped]
        public string UpdateByFullName { get; set; }
        [NotMapped]
        public string CreateByFullName { get; set; }
    }
    public class InventoryHistoryExportDto
    {
        public string StoreCode { get; set; }
        public string StoreName { get; set; }
        public string Sku { get; set; }
        public Nullable<double> Quantity { get; set; }
        public int IsTransfer { get; set; }
        public string Source { get; set; }
        public string Action { get; set; }
        public string CreateBy { get; set; }
        public string UpdateBy { get; set; }
        public string CreateDate { get; set; }
        public string UpdateDate { get; set; }
        public string TransData { get; set; }
        public string ActionText { get; set; }
        public string UpdateByFullName { get; set; }
        public string CreateByFullName { get; set; }
    }
    public class PricingHistoryDto : BaseDto
    {
        public string StoreCode { get; set; }
        public string StoreName { get; set; }
        public string Sku { get; set; }
        public Nullable<double> Price { get; set; }
        public Nullable<double> SalePrice { get; set; }
        public string ExpiredDate { get; set; }
        public Nullable<bool> IsTransfer { get; set; }
        public string Source { get; set; }
        public Nullable<int> Action { get; set; }
        public string TransData { get; set; }
        [NotMapped]
        public int Total { get; set; }
        public string ExpiredDateDisplay
        {
            get
            {
                try
                {
                    if (!string.IsNullOrEmpty(ExpiredDate))
                    {
                        string format = "yyyyMMddHHmm";
                        var _date = DateTime.ParseExact(ExpiredDate, format, CultureInfo.InvariantCulture);

                        return _date.ToString("HH:mm:ss dd/MM/yyyy");
                    }
                }
                catch (Exception ex)
                {

                }

                return "";
            }
        }
        [NotMapped]
        public string ActionText { get; set; }
        [NotMapped]
        public string UpdateByFullName { get; set; }
        [NotMapped]
        public string CreateByFullName { get; set; }
    }
    public class PricingHistoryExportDto
    {
        public string StoreCode { get; set; }
        public string StoreName { get; set; }
        public string Sku { get; set; }
        public string CreateBy { get; set; }
        public string UpdateBy { get; set; }
        public string CreateDate { get; set; }
        public string UpdateDate { get; set; }
        public Nullable<double> Price { get; set; }
        public Nullable<double> SalePrice { get; set; }
        public string ExpiredDate { get; set; }
        public int IsTransfer { get; set; }
        public string Source { get; set; }
        public string Action { get; set; }
        public string TransData { get; set; }
        public string ActionText { get; set; }
        public string UpdateByFullName { get; set; }
        public string CreateByFullName { get; set; }
    }
}
