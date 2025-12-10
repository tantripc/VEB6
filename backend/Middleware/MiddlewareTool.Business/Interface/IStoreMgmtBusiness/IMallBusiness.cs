using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static MiddlewareTool.Dto.StoreMgmtDto;

namespace MiddlewareTool.Business.Interface
{
    public interface IMallBusiness
    {
        Task<Tuple<int, List<MallDto>>> GetPagingAsync(string userName, string keyWord, int pageIndex, int pageSize);
        Task<List<MallDto>> GetAllMall();
        List<MallDto> GetAllNoTracking();
        Task<MallDto> GetByIdAsync(Guid id);
        Task<bool> InsertAsync(MallDto dto);
        Task<bool> UpdateAsync(MallDto dto);
        Task<bool> DeleteAsync(Guid id, string userName);
        Task<bool> IsExistAsync(Guid id);
        Task<int> IsExistMallId(Guid? id, string code);
    }
}
