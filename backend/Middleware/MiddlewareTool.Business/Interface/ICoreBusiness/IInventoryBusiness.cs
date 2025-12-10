using MiddlewareTool.Common;
using System;

namespace MiddlewareTool.Business.Interface
{
    public interface IInventoryBusiness
    {
        byte[] GetTransfer(byte[] template, string transData, int timeOut);
        bool UpdateTransferred(DateTime dateTime, string source, int action, string transData, int timeOut);
        bool UpdateIsTransfer(string sku, History.Action historyAction, bool IsSyncProfit = true);
        bool UpdateIsTransferBySkuAndStore(string sku, string storeCode, bool isTransfer, History.Action historyAction);
    }
}
