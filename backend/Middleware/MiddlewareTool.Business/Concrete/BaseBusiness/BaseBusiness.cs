using AutoMapper;
using MiddlewareTool.Common;
using MiddlewareTool.Entities;
using MiddlewareTool.Entities.Models;
using MiddlewareTool.Logs;
using Newtonsoft.Json;
using System.Data;
using System.Reflection;
using static MiddlewareTool.Common.AppSystemLog;
using static MiddlewareTool.Dto.SystemMgmtDto;

namespace MiddlewareTool.Business.Concrete
{
    public class BaseBusiness
    {
        #region Properties
        protected readonly AppDbContext UnitOfWork;
        protected const byte STATUS_ACTIVE = (byte)AppValue.ActiveFlag.Active;
        protected const byte STATUS_DEACTIVE = (byte)AppValue.ActiveFlag.Deactive;
        protected const byte STATUS_DELETE = (byte)AppValue.ActiveFlag.Delete;

        #region System Log
        /// <summary>
        /// SP_Resource_GetAll
        /// </summary>
        protected static string SP_Resource_GetAll { get { return "SP_Resource_GetAll"; } }
        /// <summary>
        /// SP_Resource_Delete
        /// </summary>
        protected static string SP_Resource_Delete { get { return "SP_Resource_Delete"; } }
        /// <summary>
        /// SP_Resource_Import
        /// </summary>
        protected static string SP_Resource_Import { get { return "SP_Resource_Import"; } }
        /// <summary>
        /// SP_SystemLog_GetPaging
        /// </summary>
        protected static string SP_SystemLog_GetPaging { get { return "SP_SystemLog_GetPaging"; } }
        /// <summary>
        /// SP_SystemLog_Export
        /// </summary>
        protected static string SP_SystemLog_Export { get { return "SP_SystemLog_Export"; } }
        /// <summary>
        /// SP_SystemLog_Insert
        /// </summary>
        protected static string SP_SystemLog_Insert { get { return "SP_SystemLog_Insert"; } }

