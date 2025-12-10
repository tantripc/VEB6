using MiddlewareTool.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static MiddlewareTool.Dto.UserMgmtDto;

namespace MiddlewareTool.Business.Interface
{
    public interface IDepartmentBusiness
    {
        Task<Tuple<int, List<DepartmentDto>>> GetPagingAsync(string userName, string keyWord, int pageIndex, int pageSize);
        IQueryable<Department> GetAll();
        IQueryable<Department> GetAllByUser(string user);
        Dictionary<int, List<DictDeptDto>> GetLevelByUser(string user);
        Dictionary<int, List<DictDeptDto>> GetAllByUserDeparment(Guid userId);
        Task<List<DepartmentTreeDto>> GetAllRecursiveOfLevel(string userName);
        List<DepartmentTreeDto> GetAllRecursiveOfLevelByDeptId(Guid deptId);
        List<RecursiveDeptDto> GetRecursiveDeptID(List<Guid> lstId);
        List<RecursiveDeptDto> GetRecursiveDeptByUser(string user, Guid? selId);
        List<Guid> GetChildrenByDeptID(List<Guid> lstId);
        Task<DepartmentDto> GetByIdAsync(Guid id);
        Task<string> GetCodeByIdAsync(Guid id);
        Task<DepartmentDto> GetByCodeAsync(string code);
        Task<bool> CheckCodeAsync(string code);
        Task<bool> InsertAsync(DepartmentDto dto);
        Task<bool> UpdateAsync(DepartmentDto dto);
        Task<bool> DeleteAsync(Guid id, string userName);
    }
}
