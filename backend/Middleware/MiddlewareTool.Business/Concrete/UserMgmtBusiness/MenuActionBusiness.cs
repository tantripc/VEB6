using MiddlewareTool.Common;
using MiddlewareTool.Entities;
using MiddlewareTool.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using static MiddlewareTool.Dto.UserMgmtDto;

namespace MiddlewareTool.Business.Concrete
{
    public class MenuActionBusiness : BaseBusiness
    {
        #region Variables

        #endregion

        #region Constructors
        public MenuActionBusiness(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        #endregion

        #region Methods
        public IQueryable<MenuAction> GetAll()
        {
            try
            {
                return this.UnitOfWork.GetAll<MenuAction>().Where(x => x.ActiveFlag == STATUS_ACTIVE);
            }
            catch (Exception ex)
            {
                this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
                throw;
            }
        }
        public async Task<List<MenuActionDto>> GetListByMenu(List<string> lstRoleName, string ctrl, string action)
        {
            var result = new List<MenuActionDto>();
            try
            {
                if (!string.IsNullOrEmpty(ctrl) && !string.IsNullOrEmpty(action))
                {
                    if (lstRoleName.Any(x => x == AppType.UserRole.Admin.ToString()))
                    {
                        result = await this.UnitOfWork
                            .GetAll<MenuAction>()
                            .Where(x => x.MenuController.ToLower() == ctrl.ToLower()
                                && x.MenuActionName.ToLower() == action
                                && x.ActiveFlag == STATUS_ACTIVE)
                            .Select(x => new MenuActionDto()
                            {
                                MenuId = x.MenuId,
                                Controller = x.Controller,
                                Action = x.Action,
                                ResourceID = x.ResourceID,
                                Icon = x.Icon,
                                NameEN = x.NameEN,
                                NameVI = x.NameVI,
                                Type = (AppMenu.Action)x.Type,
                                MenuActionName = x.MenuActionName,
                                MenuController = x.MenuController
                            })
                            .ToListAsync();
                    }
                    else
                    {
                        result = await this.UnitOfWork
                            .GetAll<Role>()
                            .Where(x => lstRoleName.Contains(x.Name))
                            .Join(this.UnitOfWork.GetAll<MenuRole>()
                            .Where(x => x.ActiveFlag == STATUS_ACTIVE && x.Type == (byte)AppMenu.Role.Action),
                                r => r.Id,
                                mr => mr.RoleId,
                                (r, mr) => new { r, mr }.mr)
                            .Join(this.UnitOfWork.GetAll<MenuAction>()
                            .Where(x => x.MenuController.ToLower() == ctrl.ToLower()
                                && x.MenuActionName.ToLower() == action
                                && x.ActiveFlag == STATUS_ACTIVE),
                                mr => mr.MenuActionId,
                                m => m.Id,
                                (mr, m) => new { mr, m }.m)
                            .Select(x => new MenuActionDto()
                            {
                                MenuId = x.MenuId,
                                Controller = x.Controller,
                                Action = x.Action,
                                ResourceID = x.ResourceID,
                                Icon = x.Icon,
                                NameEN = x.NameEN,
                                NameVI = x.NameVI,
                                Type = (AppMenu.Action)x.Type,
                                MenuController = x.MenuController,
                                MenuActionName = x.MenuActionName
                            })
                            .ToListAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
            }
            return result.Distinct().ToList();
        }
        public async Task<MenuActionDto> GetActionByRole(List<string> lstRoleName, string ctrl, string action)
        {
            try
            {
                if (!string.IsNullOrEmpty(ctrl) && !string.IsNullOrEmpty(action))
                {
                    if (lstRoleName.Any(x => x == AppType.UserRole.Admin.ToString()))
                    {
                        return await this.UnitOfWork
                            .GetAll<MenuAction>()
                            .Where(x => x.Controller.ToLower() == ctrl.ToLower()
                                && x.Action.ToLower() == action
                                && x.ActiveFlag == STATUS_ACTIVE)
                            .Select(x => new MenuActionDto()
                            {
                                MenuId = x.MenuId,
                                Controller = x.Controller,
                                Action = x.Action,
                                ResourceID = x.ResourceID,
                                Icon = x.Icon,
                                NameEN = x.NameEN,
                                NameVI = x.NameVI,
                                Type = (AppMenu.Action)x.Type
                            })
                            .FirstOrDefaultAsync();
                    }
                    else
                    {
                        return await this.UnitOfWork
                            .GetAll<Role>()
                            .Where(x => lstRoleName.Contains(x.Name))
                            .Join(this.UnitOfWork.GetAll<MenuRole>()
                            .Where(x => x.ActiveFlag == STATUS_ACTIVE && x.Type == (byte)AppMenu.Role.Action),
                                r => r.Id,
                                mr => mr.RoleId,
                                (r, mr) => new { r, mr }.mr)
                            .Join(this.UnitOfWork.GetAll<MenuAction>()
                            .Where(x => x.Controller.ToLower() == ctrl.ToLower()
                                && x.Action.ToLower() == action
                                && x.ActiveFlag == STATUS_ACTIVE),
                                mr => mr.MenuActionId,
                                m => m.Id,
                                (mr, m) => new { mr, m }.m)
                            .Select(x => new MenuActionDto()
                            {
                                MenuId = x.MenuId,
                                Controller = x.Controller,
                                Action = x.Action,
                                ResourceID = x.ResourceID,
                                Icon = x.Icon,
                                NameEN = x.NameEN,
                                NameVI = x.NameVI,
                                Type = (AppMenu.Action)x.Type
                            })
                            .FirstOrDefaultAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
            }
            return null;
        }

        #endregion
    }
}
