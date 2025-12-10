using MiddlewareTool.Dto;
using MiddlewareTool.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static MiddlewareTool.Common.AppType;
using static MiddlewareTool.Dto.ProductMgmtDto;
using static MiddlewareTool.Dto.StoreMgmtDto;

namespace MiddlewareTool.Business.Interface
{
    public interface IProductBusiness
    {
        byte[] GetTransfer(byte[] template, string transData, int timeOut);
        bool UpdateTransferred(DateTime dateTime, string source, int action, string transData, int timeOut);
        Task<Tuple<int, List<ProductDto>>> GetPagingAsync(string keyWord, int pageIndex, int pageSize, List<string> categoryIds, string categoryIdIsNull, bool? isPublished, bool? isAgeGated, bool? isChilled, bool? isFrozen, bool? isPerishable, bool? isManual, string brand, List<string> storeCodes, List<byte?> fulfillment, bool? isSyncProfit, bool? concess, bool? outright, string variantType, string customerScope, string slug, bool? showInProductList, bool? displayAsOos, bool? mommyItem, bool? aeonCardItem, bool? quickDelivery);
        Task<ProductDto> GetByIdAsync(Guid id);
        Task<ESLDto> GetESLBySkuAsync(string sku);
        Task<bool> InsertAsync(ProductDto dto);
        bool Insert(ProductDto dto, string fileName);
        Task<bool> IsExistAsync(string sku);
        bool IsExist(string sku);
        Task<bool> UpdateAsync(ProductDto dto);
        bool Update(ProductDto dto, string fileName);
        Task<bool> DeleteAsync(Guid id, string userName);
        List<ProductExportDto> Export(string keyWord, List<string> categoryIds, string categoryIdIsNull, bool? isPublished, bool? isAgeGated, bool? isChilled, bool? isFrozen, bool? isPerishable, string brand, List<string> storeCodes, List<byte?> fulfillment, bool? isSyncProfit, bool? concess, bool? outright, string variantType, string customerScope, string slug, bool? showInProductList, bool? displayAsOos, bool? mommyItem, bool? aeonCardItem, bool? quickDelivery);
        List<InventoryExportDto> ExportInventory(string keyWord, List<string> categoryIds, string categoryIdIsNull, bool? isPublished, bool? isAgeGated, bool? isChilled, bool? isFrozen, bool? isPerishable, string brand, List<string> storeCodes, List<byte?> fulfillment, bool? isSyncProfit, bool? concess, bool? outright, string variantType, string customerScope, string slug, bool? showInProductList, bool? displayAsOos, bool? mommyItem, bool? aeonCardItem, bool? quickDelivery);
        Task<Tuple<int, List<ProductInventoryAndPricingDto>>> GetInventoryPagingAsync(string sku, bool isAdmin, List<string> storeCodes, int pageIndex, int pageSize);
        List<ProductDto> GetAllNoTracking();
        List<string> GetAllSKU();
        bool UpdateCurrentUploadMonitor(string user, UploadMonitor uploadMonitor, int current);
        bool InsertUploadError(string user, ProductUploadErrorDto dto);
        bool InsertManualStockUploadError(string user, ProductInfoUploadErrorDto dto);
        UploadMonitor InsertUploadMonitor(string user, ProductUploadMonitorDto dto);
        bool Publish(List<ProductInfo> listInfo, bool isPublished, string userName);
        List<CategoriesDto> GetCategories();
        UploadMonitor GetUploadMonitor(Guid id);
        int GetCurrentUploadError(Guid uploadId);
        List<UploadError> GetUploadErrors(Guid uploadId);
        bool CheckExist(string id);
        bool CheckExist(string sku, string storeCode);
        List<ProductDto> GetAllSKUAndStoreCode(List<string> storeCodes = null);
        //Task<bool> InsertOrUpdsateManualStockAsync(ProductDto dto, double manualStockQuantity);
        bool InsertOrUpdateManualStock(string user, string sku, string storeCode, double manualStockQuantity, int? stockBuffer, bool? published, bool? syncProfit, int? fulfillment, bool? quickDelivery, string fileName);
        Task<double?> GetManualStockBySkuAsync(string sku);
        ProductInfoUploadMonitor InsertManualStockUploadMonitor(string user, ProductInfoUploadMonitorDto dto);
        bool UpdateCurrentManualStockUploadMonitor(string user, ProductInfoUploadMonitor uploadMonitor, int current);
        ProductInfoUploadMonitor GetManualStockUploadMonitor(Guid id);
        int GetCurrentManualStockUploadError(Guid uploadId);
        bool UpdateIsManualStatus(string userName, string sku);
        bool UpdateProductTypeAsync(string userName, string sku, byte productType, string fileName);
        Task<bool> UpdateInventoryAndPublish(string user, string sku, List<InventoryAndPublishDto> listInventoryAndPublish, bool isAdmin, List<string> storeCodes);
        List<ProductInfoDto> GetInventory(string sku, bool isAdmin, List<string> storeCodes);
        Task<Tuple<int, List<UpdateResultsDto>>> UpdateMonitorGetPagingAsync(string keyWord, int pageIndex, int pageSize, DateTime? fromDate, DateTime? toDate, string uploadBy);
        Task<Tuple<int, List<UpdateResultsDto>>> UpdateProductInfoMonitorGetPagingAsync(string keyWord, int pageIndex, int pageSize, DateTime? fromDate, DateTime? toDate, string uploadBy);
        bool SyncProfit(Guid productId, bool isSync, string userName);
        bool CheckIsSyncProfit(string sku, string storeCode);
        bool UpdateIsPublished(string sku);
        bool ResetManualStock(DateTime dateTime, int timeOut);
        List<ProductInfo> GetProductInfoBySku(string sku);
        bool UpdateManualExportZip(string userName);
        bool CheckExistNewProduct(string userName);
        InventoryAndPublishDto GetProductInfoBySkuAndStoreCode(string sku, string storeCode);
        List<ProductInventoryPricingTaxDto> GetProductInfoByStoreCode(string storeCode);
        ProductCompactDto GetProductBySku(string sku);
        double? GetSellingPriceByStoreCode(string storeCode, string sku);
        List<string> GetVariantTypes();
        List<FulfillmentTypesSetting> GetFulfillmentSettings();
        bool UpdateProductInfos(string sku, byte? Fulfillment, string userName, string fileName);
        List<ProductCompactDto> GetAllCompact();
        ProductInfo GetProductInfoBySKUAndStoreCode(string sku, string storeCode);
        List<StoreCompactDto> GetStoreListBySKU(string sku);
    }
}
