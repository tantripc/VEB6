using MiddlewareTool.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareTool.Business.Interface
{
    public interface IT4VVBusiness
    {
        List<T4VVDtoPaging> GetPaging(T4VVDtoSearchModel searchModel);
        Task<List<T4VVDto>> GetDetailBySKU(T4VVDtoSearchModel searchModel);
        Tuple<List<T4VVHistoryDto>, int> GetHistoryPaging(T4VVDtoSearchModel searchModel);
    }
}
