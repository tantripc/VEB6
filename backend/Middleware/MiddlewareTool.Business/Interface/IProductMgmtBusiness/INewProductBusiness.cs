using System;

namespace MiddlewareTool.Business.Interface
{
    public interface INewProductBusiness
    {
        Tuple<byte[], byte[], byte[], byte[]> GetTransfer(Tuple<byte[], byte[], byte[], byte[]> template, string transData, int timeOut);
        bool UpdateTransferredAndNotNew(DateTime dateTime, string source, int action, string transData, int timeOut);
    }
}
