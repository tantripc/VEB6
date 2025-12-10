using System;
using System.Collections.Generic;

namespace MiddlewareTool.Business.Interface
{
    public interface IESLBusiness
    {
        List<string> M_GetStores(DateTime datetime, int timeout = 120, string storeCodes = null);
        List<string> INV_GetStores(bool _daily = false, int timeout = 120, string storeCodes = null);
        byte[] INV_GetTransfer(byte[] template, string storeCode, bool _daily = false, int timeout = 120);
        bool INV_UpdateTransferred(DateTime dateTime, int timeout = 120);
        byte[] M_GetTransfer(byte[] template, string storeCode, int timeout = 120);
        bool M_UpdateTransferred(DateTime dateTime, int timeout = 120);
        bool UpdatePromotionEDLPFlag(DateTime dateTime, int timeout = 120);
    }
}
