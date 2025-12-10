
namespace MiddlewareTool.Common
{
    public sealed class History
    {
        public enum HistoryType
        {
            Product = 1,
            ProductInfo = 2,
            Inventory = 3,
            Price = 4,
        }
        public enum Action
        {
            Insert = 1,
            Update = 2,
            Delete = 3,
            Import = 4,
            //Export = 5,
            //Check = 6,
            ImportProductType = 7,
            ImportManualStock = 8,
            ResetManualStock = 9,
            Publish = 10,
            ExportZip = 11,
            //Download = 12,
            UnPublish = 17,
            SyncProfit = 18,
            UnSyncProfit = 19,
            ImportI = 20,
            ImportP = 21,
            ImportE = 22,
            BOXEDExportProduct = 23,
            BOXEDExportInventory = 24,
            BOXEDExportPricing = 25,
            PriceCaculation = 26,
            Reset = 27,
            UpdateFlag = 28,
            ExportM = 29,

        }
    }
}