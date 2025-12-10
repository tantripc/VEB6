using MiddlewareTool.Entities;
using MiddlewareTool.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using static MiddlewareTool.Dto.UserMgmtDto;
using UserRole = MiddlewareTool.Entities.UserRole;

namespace MiddlewareTool.Business.Concrete
{
    public class UserRoleBusiness : BaseBusiness
    {
        public UserRoleBusiness(IUnitOfWork unitOfWork) : base(unitOfWork) { }
        public IQueryable<UserRole> GetAll()
        {
            return this.UnitOfWork.GetAll<UserRole>().Where(x => x.ActiveFlag != STATUS_DELETE);
        }
        public async Task<List<UserRoleDto>> GetByUserName(string user)
        {
            try
            {
                if (!string.IsNullOrEmpty(user))
                {
                    return await this.UnitOfWork.GetAll<UserInfo>()
                        .Where(x => x.UserId == user && x.ActiveFlag == STATUS_ACTIVE)
                        .Join(this.UnitOfWork.GetAll<UserRole>()
                        .Where(x => x.ActiveFlag == STATUS_ACTIVE),
                            u => u.Id,
                            r => r.UserId,
                            (u, r) => new { u.UserId, r.RoleId })
                        .Select(x => new UserRoleDto
                        {
                            UserId = x.UserId,
                            RoleId = x.RoleId
                        })
                        .ToListAsync();
                }
            }
            catch (Exception ex)
            {
                LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
            }
            return new List<UserRoleDto>();
        }
        public async Task<bool> InsertToList(Guid userId, List<Guid> lstRoleId, string userName)
        {
            var result = false;
            try
            {
                var lstEntity = new List<UserRole>();
                foreach (var item in lstRoleId)
                {
                    lstEntity.Add(new UserRole()
                    {
                        Id = Guid.NewGuid(),
                        UserId = userId,
                        RoleId = item,
                        CreateBy = userName,
                        UpdateBy = userName,
                        CreateDate = DateTime.Now,
                        UpdateDate = DateTime.Now
                    });
                }
                if (lstEntity.Count > 0)
                {
                    var data = await this.UnitOfWork.InsertToListAsync(lstEntity);
                    if (data != null && data?.Count > 0) { result = true; }
                }
            }
            catch (Exception ex) { LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return result;
        }
        public async Task<bool> DeleteByUserId(Guid userId)
        {
            bool result = false;
            try
            {
                var iquery = this.UnitOfWork.GetAll<UserRole>()
                    .Where(x => x.UserId == userId && x.ActiveFlag == STATUS_ACTIVE)
                    .ToList();
                if (iquery.Count > 0)
                {
                    result = await this.UnitOfWork.DeleteToListAsync(iquery, true);
                }
                else { result = true; }
            }
            catch (Exception ex)
            {
                result = false;
                LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
            }
            return result;
        }
        public async Task<bool> InsertOrDeleteAsync(Guid userId, List<Guid> lstRoleId, string userName)
        {
            bool result = false;
            try
            {
                result = await this.DeleteByUserId(userId);
                if (result && lstRoleId.Count > 0) { result = await this.InsertToList(userId, lstRoleId, userName); }
            }
            catch (Exception ex)
            {
                result = false;
                this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
            }
            return result;
        }
    }
}
