using AutoMapper;
using MiddlewareTool.Business.Interface;
using MiddlewareTool.Common;
using MiddlewareTool.Entities;
using MiddlewareTool.OpenXML;
using MiddlewareTool.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using static MiddlewareTool.Dto.CoreDto;
using static MiddlewareTool.Dto.SkuMappingMgmtDto;

namespace MiddlewareTool.Business.Concrete
{
    public class SkuMappingBusiness : BaseBusiness, ISkuMappingBusiness
    {
        public SkuMappingBusiness(IUnitOfWork unitOfWork) : base(unitOfWork) { }
        public async Task<Tuple<int, List<SkuMappingDto>>> GetPagingAsync(string keyWord, string mallCode, string expressLocationGroupName, string parcelLocationGroupName, int pageIndex, int pageSize)
        {
            var total = 0;
            try
            {
                var iquery = this.UnitOfWork.GetAll<SkuMapping>()
                    .Where(x => x.ActiveFlag == STATUS_ACTIVE);

                if (!string.IsNullOrEmpty(keyWord))
                {
                    var keyTrim = keyWord.Trim().ToLower();
                    var searchURL = AppValue.ToUnsignString(keyWord.Trim());
                    iquery = iquery.Where(x => false
                        || x.MallCode.ToLower().Contains(keyTrim)
                        || x.MallName.ToLower().Contains(keyTrim)
                        || x.ExpressLocationGroupName.ToLower().Contains(keyTrim)
                        || x.ParcelLocationGroupName.ToLower().Contains(keyTrim)
                        );
                }

                if (!string.IsNullOrEmpty(mallCode))
                {
                    var keyTrim = mallCode.Trim().ToLower();
                    iquery = iquery.Where(x => x.MallCode.ToLower().Trim() == keyTrim);
                }

                if (!string.IsNullOrEmpty(expressLocationGroupName))
                {
                    var keyTrim = expressLocationGroupName.Trim().ToLower();
                    iquery = iquery.Where(x => x.ExpressLocationGroupName.ToLower().Trim() == keyTrim);
                }
                if (!string.IsNullOrEmpty(parcelLocationGroupName))
                {
                    var keyTrim = parcelLocationGroupName.Trim().ToLower();
                    iquery = iquery.Where(x => x.ParcelLocationGroupName.ToLower().Trim() == keyTrim);
                }

                total = await iquery.CountAsync();

                var data = await iquery
                    .OrderByDescending(x => x.CreateDate)
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                var mapped = Mapper.Map<List<SkuMappingDto>>(data);
                return new Tuple<int, List<SkuMappingDto>>(total, mapped);
            }
            catch (Exception ex)
            {
                LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
            }
            return new Tuple<int, List<SkuMappingDto>>(0, new List<SkuMappingDto>());
        }

        public List<SkuMappingDto> GetAllNoTracking()
        {
            try
            {
                var iquery = this.UnitOfWork.GetAllNoTracking<SkuMapping>()
                    .Where(x => x.ActiveFlag == STATUS_ACTIVE)
                    .ToList();

                var mapped = Mapper.Map<List<SkuMappingDto>>(iquery);
                return mapped;
            }
            catch (Exception ex)
            {
                LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
            }
            return new List<SkuMappingDto>();
        }

