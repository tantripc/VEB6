using MiddlewareTool.Common;
using MiddlewareTool.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using static MiddlewareTool.Dto.UserMgmtDto;

namespace MiddlewareTool.Business.Concrete
{
    public sealed class SingletonBusiness : IDisposable
    {
        private readonly MiddlewareToolEntities db = new MiddlewareToolEntities();
        private static readonly Lazy<SingletonBusiness> lazy = new Lazy<SingletonBusiness>(() => new SingletonBusiness());
        public static SingletonBusiness Instance { get { return lazy.Value; } }
        public SingletonBusiness() { }
        private const byte STATUS_ACTIVE = (byte)AppValue.ActiveFlag.Active;
        public List<MenuDto> GetPermission(string userName)
        {
            var lstDto = new List<MenuDto>();
            try
            {
                if (!string.IsNullOrEmpty(userName))
                {
                    //get role userName
                    var lstTypeRole = db.UserInfos
                        .Where(x => x.UserId == userName && x.ActiveFlag == STATUS_ACTIVE)
                        .Join(db.UserRoles.Where(x => x.ActiveFlag == STATUS_ACTIVE),
                            u => u.Id,
                            r => r.UserId,
                            (u, r) => new { r.RoleId })
                        .Join(db.Roles.Where(x => x.ActiveFlag == STATUS_ACTIVE),
                            usrRole => usrRole.RoleId,
                            role => role.Id,
                            (usrRole, role) => new { usrRole, role.Type })
                        .Select(x => (AppType.UserRole)x.Type)
                        .ToList();
                    //Get all menu
                    var iquery = db.Menus.Where(x => x.ActiveFlag == STATUS_ACTIVE);
                    //Check role user
                    if (lstTypeRole.Contains(AppType.UserRole.SuperAdmin))
                    {
                        //Add all menu            
                        lstDto = iquery.Select(x => new MenuDto
                        {
                            Controller = x.Controller,
                            Action = x.Action
                        }).ToList();
                    }
                    else
                    {
                        //Add menu by role
                        var iqueryRole = db.Roles
                            .Where(x => lstTypeRole.Contains((AppType.UserRole)x.Type)
                                    && x.ActiveFlag == STATUS_ACTIVE)
                           .Join(db.MenuRoles.Where(x => x.ActiveFlag == STATUS_ACTIVE && x.Type == (byte)AppMenu.Role.Menu),
                               r => r.Id,
                               mr => mr.RoleId,
                               (r, mr) => new { r, mr }.mr)
                           .Join(db.Menus.Where(x => x.ActiveFlag == STATUS_ACTIVE),
                               mr => mr.MenuId,
                               m => m.Id,
                               (mr, m) => new { mr, m }.m)
                           .ToList();
                        lstDto = iqueryRole.Select(x => new MenuDto
                        {
                            Controller = x.Controller,
                            Action = x.Action
                        }).ToList();
                        //Append Role is public
                        var iqueryRoleAllowAnonymous = iquery
                            .Where(x => x.MenuStatus == (byte)AppMenu.Status.AllowAnonymous
                                || x.MenuStatus == (byte)AppMenu.Status.Public)
                            .Select(x => new MenuDto
                            {
                                Controller = x.Controller,
                                Action = x.Action
                            })
                            .ToList();
                        if (iqueryRoleAllowAnonymous.Count > 0)
                        {
                            lstDto.AddRange(iqueryRoleAllowAnonymous);
                        }
                    }
                }
            }
            catch
            {
                return new List<MenuDto>();
            }
            return lstDto;
        }
        public void Dispose() => db.Dispose();
    }
}
