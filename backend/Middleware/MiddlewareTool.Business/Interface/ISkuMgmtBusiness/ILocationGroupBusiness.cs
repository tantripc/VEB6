using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static MiddlewareTool.Dto.SkuMappingMgmtDto;

namespace MiddlewareTool.Business.Interface
{
    public interface ILocationGroupBusiness
    {
        Task<List<LocationGroupDto>> GetAllAsync();
        List<LocationGroupDto> GetAllNoTracking();
        Task<Tuple<int, List<LocationGroupDto>>> GetPagingAsync(string keyWord, int pageIndex, int pageSize);
        Task<LocationGroupDto> GetByIdAsync(Guid id);
        Task<bool> InsertAsync(string user, LocationGroupDto dto);
        Task<bool> UpdateAsync(string user, LocationGroupDto dto);
        Task<bool> DeleteAsync(string user, Guid id);
    }
}
