using AutoMapper;
using MiddlewareTool.Business.Interface;
using MiddlewareTool.Dto;
using MiddlewareTool.Entities;
using MiddlewareTool.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace MiddlewareTool.Business.Concrete
{
    public class ProductHistoryBusiness : BaseBusiness, IProductHistoryBusiness
    {
        public ProductHistoryBusiness(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public List<ProductHistoryDto> Export(string keyWord, int historyType, string searchStore, string dateFrom, string dateTo, Guid createdBy, Guid updatedBy, int action)
        {
            throw new NotImplementedException();
        }

        public async Task<Tuple<int, List<ProductHistoryDto>>> GetPagingAsync(int pageIndex, string keyWord, string searchStore,
            string dateFrom, string dateTo, string createdBy, string updatedBy, int action, int pageSize)
        {
            int total = 0;
            List<ProductHistoryDto> inventoryHistoryDtos = new List<ProductHistoryDto>();
            Tuple<int, List<ProductHistoryDto>> lstResult = new Tuple<int, List<ProductHistoryDto>>(total, inventoryHistoryDtos);
            try
            {
                DateTime fromDate = DateTime.Parse(dateFrom);
                DateTime toDate = DateTime.Parse(dateTo);
                var sql = "EXEC" + BaseBusiness.SP_ProductHistory_Search;
                #region Build query
                if (!string.IsNullOrEmpty(keyWord))
                {
                    sql += $" @keyWord = N'{keyWord.Trim()}',";
                }
                sql += $"@action = {action},";
                if (!string.IsNullOrEmpty(createdBy))
                {
                    sql += $" @createdBy = N'{createdBy}',";
                }
                //if (!string.IsNullOrEmpty(searchStore))
                //{
                //    sql += $" @storeCode = '{searchStore}',";
                //}
                if (!string.IsNullOrEmpty(updatedBy))
                {
                    sql += $" @updatedBy = N'{updatedBy}',";
                }
                if (pageSize == int.MaxValue)
                    sql += " @isExport = 1,";
                sql += $"@dateFrom = '{dateFrom}', @dateTo = '{dateTo}',";
                sql += $" @pageIndex= {pageIndex}, @pageSize= {pageSize};";
                #endregion

                var data = this.UnitOfWork.SqlQuery<ProductHistoryDto>(sql).ToList();
                if (data.Count > 0)
                    total = data[0].Total;
                return new Tuple<int, List<ProductHistoryDto>>(total, data);
            }
            catch (Exception ex)
            {
                this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
                lstResult = null;
            }
            return lstResult;
        }

        public bool Insert(ProductHistoryDto dto)
        {
            bool result = false;
            try
            {
                var entity = Mapper.Map<ProductHistory>(dto);
                var res = this.UnitOfWork.Insert(entity);
                if (res != null) { result = true; }
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return result;
        }
        public bool InsertList(List<ProductHistoryDto> list)
        {
            bool result = false;
            try
            {
                var entity = Mapper.Map<List<ProductHistory>>(list);
                var res = this.UnitOfWork.InsertToList(entity);
                if (res != null) { result = true; }
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return result;
        }
        public async Task<bool> InsertAsync(ProductHistoryDto dto)
        {
            bool result = false;
            try
            {
                var entity = Mapper.Map<ProductHistory>(dto);
                var res = await this.UnitOfWork.InsertAsync(entity);
                if (res != null) { result = true; }
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return result;
        }
        public async Task<bool> InsertListAsync(List<ProductHistoryDto> list)
        {
            bool result = false;
            try
            {
                var entity = Mapper.Map<List<ProductHistory>>(list);
                var res = await this.UnitOfWork.InsertToListAsync(entity);
                if (res != null) { result = true; }
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return result;
        }
    }
}
