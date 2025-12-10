using AutoMapper;
using MiddlewareTool.Business.Interface;
using MiddlewareTool.Common;
using MiddlewareTool.Entities;
using MiddlewareTool.Logs;
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
    public class StockBusiness : BaseBusiness, IStockBusiness
    {
        public StockBusiness(IUnitOfWork unitOfWork) : base(unitOfWork) { }
        public async Task<Tuple<int, List<StockDto>>> GetPagingAsync(string userName, string keyWord, int pageIndex, int pageSize)
        {
            int total = 0;
            try
            {
                var iquery = this.UnitOfWork.GetAll<Stock>().Where(x => x.ActiveFlag == STATUS_ACTIVE);
                if (!string.IsNullOrEmpty(userName))
                {
                    iquery = iquery.Where(x => !string.IsNullOrEmpty(x.CreateBy) && x.CreateBy.ToUpper() == userName.ToUpper());
                }
                if (!string.IsNullOrEmpty(keyWord))
                {
                    keyWord = keyWord.ToLower();
                    var search = AppValue.ToUnsignString(keyWord);
                    iquery = iquery.Where(x => x.Sku.ToLower().Equals(keyWord)
                        || x.SkuDesc.ToLower().Contains(keyWord)
                        || x.StoreCode.ToLower().Contains(keyWord)
                        || x.URL.Contains(search));
                }
                total = iquery.Count();
                var data = await iquery.OrderByDescending(x => x.CreateDate).ThenByDescending(x => x.UpdateDate)
                 .Skip((pageIndex - 1) * pageSize)
                 .Take(pageSize)
                 .Select(x => new StockDto
                 {
                     Id = x.Id,
                     RecordFlag = x.RecordFlag,
                     Sku = x.Sku,
                     SkuDesc = x.SkuDesc,
                     SellingPrice = x.SellingPrice,
                     StockOnHandQty = x.StockOnHandQty,
                     StoreCode = x.StoreCode,
                     CreateBy = x.CreateBy,
                     UpdateBy = x.UpdateBy,
                     CreateDate = x.CreateDate,
                     UpdateDate = x.UpdateDate
                 })
                 .ToListAsync();
                return new Tuple<int, List<StockDto>>(total, data);
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return new Tuple<int, List<StockDto>>(total, new List<StockDto>());
        }
        public StockDto GetById(Guid id)
        {
            try
            {
                var iquery = this.UnitOfWork.GetSingle<Stock>(x => x.Id == id && x.ActiveFlag == STATUS_ACTIVE);
                if (iquery != null)
                {
                    return Mapper.Map<StockDto>(iquery);
                }
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return null;
        }
        public async Task<StockDto> GetByIdAsync(Guid id)
        {
            try
            {
                var iquery = await this.UnitOfWork.GetSingleAsync<Stock>(x => x.Id == id && x.ActiveFlag == STATUS_ACTIVE);
                if (iquery != null)
                {
                    return Mapper.Map<StockDto>(iquery);
                }
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return null;
        }
        public StockDto GetByCode(string code)
        {
            try
            {
                var iquery = this.UnitOfWork.GetSingle<Stock>(x => x.Sku.ToUpper().Equals(code.ToUpper()) && x.ActiveFlag == STATUS_ACTIVE);
                if (iquery != null)
                {
                    return Mapper.Map<StockDto>(iquery);
                }
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return null;
        }
        public bool Insert(StockDto dto)
        {
            bool result = false;
            try
            {
                var entity = Mapper.Map<StockDto>(dto);
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
        public async Task<bool> InsertAsync(StockDto dto)
        {
            bool result = false;
            try
            {
                var entity = Mapper.Map<Stock>(dto);
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
        public bool Update(StockDto dto)
        {
            bool result = false;
            try
            {
                var entity = this.UnitOfWork.GetSingle<Stock>(x => x.Id.Equals(dto.Id));
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
        public async Task<bool> UpdateAsync(StockDto dto)
        {
            bool result = false;
            try
            {
                var entity = this.UnitOfWork.GetSingle<Stock>(x => x.Id.Equals(dto.Id));
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
                    var iquery = this.UnitOfWork.GetSingle<Stock>(x => x.Id == id);
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
                    var iquery = await this.UnitOfWork.GetSingleAsync<Stock>(x => x.Id == id);
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
                    var iquery = this.UnitOfWork.GetSingle<Stock>(x => x.Sku.ToUpper().Equals(code.ToUpper()));
                    if (iquery == null) { result = false; }
                }
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return result;
        }
        public bool IsExistBySkuAndStoreCode(string sku, string storeCode)
        {
            bool result = true;
            try
            {
                if (!string.IsNullOrEmpty(sku) && !string.IsNullOrEmpty(storeCode))
                {
                    var iquery = this.UnitOfWork.GetSingle<Stock>(x => x.Sku.Equals(sku) && x.StoreCode.Equals(storeCode));
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
                    return this.UnitOfWork.ExecuteNonQuery(BaseBusiness.SP_Stock_Import, m_Param, timeOut);
                }
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return false;
        }
        public bool ImportHourly(DataTable dt, string fileName, int timeOut, out string error)
        {
            error = "";
            try
            {
                Dictionary<string, object> m_Param = new Dictionary<string, object>()
                    {
                        {"@dt", dt},
                        {"@fileName", fileName}
                    };
                return this.UnitOfWork.ExecuteNonQuery(BaseBusiness.SP_Stock_ImportHourly, m_Param, out error, timeOut);
            }
            catch (Exception ex)
            {
                this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
                error = ex.Message;
            }
            return false;
        }
        //public List<StockDto> GetStockUpdate(DataTable dt)
        //{
        //   try
        //   {
        //      var result = new List<StockDto>();
        //      if (dt != null)
        //      {
        //         Dictionary<string, object> m_Param = new Dictionary<string, object>()
        //              {
        //                  {"@dt", dt}
        //              };
        //         var dataTable = this.UnitOfWork.ExecuteQuery(BaseBusiness.SP_Stock_GetStockUpdate, m_Param);
        //         for (int i = 0; i < dataTable.Tables[0].Rows.Count; i++)
        //         {
        //            var resource = new StockDto();
        //            if (dataTable.Tables[0].Rows[i] != null && resource.ParseData(dataTable.Tables[0].Rows[i]))
        //            {
        //               result.Add(resource.ResourceID, resource);
        //            }
        //         }

        //         Logging.LogInfo($"GetStockUpdate Result: {result}");
        //      }
        //   }
        //   catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
        //   return null;
        //}
    }
}
