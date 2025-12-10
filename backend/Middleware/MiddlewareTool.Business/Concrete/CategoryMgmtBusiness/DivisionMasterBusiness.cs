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
using static MiddlewareTool.Common.AppMenu;
using static MiddlewareTool.Dto.CategoryMgmtDto;

namespace MiddlewareTool.Business.Concrete
{
    public class DivisionMasterBusiness : BaseBusiness, IDivisionMasterBusiness
    {
        private readonly IUserInfoBusiness _userInfoBusiness;
        public DivisionMasterBusiness(IUnitOfWork unitOfWork, IUserInfoBusiness userInfoBusiness) : base(unitOfWork) { _userInfoBusiness = userInfoBusiness; }
        public async Task<Tuple<int, List<DivisionMasterDto>>> GetPagingAsync(string userName, string keyWord, int? lineId, int pageIndex, int pageSize)
        {
            int total = 0;
            try
            {
                var iquery = this.UnitOfWork.GetAll<DivisionMaster>()
                    .Where(x => x.ActiveFlag == STATUS_ACTIVE)
                    .Join(this.UnitOfWork.GetAll<LineMaster>()
                        .Where(x => x.ActiveFlag == STATUS_ACTIVE),
                        d => d.LineId,
                        l => l.Id,
                        (d, l) => new { d, l });
                if (lineId > 0)
                {
                    iquery = iquery.Where(x => x.d.LineId == lineId);
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
                 .Select(x => new DivisionMasterDto
                 {
                     Id = x.d.Id,
                     Description = x.d.Description,
                     LineId = x.d.LineId,
                     LineName = x.l.Description,
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
                return new Tuple<int, List<DivisionMasterDto>>(total, data);
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return new Tuple<int, List<DivisionMasterDto>>(total, new List<DivisionMasterDto>());
        }
        public async Task<List<DivisionMasterDto>> GetAllDivisionMaster()
        {
            try
            {
                var iquery = await this.UnitOfWork.GetAllAsync<DivisionMaster>(x => x.ActiveFlag == STATUS_ACTIVE);
                if (iquery != null)
                {
                    return Mapper.Map<List<DivisionMasterDto>>(iquery);
                }
            }
            catch (Exception ex) { LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return null;
        }
        public async Task<DivisionMasterDto> GetByIdAsync(int id)
        {
            try
            {
                var iquery = await this.UnitOfWork.GetSingleAsync<DivisionMaster>(x => x.Id == id && x.ActiveFlag == STATUS_ACTIVE);
                if (iquery != null)
                {
                    return Mapper.Map<DivisionMasterDto>(iquery);
                }
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return null;
        }
        public async Task<bool> InsertAsync(DivisionMasterDto dto)
        {
            bool result = false;
            try
            {
                var entity = Mapper.Map<DivisionMaster>(dto);
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
        public async Task<bool> UpdateAsync(DivisionMasterDto dto)
        {
            bool result = false;
            try
            {
                var entity = this.UnitOfWork.GetSingle<DivisionMaster>(x => x.Id.Equals(dto.Id));
                if (entity != null)
                {
                    entity.LineId = dto.LineId;
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
                var entity = this.UnitOfWork.GetSingle<DivisionMaster>(x => x.Id.Equals(id) && x.ActiveFlag == STATUS_ACTIVE);
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

        public async  Task<List<DivisionMasterDto>> GetByLineIdAsync(int lineId)
        {
            try
            {
                var iquery = await this.UnitOfWork.GetAllAsync<DivisionMaster>(x => x.LineId == lineId && x.ActiveFlag == STATUS_ACTIVE);
                if (iquery != null)
                {
                    return Mapper.Map<List<DivisionMasterDto>>(iquery);
                }
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return null;
        }
        public async Task<bool> ExistByLineId(int id)
        {
            try
            {
                var iquery = await this.UnitOfWork.GetSingleAsync<DivisionMaster>(x => x.LineId == id && x.ActiveFlag == STATUS_ACTIVE);
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
