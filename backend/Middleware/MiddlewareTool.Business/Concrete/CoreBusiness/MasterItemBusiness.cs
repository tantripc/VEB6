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
    public class MasterItemBusiness : BaseBusiness, IMasterItemBusiness
    {
        public MasterItemBusiness(IUnitOfWork unitOfWork) : base(unitOfWork) { }
        public async Task<Tuple<int, List<MasterItemDto>>> GetPagingAsync(string userName, string keyWord, int pageIndex, int pageSize)
        {
            int total = 0;
            try
            {
                var iquery = this.UnitOfWork.GetAll<MasterItem>().Where(x => x.ActiveFlag == STATUS_ACTIVE);
                if (!string.IsNullOrEmpty(userName))
                {
                    iquery = iquery.Where(x => !string.IsNullOrEmpty(x.CreateBy) && x.CreateBy.ToUpper() == userName.ToUpper());
                }
                if (!string.IsNullOrEmpty(keyWord))
                {
                    keyWord = keyWord.ToLower();
                    var search = AppValue.ToUnsignString(keyWord);
                    iquery = iquery.Where(x => x.ITEM_NO.ToLower().Equals(keyWord)
                        || x.ITEM_SHORT_NAME.ToLower().Contains(keyWord)
                        || x.ITEM_SHORT_NAME_CHINESE.ToLower().Contains(keyWord)
                        || x.ITEM_LONG_NAME.ToLower().Contains(keyWord)
                        || x.ITEM_LONG_NAME_CHINESE.ToLower().Contains(keyWord)
                        || x.ITEM_BARCODE.ToLower().Contains(keyWord)
                        || x.ITEM_SELL.ToLower().Contains(keyWord)
                        || x.URL.Contains(search));
                }
                total = iquery.Count();
                var data = await iquery.OrderByDescending(x => x.CreateDate).ThenByDescending(x => x.UpdateDate)
                 .Skip((pageIndex - 1) * pageSize)
                 .Take(pageSize)
                 .Select(x => new MasterItemDto
                 {
                     Id = x.Id,
                     REC_ID = x.REC_ID,
                     ITEM_NO = x.ITEM_NO,
                     ITEM_SHORT_NAME = x.ITEM_SHORT_NAME,
                     ITEM_LONG_NAME = x.ITEM_LONG_NAME,
                     ITEM_SHORT_NAME_CHINESE = x.ITEM_SHORT_NAME_CHINESE,
                     ITEM_LONG_NAME_CHINESE = x.ITEM_LONG_NAME_CHINESE,
                     ITEM_BARCODE = x.ITEM_BARCODE,
                     ITEM_SELL = x.ITEM_SELL,
                     ITEM_MEMBER_SELL = x.ITEM_MEMBER_SELL,
                     ITEM_UOM = x.ITEM_UOM,
                     DIRECTION = x.DIRECTION,
                     EXPIRE_TIME = x.EXPIRE_TIME,
                     EXPIRE_LABEL_FORMAT = x.EXPIRE_LABEL_FORMAT,
                     INGREDIENT = x.INGREDIENT,
                     INSTRUCT_STORAGE = x.INSTRUCT_STORAGE,
                     ITEM_CLS = x.ITEM_CLS,
                     ITEM_DATE = x.ITEM_DATE,
                     ITEM_DEPT = x.ITEM_DEPT,
                     ITEM_DIV = x.ITEM_DIV,
                     ITEM_PLU_FLAG = x.ITEM_PLU_FLAG,
                     ITEM_SUBCLS = x.ITEM_SUBCLS,
                     ITEM_UOM2 = x.ITEM_UOM2,
                     ITEM_VAT = x.ITEM_VAT,
                     ITEM_VAT_FLAG = x.ITEM_VAT_FLAG,
                     ITEM_WEIGH = x.ITEM_WEIGH,
                     KADS1M_FLAG = x.KADS1M_FLAG,
                     NUTRI_FACTS = x.NUTRI_FACTS,
                     SALES_TAX = x.SALES_TAX,
                     SEASON_ID = x.SEASON_ID,
                     TAX_CODE = x.TAX_CODE,
                     Tax_Sign = x.Tax_Sign,
                     CreateBy = x.CreateBy,
                     UpdateBy = x.UpdateBy,
                     CreateDate = x.CreateDate,
                     UpdateDate = x.UpdateDate
                 })
                 .ToListAsync();
                return new Tuple<int, List<MasterItemDto>>(total, data);
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return new Tuple<int, List<MasterItemDto>>(total, new List<MasterItemDto>());
        }
        public MasterItemDto GetById(Guid id)
        {
            try
            {
                var iquery = this.UnitOfWork.GetSingle<MasterItem>(x => x.Id == id && x.ActiveFlag == STATUS_ACTIVE);
                if (iquery != null)
                {
                    return Mapper.Map<MasterItemDto>(iquery);
                }
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return null;
        }
        public async Task<MasterItemDto> GetByIdAsync(Guid id)
        {
            try
            {
                var iquery = await this.UnitOfWork.GetSingleAsync<MasterItem>(x => x.Id == id && x.ActiveFlag == STATUS_ACTIVE);
                if (iquery != null)
                {
                    return Mapper.Map<MasterItemDto>(iquery);
                }
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return null;
        }
        public MasterItemDto GetByCode(string code)
        {
            try
            {
                var iquery = this.UnitOfWork.GetSingle<MasterItem>(x => x.ITEM_NO.ToUpper().Equals(code.ToUpper()) && x.ActiveFlag == STATUS_ACTIVE);
                if (iquery != null)
                {
                    return Mapper.Map<MasterItemDto>(iquery);
                }
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return null;
        }
        public bool Insert(MasterItemDto dto)
        {
            bool result = false;
            try
            {
                var entity = Mapper.Map<MasterItem>(dto);
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
        public async Task<bool> InsertAsync(MasterItemDto dto)
        {
            bool result = false;
            try
            {
                var entity = Mapper.Map<MasterItem>(dto);
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
        public bool Update(MasterItemDto dto)
        {
            bool result = false;
            try
            {
                var entity = this.UnitOfWork.GetSingle<MasterItem>(x => x.Id.Equals(dto.Id));
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
        public async Task<bool> UpdateAsync(MasterItemDto dto)
        {
            bool result = false;
            try
            {
                var entity = this.UnitOfWork.GetSingle<MasterItem>(x => x.Id.Equals(dto.Id));
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
                    var iquery = this.UnitOfWork.GetSingle<MasterItem>(x => x.Id == id);
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
                    var iquery = await this.UnitOfWork.GetSingleAsync<MasterItem>(x => x.Id == id);
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
                    var iquery = this.UnitOfWork.GetSingle<MasterItem>(x => x.ITEM_NO.ToUpper().Equals(code.ToUpper()));
                    if (iquery == null) { result = false; }
                }
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return result;
        }
        public bool Import(DataTable dt, string storeCode, string fileName, int timeOut, out string error)
        {
            error = "";
            try
            {
                if (dt != null && !string.IsNullOrEmpty(storeCode))
                {
                    Dictionary<string, object> m_Param = new Dictionary<string, object>()
                    {
                        {"@dt", dt},
                        {"@storeCode", storeCode },
                        {"@fileName", fileName }
                    };
                    var rs = this.UnitOfWork.ExecuteNonQuery(BaseBusiness.SP_MasterItem_Import, m_Param, out error, timeOut * 12);
                    return rs;
                }
            }
            catch (Exception ex)
            {
                this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
                error = ex.Message;
            }
            return false;
        }
    }
}
