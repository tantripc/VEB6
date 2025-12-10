using System.Data;

namespace MiddlewareTool.Business.Interface
{
    public interface INPriceChangeBusiness
    {        
        bool Exist(string prc_no, string storeCode);
        bool Import(DataTable dt, int timeOut);
    }
}
