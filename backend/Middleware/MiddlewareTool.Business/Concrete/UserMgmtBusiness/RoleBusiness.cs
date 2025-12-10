using AutoMapper;
using MiddlewareTool.Business.Interface;
using MiddlewareTool.Common;
using MiddlewareTool.Entities;
using MiddlewareTool.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Threading.Tasks;
using static MiddlewareTool.Dto.SystemMgmtDto;
using static MiddlewareTool.Dto.UserMgmtDto;

namespace MiddlewareTool.Business.Concrete
{
    public class RoleBusiness : BaseBusiness, IRoleBusiness
    {
        #region Variables

        #endregion

        #region Constructors
        public RoleBusiness(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        #endregion

        #region Methods
        public IQueryable<Role> GetAll()
        {
            return this.UnitOfWork.GetAll<Role>().Where(x => x.ActiveFlag != STATUS_DELETE);
        }
        public async Task<Tuple<int, List<RoleDto>>> GetPagingAsync(string userName, string keyWord, int pageIndex, int pageSize)
        {
            int total = 0;
            try
            {
                var iquery = this.UnitOfWork.GetAll<Role>()
                    .Where(x => x.ActiveFlag == STATUS_ACTIVE);
                if (!string.IsNullOrEmpty(userName))
                {
                    iquery = iquery.Where(x => !string.IsNullOrEmpty(x.CreateBy) && x.CreateBy.ToUpper() == userName.ToUpper());
                }
                if (!string.IsNullOrEmpty(keyWord))
                {
                    var search = AppValue.ToUnsignString(keyWord);
                    iquery = iquery.Where(x => x.Name.Contains(search) || x.URL.Contains(search) || x.Description.Contains(search));
                }
                total = iquery.Count();
                var data = await iquery
                    .OrderByDescending(x => x.UpdateDate)
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .Select(x => new RoleDto
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Type = x.Type,
                        Description = x.Description,
                        CreateBy = x.CreateBy,
                        UpdateBy = x.UpdateBy,
                        UpdateDate = x.UpdateDate
                    })
                    .ToListAsync();
                if (data.Count > 0)
                {
                    #region Get menu name
                    var dataAction = this.UnitOfWork.GetAllNoTracking<MenuAction>().Where(x => x.ActiveFlag == STATUS_ACTIVE).OrderBy(x => x.OrderNumber).ToList();
                    var lstRoleID = data.Select(x => x.Id).ToList();
                    var dataMenu = await this.UnitOfWork
                        .GetAll<MenuRole>()
                        .Where(x => lstRoleID.Contains(x.RoleId) && x.ActiveFlag == STATUS_ACTIVE)
                        .Join(this.UnitOfWork
                                .GetAll<Menu>()
                                .Where(x => x.ActiveFlag == STATUS_ACTIVE),
                                mr => mr.MenuId,
                                m => m.Id,
                                (mr, m) => new { mr, m })
                        .Select(x => new NameMenuRoleDto()
                        {
                            RoleId = x.mr.RoleId,
                            FunctionName = x.m.NameVI,
                            Order = x.m.OrderNumber,
                            MenuId = x.m.Id,
                            MenuParentId = x.m.ParentId,
                            MenuActionId = x.mr.MenuActionId,
                        })
                        .OrderBy(x => x.Order)
                        .ToListAsync();

                    dataMenu.Where(x => x.MenuActionId.HasValue).ToList().ForEach(x =>
                    {
                        var resourceID = dataAction.FirstOrDefault(ac => ac.Id == x.MenuActionId)?.ResourceID;
                        var resource = this.UnitOfWork.GetItem<ResourceDto, Resource>(rs => rs.ResourceID == resourceID);
                        if (resource != null)
                            x.FunctionName = resource.DefaultText0;
                        else x.FunctionName = resourceID;
                    });

                    var dataMenuParent = this.UnitOfWork
                        .GetAll<Menu>()
                        .Where(x => x.ParentId == null && x.ActiveFlag == STATUS_ACTIVE)
                        .Select(x => new { x.Id, x.NameVI })
                        .ToList();
                    var lstUserUpdateByIds = data.Select(x => x.UpdateBy).ToList();
                    var lstUpdateBys = this.UnitOfWork.GetAll<UserInfo>()
                                    .Where(x => lstUserUpdateByIds.Contains(x.UserId) && x.ActiveFlag == STATUS_ACTIVE)
                                    .Select(x => new UsersDto()
                                    {
                                        Id = x.Id,
                                        UserName = x.UserId,
                                        Email = x.Email,
                                        FullName = x.FullName
                                    })
                                    .ToList();

                    foreach (var item in data)
                    {
                        if (item.Type != (byte)AppType.UserRole.SuperAdmin)
                        {
                            #region Menu name 
                            var lstMenuGroup = dataMenu
                                .Where(x => x.RoleId == item.Id && x.MenuParentId != null)
                                .GroupBy(x => x.MenuParentId)
                                .Select(x => new
                                {
                                    x.Key,
                                    Items = x.OrderBy(y => y.Order).Select(y => y.FunctionName).ToList(),
                                    ParentMenu = dataMenuParent.Where(y => y.Id == x.Key)
                                        .Select(y => y.NameVI + ": ")
                                        .FirstOrDefault()
                                })
                                .ToList();

                            //Sub
                            var lstMenuNameParent = dataMenu
                                .Where(x => x.RoleId == item.Id
                                    && x.MenuParentId == null
                                    && !lstMenuGroup.Any(y => y.Key == x.MenuId))
                                .Select(x => x.FunctionName)
                                .ToList();

                            //Parent                            
                            var lstMenuName = lstMenuGroup
                                .Select(x => string.Concat(x.ParentMenu, string.Join(", ", x.Items)))
                                .ToList();
                            item.DataMenu = string.Join(".; ", lstMenuNameParent) + string.Join(".; ", lstMenuName);
                            #endregion
                        }
                        item.UpdateByFullName = lstUpdateBys.Where(x => x.UserName.ToLower().Equals(item.UpdateBy.ToLower()))
                       .Select(x => x.FullName)
                       .FirstOrDefault();
                    }
                    #endregion
                    return new Tuple<int, List<RoleDto>>(total, data);
                }
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return new Tuple<int, List<RoleDto>>(total, new List<RoleDto>());
        }
        public List<string> GetListRoleByUser(string user)
        {
            try
            {
                return this.UnitOfWork
                    .GetAll<UserInfo>()
                    .Where(x => x.UserId == user && x.ActiveFlag == STATUS_ACTIVE)
                    .Join(this.UnitOfWork
                            .GetAll<UserRole>()
                            .Where(x => x.ActiveFlag == STATUS_ACTIVE),
                                u => u.Id,
                                ur => ur.UserId,
                                (u, ur) => new { ur.RoleId })
                    .Join(this.UnitOfWork
                            .GetAll<Role>()
                            .Where(x => x.ActiveFlag == STATUS_ACTIVE),
                                usrRole => usrRole.RoleId,
                                role => role.Id,
                                (usrRole, role) => new { usrRole, role.Name }.Name)
                    .ToList();
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return null;
        }
        public async Task<RoleDto> GetByIdAsync(Guid id)
        {
            try
            {
                var iquery = await this.UnitOfWork.GetSingleAsync<Role>(x => x.Id == id && x.ActiveFlag == STATUS_ACTIVE);
                if (iquery != null)
                {
                    return Mapper.Map<RoleDto>(iquery);
                }
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return null;
        }
        public async Task<bool> InsertAsync(RoleDto dto, List<Guid> lstMenuID)
        {
            var result = false;
            var dbtransaction = this.UnitOfWork.BeginTransaction();
            try
            {
                var entity = new Role
                {
                    Id = Guid.NewGuid(),
                    Name = dto.Name,
                    Type = dto.Type,
                    Description = dto.Description,
                    CreateBy = dto.CreateBy,
                    UpdateBy = dto.UpdateBy,
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    ActiveFlag = STATUS_ACTIVE
                };
                var iquery = await this.UnitOfWork.InsertAsync(entity);
                if (iquery != null)
                {
                    if (lstMenuID.Count > 0)
                    {
                        result = await new MenuRoleBusiness(this.UnitOfWork).Insert(entity.Id, lstMenuID);
                    }
                    else { result = true; }
                    if (result) { dbtransaction.Commit(); }
                    else { dbtransaction.Rollback(); }
                }
            }
            catch (Exception ex)
            {
                result = false;
                dbtransaction.Rollback();
                this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
            }
            return result;
        }
        public async Task<Tuple<bool, string>> DeleteAsync(Guid id, string user)
        {
            var result = false;
            string msg = "Data not found";
            try
            {
                var iquery = this.UnitOfWork.GetSingle<Role>(x => x.Id == id);
                if (iquery != null)
                {
                    iquery.UpdateBy = user;
                    iquery.UpdateDate = DateTime.Now;
                    iquery.ActiveFlag = STATUS_DELETE;
                    result = await this.UnitOfWork.UpdateAsync(iquery);
                    if (!result) { msg = "Error update"; }
                    else { msg = "Update successful"; }
                }
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return new Tuple<bool, string>(result, msg);
        }
        public async Task<List<RoleByUserDto>> GetRoleByToListUserAsync(List<string> lstUser)
        {
            try
            {
                return await this.UnitOfWork.GetAll<UserInfo>()
                    .Where(x => lstUser.Contains(x.UserId) && x.ActiveFlag == STATUS_ACTIVE)
                    .Join(this.UnitOfWork.GetAll<UserRole>()
                            .Where(x => x.ActiveFlag == STATUS_ACTIVE),
                                u => u.Id,
                                r => r.UserId,
                                (u, r) => new { r.RoleId, u.UserId })
                    .Join(this.UnitOfWork.GetAll<Role>()
                            .Where(x => x.ActiveFlag == STATUS_ACTIVE),
                                usrRole => usrRole.RoleId,
                                role => role.Id,
                                (usrRole, role) => new { usrRole, role })
                    .GroupBy(g => new { g.usrRole.UserId, g.usrRole.RoleId })
                    .Select(x => new RoleByUserDto
                    {
                        UserId = x.Key.UserId,
                        RoleId = x.Key.RoleId,
                        RoleName = x.Select(y => y.role.Name).FirstOrDefault(),
                        Type = x.Select(y => y.role.Type).FirstOrDefault()
                    })
                    .ToListAsync();
            }
            catch (Exception ex) { LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return new List<RoleByUserDto>();
        }

        public async Task<List<MenuRoleDto>> GetRoleMenuByUserAsync(string user)
        {
            try
            {
                return await this.UnitOfWork.GetAll<UserInfo>()
                    .Where(x => x.UserId == user && x.ActiveFlag == STATUS_ACTIVE)
                    .Join(this.UnitOfWork.GetAll<UserRole>()
                            .Where(x => x.ActiveFlag == STATUS_ACTIVE),
                                u => u.Id,
                                r => r.UserId,
                                (u, r) => new { r.RoleId, u.UserId })
                    .Join(this.UnitOfWork.GetAll<Role>()
                            .Where(x => x.ActiveFlag == STATUS_ACTIVE),
                                usrRole => usrRole.RoleId,
                                role => role.Id,
                                (usrRole, role) => new { usrRole, role })
                    .Join(this.UnitOfWork.GetAll<MenuRole>()
                    .Where(x => x.ActiveFlag == STATUS_ACTIVE),
                        r => r.role.Id,
                        mr => mr.RoleId,
                        (r, mr) => new { r, mr })
                    .Select(x => new MenuRoleDto
                    {
                        MenuId = x.mr.MenuId,
                        UserName = x.mr.UserName,
                        MenuActionId = x.mr.MenuActionId,
                        RoleId = x.mr.RoleId,
                        Type = (AppMenu.Role)x.mr.Type
                    })
                    .ToListAsync();
            }
            catch (Exception ex) { LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return new List<MenuRoleDto>();
        }
        public List<MenuActionCompactDto> GetRoleMenuActionByUserAsync(string user)
        {
            try
            {
                var menuActionIds = this.UnitOfWork.GetAll<UserInfo>()
                    .Where(x => x.UserId == user && x.ActiveFlag == STATUS_ACTIVE)
                    .Join(this.UnitOfWork.GetAll<UserRole>()
                            .Where(x => x.ActiveFlag == STATUS_ACTIVE),
                                u => u.Id,
                                r => r.UserId,
                                (u, r) => new { r.RoleId, u.UserId })
                    .Join(this.UnitOfWork.GetAll<Role>()
                            .Where(x => x.ActiveFlag == STATUS_ACTIVE),
                                usrRole => usrRole.RoleId,
                                role => role.Id,
                                (usrRole, role) => new { usrRole, role })
                    .Join(this.UnitOfWork.GetAll<MenuRole>()
                        .Where(x => x.ActiveFlag == STATUS_ACTIVE && x.MenuActionId.HasValue),
                            r => r.role.Id,
                            mr => mr.RoleId,
                            (r, mr) => new { r, mr })
                    .Select(x => x.mr.MenuActionId)
                    .ToList();
                if (menuActionIds != null)
                {
                    var menuActions = this.UnitOfWork.GetAllNoTracking<MenuAction>().Where(x => menuActionIds.Contains(x.Id)).Select(x => new MenuActionCompactDto()
                    {
                        MenuId = x.Id,
                        MenuController = x.MenuController,
                        MenuActionName = x.MenuActionName
                    }).ToList();
                    return menuActions;
                }
            }
            catch (Exception ex) { LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return new List<MenuActionCompactDto>();
        }
        public async Task<List<RoleDto>> GetByPermissionAsync(string user)
        {
            try
            {
                var iquery = this.UnitOfWork.GetAll<Role>().Where(x => x.ActiveFlag == STATUS_ACTIVE);
                if (!string.IsNullOrEmpty(user))
                {
                    var thisRole = await this.UnitOfWork.GetAll<UserInfo>()
                        .Where(x => x.UserId == user && x.ActiveFlag == STATUS_ACTIVE)
                        .Join(this.UnitOfWork.GetAll<UserRole>()
                                .Where(x => x.ActiveFlag == STATUS_ACTIVE),
                                    u => u.Id,
                                    r => r.UserId,
                                    (u, r) => new { r.RoleId, u.UserId }.RoleId)
                        .ToListAsync();
                    iquery = iquery.Where(x => x.CreateBy == user || thisRole.Contains(x.Id));
                }
                return iquery.Select(x => new RoleDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    CreateBy = x.CreateBy,
                    Type = x.Type
                }).ToList();
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return new List<RoleDto>();
        }
        #endregion
    }
}
