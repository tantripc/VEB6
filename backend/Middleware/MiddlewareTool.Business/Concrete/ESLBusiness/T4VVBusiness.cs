using AutoMapper;
using MiddlewareTool.Business.Interface;
using MiddlewareTool.Dto;
using MiddlewareTool.Entities;
using MiddlewareTool.Repository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace MiddlewareTool.Business.Concrete
{
    public class T4VVBusiness : BaseBusiness, IT4VVBusiness
    {
        public T4VVBusiness(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public List<T4VVDtoPaging> GetPaging(T4VVDtoSearchModel searchModel)
        {
            try
            {
                var startDate = searchModel.StartDate.Value.Date;
                var endDate = searchModel.EndDate.Value.Date.AddDays(1).AddTicks(-1);
                string sql = $@"
    EXEC [core].[SP_T4VV_GetPaging] 
        @StartDate = '{startDate:yyyy-MM-dd HH:mm:ss}', 
        @EndDate = '{endDate:yyyy-MM-dd HH:mm:ss}', 
        @Keyword = {(string.IsNullOrEmpty(searchModel.Keyword) ? "NULL" : $"'{searchModel.Keyword}'")}, 
        @StoreCode = {((searchModel.StoreList != null && searchModel.StoreList.Count > 0) ? $"'{string.Join(",", searchModel.StoreList.Where(x => !string.IsNullOrEmpty(x)).ToList())}'" : "NULL")}, 
        @T4VVFlag = {(searchModel.T4VVFlag == 0 ? "NULL" : searchModel.T4VVFlag.ToString())},
        @LineId = {(searchModel.LineId.Count > 0 ? $"'{string.Join(",", searchModel.LineId)}'" : "NULL")}, 
        @DivisionId = {(searchModel.DivisionId.Count > 0 ? $"'{string.Join(",", searchModel.DivisionId)}'" : "NULL")}, 
        @GroupId = {(searchModel.GroupId.Count > 0 ? $"'{string.Join(",", searchModel.GroupId)}'" : "NULL")}, 
         @PageIndex = {searchModel.PageIndex}, 
        @PageSize = {searchModel.PageSize}";


                var rs = this.UnitOfWork.SqlQuery<T4VVDtoPaging>(sql).OrderByDescending(x => x.UpdateDate).ToList();
                return rs;
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return new List<T4VVDtoPaging>();
        }

        public async Task<List<T4VVDto>> GetDetailBySKU(T4VVDtoSearchModel searchModel)
        {
            try
            {
                var endDate = searchModel.EndDate.Value.Date.AddDays(1).AddTicks(-1);
                var currentDateTime = DateTime.Now;
                var entity = this.UnitOfWork.GetAllNoTracking<PriceChange>()
                    .Where(x => x.ITEM_NO == searchModel.SKU
                             && x.ActiveFlag == STATUS_ACTIVE
                             && x.IsT4VV == true
                             && x.StartDateTime <= endDate
                             && x.EndDateTime >= searchModel.StartDate
                             && x.EndDateTime >= currentDateTime);

                if (searchModel.StoreCode != null)
                {
                    entity = entity.Where(x => x.StoreCode == searchModel.StoreCode);
                }

                if (searchModel.T4VVFlag == 0) // All
                {
                }
                else if (searchModel.T4VVFlag == 1) // Active
                {
                    entity = entity.Where(x =>
                        x.StartDateTime <= currentDateTime &&
                        x.EndDateTime >= currentDateTime);
                }
                else if (searchModel.T4VVFlag == 2) // Upcoming
                {
                    entity = entity.Where(x => x.StartDateTime > currentDateTime);
                }
                //else // Expired
                //{
                //    entity = entity.Where(x => x.EndDateTime < currentDateTime && x.StartDateTime < currentDateTime);
                //}
                var result = await entity
                    .Join(this.UnitOfWork.GetAllNoTracking<Store>().Where(x => x.ActiveFlag == STATUS_ACTIVE),
                        promo => promo.StoreCode,
                        store => store.Code,
                        (promo, store) => new { promo, store })
                    .Join(this.UnitOfWork.GetAllNoTracking<Product>().Where(x => x.ActiveFlag == STATUS_ACTIVE),
                        combined => combined.promo.ITEM_NO,
                        product => product.Code,
                        (combined, product) => new T4VVDto
                        {
                            Id = combined.promo.Id,
                            SKU = combined.promo.ITEM_NO,
                            StoreName = combined.store.Name,
                            StoreCode = combined.promo.StoreCode,
                            PRC_START_DATE = combined.promo.PRC_START_DATE,
                            PRC_END_DATE = combined.promo.PRC_END_DATE,
                            PRC_START_TIME = combined.promo.PRC_START_TIME,
                            PRC_END_TIME = combined.promo.PRC_END_TIME,
                            ProductName = product.Name
                        })
                    .ToListAsync();
                result.ForEach(x => x.TotalCount = result.Count);
                return result;
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return new List<T4VVDto>();
        }
        public Tuple<List<T4VVHistoryDto>, int> GetHistoryPaging(T4VVDtoSearchModel searchModel)
        {
            var result = new List<T4VVHistoryDto>();
            try
            {
                if (searchModel.EndDate.HasValue)
                {
                    searchModel.EndDate = searchModel.EndDate.Value.AddDays(1).Date.AddMilliseconds(-1);
                }
                var entity = this.UnitOfWork.GetAll<PriceChangeHistory>()
                                            .Where(x => x.UpdateDate >= searchModel.StartDate
                                                     && x.UpdateDate <= searchModel.EndDate);
                if (!String.IsNullOrEmpty(searchModel.SKU))
                {
                    entity = entity.Where(x => x.ITEM_NO == searchModel.SKU);
                }
                if (searchModel.StoreCode != null)
                {
                    entity = entity.Where(x => x.StoreCode == searchModel.StoreCode);
                }
                var currentDateTime = DateTime.Now;
                if (searchModel.T4VVFlag == 0) // All
                {
                }
                else if (searchModel.T4VVFlag == 1) // Upcoming
                {
                    entity = entity.Where(x =>
                        x.StartDateTime <= currentDateTime &&
                        x.EndDateTime >= currentDateTime);
                }
                else if (searchModel.T4VVFlag == 2) // Upcoming
                {
                    entity = entity.Where(x => x.StartDateTime > currentDateTime);
                }
                else // Expired
                {
                    entity = entity.Where(x => x.EndDateTime < currentDateTime && x.StartDateTime < currentDateTime);
                }

                if (searchModel.Action.HasValue)
                {
                    entity = entity.Where(x => x.Action == searchModel.Action);
                }
                int total = entity.Count();
                var rs = entity.OrderByDescending(x => x.UpdateDate)
                    .Join(this.UnitOfWork.GetAllNoTracking<UserInfo>().Where(x => x.ActiveFlag == STATUS_ACTIVE),
                        promo => promo.UpdateBy,
                        user => user.UserId,
                        (promo, user) => new T4VVHistoryDto
                        {
                            Id = promo.Id,
                            SKU = promo.ITEM_NO,
                            StoreCode = promo.StoreCode,
                            PRC_START_DATE = promo.PRC_START_DATE,
                            PRC_END_DATE = promo.PRC_END_DATE,
                            PRC_START_TIME = promo.PRC_START_TIME,
                            PRC_END_TIME = promo.PRC_END_TIME,
                            UpdateBy = user.FullName,
                            UpdateDate = promo.UpdateDate,
                            Action = promo.Action,
                            Source = promo.Source,
                            IsTransferESL = promo.IsTransferESL ?? false,
                            T4VVFlagHistory = promo.T4VVFlag == 1 ? "Active" : promo.T4VVFlag == 2 ? "Upcoming" : "Expried",

                        })
                    .OrderByDescending(x => x.UpdateDate)
                    .Skip((searchModel.PageIndex - 1) * searchModel.PageSize)
                    .Take(searchModel.PageSize)
                    .ToList();
                return new Tuple<List<T4VVHistoryDto>, int>(rs, total);
            }
            catch (Exception ex)
            {
                this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
            }
            return new Tuple<List<T4VVHistoryDto>, int>(result, 0);
        }

    }
}
