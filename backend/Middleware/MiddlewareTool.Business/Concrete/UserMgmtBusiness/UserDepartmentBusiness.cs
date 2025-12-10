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
    public class UserDepartmentBusiness : BaseBusiness, IUserDepartmentBusiness
    {
        public UserDepartmentBusiness(IUnitOfWork unitOfWork) : base(unitOfWork) { }
        public IQueryable<UserDepartment> GetAll()
        {
            return this.UnitOfWork.GetAll<UserDepartment>().Where(x => x.ActiveFlag != STATUS_DELETE);
        }
        public async Task<UserDepartmentDto> GetByUserId(Guid userId)
        {
            try
            {
                return await this.UnitOfWork.GetAll<UserDepartment>()
                    .Where(x => x.UserId == userId && x.ActiveFlag == STATUS_ACTIVE)
                    .OrderByDescending(x => x.UpdateDate)
                    .Select(x => new UserDepartmentDto
                    {
                        UserId = x.UserId,
                        DeptId = x.DeptId
                    })
                    .FirstOrDefaultAsync();
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return null;
        }
        public async Task<List<DeptDto>> GetListByUserId(Guid userId)
        {
            try
            {
                return await this.UnitOfWork.GetAll<UserDepartment>()
                    .Where(x => x.UserId == userId && x.ActiveFlag == STATUS_ACTIVE)
                    .Join(this.UnitOfWork.GetAll<Department>().Where(x => x.ActiveFlag == STATUS_ACTIVE),
                        ud => ud.DeptId,
                        d => d.Id,
                        (ud, d) => new { ud, d })
                    .OrderByDescending(x => x.d.Name)
                    .Select(x => new DeptDto
                    {
                        Id = x.d.Id,
                        Name = x.d.Name,
                        DisplayName = x.d.Name
                    })
                    .ToListAsync();
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return new List<DeptDto>();
        }
        public async Task<bool> InsertOrDeleteAsync(Guid userId, List<Guid> lstDeptId)
        {
            bool result = false;
            try
            {
                result = await this.DeleteAsync(userId);
                if (result && lstDeptId.Count > 0) { result = await this.InsertAsync(userId, lstDeptId); }
            }
            catch (Exception ex)
            {
                result = false;
                this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
            }
            return result;
        }
        public async Task<bool> InsertAsync(Guid userId, List<Guid> lstDeptId)
        {
            bool result = false;
            try
            {
                var lstEntity = new List<UserDepartment>();
                foreach (var item in lstDeptId)
                {
                    lstEntity.Add(new UserDepartment
                    {
                        UserId = userId,
                        DeptId = item,
                        CreateDate = DateTime.Now,
                        UpdateDate = DateTime.Now
                    });
                }

                var add = await this.UnitOfWork.InsertToListAsync(lstEntity);
                if (add != null) { result = true; }
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return result;
        }
        public async Task<bool> UpdateAsync(UserDepartmentDto dto)
        {
            bool result = false;
            try
            {
                var entity = this.UnitOfWork.GetSingle<UserDepartment>(x => x.UserId == dto.UserId && x.ActiveFlag == STATUS_ACTIVE);
                if (entity != null)
                {
                    entity.DeptId = dto.DeptId;
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
                var lstDel = this.UnitOfWork.GetAll<UserDepartment>()
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
