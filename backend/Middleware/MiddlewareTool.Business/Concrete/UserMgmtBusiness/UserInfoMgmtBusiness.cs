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
    public class UserInfoMgmtBusiness : BaseBusiness, IUserInfoMgmtBusiness
    {
        private readonly IUserInfoBusiness _repoBusiness;
        public UserInfoMgmtBusiness(IUnitOfWork unitOfWork, IUserInfoBusiness repoBusiness) : base(unitOfWork)
        {
            _repoBusiness = repoBusiness;
        }
        public IQueryable<UserInfo> GetAll()
        {
            return _repoBusiness.GetAll();
        }
        public IQueryable<UserInfo> GetAll(string user)
        {
            return _repoBusiness.GetAll(user);
        }
        public async Task<Tuple<int, List<UserInfoDto>>> GetPagingAsync(string userName, string keyWord, string deparmentId, string role, string status, int pageIndex, int pageSize)
        {
            return await _repoBusiness.GetPagingAsync(userName, keyWord, deparmentId, role, status, pageIndex, pageSize);
        }
        public async Task<UserInfoDto> GetActiveByUserIdAsync(string userId)
        {
            return await _repoBusiness.GetActiveByUserIdAsync(userId);
        }
        public async Task<UserInfoDto> GetByUserIdAsync(string userId)
        {
            return await _repoBusiness.GetByUserIdAsync(userId);
        }
        public async Task<List<UsersDto>> GetUserByListMail(List<string> lstMail)
        {
            return await _repoBusiness.GetUserByListMail(lstMail);
        }
        public async Task<UserInfoDto> GetByIdAsync(Guid id)
        {
            return await _repoBusiness.GetByIdAsync(id);
        }
        public string GetEmaiById(Guid id)
        {
            return _repoBusiness.GetEmaiById(id);
        }
        public async Task<bool> InsertAsync(UserInfoDto dto)
        {
            return await _repoBusiness.InsertAsync(dto);
        }
        public async Task<bool> UpdateAddRoleAsync(UserInfoDto dto, List<Guid> lstRoleId)
        {
            return await _repoBusiness.UpdateAddRoleAsync(dto, lstRoleId);
        }
        public async Task<bool> ChangeActiveAsync(string userId)
        {
            return await _repoBusiness.ChangeActiveAsync(userId);
        }
        public async Task<bool> UpdateAsync(UserInfoDto dto)
        {
            return await _repoBusiness.UpdateAsync(dto);
        }
        public async Task<Guid> GetIDByUserAsync(string user)
        {
            return await _repoBusiness.GetIDByUserAsync(user);
        }
        public async Task<bool> UpdateURLAsync(string userName)
        {
            return await _repoBusiness.UpdateURLAsync(userName);
        }
        public Guid GetUserIdByEmail(string email)
        {
            return _repoBusiness.GetUserIdByEmail(email);
        }
        public UsersDto GetByEmail(string email)
        {
            return _repoBusiness.GetByEmail(email);
        }
        public UserInfoDto GetByUserId(string userId)
        {
            return _repoBusiness.GetByUserId(userId);
        }
        public List<UsersDto> GetUserByListUserId(List<string> lstUserId)
        {
            return _repoBusiness.GetUserByListUserId(lstUserId);
        }
        public List<UsersDto> FindUsers(string keyword)
        {
            return _repoBusiness.FindUsers(keyword);
        }

        public bool CheckExist(UserInfoDto dto)
        {
            return _repoBusiness.CheckExist(dto);
        }
    }
}
