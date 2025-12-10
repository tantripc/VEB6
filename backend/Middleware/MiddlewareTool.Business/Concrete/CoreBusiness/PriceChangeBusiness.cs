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
    public class PriceChangeBusiness : BaseBusiness, IPriceChangeBusiness
    {
        public PriceChangeBusiness(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public async Task<Tuple<int, List<PriceChangeDto>>> GetPagingAsync(string userName, string keyWord, int pageIndex, int pageSize)
        {
            int total = 0;
            try
            {
                var iquery = this.UnitOfWork.GetAll<PriceChange>().Where(x => x.ActiveFlag == STATUS_ACTIVE);
                if (!string.IsNullOrEmpty(userName))
                {
                    iquery = iquery.Where(x => !string.IsNullOrEmpty(x.CreateBy) && x.CreateBy.ToUpper() == userName.ToUpper());
                }
                if (!string.IsNullOrEmpty(keyWord))
                {
                    keyWord = keyWord.ToLower();
                    var search = AppValue.ToUnsignString(keyWord);
                    iquery = iquery.Where(x => x.ITEM_NO.ToLower().Equals(keyWord)
                        || x.PRC_NO.ToLower().Contains(keyWord)
                        || x.PRC_TYPE.ToLower().Contains(keyWord)
                        || x.PRC_DISC_RATE.ToLower().Contains(keyWord)
                        || x.PRC_DISC_AMT.ToLower().Contains(keyWord)
                        || x.PRC_SELL.ToLower().Contains(keyWord)
                        || x.StoreCode.ToLower().Contains(keyWord)
                        || x.URL.Contains(search));
                }
                total = iquery.Count();
                var data = await iquery.OrderByDescending(x => x.CreateDate).ThenByDescending(x => x.UpdateDate)
                 .Skip((pageIndex - 1) * pageSize)
                 .Take(pageSize)
                 .Select(x => new PriceChangeDto
                 {
                     Id = x.Id,
                     ITEM_NO = x.ITEM_NO,
                     REC_ID = x.REC_ID,
                     PRC_NO = x.PRC_NO,
                     PRC_DISC_AMT = x.PRC_DISC_AMT,
                     PRC_DISC_RATE = x.PRC_DISC_RATE,
                     PRC_END_DATE = x.PRC_END_DATE,
                     PRC_END_TIME = x.PRC_END_TIME,
                     PRC_SELL = x.PRC_SELL,
                     PRC_START_DATE = x.PRC_START_DATE,
                     PRC_START_TIME = x.PRC_START_TIME,
                     PRC_TYPE = x.PRC_TYPE,
                     StoreCode = x.StoreCode,
                     CreateBy = x.CreateBy,
                     UpdateBy = x.UpdateBy,
                     CreateDate = x.CreateDate,
                     UpdateDate = x.UpdateDate
                 })
                 .ToListAsync();
                return new Tuple<int, List<PriceChangeDto>>(total, data);
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return new Tuple<int, List<PriceChangeDto>>(total, new List<PriceChangeDto>());
        }
        public PriceChangeDto GetById(Guid id)
        {
            try
            {
                var iquery = this.UnitOfWork.GetSingle<PriceChange>(x => x.Id == id && x.ActiveFlag == STATUS_ACTIVE);
                if (iquery != null)
                {
                    return Mapper.Map<PriceChangeDto>(iquery);
                }
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return null;
        }
        public async Task<PriceChangeDto> GetByIdAsync(Guid id)
        {
            try
            {
                var iquery = await this.UnitOfWork.GetSingleAsync<PriceChange>(x => x.Id == id && x.ActiveFlag == STATUS_ACTIVE);
                if (iquery != null)
                {
                    return Mapper.Map<PriceChangeDto>(iquery);
                }
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return null;
        }
        public PriceChangeDto GetByCode(string code)
        {
            try
            {
                var iquery = this.UnitOfWork.GetSingle<PriceChange>(x => x.ITEM_NO.ToUpper().Equals(code.ToUpper()) && x.ActiveFlag == STATUS_ACTIVE);
                if (iquery != null)
                {
                    return Mapper.Map<PriceChangeDto>(iquery);
                }
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return null;
        }
        //public PriceChangeDto GetByKeyCode(string code, string storeCode, string PRC_NO)
        //{
        //    try
        //    {
        //        var iquery = this.UnitOfWork.GetAllNoTracking<PriceChange>().FirstOrDefault(x =>
        //        x.ActiveFlag == STATUS_ACTIVE
        //        && x.ITEM_NO.ToUpper().Equals(code.ToUpper())
        //        && x.StoreCode == storeCode
        //        && x.PRC_NO == PRC_NO
        //        );
        //        if (iquery != null)
        //        {
        //            return Mapper.Map<PriceChangeDto>(iquery);
        //        }
        //    }
        //    catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
        //    return null;
        //}
        //public bool AnyByKeyCode(string code, string storeCode, string PRC_NO)
        //{
        //    try
        //    {
        //        return this.UnitOfWork.GetAllNoTracking<PriceChange>().Any(x =>
        //        x.ActiveFlag == STATUS_ACTIVE
        //        && x.ITEM_NO.ToUpper().Equals(code.ToUpper())
        //        && x.StoreCode == storeCode
        //        && x.PRC_NO == PRC_NO
        //        );
        //    }
        //    catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
        //    return false;
        //}
        public bool Insert(PriceChangeDto dto)
        {
            bool result = false;
            try
            {
                var entity = Mapper.Map<PriceChangeDto>(dto);
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
        public async Task<bool> InsertAsync(PriceChangeDto dto)
        {
            bool result = false;
            try
            {
                var entity = Mapper.Map<PriceChange>(dto);
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
        public bool Update(PriceChangeDto dto)
        {
            bool result = false;
            try
            {
                var entity = this.UnitOfWork.GetSingle<PriceChange>(x => x.Id.Equals(dto.Id));
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
        public async Task<bool> UpdateAsync(PriceChangeDto dto)
        {
            bool result = false;
            try
            {
                var entity = this.UnitOfWork.GetSingle<PriceChange>(x => x.Id.Equals(dto.Id));
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
                    var iquery = this.UnitOfWork.GetSingle<PriceChange>(x => x.Id == id);
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
                    var iquery = await this.UnitOfWork.GetSingleAsync<PriceChange>(x => x.Id == id);
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
                    var iquery = this.UnitOfWork.GetSingle<PriceChange>(x => x.ITEM_NO.ToUpper().Equals(code.ToUpper()));
                    if (iquery == null) { result = false; }
                }
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return result;
        }
        public bool Import(DataTable dt, string fileName, int timeOut, out string error)
        {
            error = string.Empty;
            try
            {
                if (dt != null)
                {
                    Dictionary<string, object> m_Param = new Dictionary<string, object>()
                    {
                        {"@dt", dt},
                        {"@fileName", fileName}
                    };
                    return this.UnitOfWork.ExecuteNonQuery(BaseBusiness.SP_PriceChange_Import, m_Param, out error, timeOut);
                }
            }
            catch (Exception ex)
            {
                this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
                error = ex.Message;
            }
            return false;
        }

        public List<PriceChangeCompactDto> GetAll(string storeCode = "")
        {
            List<PriceChangeCompactDto> lst = new List<PriceChangeCompactDto>();
            try
            {
                var iquery = this.UnitOfWork.GetAllNoTracking<PriceChange>()
                    .Where(x => x.ActiveFlag == STATUS_ACTIVE);
                if (!string.IsNullOrEmpty(storeCode))
                    iquery = iquery.Where(x => x.StoreCode == storeCode);

                lst = iquery.Select(x => new PriceChangeCompactDto
                {
                    ITEM_NO = x.ITEM_NO,
                    PRC_NO = x.PRC_NO,
                    PRC_SELL = x.PRC_SELL,
                    StoreCode = x.StoreCode
                }).ToList();
            }
            catch (Exception ex)
            {
                this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
            }
            return lst;
        }
    }
}
