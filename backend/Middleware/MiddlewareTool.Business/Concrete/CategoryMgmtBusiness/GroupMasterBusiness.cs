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
using static MiddlewareTool.Dto.CategoryMgmtDto;

namespace MiddlewareTool.Business.Concrete
{
    public class GroupMasterBusiness : BaseBusiness, IGroupMasterBusiness
    {
        private readonly IUserInfoBusiness _userInfoBusiness;
        public GroupMasterBusiness(IUnitOfWork unitOfWork, IUserInfoBusiness userInfoBusiness) : base(unitOfWork) { _userInfoBusiness = userInfoBusiness; }
        public async Task<Tuple<int, List<GroupMasterDto>>> GetPagingAsync(string userName, string keyWord, int? divisionId, int pageIndex, int pageSize)
        {
            int total = 0;
            try
            {
                var iquery = this.UnitOfWork.GetAll<GroupMaster>()
                    .Where(x => x.ActiveFlag == STATUS_ACTIVE)
                    .Join(this.UnitOfWork.GetAll<DivisionMaster>()
                        .Where(x => x.ActiveFlag == STATUS_ACTIVE),
                        d => d.DivisionId,
                        l => l.Id,
                        (d, l) => new { d, l });
                if (divisionId > 0)
                {
                    iquery = iquery.Where(x => x.d.DivisionId == divisionId);
                }
                if (!string.IsNullOrEmpty(keyWord))
                {
                    var keyTrim = keyWord.Trim().ToLower();
                    Int32.TryParse(keyWord, out int result);
                    var searchURL = AppValue.ToUnsignString(keyWord.Trim());
                    iquery = iquery.Where(x => x.d.Id == result
                        || x.d.Description.ToLower().Contains(keyTrim)
                        || x.l.Description.ToLower().Contains(keyTrim)
                        || x.d.UpdateBy.ToLower().Contains(keyTrim)
                        );
                }

                total = iquery.Count();
                var data = await iquery.OrderByDescending(x => x.d.CreateDate).ThenByDescending(x => x.d.UpdateDate)
                 .Skip((pageIndex - 1) * pageSize)
                 .Take(pageSize)
                 .Select(x => new GroupMasterDto
                 {
                     Id = x.d.Id,
                     Description = x.d.Description,
                     DivisionId = x.d.DivisionId,
                     DivisionName = x.l.Description,
                     OrderNumber = x.d.OrderNumber,
                     CreateBy = x.d.CreateBy,
                     UpdateBy = x.d.UpdateBy,
                     CreateDate = x.d.CreateDate,
                     UpdateDate = x.d.UpdateDate
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
                return new Tuple<int, List<GroupMasterDto>>(total, data);
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return new Tuple<int, List<GroupMasterDto>>(total, new List<GroupMasterDto>());
        }
        public async Task<List<GroupMasterDto>> GetAllGroupMaster()
        {
            try
            {
                var iquery = await this.UnitOfWork.GetAllAsync<GroupMaster>(x => x.ActiveFlag == STATUS_ACTIVE);
                if (iquery != null)
                {
                    return Mapper.Map<List<GroupMasterDto>>(iquery);
                }
            }
            catch (Exception ex) { LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return null;
        }
        public async Task<GroupMasterDto> GetByIdAsync(int id)
        {
            try
            {
                var iquery = await this.UnitOfWork.GetSingleAsync<GroupMaster>(x => x.Id == id && x.ActiveFlag == STATUS_ACTIVE);
                if (iquery != null)
                {
                    return Mapper.Map<GroupMasterDto>(iquery);
                }
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return null;
        }
        public async Task<bool> InsertAsync(GroupMasterDto dto)
        {
            bool result = false;
            try
            {
                var entity = Mapper.Map<GroupMaster>(dto);
                entity.Id = int.Parse(dto.Id.ToString());
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
        public async Task<bool> UpdateAsync(GroupMasterDto dto)
        {
            bool result = false;
            try
            {
                var entity = this.UnitOfWork.GetSingle<GroupMaster>(x => x.Id.Equals(dto.Id));
                if (entity != null)
                {
                    entity.DivisionId = dto.DivisionId;
                    entity.Description = dto.Description;
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
        public async Task<bool> DeleteAsync(int id, string userName)
        {
            bool result = false;
            try
            {
                var entity = this.UnitOfWork.GetSingle<GroupMaster>(x => x.Id.Equals(id) && x.ActiveFlag == STATUS_ACTIVE);
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
        public async Task<List<GroupMasterDto>> GetByDivisionIdAsync(int divisionId)
        {
            try
            {
                var iquery = await this.UnitOfWork.GetAllAsync<GroupMaster>(x => x.DivisionId == divisionId && x.ActiveFlag == STATUS_ACTIVE);
                if (iquery != null)
                {
                    return Mapper.Map<List<GroupMasterDto>>(iquery);
                }
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return null;
        }
        public async Task<bool> ExistByDivisionId(int id)
        {
            try
            {
                var iquery = await this.UnitOfWork.GetSingleAsync<GroupMaster>(x => x.DivisionId == id && x.ActiveFlag == STATUS_ACTIVE);
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
