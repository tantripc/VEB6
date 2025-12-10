using System.Collections.Generic;
using static MiddlewareTool.Dto.SaleDto;

namespace MiddlewareTool.Business.Concrete
{
    public interface IRefundSkuBusiness
    {
        List<string> GetAllSku();
    }
}
