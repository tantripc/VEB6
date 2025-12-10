using MiddlewareTool.Business.Concrete;
using MiddlewareTool.Common;
using MiddlewareTool.Entities;
using MiddlewareTool.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using static MiddlewareTool.Dto.SystemMgmtDto;
using static MiddlewareTool.Dto.UserMgmtDto;

namespace MiddlewareTool.Business.Concrete
{
    public class MenuRoleBusiness : BaseBusiness
    {
        #region Variables
        #endregion

        #region Constructors
        public MenuRoleBusiness(IUnitOfWork unitOfWork) : base(unitOfWork) { }
        #endregion

        #region Methods
        public async Task<List<MenuRoleDto>> GetByRoleId(Guid roleId)
        {
            try
            {
                return await this.UnitOfWork
                    .GetAll<MenuRole>()
                    .Where(x => x.RoleId == roleId && x.ActiveFlag == STATUS_ACTIVE)
                    .Select(x => new MenuRoleDto
                    {
                        MenuId = x.MenuId,
                        UserName = x.UserName,
                        MenuActionId = x.MenuActionId,
                        RoleId = x.RoleId,
                        Type = (AppMenu.Role)x.Type
                    })
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
                throw;
            }
        }
        public async Task<List<MenuRoleDto>> GetByListRoleId(List<Guid> lstId)
        {
            try
            {
                return await this.UnitOfWork
                    .GetAll<MenuRole>()
                    .Where(x => lstId.Contains(x.RoleId) && x.ActiveFlag == STATUS_ACTIVE)
                    .Select(x => new MenuRoleDto
                    {
                        MenuId = x.MenuId,
                        UserName = x.UserName,
                        MenuActionId = x.MenuActionId,
                        RoleId = x.RoleId,
                        Type = (AppMenu.Role)x.Type
                    })
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
                throw;
            }
        }
        public async Task<bool> DeleteByRoleId(Guid roleId)
        {
            bool result = false;
            try
            {
                var iquery = await this.UnitOfWork.GetAll<MenuRole>().Where(x => x.RoleId == roleId).ToListAsync();
                if (iquery.Count > 0) { result = await this.UnitOfWork.DeleteToListAsync(iquery, true); }
                else { result = true; }
            }
            catch (Exception ex)
            {
                this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
            }
            return result;
        }
        public async Task<bool> Insert(Guid roleId, List<Guid> lstMenuID)
        {
            bool result = false;
            try
            {
                if (lstMenuID.Count > 0 && roleId != Guid.Empty)
                {
                    var iqueryAction = await this.UnitOfWork
                        .GetAll<MenuAction>()
                        .Where(x => lstMenuID.Contains(x.Id) && x.ActiveFlag == STATUS_ACTIVE)
                        .ToListAsync();
                    var lstEntity = new List<MenuRole>();
                    foreach (var item in lstMenuID)
                    {
                        var dataAction = iqueryAction.FirstOrDefault(x => x.Id == item);
                        if (dataAction != null)
                        {
                            #region Add menu action                       
                            lstEntity.Add(new MenuRole
                            {
                                Id = Guid.NewGuid(),
                                RoleId = roleId,
                                MenuId = dataAction.MenuId,
                                MenuActionId = item,
                                CreateDate = DateTime.Now,
                                UpdateDate = DateTime.Now,
                                Type = (byte)AppMenu.Role.Action
                            });
                            #endregion
                        }
                        else
                        {
                            #region Add menu
                            lstEntity.Add(new MenuRole
                            {
                                Id = Guid.NewGuid(),
                                RoleId = roleId,
                                MenuId = item,
                                CreateDate = DateTime.Now,
                                UpdateDate = DateTime.Now,
                                Type = (byte)AppMenu.Role.Menu
                            });
                            #endregion
                        }
                    }
                    var data = await this.UnitOfWork.InsertToListAsync(lstEntity);
                    if (data?.Count > 0) { result = true; }
                }
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return result;
        }
        public async Task<bool> Update(RoleDto dto, List<Guid> lstMenuID)
        {
            bool result = false;
            var dbtransaction = this.UnitOfWork.BeginTransaction();
            try
            {
                var upRole = this.UnitOfWork.GetSingle<Role>(x => x.Id == dto.Id);
                if (upRole != null)
                {
                    //Update Role
                    upRole.Name = dto.Name;
                    upRole.Type = dto.Type;
                    upRole.Description = dto.Description;
                    upRole.UpdateDate = DateTime.Now;
                    upRole.UpdateBy = dto.UpdateBy;
                    result = await this.UnitOfWork.UpdateAsync(upRole);
                    if (result)
                    {
                        //Delete menu Role
                        result = await this.DeleteByRoleId(dto.Id);
                        //Insert Menu Role
                        if (result) { result = await this.Insert(dto.Id, lstMenuID); }
                    }
                }
                if (result) { dbtransaction.Commit(); }
                else { dbtransaction.Rollback(); }
            }
            catch (Exception ex)
            {
                dbtransaction.Rollback();
                this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
            }
            return result;
        }
        #endregion
    }
}
