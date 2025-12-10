using Microsoft.EntityFrameworkCore;
using MiddlewareTool.Business.Interface;
using MiddlewareTool.Common;
using MiddlewareTool.Entities;
using MiddlewareTool.Entities.Models;
using System.Data;
using System.Reflection;
using static MiddlewareTool.Dto.UserMgmtDto;

namespace MiddlewareTool.Business.Concrete
{
    public class UserInfoBusiness : BaseBusiness, IUserInfoBusiness
    {
        #region Constructor        
        public UserInfoBusiness(AppDbContext unitOfWork) : base(unitOfWork)
        { }
        #endregion

        #region Method
        public IQueryable<UserInfo> GetAll()
        {
            return this.UnitOfWork.UserInfos.Where(x => x.ActiveFlag != STATUS_DELETE);
        }
        public IQueryable<UserInfo> GetAll(string user)
        {
            var iquery = this.UnitOfWork.UserInfos.Where(x => x.ActiveFlag != STATUS_DELETE);
            if (!string.IsNullOrEmpty(user))
            {
                iquery = iquery.Where(x => !string.IsNullOrEmpty(x.CreateBy) && x.CreateBy.ToUpper() == user.ToUpper());
            }
            return iquery;
        }
        public async Task<Tuple<int, List<UserInfoDto>>> GetPagingAsync(string userName, string keyWord, string deparmentId, string role, string status, int pageIndex, int pageSize)
        {
            int total = 0;
            try
            {
                var iquery = this.UnitOfWork.UserInfos.Where(x => x.ActiveFlag == STATUS_ACTIVE);
                if (!string.IsNullOrEmpty(userName))
                {
                    var dataUsers = await _userPermissionDeptBusiness.GetListUserIdByUserName(userName);
                    iquery = iquery.Where(x => (!string.IsNullOrEmpty(x.CreateBy)
                        && x.CreateBy.ToUpper() == userName.ToUpper())
                        || dataUsers.Contains(x.Id));
                }
                if (!string.IsNullOrEmpty(keyWord))
                {
                    var keyTrim = keyWord.Trim().ToLower();
                    var searchURL = AppValue.ToUnsignString(keyWord.Trim());
                    iquery = iquery.Where(x => x.UserId.ToLower().Contains(keyTrim)
                        || x.URL.Contains(searchURL)
                        || x.Email.ToLower().Contains(keyTrim)
                        || x.FullName.ToLower().Contains(keyTrim));
                }
                if (!string.IsNullOrEmpty(role))
                {
                    #region Get data role
                    Guid.TryParse(role, out Guid _role);
                    var dataRole = new UserRoleBusiness(UnitOfWork).GetAll()
                        .Where(x => x.RoleId == _role);
                    //search by role 
                    iquery = iquery.Join(dataRole,
                        u => u.Id,
                        ud => ud.UserId,
                        (u, ud) => new { u, ud }.u);
                    #endregion
                }
                if (!string.IsNullOrEmpty(status))
                {
                    bool isActive = status == "1";
                    iquery = iquery.Where(x => x.IsActive == isActive);
                }
                total = iquery.Count();
                var data = await iquery.OrderByDescending(x => x.UpdateDate)
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .Select(x => new UserInfoDto
                    {
                        Id = x.Id,
                        UserId = x.UserId,
                        IsActive = x.IsActive,
                        FullName = x.FullName,
                        Email = x.Email,
                        Mobile = x.Mobile,
                        CreateBy = x.CreateBy,
                        UpdateBy = x.UpdateBy,
                        UpdateDate = x.UpdateDate
                    })
                    .ToListAsync();
                if (data.Count > 0)
                {
                    #region Get Role name and Get Store Code
                    var lstUserName = data.Select(x => x.UserId).ToList();
                    var lstUserId = data.Select(x => x.Id).ToList();
                    var dataRole = await new RoleBusiness(this.UnitOfWork).GetRoleByToListUserAsync(lstUserName);
                    var lstUserUpdateByIds = data.Select(x => x.UpdateBy).ToList();
                    var lstUpdateBys = this.UnitOfWork.UserInfos
                                    .Where(x => lstUserUpdateByIds.Contains(x.UserId) && x.ActiveFlag == STATUS_ACTIVE)
                                    .Select(x => new UsersDto()
                                    {
                                        Id = x.Id,
                                        UserName = x.UserId,
                                        Email = x.Email,
                                        FullName = x.FullName
                                    })
                                    .ToList();
                    foreach (var item in data)
                    {
                        var lstRoleName = dataRole.Where(x => x.UserId == item.UserId).Select(x => x.RoleName).ToList();
                        item.RoleName = string.Join("; ", lstRoleName);
                        item.IsSuperAdmin = dataRole.Any(x => x.UserId == item.UserId && x.Type == (byte)AppType.UserRole.SuperAdmin);
                        var lstStoreCode = this.UnitOfWork.GetAll<UserStore>()
                        .Where(x => x.UserName == item.UserId && x.ActiveFlag == STATUS_ACTIVE)
                        .OrderBy(x => x.StoreCode)
                        .Select(x => x.StoreCode)
                        .ToList();
                        item.DisplayStoreCode = string.Join("; ", lstStoreCode);
                        item.UpdateByFullName = lstUpdateBys.Where(x => x.UserName.ToLower().Equals(item.UpdateBy.ToLower()))
                             .Select(x => x.FullName)
                             .FirstOrDefault();
                    }
                    #endregion
                    return new Tuple<int, List<UserInfoDto>>(total, data);
                }
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return new Tuple<int, List<UserInfoDto>>(total, new List<UserInfoDto>());
        }
        public async Task<UserInfoDto> GetByIdAsync(Guid id)
        {
            try
            {
                var iquery = await this.UnitOfWork.UserInfos
                    .Where(x => x.Id == id && x.ActiveFlag == STATUS_ACTIVE)
                    .Select(x => new UserInfoDto
                    {
                        Id = x.Id,
                        UserId = x.UserId,
                        IsActive = x.IsActive,
                        FullName = x.FullName,
                        Email = x.Email,
                        Mobile = x.Mobile,
                        Address = x.Address,
                        HomePhone = x.HomePhone,
                        LanguageCode = x.LanguageCode,
                        Birthday = x.Birthday
                    }).FirstOrDefaultAsync();
                if (iquery != null)
                {
                    var LstDataDept = await _repoUserDeptBusiness.GetListByUserId(iquery.Id);
                    if (LstDataDept?.Count > 0) { iquery.LstDepartment = LstDataDept; }
                    var LstDataStore = await _userStoreBusiness.GetListByUserId(iquery.Id);
                    if (LstDataStore?.Count > 0) { iquery.LstStore = LstDataStore; }
                    return iquery;
                }
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return null;
        }
        public async Task<UserInfoDto> GetByUserIdAsync(string userId)
        {
            return await this.UnitOfWork.UserInfos
                .Where(x => x.UserId.ToLower().EndsWith(userId.ToLower()) && x.ActiveFlag == STATUS_ACTIVE)
                .Select(x => new UserInfoDto
                {
                    Id = x.Id,
                    UserId = x.UserId,
                    IsActive = x.IsActive,
                    FullName = x.FullName,
                    Email = x.Email,
                    Mobile = x.Mobile,
                    Address = x.Address,
                    HomePhone = x.HomePhone,
                    LanguageCode = x.LanguageCode,
                    Birthday = x.Birthday
                })
                .FirstOrDefaultAsync();
        }
        public async Task<UserInfoDto> GetActiveByUserIdAsync(string userId)
        {
            return await this.UnitOfWork.UserInfos
                .Where(x => x.UserId.ToLower().EndsWith(userId.ToLower()) && x.IsActive && x.ActiveFlag == STATUS_ACTIVE)
                .Select(x => new UserInfoDto
                {
                    Id = x.Id,
                    UserId = x.UserId,
                    FullName = x.FullName,
                    Email = x.Email,
                    Mobile = x.Mobile,
                    Address = x.Address,
                    HomePhone = x.HomePhone,
                    LanguageCode = x.LanguageCode,
                    Birthday = x.Birthday
                }).FirstOrDefaultAsync();
        }
        public async Task<List<UsersDto>> GetUserByListMail(List<string> lstMail)
        {
            return await this.UnitOfWork.UserInfos
                .Where(x => lstMail.Contains(x.Email) && x.ActiveFlag == STATUS_ACTIVE)
                .Select(x => new UsersDto()
                {
                    Id = x.Id,
                    UserName = x.UserId,
                    Email = x.Email,
                    FullName = x.FullName
                })
                .ToListAsync();
        }
        public string GetEmaiById(Guid id)
        {
            return this.UnitOfWork.UserInfos
                .Where(x => x.Id == id && x.ActiveFlag == STATUS_ACTIVE)
                .Select(x => x.Email).FirstOrDefault();
        }
        public async Task<bool> InsertAsync(UserInfoDto dto)
        {
            bool result = false;
            try
            {
                var entity = new UserInfo()
                {
                    Id = Guid.NewGuid(),
                    UserId = dto.UserId,
                    FullName = dto.FullName,
                    Email = dto.Email,
                    Mobile = dto.Mobile,
                    Address = dto.Address,
                    HomePhone = dto.HomePhone,
                    LanguageCode = dto.LanguageCode,
                    Birthday = dto.Birthday,
                    CreateBy = dto.CreateBy,
                    UpdateBy = dto.UpdateBy,
                    IsActive = dto.IsActive,
                    CreateDate = dto.CreateDate,
                    UpdateDate = DateTime.Now,
                    URL = AppValue.ToUnsignString(dto.FullName)
                };
                var add = await this.UnitOfWork.UserInfos.AddAsync(entity);
                if (add != null)
                {
                    result = true;
                    #region Add Department
                    if (dto.DepartmentId != null)
                    {
                        await _repoUserDeptBusiness.InsertOrDeleteAsync(entity.Id, new List<Guid> { (Guid)dto.DepartmentId });
                    }
                    if (dto.DepartmentId != null)
                    {
                        await _repoUserDeptBusiness.InsertOrDeleteAsync(entity.Id, new List<Guid> { (Guid)dto.DepartmentId });
                    }
                    //Add user store

                    if (dto.StoreIdSelect?.Count() > 0)
                    {
                        await _userStoreBusiness.InsertOrDeleteAsync(entity.Id, entity.UserId, dto.StoreIdSelect);
                    }

                    #endregion
                }
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return result;
        }
        public async Task<bool> UpdateAddRoleAsync(UserInfoDto dto, List<Guid> lstRoleId)
        {
            bool result = false;
            var dbtransaction = this.UnitOfWork.BeginTransaction();
            try
            {
                //Update port username
                result = await UpdateAsync(dto);
                if (result)
                {
                    //Add role permission
                    result = await new UserRoleBusiness(this.UnitOfWork).InsertOrDeleteAsync(dto.Id, lstRoleId, dto.UpdateBy);
                    if (result)
                    {
                        var LstDept = dto.LstDepartment.Select(x => x.Id).ToList();
                        //Add dept permission
                        result = await _userPermissionDeptBusiness.InsertOrDeleteAsync(dto.Id, (lstRoleId?.Count > 0 ? LstDept : new List<Guid>()), dto.UpdateBy);
                        //Add user department
                        result = await _repoUserDeptBusiness.InsertOrDeleteAsync(dto.Id, LstDept);
                        //Add user store
                        await _userStoreBusiness.InsertOrDeleteAsync(dto.Id, dto.UserId, dto.StoreIdSelect);
                    }
                }
                if (result) { dbtransaction.Commit(); }
                else { dbtransaction.Rollback(); }
            }
            catch (Exception ex)
            {
                dbtransaction.Rollback();
                LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
            }
            return result;
        }
        public async Task<bool> UpdateAsync(UserInfoDto dto)
        {
            bool result = false;
            try
            {
                var entity = this.UnitOfWork.UserInfos.Single(x => x.Id == dto.Id && x.ActiveFlag == STATUS_ACTIVE);
                if (entity != null)
                {
                    entity.FullName = dto.FullName;
                    entity.Email = dto.Email;
                    entity.Mobile = dto.Mobile;
                    entity.UpdateDate = DateTime.Now;
                    entity.UpdateBy = dto.UpdateBy;
                    entity.URL = AppValue.ToUnsignString(dto.FullName);
                    this.UnitOfWork.UserInfos.Update(entity);
                    result = this.UnitOfWork.SaveChanges() > 0;
                }
            }
            catch (Exception ex) { LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return result;
        }
        public async Task<bool> ChangeActiveAsync(string userId)
        {
            try
            {
                var entity = this.UnitOfWork.UserInfos.Single(x => x.UserId == userId && x.ActiveFlag == STATUS_ACTIVE);
                if (entity.IsActive) { entity.IsActive = false; }
                else { entity.IsActive = true; }
                this.UnitOfWork.UserInfos.Update(entity);

                return this.UnitOfWork.SaveChanges() > 0;
            }
            catch (Exception ex) { LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return false;
        }
        public async Task<Guid> GetIDByUserAsync(string user)
        {
            return await this.UnitOfWork.UserInfos
                .Where(x => x.UserId.ToLower().EndsWith(user.ToLower()) && x.ActiveFlag == STATUS_ACTIVE)
                .Select(x => x.Id).FirstOrDefaultAsync();
        }
        public async Task<bool> UpdateURLAsync(string userName)
        {
            bool result = false;
            try
            {
                DateTime dateUpdate = new DateTime(2020, 11, 30);
                if (DateTime.Now < dateUpdate)
                {
                    var LstEntity = this.UnitOfWork.UserInfos
                        .Where(x => string.IsNullOrEmpty(x.URL))
                        .Take(500)
                        .ToList();
                    if (LstEntity.Count > 0)
                    {
                        foreach (var item in LstEntity)
                        {
                            item.URL = AppValue.ToUnsignString(item.FullName);
                            item.UpdateBy = userName;
                            item.UpdateDate = DateTime.Now;
                        }
                        result = await this.UnitOfWork.UpdateToListAsync(LstEntity);
                    }
                }
            }
            catch (Exception ex)
            {
                LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
                result = false;
            }
            return result;
        }
        public UsersDto GetByEmail(string email)
        {
            try
            {
                if (!string.IsNullOrEmpty(email))
                {
                    return this.UnitOfWork.UserInfos
                        .Where(x => x.Email.ToLower().Trim() == email.ToLower().Trim()
                            && x.ActiveFlag == STATUS_ACTIVE)
                        .OrderByDescending(x => x.UpdateDate)
                        .Select(x => new UsersDto()
                        {
                            Id = x.Id,
                            UserName = x.UserId,
                            FullName = x.FullName,
                            Email = x.Email,
                            Mobile = x.Mobile
                        })
                        .FirstOrDefault();
                }
            }
            catch (Exception ex) { LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return null;
        }
        public Guid GetUserIdByEmail(string email)
        {
            try
            {
                if (!string.IsNullOrEmpty(email))
                {
                    var iquery = this.UnitOfWork.UserInfos
                        .Where(x => x.Email.ToLower().Trim() == email.ToLower().Trim()
                            && x.ActiveFlag == STATUS_ACTIVE)
                        .OrderByDescending(x => x.UpdateDate)
                        .FirstOrDefault();
                    if (iquery != null) { return iquery.Id; }
                }
            }
            catch (Exception ex) { LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return Guid.Empty;
        }
        public UserInfoDto GetByUserId(string userId)
        {
            return this.UnitOfWork.UserInfos
                .Where(x => x.ActiveFlag == STATUS_ACTIVE && x.UserId.ToLower() == userId.ToLower())
                .Select(x => new UserInfoDto
                {
                    Id = x.Id,
                    UserId = x.UserId,
                    IsActive = x.IsActive,
                    FullName = x.FullName,
                    Email = x.Email,
                    Mobile = x.Mobile,
                    Address = x.Address,
                    HomePhone = x.HomePhone,
                    LanguageCode = x.LanguageCode,
                    Birthday = x.Birthday
                })
                .FirstOrDefault();
        }
        public List<UsersDto> GetUserByListUserId(List<string> lstUserId)
        {
            return this.UnitOfWork.UserInfos
                .Where(x => lstUserId.Contains(x.UserId) && x.ActiveFlag == STATUS_ACTIVE)
                .Select(x => new UsersDto()
                {
                    Id = x.Id,
                    UserName = x.UserId,
                    Email = x.Email,
                    FullName = x.FullName
                })
                .ToList();
        }
        public List<UsersDto> FindUsers(string keyword)
        {
            return this.UnitOfWork.UserInfos.AsNoTracking()
                .Where(x => x.FullName.Contains(keyword) ||
                x.UserId.Contains(keyword))
                .Select(x => new UsersDto()
                {
                    Id = x.Id,
                    UserName = x.UserId,
                    Email = x.Email,
                    FullName = x.FullName
                })
                .ToList();
        }
        public bool CheckExist(UserInfoDto dto)
        {
            bool result = false;
            try
            {
                result = this.UnitOfWork.UserInfos
                    .Where(x => x.ActiveFlag == STATUS_ACTIVE
                    && x.Id != dto.Id
                    && (x.UserId.ToUpper() == dto.UserId || x.Email.ToUpper() == dto.Email))
                    .Any();
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return result;
        }
        #endregion
    }
}
