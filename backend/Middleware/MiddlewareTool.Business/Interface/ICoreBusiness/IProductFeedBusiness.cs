using System.Data;

namespace MiddlewareTool.Business.Interface
{
    public interface IProductFeedBusiness
    {        
        bool Import(DataTable dt, int timeOut);
    }
}
