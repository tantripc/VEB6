using System.Data;

namespace MiddlewareTool.Business.Interface
{
    public interface IHPriceChangeBusiness
    {
        bool Import(DataTable dt, int timeOut);
    }
}
