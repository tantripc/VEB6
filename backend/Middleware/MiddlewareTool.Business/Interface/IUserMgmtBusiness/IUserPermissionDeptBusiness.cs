using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static MiddlewareTool.Dto.UserMgmtDto;

namespace MiddlewareTool.Business.Interface
{
    public interface IUserPermissionDeptBusiness
    {
        Task<List<Guid>> GetListDepartmentByUserName(string userName);
        Task<List<Guid>> GetListUserIdByUserName(string userName);
        Task<UserPermissionDeptDto> GetByUserId(Guid userId);
        Task<List<UserPermissionDeptDto>> GetListByUserId(Guid userId);
        Task<bool> InsertOrDeleteAsync(Guid userId, List<Guid> lstDeptId, string userName);
        Task<bool> InsertAsync(Guid userId, List<Guid> lstDeptId, string userName);
        Task<bool> UpdateAsync(UserPermissionDeptDto dto);
        Task<bool> DeleteAsync(Guid userId);
    }
}
