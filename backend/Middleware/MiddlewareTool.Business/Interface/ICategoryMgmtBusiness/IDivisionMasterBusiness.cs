using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static MiddlewareTool.Dto.CategoryMgmtDto;

namespace MiddlewareTool.Business.Interface
{
    public interface IDivisionMasterBusiness
    {
        Task<Tuple<int, List<DivisionMasterDto>>> GetPagingAsync(string userName, string keyWord, int? lineId, int pageIndex, int pageSize);
        Task<List<DivisionMasterDto>> GetAllDivisionMaster();
        Task<DivisionMasterDto> GetByIdAsync(int id);
        Task<bool> InsertAsync(DivisionMasterDto dto);
        Task<bool> UpdateAsync(DivisionMasterDto dto);
        Task<bool> DeleteAsync(int id, string userName);
        Task<bool> IsExistAsync(int id);
        Task<List<DivisionMasterDto>> GetByLineIdAsync(int lineId);
        Task<bool> ExistByLineId(int id);
    }
}
