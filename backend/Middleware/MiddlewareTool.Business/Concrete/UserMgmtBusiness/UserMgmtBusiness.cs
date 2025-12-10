using MiddlewareTool.Business.Interface;
using MiddlewareTool.Entities;
using MiddlewareTool.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static MiddlewareTool.Dto.SystemMgmtDto;
using static MiddlewareTool.Dto.UserMgmtDto;

namespace MiddlewareTool.Business.Concrete
{
    public class UserMgmtBusiness : BaseBusiness, IUserMgmtBusiness
    {
        #region Constructors        
        public UserMgmtBusiness(IUnitOfWork unitOfWork) : base(unitOfWork) {  }

        #endregion

        #region Methods

        #region MenuAction
        public IQueryable<MenuAction> MenuAction_GetAll()
        {
            return new MenuActionBusiness(this.UnitOfWork).GetAll();
        }
        public async Task<List<MenuActionDto>> MenuAction_GetListByMenu(List<string> lstRoleName, string ctrl, string action)
        {
            return await new MenuActionBusiness(this.UnitOfWork).GetListByMenu(lstRoleName, ctrl, action);
        }
        public async Task<MenuActionDto> MenuAction_GetActionByRole(List<string> lstRoleName, string ctrl, string action)
        {
            return await new MenuActionBusiness(this.UnitOfWork).GetActionByRole(lstRoleName, ctrl, action);
        }
        #endregion

        #region Menu
        public IQueryable<Menu> Menu_GetAll()
        {
            return new MenuBusiness(this.UnitOfWork).GetAll();
        }
        public IQueryable<Menu> Menu_GetListMenuByRole(List<string> lstRoleName)
        {
            return new MenuBusiness(this.UnitOfWork).GetListMenuByRole(lstRoleName);
        }
        public IQueryable<Menu> Menu_GetListSubMenu(List<Guid> lstMenuId)
        {
            return new MenuBusiness(this.UnitOfWork).GetListSubMenu(lstMenuId);
        }
        public async Task<List<NameMenuRoleDto>> Menu_GetListMenuByRoleId(List<Guid> lstRole)
        {
            return await new MenuBusiness(this.UnitOfWork).GetListMenuByRoleId(lstRole);
        }
        public async Task<bool> Menu_Insert(List<Menu> lstEntity)
        {
            return await new MenuBusiness(this.UnitOfWork).Insert(lstEntity);
        }
        #endregion

        #region MenuRole
        public async Task<List<MenuRoleDto>> MenuRole_GetByRoleId(Guid roleId)
        {
            return await new MenuRoleBusiness(this.UnitOfWork).GetByRoleId(roleId);
        }
        public async Task<List<MenuRoleDto>> MenuRole_GetByListRoleId(List<Guid> lstId)
        {
            return await new MenuRoleBusiness(this.UnitOfWork).GetByListRoleId(lstId);
        }
        public async Task<bool> MenuRole_DeleteByRoleId(Guid roleId)
        {
            return await new MenuRoleBusiness(this.UnitOfWork).DeleteByRoleId(roleId);
        }
        public async Task<bool> MenuRole_Insert(Guid roleId, List<Guid> lstMenuID)
        {
            return await new MenuRoleBusiness(this.UnitOfWork).Insert(roleId, lstMenuID);
        }
        public async Task<bool> MenuRole_Update(RoleDto dto, List<Guid> lstMenuID)
        {
            return await new MenuRoleBusiness(this.UnitOfWork).Update(dto, lstMenuID);
        }

        #endregion

        #region Role
        public List<string> Role_GetListRoleByUser(string user)
        {
            return new RoleBusiness(this.UnitOfWork).GetListRoleByUser(user);
        }
        #endregion

        #region User Role
        public async Task<List<UserRoleDto>> UserRole_GetByUserName(string user)
        {
            return await new UserRoleBusiness(this.UnitOfWork).GetByUserName(user);
        }

        #endregion

        #region UserDept
        public async Task<bool> InsertOrDeleteAsync(Guid userId, List<Guid> lstDeptId)
        {
            return await new UserDepartmentBusiness(this.UnitOfWork).InsertOrDeleteAsync(userId, lstDeptId);
        }
        #endregion

        #endregion
    }
}
