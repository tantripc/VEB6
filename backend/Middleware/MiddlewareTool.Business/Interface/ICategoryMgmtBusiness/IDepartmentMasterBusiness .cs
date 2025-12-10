using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static MiddlewareTool.Dto.CategoryMgmtDto;

namespace MiddlewareTool.Business.Interface
{
    public interface IDepartmentMasterBusiness
    {
        Task<Tuple<int, List<DepartmentMasterDto>>> GetPagingAsync(string userName, string keyWord, int? groupId, int pageIndex, int pageSize);   
        Task<List<DepartmentMasterDto>> GetAllDepartmentMaster();
        Task<DepartmentMasterDto> GetByIdAsync(int id);
        Task<bool> InsertAsync(DepartmentMasterDto dto);
        Task<bool> UpdateAsync(DepartmentMasterDto dto);
        Task<bool> DeleteAsync(int id, string userName);
        Task<bool> IsExistAsync(int id);
        Task<List<DepartmentMasterDto>> GetDepartmentByGrouptId(int groupId);
        Task<bool> ExistByGroupId(int id);
    }
}
