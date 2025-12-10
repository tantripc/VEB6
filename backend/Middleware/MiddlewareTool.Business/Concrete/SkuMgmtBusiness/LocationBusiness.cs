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
    public class LocationBusiness : BaseBusiness, ILocationBusiness
    {
        public LocationBusiness(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public async Task<List<LocationDto>> GetAllAsync()
        {
            try
            {
                var iquery = await this.UnitOfWork.GetAll<Location>()
                    .Where(x => x.ActiveFlag == STATUS_ACTIVE)
                    .OrderByDescending(x => x.CreateDate)
                    .ToListAsync();

                var mapped = Mapper.Map<List<LocationDto>>(iquery);
                return mapped;
            }
            catch (Exception ex)
            {
                this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
            }
            return new List<LocationDto>();
        }

        public List<LocationDto> GetAllNoTracking()
        {
            try
            {
                var iquery = this.UnitOfWork.GetAllNoTracking<Location>()
                    .Where(x => x.ActiveFlag == STATUS_ACTIVE)
                    .ToList();

                var mapped = Mapper.Map<List<LocationDto>>(iquery);
                return mapped;
            }
            catch (Exception ex)
            {
                LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
            }
            return new List<LocationDto>();
        }

        public async Task<Tuple<int, List<LocationDto>>> GetPagingAsync(string keyWord, string locationGroupName, int pageIndex, int pageSize)
        {
            var total = 0;
            try
            {
                var iquery = this.UnitOfWork.GetAll<Location>()
                    .Where(x => x.ActiveFlag == STATUS_ACTIVE);

                if (!string.IsNullOrEmpty(keyWord))
                {
                    var keyTrim = keyWord.Trim().ToLower();
                    var searchURL = AppValue.ToUnsignString(keyWord.Trim());
                    iquery = iquery.Where(x => false
                        || x.Name.ToLower().Contains(keyTrim)
                        || x.CityCode.ToLower().Contains(keyTrim)
                        || x.CityName.ToLower().Contains(keyTrim)
                        || x.DistrictCode.ToLower().Contains(keyTrim)
                        || x.DistrictName.ToLower().Contains(keyTrim)
                        || x.WardCode.ToLower().Contains(keyTrim)
                        || x.WardName.ToLower().Contains(keyTrim)
                        );
                }

                if (!string.IsNullOrEmpty(locationGroupName))
                {
                    var keyTrim = locationGroupName.Trim().ToLower();
                    iquery = iquery.Where(x => x.Name.ToLower().Trim() == keyTrim);
                }

                total = await iquery.CountAsync();

                var data = await iquery
                    .OrderByDescending(x => x.CreateDate)
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                var mapped = Mapper.Map<List<LocationDto>>(data);
                return new Tuple<int, List<LocationDto>>(total, mapped);
            }
            catch (Exception ex)
            {
                this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
            }
            return new Tuple<int, List<LocationDto>>(0, new List<LocationDto>());
        }
        public async Task<LocationDto> GetByIdAsync(Guid id)
        {
            try
            {
                var iquery = await this.UnitOfWork
                    .GetAll<Location>()
                    .FirstOrDefaultAsync(x => x.Id == id && x.ActiveFlag == STATUS_ACTIVE);
                if (iquery != null)
                {
                    var dto = Mapper.Map<LocationDto>(iquery);
                    return dto;
                }
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return null;
        }

        public async Task<bool> InsertAsync(string user, LocationDto dto)
        {
            try
            {
                dto.SetDefaultValueInsert();
                dto.CreateBy = user;
                dto.UpdateBy = user;

                var entity = Mapper.Map<Location>(dto);
                var add = await this.UnitOfWork.InsertAsync(entity);
                if (add != null) { return true; }
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return false;
        }

        public bool Insert(string user, LocationDto dto)
        {
            try
            {
                dto.SetDefaultValueInsert();
                dto.CreateBy = user;
                dto.UpdateBy = user;

                var entity = Mapper.Map<Location>(dto);
                var add = this.UnitOfWork.Insert(entity);
                if (add != null) { return true; }
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return false;
        }

        public async Task<bool> UpdateAsync(string user, LocationDto dto)
        {
            try
            {
                var entity = await this.UnitOfWork.GetSingleAsync<Location>(x => x.Id == dto.Id);
                entity.UpdateDate = DateTime.Now;
                entity.UpdateBy = user;

                entity.Name = dto.Name;
                entity.CityCode = dto.CityCode;
                entity.CityName = dto.CityName;
                entity.DistrictCode = dto.DistrictCode;
                entity.DistrictName = dto.DistrictName;
                entity.WardCode = dto.WardCode;
                entity.WardName = dto.WardName;
                return await this.UnitOfWork.UpdateAsync(entity);
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return false;
        }

        public bool Update(string user, LocationDto dto)
        {
            try
            {
                var entity = this.UnitOfWork.GetSingle<Location>(x => x.Id == dto.Id);
                entity.UpdateDate = DateTime.Now;
                entity.UpdateBy = user;

                entity.Name = dto.Name;
                entity.CityCode = dto.CityCode;
                entity.CityName = dto.CityName;
                entity.DistrictCode = dto.DistrictCode;
                entity.DistrictName = dto.DistrictName;
                entity.WardCode = dto.WardCode;
                entity.WardName = dto.WardName;
                return this.UnitOfWork.Update(entity);
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return false;
        }
        public async Task<bool> DeleteAsync(string user, Guid id)
        {
            try
            {
                var entity = await this.UnitOfWork.GetSingleAsync<Location>(x => x.Id == id);
                return await this.UnitOfWork.DeleteAsync(entity);
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return false;
        }

        public async Task<List<LocationDto>> ExportAsync(string keyWord, string locationGroupName)
        {
            var lstData = new List<LocationDto>();
            try
            {
                var iquery = this.UnitOfWork.GetAll<Location>()
                    .Where(x => x.ActiveFlag == STATUS_ACTIVE);

                if (!string.IsNullOrEmpty(keyWord))
                {
                    var keyTrim = keyWord.Trim().ToLower();
                    var searchURL = AppValue.ToUnsignString(keyWord.Trim());
                    iquery = iquery.Where(x => false
                        || x.Name.ToLower().Contains(keyTrim)
                        || x.CityCode.ToLower().Contains(keyTrim)
                        || x.CityName.ToLower().Contains(keyTrim)
                        || x.DistrictCode.ToLower().Contains(keyTrim)
                        || x.DistrictName.ToLower().Contains(keyTrim)
                        || x.WardCode.ToLower().Contains(keyTrim)
                        || x.WardName.ToLower().Contains(keyTrim)
                        );
                }

                if (!string.IsNullOrEmpty(locationGroupName))
                {
                    var keyTrim = locationGroupName.Trim().ToLower();
                    iquery = iquery.Where(x => x.Name.ToLower().Trim() == keyTrim);
                }


                var data = await iquery
                    .OrderByDescending(x => x.CreateDate)
                    .ToListAsync();

                var mapped = Mapper.Map<List<LocationDto>>(data);
                return mapped;
            }
            catch (Exception ex)
            {
                this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
            }
            return lstData;
        }

        public LocationUploadMonitor InsertUploadMonitor(string user, LocationUploadMonitorDto dto)
        {
            try
            {
                dto.SetDefaultValueInsert();
                dto.CreateBy = user;
                dto.UpdateBy = user;

                var entity = new LocationUploadMonitor()
                {
                    Id = dto.Id,
                    FileName = dto.FileName,
                    FileContent = dto.FileContent,
                    FileExt = dto.FileExt,
                    TotalRow = dto.TotalRow,
                    Curent = dto.Curent,
                    CreateBy = dto.CreateBy,
                    UpdateBy = dto.UpdateBy,
                    CreateDate = dto.CreateDate,
                    UpdateDate = dto.UpdateDate,
                    Description = dto.Description,
                    OrderNumber = dto.OrderNumber,
                    URL = dto.URL,
                    ActiveFlag = (byte)dto.ActiveFlag
                };

                var add = this.UnitOfWork.Insert(entity);
                if (add != null) { return add; }
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return null;
        }

        public bool InsertUploadError(string user, LocationUploadErrorDto dto)
        {
            try
            {
                dto.SetDefaultValueInsert();
                dto.CreateBy = user;
                dto.UpdateBy = user;

                var entity = new LocationUploadError()
                {
                    Id = dto.Id,
                    UploadId = dto.UploadId,
                    CityCode = dto.CityCode,
                    CityName = dto.CityName,
                    DistrictCode = dto.DistrictCode,
                    DistrictName = dto.DistrictName,
                    WardCode = dto.WardCode,
                    WardName = dto.WardName,
                    LocationGroupName = dto.LocationGroupName,
                    Infor = dto.Infor,
                    CreateBy = dto.CreateBy,
                    UpdateBy = dto.UpdateBy,
                    CreateDate = dto.CreateDate,
                    UpdateDate = dto.UpdateDate,
                    Description = dto.Description,
                    OrderNumber = dto.OrderNumber,
                    URL = dto.URL,
                    ActiveFlag = (byte)dto.ActiveFlag
                };

                var add = this.UnitOfWork.Insert(entity);
                if (add != null) { return true; }
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return false;
        }

        public bool UpdateCurrentUploadMonitor(string user, LocationUploadMonitor uploadMonitor, int current)
        {
            try
            {
                uploadMonitor.Curent = current.ToString();

                var updated = this.UnitOfWork.Update(uploadMonitor, new List<System.Linq.Expressions.Expression<Func<LocationUploadMonitor, object>>>() { x => x.Curent });
                return updated;
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return false;
        }

        public LocationUploadMonitor GetUploadMonitor(Guid id)
        {
            try
            {
                var entity = this.UnitOfWork.GetSingle<LocationUploadMonitor>(x => x.Id == id);
                return entity;
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return null;
        }

        public int GetCurrentUploadError(Guid uploadId)
        {
            try
            {
                var iquery = this.UnitOfWork.GetAll<LocationUploadError>(x => x.UploadId == uploadId);
                if (iquery == null)
                    return 0;
                return iquery.Count();
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return 0;
        }

        public List<LocationUploadError> GetUploadErrors(Guid uploadId)
        {
            try
            {
                var uploadErrors = this.UnitOfWork.GetAll<LocationUploadError>(x => x.UploadId == uploadId);
                return uploadErrors.ToList();
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return new List<LocationUploadError>();
        }
    }
}
