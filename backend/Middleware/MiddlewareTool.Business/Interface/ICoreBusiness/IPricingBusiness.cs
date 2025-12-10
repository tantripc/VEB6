using System;

namespace MiddlewareTool.Business.Interface
{
    public interface IPricingBusiness
    {
        byte[] GetTransfer(byte[] template, string transData, int timeOut);
        byte[] GetTransferRealTime(byte[] template, string transData, int timeOut);
        bool UpdateTransferred(DateTime dateTime, string source, int action, string transData, int timeOut);
        bool UpdateIsTransfer(string sku);
    }
}
