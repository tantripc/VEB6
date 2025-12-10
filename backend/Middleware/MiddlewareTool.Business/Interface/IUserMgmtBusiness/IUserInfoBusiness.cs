using MiddlewareTool.Entities.Models;
using static MiddlewareTool.Dto.UserMgmtDto;

namespace MiddlewareTool.Business.Interface
{
    public interface IUserInfoBusiness
    {
        IQueryable<UserInfo> GetAll();
        IQueryable<UserInfo> GetAll(string user);
        Task<Tuple<int, List<UserInfoDto>>> GetPagingAsync(string userName, string keyWord, string deparmentId, string role, string status, int pageIndex, int pageSize);
        Task<UserInfoDto> GetByIdAsync(Guid id);
        Task<UserInfoDto> GetByUserIdAsync(string userId);
        Task<UserInfoDto> GetActiveByUserIdAsync(string userId);
        Task<List<UsersDto>> GetUserByListMail(List<string> lstMail);
        string GetEmaiById(Guid id);
        Task<bool> InsertAsync(UserInfoDto dto);
        Task<bool> UpdateAddRoleAsync(UserInfoDto dto, List<Guid> lstRoleId);
        Task<bool> UpdateAsync(UserInfoDto dto);
        Task<bool> ChangeActiveAsync(string userId);
        Task<Guid> GetIDByUserAsync(string user);
        Task<bool> UpdateURLAsync(string userName);
        UsersDto GetByEmail(string email);
        Guid GetUserIdByEmail(string email);
        UserInfoDto GetByUserId(string userId);
        List<UsersDto> GetUserByListUserId(List<string> lstUserId);
        List<UsersDto> FindUsers(string keyword);
        bool CheckExist(UserInfoDto dto);
    }
}
