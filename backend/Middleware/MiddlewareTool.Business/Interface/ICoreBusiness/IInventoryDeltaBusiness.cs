using System;
using System.Threading.Tasks;

namespace MiddlewareTool.Business.Interface
{
    public interface IInventoryDeltaBusiness
    {
        byte[] GetTransfer(byte[] template, int timeOut);
        bool UpdateTransferred(DateTime dateTime, int timeOut);
        bool UpdateIsTransfer(string sku);
    }
}
