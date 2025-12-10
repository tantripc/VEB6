using System.Data;

namespace MiddlewareTool.Business.Interface
{
    public interface IPOPMasterItemBusiness
    {
        bool Import(DataTable dt, int timeOut);
        bool CheckReadFile(string path);
    }
}
