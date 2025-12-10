using MiddlewareTool.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static MiddlewareTool.Dto.UserMgmtDto;

namespace MiddlewareTool.Business.Interface
{
    public interface IUserDepartmentBusiness
    {
        IQueryable<UserDepartment> GetAll();
        Task<UserDepartmentDto> GetByUserId(Guid userId);
        Task<List<DeptDto>> GetListByUserId(Guid userId);
        Task<bool> InsertOrDeleteAsync(Guid userId, List<Guid> lstDeptId);
        Task<bool> InsertAsync(Guid userId, List<Guid> lstDeptId);
        Task<bool> UpdateAsync(UserDepartmentDto dto);
        Task<bool> DeleteAsync(Guid userId);
    }
}
