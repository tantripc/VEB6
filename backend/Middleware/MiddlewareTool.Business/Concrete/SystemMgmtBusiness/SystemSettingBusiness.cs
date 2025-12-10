using AutoMapper;
using MiddlewareTool.Business.Interface;
using MiddlewareTool.Common;
using MiddlewareTool.Entities;
using MiddlewareTool.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using static MiddlewareTool.Dto.SystemMgmtDto;
using static MiddlewareTool.Dto.UserMgmtDto;

namespace MiddlewareTool.Business.Concrete
{
    public class SystemSettingBusiness : BaseBusiness, ISystemSettingBusiness
    {
        public SystemSettingBusiness(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
        public async Task<Tuple<int, List<SystemSettingDto>>> GetPagingAsync(string userName, string keyWord, int layout, int pageIndex, int pageSize)
        {
            int total = 0;
            try
            {
                var iquery = this.UnitOfWork.GetAll<SystemSetting>()
                    .Where(x => x.ActiveFlag == STATUS_ACTIVE);
                if (layout >= 0)
                {
                    iquery = iquery.Where(x => x.Layout == (byte)layout);
                }
                if (!string.IsNullOrEmpty(userName))
                {
                    iquery = iquery.Where(x => !string.IsNullOrEmpty(x.CreateBy) && x.CreateBy.ToUpper() == userName.ToUpper());
                }
                if (!string.IsNullOrEmpty(keyWord))
                {
                    var search = AppValue.ToUnsignString(keyWord);
                    iquery = iquery.Where(x => x.Name.Contains(keyWord) || x.Code.Equals(keyWord) || x.Value.Contains(keyWord) || x.URL.Contains(search));
                }
                total = iquery.Count();
                var data = await iquery
                        .OrderByDescending(x => x.UpdateDate)
                        .ThenBy(x => x.Type)
                        .ThenByDescending(x => x.UpdateDate)
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .Select(x => new SystemSettingDto
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Value = x.Value,
                        Description = x.Description,
                        Type = x.Type,
                        CreateBy = x.CreateBy,
                        UpdateBy = x.UpdateBy,
                        UpdateDate = x.UpdateDate
                    })
                    .ToListAsync();
                if (data.Count > 0)
                {
                    var lstUserUpdateByIds = data.Select(x => x.UpdateBy).ToList();
                    var lstUpdateBys = this.UnitOfWork.GetAll<UserInfo>()
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
                        item.UpdateByFullName = lstUpdateBys.Where(x => x.UserName.ToLower().Equals(item.UpdateBy.ToLower()))
                          .Select(x => x.FullName)
                          .FirstOrDefault();
                    }
                }
                return new Tuple<int, List<SystemSettingDto>>(total, data);
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return new Tuple<int, List<SystemSettingDto>>(total, new List<SystemSettingDto>());
        }
        public async Task<SystemSettingDto> GetByIdAsync(Guid id)
        {
            try
            {
                var iquery = await this.UnitOfWork.GetSingleAsync<SystemSetting>(x => x.Id == id);
                if (iquery != null)
                {
                    return Mapper.Map<SystemSettingDto>(iquery);
                }
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return null;
        }
        public async Task<SystemSettingDto> GetByTypeAsync(AppType.Setting type)
        {
            try
            {
                var iquery = await this.UnitOfWork.GetSingleAsync<SystemSetting>(x => x.Type == (byte)type);
                if (iquery != null)
                {
                    return Mapper.Map<SystemSettingDto>(iquery);
                }
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return null;
        }
        public List<SystemSettingDto> GetByType(AppType.Setting type)
        {
            try
            {
                var iquery = this.UnitOfWork.GetAllNoTracking<SystemSetting>().Where(x => x.Type == (byte)type).ToList();
                if (iquery != null)
                {
                    return Mapper.Map<List<SystemSettingDto>>(iquery);
                }
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return new List<SystemSettingDto>();
        }
        public async Task<SystemSettingDto> GetByCodeAsync(string code)
        {
            try
            {
                var iquery = await this.UnitOfWork.GetSingleAsync<SystemSetting>(x => x.Code.Equals(code));
                if (iquery != null)
                {
                    return Mapper.Map<SystemSettingDto>(iquery);
                }
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return null;
        }
        public SystemSettingDto GetByCode(string code)
        {
            try
            {
                var iquery = this.UnitOfWork.GetSingle<SystemSetting>(x => x.Code.Equals(code));
                if (iquery != null)
                {
                    return Mapper.Map<SystemSettingDto>(iquery);
                }
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return null;
        }
        public bool CheckExistByCode(string code)
        {
            try
            {
                var iquery = this.UnitOfWork.GetAllNoTracking<SystemSetting>().Any(x => x.Code == code);
                return iquery;
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return false;
        }
        public async Task<bool> InsertAsync(SystemSettingDto dto)
        {
            bool result = false;
            try
            {
                var entity = Mapper.Map<SystemSetting>(dto);
                entity.Id = Guid.NewGuid();
                entity.URL = AppValue.ToUnsignString(dto.Value);
                entity.CreateDate = DateTime.Now;
                entity.UpdateDate = DateTime.Now;
                entity.UpdateBy = dto.CreateBy;
                var add = await this.UnitOfWork.InsertAsync(entity);
                if (add != null) { result = true; }
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return result;
        }
        public async Task<bool> UpdateAsync(SystemSettingDto dto)
        {
            bool result = false;
            try
            {
                var entity = this.UnitOfWork.GetSingle<SystemSetting>(x => x.Id == dto.Id);
                if (entity != null)
                {
                    entity.Name = dto.Name;
                    entity.Value = dto.Value;
                    entity.Description = dto.Description;
                    entity.Type = dto.Type;
                    entity.URL = AppValue.ToUnsignString(dto.Name);
                    entity.UpdateBy = dto.UpdateBy;
                    entity.UpdateDate = DateTime.Now;
                    result = await this.UnitOfWork.UpdateAsync(entity);
                }
            }
            catch (Exception ex) { LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return result;
        }
        public bool Update(SystemSettingDto dto)
        {
            bool result = false;
            try
            {
                var entity = this.UnitOfWork.GetSingle<SystemSetting>(x => x.Id == dto.Id);
                if (entity != null)
                {
                    entity.Name = dto.Name;
                    entity.Value = dto.Value;
                    entity.Description = dto.Description;
                    entity.Type = dto.Type;
                    entity.URL = AppValue.ToUnsignString(dto.Name);
                    entity.UpdateBy = dto.UpdateBy;
                    entity.UpdateDate = DateTime.Now;
                    result = this.UnitOfWork.Update(entity);
                }
            }
            catch (Exception ex) { LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return result;
        }
        public async Task<bool> DeleteAsync(Guid id, string userName)
        {
            bool result = false;
            try
            {
                var entity = this.UnitOfWork.GetSingle<SystemSetting>(x => x.Id == id && x.ActiveFlag == STATUS_ACTIVE);
                if (entity != null)
                {
                    entity.UpdateDate = DateTime.Now;
                    entity.UpdateBy = userName;
                    result = await this.UnitOfWork.DeleteAsync(entity);
                }
            }
            catch (Exception ex) { LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return result;
        }
        public async Task<List<SystemSettingDto>> GetListByLayoutAsync(AppType.Layout layout)
        {
            try
            {
                var iquery = await this.UnitOfWork.GetAllAsync<SystemSetting>(x => x.Layout == (byte)layout);
                if (iquery != null)
                {
                    return Mapper.Map<List<SystemSettingDto>>(iquery);
                }
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return new List<SystemSettingDto>();
        }
        public List<SystemSettingDto> GetListByLayout(AppType.Layout layout)
        {
            try
            {
                var iquery = this.UnitOfWork.GetAll<SystemSetting>(x => x.Layout == (byte)layout);
                if (iquery != null)
                {
                    return Mapper.Map<List<SystemSettingDto>>(iquery);
                }
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return new List<SystemSettingDto>();
        }
    }
}
