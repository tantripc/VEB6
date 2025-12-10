namespace MiddlewareTool.Common
{
    public sealed class AppSystemLog
    {
        public enum Action
        {
            Login = 0,
            Insert = 1,
            Update = 2,
            Delete = 3,
            Import = 4,
            Export = 5,
            Check = 6,
            ImportProductType = 7,//
            ImportManualStock = 8,//
            ResetManualStock = 9,//
            Publish = 10,
            ExportZip = 11,
            Download = 12,//
            SendRequest = 13,
            Reject = 14,
            Approve = 15,
            Invoice = 16,
            UnPublish = 17
        }
        public enum EventResult
        {
            Fail = 0,
            Success = 1
        }
    }
}
