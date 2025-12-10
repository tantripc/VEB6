using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static MiddlewareTool.Dto.CategoryMgmtDto;

namespace MiddlewareTool.Business.Interface
{
    public interface IGroupMasterBusiness
    {
        Task<Tuple<int, List<GroupMasterDto>>> GetPagingAsync(string userName, string keyWord, int? divisionId, int pageIndex, int pageSize);
        Task<List<GroupMasterDto>> GetAllGroupMaster();
        Task<GroupMasterDto> GetByIdAsync(int id);
        Task<bool> InsertAsync(GroupMasterDto dto);
        Task<bool> UpdateAsync(GroupMasterDto dto);
        Task<bool> DeleteAsync(int id, string userName);
        Task<bool> IsExistAsync(int id);
        Task<List<GroupMasterDto>> GetByDivisionIdAsync(int divisionId);
        Task<bool> ExistByDivisionId(int id);
    }
}
