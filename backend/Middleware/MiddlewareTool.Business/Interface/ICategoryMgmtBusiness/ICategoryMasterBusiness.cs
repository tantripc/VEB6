using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using static MiddlewareTool.Dto.CategoryMgmtDto;

namespace MiddlewareTool.Business.Interface
{
    public interface ICategoryMasterBusiness
    {
        Task<Tuple<int, List<CategoryMasterDto>>> GetPagingAsync(string userName, string keyWord, int? departmentId, int pageIndex, int pageSize);
        Task<CategoryMasterDto> GetByIdAsync(string id);
        Task<List<CategoryMasterDto>> GetByIdsAsync(IList<string> ids);
        Task<bool> InsertAsync(CategoryMasterDto dto);
        Task<bool> UpdateAsync(CategoryMasterDto dto);
        Task<bool> DeleteAsync(string id, string userName);
        Task<bool> IsExistAsync(int id);
        Task<DivisionMasterDto> GetByLineIdAsync(int lineId);
        bool Import(DataTable dt, int timeOut);
        Task<bool> ExistByDepId(int depId);
    }
}
