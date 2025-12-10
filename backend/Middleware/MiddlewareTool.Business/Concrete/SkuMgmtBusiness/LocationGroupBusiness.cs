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
using static MiddlewareTool.Dto.SkuMappingMgmtDto;

namespace MiddlewareTool.Business.Concrete
{
    public class LocationGroupBusiness : BaseBusiness, ILocationGroupBusiness
    {
        public LocationGroupBusiness(IUnitOfWork unitOfWork) : base(unitOfWork) { }
        public List<LocationGroupDto> GetAllNoTracking()
        {
            try
            {
                var iquery = this.UnitOfWork.GetAllNoTracking<LocationGroup>()
                    .Where(x => x.ActiveFlag == STATUS_ACTIVE)
                    .ToList();

                var mapped = Mapper.Map<List<LocationGroupDto>>(iquery);
                return mapped;
            }
            catch (Exception ex)
            {
                this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
            }
            return new List<LocationGroupDto>();
        }
        public async Task<List<LocationGroupDto>> GetAllAsync()
        {
            try
            {
                var iquery = await this.UnitOfWork.GetAll<LocationGroup>()
                    .Where(x => x.ActiveFlag == STATUS_ACTIVE)
                    .OrderByDescending(x => x.CreateDate)
                    .ToListAsync();

                var mapped = Mapper.Map<List<LocationGroupDto>>(iquery);
                return mapped;
            }
            catch (Exception ex)
            {
                this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
            }
            return new List<LocationGroupDto>();
        }
        public async Task<Tuple<int, List<LocationGroupDto>>> GetPagingAsync(string keyWord, int pageIndex, int pageSize)
        {
            var total = 0;
            try
            {
                var iquery = this.UnitOfWork.GetAll<LocationGroup>()
                    .Where(x => x.ActiveFlag == STATUS_ACTIVE);

                if (!string.IsNullOrEmpty(keyWord))
                {
                    var keyTrim = keyWord.Trim().ToLower();
                    var searchURL = AppValue.ToUnsignString(keyWord.Trim());
                    iquery = iquery.Where(x => false
                        || x.Name.ToLower().Contains(keyTrim)
                        || x.Code.ToLower().Contains(keyTrim)
                        );
                }

                total = await iquery.CountAsync();

                var data = await iquery
                    .OrderByDescending(x => x.CreateDate)
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                var mapped = Mapper.Map<List<LocationGroupDto>>(data);
                return new Tuple<int, List<LocationGroupDto>>(total, mapped);
            }
            catch (Exception ex)
            {
                this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
            }
            return new Tuple<int, List<LocationGroupDto>>(0, new List<LocationGroupDto>());
        }
        public async Task<LocationGroupDto> GetByIdAsync(Guid id)
        {
            try
            {
                var iquery = await this.UnitOfWork
                    .GetAll<LocationGroup>()
                    .FirstOrDefaultAsync(x => x.Id == id && x.ActiveFlag == STATUS_ACTIVE);
                if (iquery != null)
                {
                    var dto = Mapper.Map<LocationGroupDto>(iquery);
                    return dto;
                }
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return null;
        }
        public async Task<bool> InsertAsync(string user, LocationGroupDto dto)
        {
            try
            {
                dto.SetDefaultValueInsert();
                dto.CreateBy = user;
                dto.UpdateBy = user;

                var entity = Mapper.Map<LocationGroup>(dto);
                var add = await this.UnitOfWork.InsertAsync(entity);
                if (add != null) { return true; }
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return false;
        }
        public async Task<bool> UpdateAsync(string user, LocationGroupDto dto)
        {
            try
            {
                var entity = await this.UnitOfWork.GetSingleAsync<LocationGroup>(x => x.Id == dto.Id);
                entity.UpdateDate = DateTime.Now;
                entity.UpdateBy = user;

                entity.Name = dto.Name;
                //entity.Code = dto.Code;
                return await this.UnitOfWork.UpdateAsync(entity);
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return false;
        }
        public async Task<bool> DeleteAsync(string user, Guid id)
        {
            try
            {
                var entity = await this.UnitOfWork.GetSingleAsync<LocationGroup>(x => x.Id == id);
                return await this.UnitOfWork.DeleteAsync(entity);
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return false;
        }
    }
}
