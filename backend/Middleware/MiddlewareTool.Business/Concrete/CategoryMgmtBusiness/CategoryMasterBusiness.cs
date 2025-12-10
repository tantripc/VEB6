using AutoMapper;
using MiddlewareTool.Business.Interface;
using MiddlewareTool.Common;
using MiddlewareTool.Entities;
using MiddlewareTool.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using static MiddlewareTool.Dto.CategoryMgmtDto;

namespace MiddlewareTool.Business.Concrete
{
    public class CategoryMasterBusiness : BaseBusiness, ICategoryMasterBusiness
    {
        private readonly IUserInfoBusiness _userInfoBusiness;
        public CategoryMasterBusiness(IUnitOfWork unitOfWork, IUserInfoBusiness userInfoBusiness) : base(unitOfWork) { _userInfoBusiness = userInfoBusiness; }
        public async Task<Tuple<int, List<CategoryMasterDto>>> GetPagingAsync(string userName, string keyWord, int? departmentId, int pageIndex, int pageSize)
        {
            int total = 0;
            try
            {
                var iquery = this.UnitOfWork.GetAll<CategoryMaster>()
                    .Include(x => x.DepartmentMaster)
                    .Include(x => x.DepartmentMaster.GroupMaster)
                    .Include(x => x.DepartmentMaster.GroupMaster.DivisionMaster)
                    .Include(x => x.DepartmentMaster.GroupMaster.DivisionMaster.LineMaster)
                    .Where(x => x.ActiveFlag == STATUS_ACTIVE);

                if (departmentId.HasValue && departmentId > 0)
                {
                    iquery = iquery.Where(x => x.DepartmentId == departmentId);
                }
                if (!string.IsNullOrEmpty(keyWord))
                {
                    var keyTrim = keyWord.Trim().ToLower();
                    var searchURL = AppValue.ToUnsignString(keyWord.Trim());
                    int.TryParse(keyTrim, out var _departmentId);
                    iquery = iquery.Where(x => x.Id == keyTrim
                        || x.Description.ToLower().Contains(keyTrim)
                        || x.DepartmentId == _departmentId
                        || x.DepartmentMaster.Description.ToLower().Contains(keyTrim)
                        || x.UpdateBy.ToLower().Contains(keyTrim)
                        );
                }

                total = iquery.Count();
                var data = await iquery
                 .OrderByDescending(x => x.CreateDate)
                 .ThenByDescending(x => x.UpdateDate)
                 .Skip((pageIndex - 1) * pageSize)
                 .Take(pageSize)
                 .Select(x => new CategoryMasterDto
                 {
                     Id = x.Id,
                     Description = x.Description,
                     DepartmentId = x.DepartmentId,
                     DepartmentName = x.DepartmentMaster.Description,
                     OrderNumber = x.OrderNumber,
                     CreateBy = x.CreateBy,
                     UpdateBy = x.UpdateBy,
                     CreateDate = x.CreateDate.HasValue ? x.CreateDate.Value : DateTime.MinValue,
                     UpdateDate = x.UpdateDate.HasValue ? x.UpdateDate.Value : DateTime.MinValue,
                     GroupId = x.DepartmentMaster.GroupId,
                     GroupName = x.DepartmentMaster.GroupMaster.Description,
                     DivisionId = x.DepartmentMaster.GroupMaster.DivisionId,
                     DivisionName = x.DepartmentMaster.GroupMaster.DivisionMaster.Description,
                     LineId = x.DepartmentMaster.GroupMaster.DivisionMaster.LineId,
                     LineName = x.DepartmentMaster.GroupMaster.DivisionMaster.LineMaster.Description,
                 })
                 .ToListAsync();
                if (data.Count > 0)
                {
                    var lstUserCreateByIds = data.Select(x => x.CreateBy).ToList();
                    var lstCreateBys = _userInfoBusiness.GetUserByListUserId(lstUserCreateByIds);

                    var lstUserUpdateByIds = data.Select(x => x.UpdateBy).ToList();
                    var lstUpdateBys = _userInfoBusiness.GetUserByListUserId(lstUserUpdateByIds);

                    foreach (var item in data)
                    {

                        item.CreateByFullName = lstCreateBys.Where(x => x.UserName.ToLower().Equals(item.CreateBy.ToLower()))
                            .Select(x => x.FullName)
                            .FirstOrDefault();
                        item.UpdateByFullName = lstUpdateBys.Where(x => x.UserName.ToLower().Equals(item.UpdateBy.ToLower()))
                          .Select(x => x.FullName)
                          .FirstOrDefault();
                    }
                }
                return new Tuple<int, List<CategoryMasterDto>>(total, data);
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return new Tuple<int, List<CategoryMasterDto>>(total, new List<CategoryMasterDto>());
        }
        public async Task<CategoryMasterDto> GetByIdAsync(string id)
        {
            try
            {
                var iquery = await this.UnitOfWork.GetSingleAsync<CategoryMaster>(x => x.Id == id && x.ActiveFlag == STATUS_ACTIVE);
                if (iquery != null)
                {
                    return Mapper.Map<CategoryMasterDto>(iquery);
                }
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return null;
        }
        public async Task<List<CategoryMasterDto>> GetByIdsAsync(IList<string> ids)
        {
            try
            {
                var iquery = await this.UnitOfWork.GetAll<CategoryMaster>()
                .Include(x => x.DepartmentMaster)
                .Include(x => x.DepartmentMaster.GroupMaster)
                .Include(x => x.DepartmentMaster.GroupMaster.DivisionMaster)
                .Include(x => x.DepartmentMaster.GroupMaster.DivisionMaster.LineMaster)
                .Where(x => ids.Contains(x.Id))
                .Select(x => new CategoryMasterDto()
                {
                    Id = x.Id,
                    DepartmentId = x.DepartmentId,
                    Description = x.Description,
                    AutoPA = x.AutoPA,
                    PosFlag = x.PosFlag,
                    PwpExclusion = x.PwpExclusion,
                    AgeStockRetenPeriod = x.AgeStockRetenPeriod,
                    MbrDiscFlag = x.MbrDiscFlag,
                    MbrDiscPerc = x.MbrDiscPerc,
                    MommyDiscPerc = x.MommyDiscPerc,
                    OrderNumber = x.OrderNumber,
                    URL = x.URL,
                    ActiveFlag = (AppValue.ActiveFlag)x.ActiveFlag,

                    DepartmentName = x.DepartmentMaster.Description,

                    GroupId = x.DepartmentMaster.GroupId,
                    GroupName = x.DepartmentMaster.GroupMaster.Description,

                    DivisionId = x.DepartmentMaster.GroupMaster.DivisionId,
                    DivisionName = x.DepartmentMaster.GroupMaster.DivisionMaster.Description,

                    LineId = x.DepartmentMaster.GroupMaster.DivisionMaster.LineId,
                    LineName = x.DepartmentMaster.GroupMaster.DivisionMaster.LineMaster.Description,
                })
                .ToListAsync();

                return iquery;
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return new List<CategoryMasterDto>();
        }
        public async Task<bool> InsertAsync(CategoryMasterDto dto)
        {
            bool result = false;
            try
            {
                var entity = Mapper.Map<CategoryMaster>(dto);
                entity.Id = dto.Id;
                entity.DepartmentId = dto.DepartmentId;
                entity.PosFlag = dto.PosFlag;
                entity.AutoPA = dto.AutoPA;
                entity.MbrDiscFlag = dto.MbrDiscFlag;
                entity.MbrDiscPerc = dto.MbrDiscPerc;
                entity.MommyDiscPerc = dto.MommyDiscPerc;
                entity.URL = AppValue.ToUnsignString(dto.Description);
                entity.CreateDate = DateTime.Now;
                entity.UpdateDate = DateTime.Now;
                entity.CreateBy = dto.CreateBy;
                entity.UpdateBy = dto.CreateBy;
                var add = await this.UnitOfWork.InsertAsync(entity);
                if (add != null) { result = true; }
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return result;
        }
        public async Task<bool> UpdateAsync(CategoryMasterDto dto)
        {
            bool result = false;
            try
            {
                var entity = this.UnitOfWork.GetSingle<CategoryMaster>(x => x.Id.Equals(dto.Id));
                if (entity != null)
                {
                    entity.Description = dto.Description;
                    entity.AgeStockRetenPeriod = dto.AgeStockRetenPeriod;
                    entity.PwpExclusion = dto.PwpExclusion;
                    entity.DepartmentId = dto.DepartmentId;
                    entity.PosFlag = dto.PosFlag;
                    entity.MbrDiscFlag = dto.MbrDiscFlag;
                    entity.AutoPA = dto.AutoPA;
                    entity.MbrDiscPerc = dto.MbrDiscPerc;
                    entity.MommyDiscPerc = dto.MommyDiscPerc;
                    entity.OrderNumber = dto.OrderNumber;
                    entity.URL = AppValue.ToUnsignString(dto.Description);
                    entity.UpdateBy = dto.UpdateBy;
                    entity.UpdateDate = DateTime.Now;
                    result = await this.UnitOfWork.UpdateAsync(entity);
                }
            }
            catch (Exception ex) { LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return result;
        }
        public async Task<bool> DeleteAsync(string id, string userName)
        {
            bool result = false;
            try
            {
                var entity = this.UnitOfWork.GetSingle<CategoryMaster>(x => x.Id.Equals(id) && x.ActiveFlag == STATUS_ACTIVE);
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
        public async Task<bool> IsExistAsync(int id)
        {
            bool result = true;
            try
            {
                if (id > 0)
                {
                    var iquery = await this.UnitOfWork.GetSingleAsync<DivisionMaster>(x => x.Id.Equals(id));
                    if (iquery == null) { result = false; }
                }
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return result;
        }
        public async Task<DivisionMasterDto> GetByLineIdAsync(int lineId)
        {
            try
            {
                var iquery = await this.UnitOfWork.GetSingleAsync<DivisionMaster>(x => x.LineId == lineId && x.ActiveFlag == STATUS_ACTIVE);
                if (iquery != null)
                {
                    return Mapper.Map<DivisionMasterDto>(iquery);
                }
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return null;
        }
        public bool Import(DataTable dt, int timeOut)
        {
            try
            {
                if (dt != null)
                {
                    Dictionary<string, object> m_Param = new Dictionary<string, object>()
                    {
                        {"@dt", dt}
                    };
                    return this.UnitOfWork.ExecuteNonQuery(SP_CategoryMaster_Import, m_Param, timeOut);
                }
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return false;
        }
        public async Task<bool> ExistByDepId(int depId)
        {
            try
            {
                var iquery = await this.UnitOfWork.GetSingleAsync<CategoryMaster>(x => x.DepartmentId == depId && x.ActiveFlag == STATUS_ACTIVE);
                if (iquery != null)
                {
                    return true;
                }
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return false;
        }
    }
}
