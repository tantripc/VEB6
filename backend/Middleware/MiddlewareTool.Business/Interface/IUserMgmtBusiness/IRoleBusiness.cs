using MiddlewareTool.Common;
using MiddlewareTool.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static MiddlewareTool.Dto.SystemMgmtDto;
using static MiddlewareTool.Dto.UserMgmtDto;

namespace MiddlewareTool.Business.Interface
{
    public interface IRoleBusiness
    {
        IQueryable<Role> GetAll();
        Task<Tuple<int, List<RoleDto>>> GetPagingAsync(string userName, string keyWord, int pageIndex, int pageSize);
        List<string> GetListRoleByUser(string user);
        Task<RoleDto> GetByIdAsync(Guid id);
        Task<bool> InsertAsync(RoleDto dto, List<Guid> lstMenuID);
        Task<Tuple<bool, string>> DeleteAsync(Guid id, string user);
        Task<List<RoleByUserDto>> GetRoleByToListUserAsync(List<string> lstUser);
        Task<List<MenuRoleDto>> GetRoleMenuByUserAsync(string user);
        List<MenuActionCompactDto> GetRoleMenuActionByUserAsync(string user);
        Task<List<RoleDto>> GetByPermissionAsync(string user);
    }
}
