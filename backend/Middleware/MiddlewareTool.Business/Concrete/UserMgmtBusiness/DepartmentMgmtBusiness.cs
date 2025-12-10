using MiddlewareTool.Business.Interface;
using MiddlewareTool.Entities;
using MiddlewareTool.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static MiddlewareTool.Dto.UserMgmtDto;

namespace MiddlewareTool.Business.Concrete
{
    public class DepartmentMgmtBusiness : BaseBusiness, IDepartmentMgmtBusiness
    {
        private readonly IDepartmentBusiness _repoBusiness;
        public DepartmentMgmtBusiness(IUnitOfWork unitOfWork, IDepartmentBusiness repoBusiness) : base(unitOfWork)
        {
            _repoBusiness = repoBusiness;
        }
        public async Task<Tuple<int, List<DepartmentDto>>> GetPagingAsync(string userName, string keyWord, int pageIndex, int pageSize)
        {
            return await _repoBusiness.GetPagingAsync(userName, keyWord, pageIndex, pageSize);
        }
        public IQueryable<Department> GetAll()
        {
            return _repoBusiness.GetAll();
        }
        public IQueryable<Department> GetAllByUser(string user)
        {
            return _repoBusiness.GetAllByUser(user);
        }
        public Dictionary<int, List<DictDeptDto>> GetLevelByUser(string user)
        {
            return _repoBusiness.GetLevelByUser(user);
        }
        public Dictionary<int, List<DictDeptDto>> GetAllByUserDeparment(Guid userId)
        {
            return _repoBusiness.GetAllByUserDeparment(userId);
        }
        public async Task<List<DepartmentTreeDto>> GetAllRecursiveOfLevel(string userName)
        {
            return await _repoBusiness.GetAllRecursiveOfLevel(userName);
        }
        public List<DepartmentTreeDto> GetAllRecursiveOfLevelByDeptId(Guid deptId)
        {
            return _repoBusiness.GetAllRecursiveOfLevelByDeptId(deptId);
        }
        public List<RecursiveDeptDto> GetRecursiveDeptByUser(string user, Guid? selId)
        {
            return _repoBusiness.GetRecursiveDeptByUser(user, selId);
        }
        public List<RecursiveDeptDto> GetRecursiveDeptID(List<Guid> lstId)
        {
            return _repoBusiness.GetRecursiveDeptID(lstId);
        }
        public async Task<DepartmentDto> GetByIdAsync(Guid id)
        {
            return await _repoBusiness.GetByIdAsync(id);
        }
        public async Task<DepartmentDto> GetByCodeAsync(string code)
        {
            return await _repoBusiness.GetByCodeAsync(code);
        }
        public async Task<bool> InsertAsync(DepartmentDto dto)
        {
            return await _repoBusiness.InsertAsync(dto);
        }
        public async Task<bool> UpdateAsync(DepartmentDto dto)
        {
            return await _repoBusiness.UpdateAsync(dto);
        }
        public async Task<bool> DeleteAsync(Guid id, string userName)
        {
            return await _repoBusiness.DeleteAsync(id, userName);
        }
        public async Task<string> GetCodeByIdAsync(Guid id)
        {
            return await _repoBusiness.GetCodeByIdAsync(id);
        }
        public List<Guid> GetChildrenByDeptID(List<Guid> lstId)
        {
            return _repoBusiness.GetChildrenByDeptID(lstId);
        }
        public async Task<bool> CheckCodeAsync(string code)
        {
            return await _repoBusiness.CheckCodeAsync(code);
        }
    }
}
