using System.Data;

namespace MiddlewareTool.Business.Interface
{
    public interface IMPriceChangeBusiness
    {        
        bool Exist(string prc_no, string storeCode, string item_no);
        bool Import(DataTable dt, int timeOut);
    }
}
