using MiddlewareTool.Business.Interface;
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
    public class MenuBusiness : BaseBusiness, IMenuBusiness
    {
        #region Variables
        #endregion

        #region Constructors
        public MenuBusiness(IUnitOfWork unitOfWork) : base(unitOfWork) { }
        #endregion

        #region Methods
        public IQueryable<Menu> GetAll()
        {
            try
            {
                return this.UnitOfWork.GetAll<Menu>().Where(x => x.ActiveFlag == STATUS_ACTIVE);
            }
            catch (Exception ex)
            {
                this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
                throw;
            }
        }
        public IQueryable<Menu> GetListMenuByRole(List<string> lstRoleName)
        {
            try
            {
                return this.UnitOfWork
                    .GetAll<Role>()
                    .Where(x => lstRoleName.Contains(x.Name) && x.ActiveFlag == STATUS_ACTIVE)
                    .Join(this.UnitOfWork
                            .GetAll<MenuRole>()
                            .Where(x => x.ActiveFlag == STATUS_ACTIVE && x.Type == (byte)AppMenu.Role.Menu),
                                r => r.Id,
                                mr => mr.RoleId,
                                (r, mr) => new { r, mr }.mr)
                    .Join(this.UnitOfWork
                            .GetAll<Menu>()
                            .Where(x => x.ActiveFlag == STATUS_ACTIVE),
                                mr => mr.MenuId,
                                m => m.Id,
                                (mr, m) => new { mr, m }.m);
            }
            catch (Exception ex)
            {
                this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
                throw;
            }
        }
        public IQueryable<Menu> GetListSubMenu(List<Guid> lstMenuId)
        {
            try
            {
                return this.UnitOfWork
                    .GetAll<Menu>()
                    .Where(x => lstMenuId.Any(y => y == x.ParentId)
                        && x.ParentId != null 
                        && x.ActiveFlag == STATUS_ACTIVE);
            }
            catch (Exception ex)
            {
                this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
                throw;
            }
        }
        public async Task<List<NameMenuRoleDto>> GetListMenuByRoleId(List<Guid> lstRole)
        {
            try
            {
                return await this.UnitOfWork
                    .GetAll<Role>()
                    .Where(x => lstRole.Contains(x.Id))
                    .Join(this.UnitOfWork
                            .GetAll<MenuRole>()
                            .Where(x => x.ActiveFlag == STATUS_ACTIVE),
                                 r => r.Id,
                                 mr => mr.RoleId,
                                 (r, mr) => new { r, mr }.mr)
                    .Join(this.UnitOfWork
                            .GetAll<Menu>()
                            .Where(x => x.ActiveFlag == STATUS_ACTIVE),
                                 mr => mr.MenuId,
                                 m => m.Id,
                                 (mr, m) => new { mr, m })
                    .Select(x => new NameMenuRoleDto()
                     {
                         RoleId = x.mr.RoleId,
                         Order = x.m.OrderNumber
                     })
                    .OrderBy(x => x.Order)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
                throw;
            }
        }
        public async Task<bool> Insert(List<Menu> lstEntity)
        {
            var result = false;
            try
            {
                var iquery = await this.UnitOfWork.InsertToListAsync(lstEntity);
                result = iquery?.Count > 0;
            }
            catch (Exception ex)
            {
                this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
                throw;
            }
            return result;
        }
        #endregion
    }
}
