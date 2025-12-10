using MiddlewareTool.Business.Interface;
using MiddlewareTool.Common;
using System.Data.Entity;
using MiddlewareTool.Entities;
using MiddlewareTool.Repository;
using System.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MiddlewareTool.Dto.StoreMgmtDto;
using AutoMapper;

namespace MiddlewareTool.Business.Concrete
{
    public class MallBusiness : BaseBusiness, IMallBusiness
    {
        public MallBusiness(IUnitOfWork unitOfWork) : base(unitOfWork) { }
        public async Task<Tuple<int, List<MallDto>>> GetPagingAsync(string userName, string keyWord, int pageIndex, int pageSize)
        {
            int total = 0;
            try
            {
                var iquery = this.UnitOfWork.GetAll<Mall>().Where(x => x.ActiveFlag == STATUS_ACTIVE);
                //if (!string.IsNullOrEmpty(userName))
                //{
                //    iquery = iquery.Where(x => !string.IsNullOrEmpty(x.CreateBy) && x.CreateBy.ToUpper() == userName.ToUpper());
                //}
                if (!string.IsNullOrEmpty(keyWord))
                {
                    var keyTrim = keyWord.Trim().ToLower();
                    iquery = iquery.Where(x => x.Code.ToLower().Equals(keyTrim)
                        || x.Name.ToLower().Contains(keyTrim)
                        );
                }
                total = iquery.Count();
                var data = await iquery.OrderBy(x => x.Code).ThenByDescending(x => x.Name)
                 .Skip((pageIndex - 1) * pageSize)
                 .Take(pageSize)
                 .Select(x => new MallDto
                 {
                     Id = x.Id,
                     Code = x.Code,
                     Name = x.Name,
                     Phone = x.Phone,
                     Email = x.Email,
                     CityName = x.CityName,
                     DistrictName = x.DistrictName,
                     WardName = x.WardName,
                     AddressLine = x.AddressLine,
                     MerchantId = x.MerchantId,
                     Description = x.Description,
                     CreateBy = x.CreateBy,
                     UpdateBy = x.UpdateBy,
                     CreateDate = x.CreateDate,
                     UpdateDate = x.UpdateDate
                 })
                 .ToListAsync();
                return new Tuple<int, List<MallDto>>(total, data);
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return new Tuple<int, List<MallDto>>(total, new List<MallDto>());
        }
        public async Task<List<MallDto>> GetAllMall()
        {
            try
            {
                var iquery = await this.UnitOfWork.GetAllAsync<Mall>(x => x.ActiveFlag == STATUS_ACTIVE);
                if (iquery != null)
                {
                    return Mapper.Map<List<MallDto>>(iquery);
                }
            }
            catch (Exception ex) { LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return null;
        }
        public List<MallDto> GetAllNoTracking()
        {
            try
            {
                var iquery = this.UnitOfWork.GetAllNoTracking<Mall>()
                    .Where(x => x.ActiveFlag == STATUS_ACTIVE);
                if (iquery != null)
                {
                    return Mapper.Map<List<MallDto>>(iquery);
                }
            }
            catch (Exception ex) { LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return null;
        }
        public async Task<MallDto> GetByIdAsync(Guid id)
        {
            try
            {
                var iquery = await this.UnitOfWork.GetSingleAsync<Mall>(x => x.Id == id && x.ActiveFlag == STATUS_ACTIVE);
                if (iquery != null)
                {
                    return Mapper.Map<MallDto>(iquery);
                }
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return null;
        }
        public async Task<bool> InsertAsync(MallDto dto)
        {
            bool result = false;
            try
            {
                var entity = Mapper.Map<Mall>(dto);
                entity.Id = Guid.NewGuid();
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
        public async Task<bool> UpdateAsync(MallDto dto)
        {
            bool result = false;
            try
            {
                var entity = this.UnitOfWork.GetSingle<Mall>(x => x.Id.Equals(dto.Id));
                if (entity != null)
                {
                    //entity.Code = dto.Code;
                    entity.Name = dto.Name;
                    entity.Phone = dto.Phone;
                    entity.Email = dto.Email;
                    entity.AddressLine = dto.AddressLine;
                    entity.CityCode = dto.CityCode;
                    entity.CityName = dto.CityName;
                    entity.DistrictCode = dto.DistrictCode;
                    entity.DistrictName = dto.DistrictName;
                    entity.WardCode = dto.WardCode;
                    entity.WardName = dto.WardName;
                    entity.MerchantId = dto.MerchantId;
                    entity.URL = AppValue.ToUnsignString(dto.Description);
                    entity.UpdateBy = dto.UpdateBy;
                    entity.UpdateDate = DateTime.Now;
                    result = await this.UnitOfWork.UpdateAsync(entity);
                }
            }
            catch (Exception ex) { LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return result;
        }
        public async Task<bool> DeleteAsync(Guid id, string userName)
        {
            bool result = false;
            try
            {
                var entity = this.UnitOfWork.GetSingle<Mall>(x => x.Id == id && x.ActiveFlag == STATUS_ACTIVE);
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
        public async Task<bool> IsExistAsync(Guid id)
        {
            bool result = true;
            try
            {
                if (id != Guid.Empty)
                {
                    var iquery = await this.UnitOfWork.GetSingleAsync<Mall>(x => x.Id == id);
                    if (iquery == null) { result = false; }
                }
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return result;
        }
        public async Task<int> IsExistMallId(Guid? id, string code)
        {
            int result = 0;
            try
            {
                if (code != null)
                {
                    result = await this.UnitOfWork.CountAsync<Mall>(x =>
                    (!id.HasValue || (id.HasValue && x.Id != id.Value))
                    && x.Code.Trim() == code.Trim());
                }
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return result;
        }

    }
}
