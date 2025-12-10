using System.Data;

namespace MiddlewareTool.Business.Interface
{
    public interface IBarcodeMasterBusiness
    {
        bool Import(DataTable dt, int timeOut);
    }
}
