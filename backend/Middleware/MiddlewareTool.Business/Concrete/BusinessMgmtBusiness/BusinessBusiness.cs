using AutoMapper;
using MiddlewareTool.Business.Interface;
using MiddlewareTool.Common;
using MiddlewareTool.Dto;
using MiddlewareTool.Entities;
using MiddlewareTool.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareTool.Business.Concrete
{
    public class BusinessBusiness : BaseBusiness, IBusinessBusiness
    {
        public BusinessBusiness(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public async Task<bool> CheckExist(Guid id)
        {
            try
            {
                return await UnitOfWork.GetAllNoTracking<Entities.Business>().AnyAsync(x => x.Id == id && x.ActiveFlag != STATUS_DELETE);
            }
            catch (Exception ex)
            {
                LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
            }
            return false;
        }
        public bool CheckDelete(Guid id)
        {
            try
            {
                var result = UnitOfWork.GetAllNoTracking<Entities.Business>()
                    .Include(x => x.Headers1).Any(x => x.Id == id
                    && x.ActiveFlag != STATUS_DELETE
                    && !x.Headers1.Any(header => header.ActiveFlag == STATUS_ACTIVE));
                return result;
            }
            catch (Exception ex)
            {
                LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
            }
            return false;
        }
        public async Task<bool> DeleteAsync(Guid id, string userId)
        {
            Entities.Business entity = null;
            bool result = false;
            BusinessDto dto = new BusinessDto() { Id = id, UpdateBy = userId };
            try
            {
                entity = this.UnitOfWork.GetAllNoTracking<Entities.Business>().FirstOrDefault(x => x.Id == id);

                entity.UpdateBy = userId;
                entity.UpdateDate = DateTime.Now;
                entity.ActiveFlag = STATUS_DELETE;
                dto = Mapper.Map<BusinessDto>(entity);

                result = await this.UnitOfWork.UpdateAsync(entity, new List<System.Linq.Expressions.Expression<Func<Entities.Business, object>>>() { x => x.UpdateBy, x => x.UpdateDate, x => x.ActiveFlag });
            }
            catch (Exception ex)
            {
                LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
                dto.Comment = ex.StackTrace + "---" + ex.Message;
            }
            InsertHistory(dto, (int)AppSystemLog.Action.Delete, result ? 1 : 0);
            return result;
        }

        public async Task<List<BusinessDto>> GetAllAsync()
        {
            try
            {
                return UnitOfWork.GetItems<BusinessDto, Entities.Business>(x => x.ActiveFlag == STATUS_ACTIVE).OrderByDescending(x => x.UpdateDate).ToList();
            }
            catch (Exception ex)
            {
                LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
            }
            return null;
        }

        public async Task<BusinessDto> GetAsync(Guid id)
        {
            try
            {
                return await UnitOfWork.GetItemAsync<BusinessDto, Entities.Business>(x => x.Id == id && x.ActiveFlag == STATUS_ACTIVE);
            }
            catch (Exception ex)
            {
                LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
            }
            return null;
        }

        public async Task<Tuple<int, List<BusinessDto>>> GetPagingAsync(SaleOrderFilterDto filter)
        {
            List<BusinessDto> dto = new List<BusinessDto>();
            int totalItem = 0;
            try
            {
                var sql = BaseBusiness.SP_B2B_GetBusiness;

                var paramerters = new Dictionary<string, object>();
                paramerters.Add("@keyword", filter.Keyword?.Trim());
                paramerters.Add("@pageIndex", filter.PageIndex);
                paramerters.Add("@pageSize", filter.PageSize);
                var ds = this.UnitOfWork.ExecuteQuery(sql, paramerters);
                if (ds.Tables != null && ds.Tables.Count > 0)
                {
                    #region TotalItem
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        var obj = row["TotalItem"];
                        if (obj != null)
                            int.TryParse(obj.ToString(), out totalItem);
                    }
                    #endregion
                    #region List item
                    foreach (DataRow row in ds.Tables[1].Rows)
                    {
                        BusinessDto businessDto = new BusinessDto();
                        businessDto.ParseData(row);
                        dto.Add(businessDto);
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
            }
            return new Tuple<int, List<BusinessDto>>(totalItem, dto);
        }

        public async Task<BusinessDto> InsertAsync(BusinessDto dto)
        {
            Entities.Business entity = null;
            bool result = false;
            try
            {
                dto.SetDefaultValueInsert();
                entity = Mapper.Map<Entities.Business>(dto);
                entity = await this.UnitOfWork.InsertAsync(entity);
                result = entity != null;
            }
            catch (Exception ex)
            {
                LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
                dto.Comment = ex.StackTrace + "---" + ex.Message;
            }
            InsertHistory(dto, (int)AppSystemLog.Action.Insert, result ? 1 : 0);
            if (result)
                return dto;
            return null;
        }
        public async Task<bool> UpdateAsync(BusinessDto dto)
        {
            Entities.Business entity = null;
            bool result = false;
            try
            {
                dto.UpdateDate = DateTime.Now;
                entity = this.UnitOfWork.GetAllNoTracking<Entities.Business>().FirstOrDefault(x => x.Id == dto.Id);

                entity.Name = dto.Name;
                entity.TaxName = dto.TaxName;
                entity.CustomerName = dto.CustomerName;
                entity.TaxCode = dto.TaxCode;
                entity.TaxAddress = dto.TaxAddress;
                entity.Email = dto.Email;
                entity.Phone = dto.Phone;
                entity.Fax = dto.Fax;
                entity.PayMethodCode = dto.PayMethodCode;
                entity.UpdateBy = dto.UpdateBy;
                entity.UpdateDate = dto.UpdateDate;
                entity.ActiveFlag = (byte)dto.ActiveFlag;
                entity.City = dto.City;
                entity.District = dto.District;
                entity.Ward = dto.Ward;
                entity.NoStreet = dto.NoStreet;

                result = await this.UnitOfWork.UpdateAsync(entity);
            }
            catch (Exception ex)
            {
                LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
                dto.Comment = ex.StackTrace + "---" + ex.Message;
            }
            InsertHistory(dto, (int)AppSystemLog.Action.Update, result ? 1 : 0);
            return result;
        }
        private void InsertHistory(BusinessDto dto, int action, int sucess)
        {
            var rs = this.UnitOfWork.Insert(new SystemLog
            {
                LogId = Guid.NewGuid(),
                Module = AppModule.BusinessMgmt.ToString(),
                UserId = dto.UpdateBy,
                UserFunction = action,
                EventResult = sucess,
                FuncDateTime = DateTime.Now,
                Source = dto.Id.ToString(),
                Transdata = dto.Comment,
                WSName = ""
            });
        }
    }
}
