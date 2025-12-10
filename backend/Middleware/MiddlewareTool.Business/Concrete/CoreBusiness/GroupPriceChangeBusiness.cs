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
using static MiddlewareTool.Dto.CoreDto;

namespace MiddlewareTool.Business.Concrete
{
    public class GroupPriceChangeBusiness : BaseBusiness, IGroupPriceChangeBusiness
    {
        public GroupPriceChangeBusiness(IUnitOfWork unitOfWork) : base(unitOfWork) { }
        public async Task<Tuple<int, List<GroupPriceChangeDto>>> GetPagingAsync(string userName, string keyWord, int pageIndex, int pageSize)
        {
            int total = 0;
            try
            {
                var iquery = this.UnitOfWork.GetAll<GroupPriceChange>().Where(x => x.ActiveFlag == STATUS_ACTIVE);
                if (!string.IsNullOrEmpty(userName))
                {
                    iquery = iquery.Where(x => !string.IsNullOrEmpty(x.CreateBy) && x.CreateBy.ToUpper() == userName.ToUpper());
                }
                if (!string.IsNullOrEmpty(keyWord))
                {
                    keyWord = keyWord.ToLower();
                    var search = AppValue.ToUnsignString(keyWord);
                    iquery = iquery.Where(x => x.PRC_NO.ToLower().Equals(keyWord)
                        || x.PRC_TYPE.ToLower().Contains(keyWord)
                        || x.PRC_DISC_RATE.ToLower().Contains(keyWord)
                        || x.SUBCLASS.ToLower().Contains(keyWord)
                        || x.EXCLUDE_SSN_ID.ToLower().Contains(keyWord)
                        || x.StoreCode.ToLower().Contains(keyWord)
                        || x.URL.Contains(search));
                }
                total = iquery.Count();
                var data = await iquery.OrderByDescending(x => x.CreateDate).ThenByDescending(x => x.UpdateDate)
                 .Skip((pageIndex - 1) * pageSize)
                 .Take(pageSize)
                 .Select(x => new GroupPriceChangeDto
                 {
                     Id = x.Id,
                     REC_ID = x.REC_ID,
                     PRC_NO = x.PRC_NO,
                     PRC_TYPE = x.PRC_TYPE,
                     SUBCLASS = x.SUBCLASS,
                     PRC_DISC_RATE = x.PRC_DISC_RATE,
                     PRC_END_DATE = x.PRC_END_DATE,
                     PRC_END_TIME = x.PRC_END_TIME,
                     EXCLUDE_SSN_ID = x.EXCLUDE_SSN_ID,
                     PRC_START_DATE = x.PRC_START_DATE,
                     PRC_START_TIME = x.PRC_START_TIME,
                     EndOfRecord = x.EndOfRecord,
                     StoreCode = x.StoreCode,
                     CreateBy = x.CreateBy,
                     UpdateBy = x.UpdateBy,
                     CreateDate = x.CreateDate,
                     UpdateDate = x.UpdateDate
                 })
                 .ToListAsync();
                return new Tuple<int, List<GroupPriceChangeDto>>(total, data);
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return new Tuple<int, List<GroupPriceChangeDto>>(total, new List<GroupPriceChangeDto>());
        }
        public GroupPriceChangeDto GetById(Guid id)
        {
            try
            {
                var iquery = this.UnitOfWork.GetSingle<GroupPriceChange>(x => x.Id == id && x.ActiveFlag == STATUS_ACTIVE);
                if (iquery != null)
                {
                    return Mapper.Map<GroupPriceChangeDto>(iquery);
                }
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return null;
        }
        public async Task<GroupPriceChangeDto> GetByIdAsync(Guid id)
        {
            try
            {
                var iquery = await this.UnitOfWork.GetSingleAsync<GroupPriceChange>(x => x.Id == id && x.ActiveFlag == STATUS_ACTIVE);
                if (iquery != null)
                {
                    return Mapper.Map<GroupPriceChangeDto>(iquery);
                }
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return null;
        }
        public GroupPriceChangeDto GetByCode(string code)
        {
            try
            {
                var iquery = this.UnitOfWork.GetSingle<GroupPriceChange>(x => x.PRC_NO.ToUpper().Equals(code.ToUpper()) && x.ActiveFlag == STATUS_ACTIVE);
                if (iquery != null)
                {
                    return Mapper.Map<GroupPriceChangeDto>(iquery);
                }
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return null;
        }
        public bool Insert(GroupPriceChangeDto dto)
        {
            bool result = false;
            try
            {
                var entity = Mapper.Map<GroupPriceChangeDto>(dto);
                entity.URL = AppValue.ToUnsignString(dto.Description);
                entity.CreateDate = DateTime.Now;
                entity.UpdateDate = DateTime.Now;
                entity.CreateBy = dto.CreateBy;
                entity.UpdateBy = dto.CreateBy;
                var add = this.UnitOfWork.Insert(entity);
                if (add != null) { result = true; }
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return result;
        }
        public async Task<bool> InsertAsync(GroupPriceChangeDto dto)
        {
            bool result = false;
            try
            {
                var entity = Mapper.Map<GroupPriceChange>(dto);
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
        public bool Update(GroupPriceChangeDto dto)
        {
            bool result = false;
            try
            {
                var entity = this.UnitOfWork.GetSingle<GroupPriceChange>(x => x.Id.Equals(dto.Id));
                if (entity != null)
                {
                    entity.URL = AppValue.ToUnsignString(dto.Description);
                    entity.UpdateBy = dto.UpdateBy;
                    entity.UpdateDate = DateTime.Now;
                    result = this.UnitOfWork.Update(entity);
                }
            }
            catch (Exception ex) { LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return result;
        }
        public async Task<bool> UpdateAsync(GroupPriceChangeDto dto)
        {
            bool result = false;
            try
            {
                var entity = this.UnitOfWork.GetSingle<GroupPriceChange>(x => x.Id.Equals(dto.Id));
                if (entity != null)
                {
                    entity.URL = AppValue.ToUnsignString(dto.Description);
                    entity.UpdateBy = dto.UpdateBy;
                    entity.UpdateDate = DateTime.Now;
                    result = await this.UnitOfWork.UpdateAsync(entity);
                }
            }
            catch (Exception ex) { LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return result;
        }
        public bool IsExist(Guid id)
         {
            bool result = true;
            try
            {
                if (id != Guid.Empty)
                {
                    var iquery = this.UnitOfWork.GetSingle<GroupPriceChange>(x => x.Id == id);
                    if (iquery == null) { result = false; }
                }
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return result;
        }
        public async Task<bool> IsExistAsync(Guid id)
        {
            bool result = true;
            try
            {
                if (id != Guid.Empty)
                {
                    var iquery = await this.UnitOfWork.GetSingleAsync<GroupPriceChange>(x => x.Id == id);
                    if (iquery == null) { result = false; }
                }
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return result;
        }
        public bool IsExistByCode(string code)
        {
            bool result = true;
            try
            {
                if (!string.IsNullOrEmpty(code))
                {
                    var iquery = this.UnitOfWork.GetSingle<GroupPriceChange>(x => x.PRC_NO.ToUpper().Equals(code.ToUpper()));
                    if (iquery == null) { result = false; }
                }
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return result;
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
                    return this.UnitOfWork.ExecuteNonQuery(BaseBusiness.SP_GroupPriceChange_Import, m_Param, timeOut);
                }
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return false;
        }
    }
}
