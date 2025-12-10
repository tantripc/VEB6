using MiddlewareTool.Business.Interface;
using MiddlewareTool.Entities;
using MiddlewareTool.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static MiddlewareTool.Dto.SystemMgmtDto;
using static MiddlewareTool.Dto.UserMgmtDto;

namespace MiddlewareTool.Business.Concrete
{
    public class RoleMgmtBusiness : BaseBusiness, IRoleMgmtBusiness
    {
        private readonly IRoleBusiness _repoBusiness;
        public RoleMgmtBusiness(IUnitOfWork unitOfWork, IRoleBusiness repoBusiness) : base(unitOfWork)
        {
            _repoBusiness = repoBusiness;
        }
        #region Role
        public IQueryable<Role> GetAll()
        {
            return _repoBusiness.GetAll();
        }
        public async Task<Tuple<int, List<RoleDto>>> GetPagingAsync(string userName, string keyWord, int pageIndex, int pageSize)
        {
            return await _repoBusiness.GetPagingAsync(userName, keyWord, pageIndex, pageSize);
        }
        public List<string> GetListRoleByUserAsync(string user)
        {
            return _repoBusiness.GetListRoleByUser(user);
        }
        public async Task<RoleDto> GetByIdAsync(Guid id)
        {
            return await _repoBusiness.GetByIdAsync(id);
        }
        public async Task<List<RoleDto>> GetByPermissionAsync(string user)
        {
            return await _repoBusiness.GetByPermissionAsync(user);
        }
        public async Task<bool> InsertAsync(RoleDto dto, List<Guid> lstMenuID)
        {
            return await _repoBusiness.InsertAsync(dto, lstMenuID);
        }
        public async Task<Tuple<bool, string>> DeleteAsync(Guid id, string user)
        {
            return await _repoBusiness.DeleteAsync(id, user);
        }
        public List<string> GetListRoleByUser(string user)
        {
            return _repoBusiness.GetListRoleByUser(user);
        }
        public Task<List<RoleByUserDto>> GetRoleByToListUserAsync(List<string> lstUser)
        {
            return _repoBusiness.GetRoleByToListUserAsync(lstUser);
        }
        public Task<List<MenuRoleDto>> GetRoleMenuByUserAsync(string user)
        {
            return _repoBusiness.GetRoleMenuByUserAsync(user);
        }

        public List<MenuActionCompactDto> GetRoleMenuActionByUserAsync(string user)
        {
            return _repoBusiness.GetRoleMenuActionByUserAsync(user);
        }
        #endregion
    }
}
