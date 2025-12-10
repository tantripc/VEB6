using MiddlewareTool.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static MiddlewareTool.Dto.CategoryMgmtDto;
using static MiddlewareTool.Dto.UserMgmtDto;

namespace MiddlewareTool.Business.Interface
{
    public interface ILineMasterBusiness
    {
        Task<Tuple<int, List<LineMasterDto>>> GetPagingAsync(string userName, string keyWord, int pageIndex, int pageSize);
        Task<List<LineMasterDto>> GetAllLineMaster();
        Task<LineMasterDto> GetByIdAsync(int id);
        Task<bool> InsertAsync(LineMasterDto dto);
        Task<bool> UpdateAsync(LineMasterDto dto);
        Task<bool> DeleteAsync(int id, string userName);
        Task<bool> IsExistAsync(int id);
    }
}