        #endregion
        public static DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo prop in Props)
            {
                Type propType = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                dataTable.Columns.Add(prop.Name, propType);
            }

            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    var value = Props[i].GetValue(item, null);
                    values[i] = value ?? DBNull.Value;
                }
                dataTable.Rows.Add(values);
            }

            return dataTable;
        }

        #region System Setting

        /// <summary>
        /// SP_SystemSetting_GetValue
        /// </summary>
        public static string SP_SystemSetting_GetValue { get { return "SP_SystemSetting_GetValue"; } }

        #endregion

        #region Mailbox

        /// <summary>
        /// SP_Mailbox_Insert
        /// </summary>
        public static string SP_Mailbox_Insert { get { return "SP_Mailbox_Insert"; } }
        /// <summary>
        /// SP_Mailbox_GetNotSent
        /// </summary>
        public static string SP_Mailbox_GetNotSent { get { return "SP_Mailbox_GetNotSent"; } }
        // <summary>
        /// SP_Mailbox_GetPaging
        /// </summary>
        public static string SP_Mailbox_GetPaging { get { return "SP_Mailbox_GetPaging"; } }
        /// <summary>
        /// SP_Mailbox_UpdateSentNumSend
        /// </summary>
        public static string SP_Mailbox_UpdateNumSend { get { return "SP_Mailbox_UpdateNumSend"; } }
        // <summary>
        /// SP_Mailbox_GetSent
        /// </summary>
        public static string SP_Mailbox_GetSent { get { return "SP_Mailbox_GetSent"; } }

        #endregion

        #region Master Items

        /// <summary>
        /// core.SP_MasterItem_Import
        /// </summary>
        protected static string SP_MasterItem_Import { get { return "core.SP_MasterItem_Import"; } }

        #endregion

        #region Price Change

        /// <summary>
        /// core.SP_PriceChange_Import
        /// </summary>
        protected static string SP_PriceChange_Import { get { return "core.SP_PriceChange_Import"; } }
        /// <summary>
        /// core.SP_MPriceChange_Import
        /// </summary>
        protected static string SP_MPriceChange_Import => "core.SP_MPriceChange_Import";
        /// <summary>
        /// core.SP_NPriceChange_Import
        /// </summary>
        protected static string SP_NPriceChange_Import => "core.SP_NPriceChange_Import";
        /// <summary>
        /// core.SP_HPriceChange_Import
        /// </summary>
        protected static string SP_HPriceChange_Import => "core.SP_HPriceChange_Import";
        /// <summary>
        /// core.SP_POPMasterItem_Import
        /// </summary>
        protected static string SP_POPMasterItem_Import => "core.SP_POPMasterItem_Import";
        /// <summary>
        /// core.SP_BarcodeMaster_Import
        /// </summary>
        protected static string SP_BarcodeMaster_Import => "core.SP_BarcodeMaster_Import";
        /// <summary>
        /// core.SP_SubClassMaster_Import
        /// </summary>
        protected static string SP_SubClassMaster_Import => "core.SP_SubClassMaster_Import";
        /// <summary>
        /// cat.SP_CategoryMaster_Import
        /// </summary>
        protected static string SP_CategoryMaster_Import => "cat.SP_CategoryMaster_Import";
        #endregion

        #region Group Price Change

        /// <summary>
        /// core.SP_GroupPriceChange_Import
        /// </summary>
        protected static string SP_GroupPriceChange_Import { get { return "core.SP_GroupPriceChange_Import"; } }

        #endregion

        #region Stock

        /// <summary>
        /// core.SP_Stock_Import
        /// </summary>
        protected static string SP_Stock_Import { get { return "core.SP_Stock_Import"; } }
        protected static string SP_Stock_ImportHourly { get { return "core.SP_Stock_ImportHourly"; } }

        #endregion

        #region Category

        /// <summary>
        /// cat.SP_Category_GetTransfer
        /// </summary>
        protected static string SP_Category_GetTransfer { get { return "cat.SP_Category_GetTransfer"; } }
        /// <summary>
        /// cat.SP_Category_UpdateTransferred
        /// </summary>
        protected static string SP_Category_UpdateTransferred { get { return "cat.SP_Category_UpdateTransferred"; } }
        /// <summary>
        /// cat.SP_Category_GetTransferFirstTime
        /// </summary>
        protected static string SP_Category_GetTransferFirstTime { get { return "cat.SP_Category_GetTransferFirstTime"; } }
        /// <summary>
        /// cat.SP_Category_UpdateTransferredAndNotNew
        /// </summary>
        protected static string SP_Category_UpdateTransferredAndNotNew { get { return "cat.SP_Category_UpdateTransferredAndNotNew"; } }

        #endregion

        #region Product

        /// <summary>
        /// prod.SP_Product_GetTransfer
        /// </summary>
        protected static string SP_Product_GetTransfer { get { return "prod.SP_Product_GetTransfer"; } }
        /// <summary>
        /// prod.SP_Product_UpdateTransferred
        /// </summary>
        protected static string SP_Product_UpdateTransferred { get { return "prod.SP_Product_UpdateTransferred"; } }
        /// <summary>
        /// prod.SP_Product_GetTransferFirstTime
        /// </summary>
        protected static string SP_Product_GetTransferFirstTime { get { return "prod.SP_Product_GetTransferFirstTime"; } }
        /// <summary>
        /// prod.SP_Product_UpdateTransferredAndNotNew
        /// </summary>
        protected static string SP_Product_UpdateTransferredAndNotNew { get { return "prod.SP_Product_UpdateTransferredAndNotNew"; } }

        /// <summary>
        /// prod.SP_B2B_GET_PRODUCT_BY_STORE
        /// </summary>
        protected static string SP_B2B_GET_PRODUCT_BY_STORE => "prod.SP_B2B_GET_PRODUCT_BY_STORE";

        /// <summary>
        /// core.SP_ProductFeed_Import
        /// </summary>
        protected static string SP_ProductFeed_Import => "core.SP_ProductFeed_Import";
        #endregion

        #region Pricing

        /// <summary>
        /// core.SP_Pricing_GetTransfer
        /// </summary>
        protected static string SP_Pricing_GetTransfer { get { return "core.SP_Pricing_GetTransfer"; } }
        /// <summary>
        /// core.SP_Pricing_GetTransferRealTime
        /// </summary>
        protected static string SP_Pricing_GetTransferRealTime { get { return "core.SP_Pricing_GetTransferRealTime"; } }
        /// <summary>
        /// core.SP_Pricing_UpdateTransferred
        /// </summary>
        protected static string SP_Pricing_UpdateTransferred { get { return "core.SP_Pricing_UpdateTransferred"; } }
        /// <summary>
        /// core.SP_Pricing_CalculateSalePrice
        /// </summary>
        protected static string SP_Pricing_CalculateSalePrice { get { return "core.SP_Pricing_CalculateSalePrice"; } }
        /// <summary>
        /// core.SP_Pricing_GetTransferFirstTime
        /// </summary>
        protected static string SP_Pricing_GetTransferFirstTime { get { return "core.SP_Pricing_GetTransferFirstTime"; } }

        #endregion

        #region Inventory

        /// <summary>
        /// core.SP_Inventory_GetTransfer
        /// </summary>
        protected static string SP_Inventory_GetTransfer { get { return "core.SP_Inventory_GetTransfer"; } }
        /// <summary>
        /// core.SP_Inventory_UpdateTransferred
        /// </summary>
        protected static string SP_Inventory_UpdateTransferred { get { return "core.SP_Inventory_UpdateTransferred"; } }
        /// <summary>
        /// core.SP_Inventory_GetTransferFirstTime
        /// </summary>
        protected static string SP_Inventory_GetTransferFirstTime { get { return "core.SP_Inventory_GetTransferFirstTime"; } }

        #endregion

        #region Inventory Delta

        /// <summary>
        /// core.SP_InventoryDelta_GetTransfer
        /// </summary>
        protected static string SP_InventoryDelta_GetTransfer { get { return "core.SP_InventoryDelta_GetTransfer"; } }
        /// <summary>
        /// core.SP_InventoryDelta_UpdateTransferred
        /// </summary>
        protected static string SP_InventoryDelta_UpdateTransferred { get { return "core.SP_InventoryDelta_UpdateTransferred"; } }

        #endregion

        #region Sku Mapping

        /// <summary>
        /// core.SP_SkuMapping_GetTransfer
        /// </summary>
        protected static string SP_SkuMapping_GetTransfer { get { return "core.SP_SkuMapping_GetTransfer"; } }
        /// <summary>
        /// core.SP_SkuMapping_UpdateTransferred
        /// </summary>
        protected static string SP_SkuMapping_UpdateTransferred { get { return "core.SP_SkuMapping_UpdateTransferred"; } }

        #endregion

        #region Store Creation
        /// <summary>
        /// core.SP_StoreCreation_Export
        /// </summary>
        protected static string SP_StoreCreation_Export { get { return "core.SP_StoreCreation_Export"; } }

        #endregion

        #region Sales

        /// <summary>
        /// se.SP_Sale_Import
        /// </summary>
        protected static string SP_Sale_Import { get { return "se.SP_Sale_Import"; } }
        /// <summary>
        /// se.SP_SaleByStore_GetTransfer
        /// </summary>
        protected static string SP_SaleByStore_GetTransfer { get { return "se.SP_SaleByStore_GetTransfer"; } }
        /// <summary>
        /// se.SP_Sale_GetStores
        /// </summary>
        protected static string SP_Sale_GetStores { get { return "se.SP_Sale_GetStores"; } }
        protected static string SP_SAP_Sale_GetStores { get { return "se.SP_SAP_Sale_GetStores"; } }
        protected static string SP_S4_Sale_GetStores { get { return "se.SP_S4_Sale_GetStores"; } }
        /// <summary>
        /// se.SP_Sale_UpdateTransferred
        /// </summary>
        protected static string SP_Sale_UpdateTransferred { get { return "se.SP_Sale_UpdateTransferred"; } }
        protected static string SP_SAP_Sale_UpdateTransferred { get { return "[se].[SP_SAP_Sale_UpdateTransferred]"; } }
        protected static string SP_S4_Sale_UpdateTransferred { get { return "[se].[SP_S4_Sale_UpdateTransferred]"; } }

        #endregion

        #region Refund

        /// <summary>
        /// re.SP_Refund_Import
        /// </summary>
        protected static string SP_Refund_Import { get { return "re.SP_Refund_Import"; } }
        /// <summary>
        /// re.SP_RefundByStore_GetTransfer
        /// </summary>
        protected static string SP_RefundByStore_GetTransfer { get { return "re.SP_RefundByStore_GetTransfer"; } }
        /// <summary>
        /// re.SP_Refund_GetStores
        /// </summary>
        protected static string SP_Refund_GetStores { get { return "re.SP_Refund_GetStores"; } }
        protected static string SP_SAP_Refund_GetStores { get { return "re.SP_SAP_Refund_GetStores"; } }
        protected static string SP_S4_Refund_GetStores { get { return "re.SP_S4_Refund_GetStores"; } }
        /// <summary>
        /// re.SP_Refund_UpdateTransferred
        /// </summary>
        protected static string SP_Refund_UpdateTransferred { get { return "re.SP_Refund_UpdateTransferred"; } }
        protected static string SP_SAP_Refund_UpdateTransferred { get { return "re.SP_SAP_Refund_UpdateTransferred"; } }
        protected static string SP_S4_Refund_UpdateTransferred { get { return "re.SP_S4_Refund_UpdateTransferred"; } }

        #endregion

        #region Record Sale

        /// <summary>
        /// se.SP_RecordSale_GetPaging
        /// </summary>
        protected static string SP_RecordSale_GetPaging { get { return "se.SP_RecordSale_GetPaging"; } }
        protected static string SP_RecordSale_GetDetail { get { return "se.SP_RecordSale_GetDetail"; } }
        protected static string SP_RecordSale_GetItemsPaging { get { return "se.SP_RecordSale_GetItemsPaging"; } }
        /// <summary>
        /// se.SP_RecordSale_Export
        /// </summary>
        protected static string SP_RecordSale_Export { get { return "se.SP_RecordSale_Export"; } }
        /// <summary>
        /// se.SP_RecordSale_GetTransfer
        /// </summary>
        protected static string SP_RecordSale_GetTransfer { get { return "se.SP_RecordSale_GetTransfer"; } }
        /// <summary>
        /// se.SP_RecordSale_UpdateTransferred
        /// </summary>
        protected static string SP_RecordSale_UpdateTransferred { get { return "se.SP_RecordSale_UpdateTransferred"; } }
        /// <summary>
        /// re.SP_RecordRefund_GetTransfer
        /// </summary>
        protected static string SP_RecordRefund_GetTransfer { get { return "[re].[SP_RecordRefund_GetTransfer]"; } }
        /// <summary>
        /// se.SP_RecordSale_UpdateTransferred
        /// </summary>
        protected static string SP_RecordRefund_UpdateTransferred { get { return "re.SP_RecordRefund_UpdateTransferred"; } }

        #endregion

        #region Record Refund

        /// <summary>
        /// re.SP_RecordRefund_GetPaging
        /// </summary>
        protected static string SP_RecordRefund_GetPaging { get { return "re.SP_RecordRefund_GetPaging"; } }
        protected static string SP_RecordRefund_GetDetail { get { return "re.SP_RecordRefund_GetDetail"; } }
        protected static string SP_RecordRefund_GetRefundHistories { get { return "re.SP_RecordRefund_GetRefundHistories"; } }
        /// <summary>
        /// re.SP_RecordRefund_Export
        /// </summary>
        protected static string SP_RecordRefund_Export { get { return "re.SP_RecordRefund_Export"; } }
        protected static string SP_RecordRefund_GetItemsPaging { get { return "re.SP_RecordRefund_GetItemsPaging"; } }

        #endregion

        #region B2B
        protected static string SP_B2B_GetBusiness => "so.SP_B2B_GetBusiness";

        #endregion

        #region PromotionESL
        /// <summary>
        /// cat.SP_CategoryMaster_Import
        /// </summary>
        protected static string SP_PromotionESL_Import => "core.SP_PromotionESL_Import";
        protected static string SP_PromotionESL_GetPaing => "core.SP_PromotionESL_GetPaging";
        #endregion

        #endregion

        #region Constructors
        public BaseBusiness(AppDbContext unitOfWork)
        {
            this.UnitOfWork = unitOfWork;
        }

        #endregion

        #region Methods
        protected void LogInfo(string message)
        {
            Logging.LogInfo($"{message}");
        }
        protected void LogError(string message)
        {
            Logging.LogError($"{message}");
        }
        protected void LogError(string message, object data)
        {
            try
            {
                var jsonString = JsonConvert.SerializeObject(data);
                Logging.LogError($"{message}: {jsonString}");
            }
            catch (Exception ex)
            {
                Logging.LogError(GetType().FullName, ex);
            }
        }
        protected void LogError(string method, Exception ex, bool insertDB = false)
        {
            try
            {
                var services = this.GetType();
                string _service = services.FullName;
                Logging.LogError($"{_service}-{method}", ex);
                if (insertDB)
                {
                    var systemLog = new SystemLog()
                    {
                        LogId = Guid.NewGuid(),
                        UserId = "system",
                        UserFunction = (int)AppSystemLog.Action.SendRequest,
                        EventResult = (int)EventResult.Fail,
                        Transdata = ex.Message,
                        Source = method,
                        FuncDateTime = DateTime.Now,
                        WSName = ""
                    };
                    this.UnitOfWork.SystemLogs.Add(systemLog);
                }
            }
            catch (Exception exce)
            {
                Logging.LogError(GetType().FullName, exce);
            }
        }

        #endregion

        #region Product Info
        protected static string SP_ProductInfo_ResetManualStock { get { return "prod.SP_ProductInfo_ResetManualStock"; } }
        #endregion
        #region Manual Export Zip
        protected static string SP_ManualExportZip { get { return "core.SP_ManualExportZip"; } }
        protected static string SP_CheckProductNew { get { return "prod.SP_CheckProductNew"; } }
        #endregion

        #region Convert String
        private static int[] Map_VN1258 = {194,226,258,259,202,234,212,244,431,432,416,417,272,273,
                                              65,803,97,803,65,777,97,777,194,769,226,769,194,768,226,768,194,777,
                                              226,777,194,771,226,771,194,803,226,803,258,769,259,769,258,768,259,
                                              768,258,777,259,777,258,771,259,771,258,803,259,803,69,803,101,803,
                                              69,777,101,777,69,771,101,771,202,769,234,769,202,768,234,768,202,
                                              777,234,777,202,771,234,771,202,803,234,803,73,777,105,777,73,803,
                                              105,803,79,803,111,803,79,777,111,777,212,769,244,769,212,768,244,
                                              768,212,777,244,777,212,771,244,771,212,803,244,803,416,769,417,769,
                                              416,768,417,768,416,777,417,777,416,771,417,771,416,803,417,803,85,
                                              803,117,803,85,777,117,777,431,769,432,769,431,768,432,768,431,777,
                                              432,777,431,771,432,771,431,803,432,803,89,768,121,768,89,803,121,
                                              803,89,777,121,777,89,771,121,771,65,768,65,769,65,771,69,768,69,
                                              769,73,768,73,769,79,768,79,769,79,771,85,768,85,769,89,769,97,768,
                                              97,769,97,771,101,768,101,769,105,768,105,769,111,768,111,769,111,
                                              771,117,768,117,769,121,769,85,771,117,771,73,771,105,771};
        private static int[] Map_VNOrigin = {194,226,258,259,202,234,212,244,431,432,416,417,272,
                                                273,7840,7841,7842,7843,7844,7845,7846,7847,7848,7849,7850,7851,
                                                7852,7853,7854,7855,7856,7857,7858,7859,7860,7861,7862,7863,7864,
                                                7865,7866,7867,7868,7869,7870,7871,7872,7873,7874,7875,7876,7877,
                                                7878,7879,7880,7881,7882,7883,7884,7885,7886,7887,7888,7889,7890,
                                                7891,7892,7893,7894,7895,7896,7897,7898,7899,7900,7901,7902,7903,
                                                7904,7905,7906,7907,7908,7909,7910,7911,7912,7913,7914,7915,7916,
                                                7917,7918,7919,7920,7921,7922,7923,7924,7925,7926,7927,7928,7929,
                                                192,193,195,200,201,204,205,210,211,213,217,218,221,224,225,227,
                                                232,233,236,237,242,243,245,249,250,253,360,361,296,297};
        public static string UnicodeOriginToUnicodeVN1258(string strUnicode)
        {
            string text = "";
            int num = 0;
            int num2 = 134;
            while (num < strUnicode.Length)
            {
                if (strUnicode[num] == '–')
                {
                    text += "\u0096";
                    num++;
                    continue;
                }

                if (strUnicode[num] == '—')
                {
                    text += "\u0097";
                    num++;
                    continue;
                }

                if (strUnicode[num] == '’')
                {
                    text += "\u0092";
                    num++;
                    continue;
                }

                if (strUnicode[num] == '‘')
                {
                    text += "\u0091";
                    num++;
                    continue;
                }

                if (strUnicode[num] == '“')
                {
                    text += "\u0093";
                    num++;
                    continue;
                }

                if (strUnicode[num] == '”')
                {
                    text += "\u0094";
                    num++;
                    continue;
                }

                bool flag = false;
                for (int i = 0; i < num2; i++)
                {
                    if (strUnicode[num] == Map_VNOrigin[i])
                    {
                        if (i >= 14)
                        {
                            text += (char)Map_VN1258[(i - 14) * 2 + 14];
                            text += (char)Map_VN1258[(i - 14) * 2 + 14 + 1];
                        }
                        else
                        {
                            text += (char)Map_VN1258[i];
                        }

                        flag = true;
                        num++;
                        break;
                    }
                }

                if (!flag)
                {
                    flag = false;
                    text += strUnicode[num++];
                }
            }
            return text;
        }
        #endregion

        #region ResourceMgmt
        /// <summary>
        ///Get ResourceID error!.
        /// </summary>
        public const string Error_GetResourceID = "Get ResourceID error!.";
        public static Dictionary<string, ResourceDto> DicResources { get; set; }
        #endregion
        #region History
        /// <summary>
        /// core.SP_ProductFeed_Import
        /// </summary>
        protected static string SP_ProductHistory_Search => "[prod].[SP_ProductHistory_Search]";
        /// <summary>
        /// core.SP_ProductFeed_Import
        /// </summary>
        protected static string SP_ProductInfoHistory_Search => "[prod].[SP_ProductInfoHistory_Search]";
        /// <summary>
        /// core.SP_ProductFeed_Import
        /// </summary>
        protected static string SP_PricingHistory_Search => "core.SP_PricingHistory_Search";
        /// <summary>
        /// core.SP_ProductFeed_Import
        /// </summary>
        protected static string SP_InventoryHistory_Search => "core.SP_InventoryHistory_Search";
        #endregion
    }
}