        public async Task<SkuMappingDto> GetByIdAsync(Guid id)
        {
            try
            {
                var iquery = await this.UnitOfWork
                    .GetAll<SkuMapping>()
                    .FirstOrDefaultAsync(x => x.Id == id && x.ActiveFlag == STATUS_ACTIVE);
                if (iquery != null)
                {
                    var dto = Mapper.Map<SkuMappingDto>(iquery);
                    return dto;
                }
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return null;
        }

        public async Task<bool> InsertAsync(string user, SkuMappingDto dto)
        {
            try
            {
                dto.SetDefaultValueInsert();
                dto.CreateBy = user;
                dto.UpdateBy = user;

                var entity = Mapper.Map<SkuMapping>(dto);

                var add = await this.UnitOfWork.InsertAsync(entity);
                if (add != null) { return true; }
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return false;
        }

        public bool Insert(string user, SkuMappingDto dto)
        {
            try
            {
                dto.SetDefaultValueInsert();
                dto.CreateBy = user;
                dto.UpdateBy = user;

                var entity = Mapper.Map<SkuMapping>(dto);

                var add = this.UnitOfWork.Insert(entity);
                if (add != null) { return true; }
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return false;
        }

        public bool InsertToList(string user, List<SkuMappingDto> dtos)
        {
            try
            {
                var entites = new List<SkuMapping>();
                foreach (var dto in dtos)
                {
                    dto.SetDefaultValueInsert();
                    dto.CreateBy = user;
                    dto.UpdateBy = user;

                    var entity = Mapper.Map<SkuMapping>(dto);
                    entites.Add(entity);
                }

                var add = this.UnitOfWork.InsertToList(entites);
                if (add != null) { return true; }
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return false;
        }

        public async Task<bool> UpdateAsync(string user, SkuMappingDto dto)
        {
            try
            {
                var entity = await this.UnitOfWork.GetSingleAsync<SkuMapping>(x => x.Id == dto.Id);
                entity.UpdateDate = DateTime.Now;
                entity.UpdateBy = user;

                entity.MallCode = dto.MallCode;
                entity.MallName = dto.MallName;
                entity.ExpressLocationGroupName = dto.ExpressLocationGroupName;
                entity.ParcelLocationGroupName = dto.ParcelLocationGroupName;
                return await this.UnitOfWork.UpdateAsync(entity);
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return false;
        }

        public bool Update(string user, SkuMappingDto dto)
        {
            try
            {
                var entity = this.UnitOfWork.GetSingle<SkuMapping>(x => x.Id == dto.Id);
                entity.UpdateDate = DateTime.Now;
                entity.UpdateBy = user;

                entity.MallCode = dto.MallCode;
                entity.MallName = dto.MallName;
                entity.ExpressLocationGroupName = dto.ExpressLocationGroupName;
                entity.ParcelLocationGroupName = dto.ParcelLocationGroupName;
                return this.UnitOfWork.Update(entity);
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return false;
        }

        public bool UpdateToList(string user, List<SkuMappingDto> dtos)
        {
            try
            {
                var ids = dtos.Select(x => x.Id);
                var entites = this.UnitOfWork.GetAll<SkuMapping>()
                    .Where(x => ids.Contains(x.Id))
                    .ToList();
                foreach (var entity in entites)
                {
                    var dto = dtos.FirstOrDefault(x => x.Id == entity.Id);
                    if (dto == null)
                    {
                        continue;
                    }

                    entity.UpdateBy = user;
                    entity.UpdateDate = DateTime.Now;

                    entity.MallCode = dto.MallCode;
                    entity.MallName = dto.MallName;
                    entity.ExpressLocationGroupName = dto.ExpressLocationGroupName;
                    entity.ParcelLocationGroupName = dto.ParcelLocationGroupName;
                }

                var rs = this.UnitOfWork.UpdateToList(entites);
                return rs;
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return false;
        }

        public async Task<bool> DeleteAsync(string user, Guid id)
        {
            try
            {
                var entity = await this.UnitOfWork.GetSingleAsync<SkuMapping>(x => x.Id == id);
                return await this.UnitOfWork.DeleteAsync(entity);
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return false;
        }
        public async Task<bool> CheckExistAsync(Guid? id, string mallCode, string expressLocationGroupName, string parcelLocationGroupName)
        {
            try
            {
                var iquery = await this.UnitOfWork
                    .GetAll<SkuMapping>()
                    .FirstOrDefaultAsync(x => x.ActiveFlag == STATUS_ACTIVE
                    && (!id.HasValue || (id.HasValue && x.Id != id.Value))
                    && x.MallCode.ToLower().Trim() == mallCode.ToLower().Trim()
                    && x.ExpressLocationGroupName.ToLower().Trim() == expressLocationGroupName.ToLower().Trim()
                    && x.ParcelLocationGroupName.ToLower().Trim() == parcelLocationGroupName.ToLower().Trim()
                    );
                return iquery != null;
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return false;
        }

        public async Task<List<SkuMappingExportDto>> ExportAsync(string keyWord, string mallCode, string expressLocationGroupName, string parcelLocationGroupName)
        {
            try
            {
                var iquery = this.UnitOfWork.GetAll<SkuMapping>()
                    .Where(x => x.ActiveFlag == STATUS_ACTIVE);

                if (!string.IsNullOrEmpty(keyWord))
                {
                    var keyTrim = keyWord.Trim().ToLower();
                    var searchURL = AppValue.ToUnsignString(keyWord.Trim());
                    iquery = iquery.Where(x => false
                        || x.MallCode.ToLower().Contains(keyTrim)
                        || x.MallName.ToLower().Contains(keyTrim)
                        || x.ExpressLocationGroupName.ToLower().Contains(keyTrim)
                        || x.ParcelLocationGroupName.ToLower().Contains(keyTrim)
                        );
                }

                if (!string.IsNullOrEmpty(mallCode))
                {
                    var keyTrim = mallCode.Trim().ToLower();
                    iquery = iquery.Where(x => x.MallCode.ToLower().Trim() == keyTrim);
                }

                if (!string.IsNullOrEmpty(expressLocationGroupName))
                {
                    var keyTrim = expressLocationGroupName.Trim().ToLower();
                    iquery = iquery.Where(x => x.ExpressLocationGroupName.ToLower().Trim() == keyTrim);
                }
                if (!string.IsNullOrEmpty(parcelLocationGroupName))
                {
                    var keyTrim = parcelLocationGroupName.Trim().ToLower();
                    iquery = iquery.Where(x => x.ParcelLocationGroupName.ToLower().Trim() == keyTrim);
                }

                var data = await iquery
                    .OrderByDescending(x => x.CreateDate)
                    .Select(x => new SkuMappingExportDto()
                    {
                        MallCode = x.MallCode.Trim(),
                        MallName = x.MallName,
                        ExpressLocationGroupName = x.ExpressLocationGroupName,
                        ParcelLocationGroupName = x.ParcelLocationGroupName
                    })
                    .ToListAsync();

                return data;
            }
            catch (Exception ex)
            {
                LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
            }
            return new List<SkuMappingExportDto>();
        }


        //public byte[] GetTransfer(byte[] template)
        //{
        //    try
        //    {
        //        if (template != null && template.Length > 0)
        //        {
        //            var sql = "Exec " + BaseBusiness.SP_SkuMapping_GetTransfer;
        //            var lstData = this.UnitOfWork.SqlQuery<SkuMappingBoxedDto>(sql).ToList();

        //            var m_Excel = new Excel();
        //            m_Excel.TemplateFileData = template;
        //            m_Excel.ParameterData.Add("MallCode", "MallCode");
        //            m_Excel.ParameterData.Add("MallName", "MallName");
        //            m_Excel.ParameterData.Add("StoreCode", "StoreCode");
        //            m_Excel.ParameterData.Add("StoreName", "StoreName");
        //            m_Excel.ParameterData.Add("Sku", "Sku");
        //            m_Excel.ParameterData.Add("ExpressLocationGroupName", "ExpressLocationGroupName");
        //            m_Excel.ParameterData.Add("ParcelLocationGroupName", "ParcelLocationGroupName");
        //            //m_Excel.ParameterData.Add("Fulfillment", "Fulfillment");
        //            var data = m_Excel.Export(lstData);
        //            return data;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
        //    }
        //    return null;
        //}

        //public bool UpdateTransferred(DateTime dateTime)
        //{
        //    try
        //    {
        //        var sql = BaseBusiness.SP_SkuMapping_UpdateTransferred;
        //        var parameters = new Dictionary<string, object>();
        //        parameters.Add("@datetime", dateTime);
        //        return this.UnitOfWork.ExecuteNonQuery(sql, parameters);
        //    }
        //    catch (Exception ex)
        //    {
        //        LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
        //    }
        //    return false;
        //}

        public SkuUploadMonitor InsertUploadMonitor(string user, SkuUploadMonitorDto dto)
        {
            try
            {
                dto.SetDefaultValueInsert();
                dto.CreateBy = user;
                dto.UpdateBy = user;

                var entity = new SkuUploadMonitor()
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

        public bool UpdateCurrentUploadMonitor(string user, SkuUploadMonitor uploadMonitor, int current)
        {
            try
            {
                uploadMonitor.Curent = current.ToString();

                var updated = this.UnitOfWork.Update(uploadMonitor, new List<System.Linq.Expressions.Expression<Func<SkuUploadMonitor, object>>>() { x => x.Curent });
                return updated;
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return false;
        }

        public bool InsertUploadError(string user, SkuUploadErrorDto dto)
        {
            try
            {
                dto.SetDefaultValueInsert();
                dto.CreateBy = user;
                dto.UpdateBy = user;

                var entity = new SkuUploadError()
                {
                    Id = dto.Id,
                    UploadId = dto.UploadId,
                    MallCode = dto.MallCode,
                    ExpressLocationGroupName = dto.ExpressLocationGroupName,
                    ParcelLocationGroupName = dto.ParcelLocationGroupName,
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

        public SkuUploadMonitor GetUploadMonitor(Guid id)
        {
            try
            {
                var entity = this.UnitOfWork.GetSingle<SkuUploadMonitor>(x => x.Id == id);
                return entity;
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return null;
        }

        public int GetCurrentUploadError(Guid uploadId)
        {
            try
            {
                var iquery = this.UnitOfWork.GetAll<SkuUploadError>(x => x.UploadId == uploadId);
                if (iquery == null)
                    return 0;
                return iquery.Count();
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return 0;
        }
        public List<SkuUploadError> GetUploadErrors(Guid uploadId)
        {
            try
            {
                var uploadErrors = this.UnitOfWork.GetAll<SkuUploadError>(x => x.UploadId == uploadId);
                return uploadErrors.ToList();
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return new List<SkuUploadError>();
        }
    }
}
