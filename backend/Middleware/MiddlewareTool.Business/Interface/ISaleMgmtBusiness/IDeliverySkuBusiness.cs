using System.Collections.Generic;
using System.Threading.Tasks;
using static MiddlewareTool.Dto.SaleDto;

namespace MiddlewareTool.Business.Concrete
{
    public interface IDeliverySkuBusiness
    {
        List<string> GetAllSku();
    }
}
