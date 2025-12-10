using AutoMapper;
using MiddlewareTool.Business.Interface;
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
    public class UserPermissionDeptBusiness : BaseBusiness, IUserPermissionDeptBusiness
    {
        public UserPermissionDeptBusiness(IUnitOfWork unitOfWork) : base(unitOfWork) { }
        public async Task<List<Guid>> GetListDepartmentByUserName(string userName)
        {
            var results = new List<Guid>();
            try
            {
                var data = await this.UnitOfWork.GetAll<UserInfo>()
                        .Where(x => x.UserId == userName && x.ActiveFlag == STATUS_ACTIVE)
                    .Join(this.UnitOfWork.GetAll<UserPermissionDept>()
                        .Where(x => x.ActiveFlag == STATUS_ACTIVE),
                        u => u.Id,
                        upd => upd.UserId,
                        (u, upd) => new { u, upd.DepartmentId }.DepartmentId)
                    .ToListAsync();
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return results;
        }
        public async Task<List<Guid>> GetListUserIdByUserName(string userName)
        {
            try
            {
                var data = await this.UnitOfWork.GetAll<UserInfo>()
                        .Where(x => x.UserId == userName && x.ActiveFlag == STATUS_ACTIVE)
                    .Join(this.UnitOfWork.GetAll<UserPermissionDept>()
                        .Where(x => x.ActiveFlag == STATUS_ACTIVE),
                        u => u.Id,
                        upd => upd.UserId,
                        (u, upd) => new { u, upd.DepartmentId }.DepartmentId)
                    .ToListAsync();
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return new List<Guid>();
        }
        public async Task<UserPermissionDeptDto> GetByUserId(Guid userId)
        {
            try
            {
                var iquery = await this.UnitOfWork.GetSingleAsync<UserPermissionDept>(x => x.Id == userId && x.ActiveFlag == STATUS_ACTIVE);
                if (iquery != null)
                {
                    return Mapper.Map<UserPermissionDeptDto>(iquery);
                }
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return null;
        }
        public async Task<List<UserPermissionDeptDto>> GetListByUserId(Guid userId)
        {
            try
            {
                return await this.UnitOfWork.GetAll<UserPermissionDept>()
                    .Where(x => x.UserId == userId && x.ActiveFlag == STATUS_ACTIVE)
                    .Join(this.UnitOfWork.GetAll<Department>()
                    .Where(x => x.ActiveFlag == STATUS_ACTIVE),
                        ud => ud.DepartmentId,
                        d => d.Id,
                        (ud, d) => new { ud, d })
                    .OrderByDescending(x => x.d.Name)
                    .Select(x => new UserPermissionDeptDto
                    {
                        Id = x.d.Id,
                        DepartmentName = x.d.Name
                    })
                    .ToListAsync();
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return new List<UserPermissionDeptDto>();
        }
        public async Task<bool> InsertAsync(Guid userId, List<Guid> lstDeptId, string userName)
        {
            bool result = false;
            try
            {
                var LstEntity = new List<UserPermissionDept>();
                foreach (var item in lstDeptId)
                {
                    LstEntity.Add(new UserPermissionDept
                    {
                        Id = Guid.NewGuid(),
                        UserId = userId,
                        DepartmentId = item,
                        CreateDate = DateTime.Now,
                        UpdateDate = DateTime.Now,
                        CreateBy = userName,
                        UpdateBy = userName
                    });
                }
                var add = await this.UnitOfWork.InsertToListAsync(LstEntity);
                if (add != null) { result = true; }
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return result;
        }
        public async Task<bool> InsertOrDeleteAsync(Guid userId, List<Guid> LstDeptId, string userName)
        {
            bool result = false;
            try
            {
                result = await this.DeleteAsync(userId);
                if (result && LstDeptId.Count > 0) { result = await this.InsertAsync(userId, LstDeptId, userName); }
            }
            catch (Exception ex)
            {
                result = false;
                this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
            }
            return result;
        }
        public async Task<bool> UpdateAsync(UserPermissionDeptDto dto)
        {
            bool result = false;
            try
            {
                var entity = this.UnitOfWork.GetSingle<UserPermissionDept>(x => x.UserId == dto.UserId && x.ActiveFlag == STATUS_ACTIVE);
                if (entity != null)
                {
                    entity.DepartmentId = dto.DepartmentId;
                    entity.UpdateDate = DateTime.Now;
                    entity.UpdateBy = dto.UpdateBy;
                    result = await this.UnitOfWork.UpdateAsync(entity);
                }
            }
            catch (Exception ex) { LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return result;
        }
        public async Task<bool> DeleteAsync(Guid userId)
        {
            bool result = false;
            try
            {
                var lstDel = this.UnitOfWork.GetAll<UserPermissionDept>()
                    .Where(x => x.UserId == userId && x.ActiveFlag == STATUS_ACTIVE)
                    .ToList();
                if (lstDel.Count > 0)
                {
                    result = await this.UnitOfWork.DeleteToListAsync(lstDel, true);
                }
                else { result = true; }
            }
            catch (Exception ex) { LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return result;
        }
    }
}
