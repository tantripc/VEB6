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
    public interface IUserMgmtBusiness
    {
        #region MenuAction
        IQueryable<MenuAction> MenuAction_GetAll();
        Task<List<MenuActionDto>> MenuAction_GetListByMenu(List<string> lstRoleName, string ctrl, string action);
        Task<MenuActionDto> MenuAction_GetActionByRole(List<string> lstRoleName, string ctrl, string action);

        #endregion

        #region Menu
        IQueryable<Menu> Menu_GetAll();
        IQueryable<Menu> Menu_GetListMenuByRole(List<string> lstRoleName);
        IQueryable<Menu> Menu_GetListSubMenu(List<Guid> lstMenuId);
        Task<List<NameMenuRoleDto>> Menu_GetListMenuByRoleId(List<Guid> lstRole);
        Task<bool> Menu_Insert(List<Menu> lstEntity);

        #endregion

        #region MenuRole
        Task<List<MenuRoleDto>> MenuRole_GetByRoleId(Guid roleId);
        Task<List<MenuRoleDto>> MenuRole_GetByListRoleId(List<Guid> lstId);
        Task<bool> MenuRole_DeleteByRoleId(Guid roleId);
        Task<bool> MenuRole_Insert(Guid roleId, List<Guid> lstMenuId);
        Task<bool> MenuRole_Update(RoleDto dto, List<Guid> lstMenuId);

        #endregion

        #region Role
        List<string> Role_GetListRoleByUser(string user);
        #endregion

        #region UserRole
        Task<List<UserRoleDto>> UserRole_GetByUserName(string user);
        #endregion

        #region UserDept
        Task<bool> InsertOrDeleteAsync(Guid userId, List<Guid> lstDeptId);
        #endregion
    }
}
