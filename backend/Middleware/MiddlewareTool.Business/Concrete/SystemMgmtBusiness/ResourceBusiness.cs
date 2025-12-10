using AutoMapper;
using MiddlewareTool.Business.Interface;
using MiddlewareTool.Entities;
using MiddlewareTool.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using static MiddlewareTool.Dto.SystemMgmtDto;

namespace MiddlewareTool.Business.Concrete
{
    public class ResourceBusiness : BaseBusiness, IResourceBusiness
    {
        #region Constructors
        public ResourceBusiness(IUnitOfWork unitOfWork) : base(unitOfWork) { }
        #endregion

        #region Methods

        public async Task<Tuple<int, List<ResourceDto>>> GetPagingAsync(string keyWord, int pageIndex, int pageSize)
        {
            int total = 0;
            try
            {
                var iquery = this.UnitOfWork.GetAll<Resource>();
                if (!string.IsNullOrEmpty(keyWord))
                {
                    iquery = iquery.Where(x => x.ResourceID.ToUpper().Contains(keyWord.ToUpper()) 
                        || x.DefaultText0.ToUpper().Contains(keyWord.ToUpper())
                        || x.DefaultText1.ToUpper().Contains(keyWord.ToUpper()));
                }
                total = iquery.Count();
                var data = await iquery
                    .OrderByDescending(x => x.CreateDate)
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .Select(x => new ResourceDto
                    {
                        ResourceID = x.ResourceID,
                        DefaultText0 = x.DefaultText0,
                        DefaultText1 = x.DefaultText1,
                        DefaultText2 = x.DefaultText2,
                        CreateDate = x.CreateDate
                    })
                    .ToListAsync();
                return new Tuple<int, List<ResourceDto>>(total, data);
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return new Tuple<int, List<ResourceDto>>(total, new List<ResourceDto>());
        }
        public Dictionary<string, ResourceDto> GetAll()
        {
            var lstResult = new Dictionary<string, ResourceDto>();
            try
            {
                var ds = this.UnitOfWork.ExecuteQuery(BaseBusiness.SP_Resource_GetAll, null);
                if (ds != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        var resource = new ResourceDto();
                        if (ds.Tables[0].Rows[i] != null && resource.ParseData(ds.Tables[0].Rows[i]))
                        {
                            lstResult.Add(resource.ResourceID, resource);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
                lstResult = null;
            }
            return lstResult;
        }
        public bool Import(DataTable resource)
        {
            try
            {
                if (resource != null)
                {
                    Dictionary<string, object> m_Param = new Dictionary<string, object>()
                    {
                        { "@dt", resource}
                    };
                    return this.UnitOfWork.ExecuteNonQuery(BaseBusiness.SP_Resource_Import, m_Param);
                }
            }
            catch (Exception ex)
            {
                this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
            }
            return false;
        }
        /// <summary>
        /// GetByIdAsync
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ResourceDto> GetByIdAsync(string id)
        {
            try
            {
                var iquery = await this.UnitOfWork.GetSingleAsync<Resource>(x => x.ResourceID == id);
                if (iquery != null)
                {
                    return Mapper.Map<ResourceDto>(iquery);
                }
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return null;
        }
        public bool Insert(ResourceDto dto)
        {
            bool result = false;
            try
            {
                var iquery = this.UnitOfWork.GetAll<Resource>()
                    .Where(x => x.ResourceID == dto.ResourceID)
                    .FirstOrDefault();
                if (iquery == null)
                {
                    var entity = Mapper.Map<Resource>(dto);
                    entity.CreateDate = DateTime.Now;
                    var add = this.UnitOfWork.Insert(entity);
                    if (add != null) { result = true; }
                }
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return result;
        }
        /// <summary>
        /// InsertAsync
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<bool> InsertAsync(ResourceDto dto)
        {
            bool result = false;
            try
            {
                var iquery = this.UnitOfWork.GetAll<Resource>()
                   .Where(x => x.ResourceID == dto.ResourceID)
                   .FirstOrDefault();
                if (iquery == null)
                {
                    var entity = Mapper.Map<Resource>(dto);
                    entity.CreateDate = DateTime.Now;
                    var add = await this.UnitOfWork.InsertAsync(entity);
                    if (add != null) { result = true; }
                }
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return result;
        }
        public async Task<bool> InsertToListAsync(List<ResourceDto> LstDto)
        {
            bool result = false;
            try
            {
                var lstID = LstDto.Select(x => x.ResourceID).ToList();
                var entityData = this.UnitOfWork.GetAll<Resource>()
                       .Where(x => lstID.Contains(x.ResourceID))
                       .Select(x => x.ResourceID)
                     .ToList();
                var LstEntity = new List<Resource>();
                foreach (var item in LstDto)
                {
                    var checkExits = entityData.Any(x => x == item.ResourceID);
                    if (!checkExits)
                    {
                        var entity = Mapper.Map<Resource>(item);
                        entity.CreateDate = DateTime.Now;
                        LstEntity.Add(entity);
                    }
                }
                if (LstEntity.Count > 0)
                {
                    var add = await this.UnitOfWork.InsertToListAsync(LstEntity);
                    if (add != null) { result = true; }
                }
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return result;
        }
        public bool Update(ResourceDto dto)
        {
            bool result = false;
            try
            {
                var entity = this.UnitOfWork.GetSingle<Resource>(x => x.ResourceID == dto.ResourceID);
                if (entity != null)
                {
                    entity.ResourceText0 = dto.ResourceText0;
                    entity.ResourceText1 = dto.ResourceText1;
                    entity.DefaultText0 = dto.DefaultText0;
                    entity.DefaultText1 = dto.DefaultText1;
                    result = this.UnitOfWork.Update(entity);
                }
            }
            catch (Exception ex) { LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return result;
        }
        /// <summary>
        /// UpdateAsync
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(ResourceDto dto)
        {
            bool result = false;
            try
            {
                var entity = this.UnitOfWork.GetSingle<Resource>(x => x.ResourceID == dto.ResourceID);
                if (entity != null)
                {
                    entity.ResourceText0 = dto.ResourceText0;
                    entity.ResourceText1 = dto.ResourceText1;
                    entity.DefaultText0 = dto.DefaultText0;
                    entity.DefaultText1 = dto.DefaultText1;
                    result = await this.UnitOfWork.UpdateAsync(entity);
                }
            }
            catch (Exception ex) { LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return result;
        }
        public async Task<bool> DeleteAsync(string id)
        {
            bool result = false;
            try
            {
                var entity = this.UnitOfWork.GetSingle<Resource>(x => x.ResourceID == id);
                if (entity != null)
                {
                    result = await this.UnitOfWork.DeleteAsync(entity, true);
                }
            }
            catch (Exception ex) { LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return result;
        }
        public async Task<bool> DeleteToListAsync(List<string> lstId)
        {
            bool result = false;
            try
            {
                var iquery = await this.UnitOfWork.GetAll<Resource>()
                    .Where(x => lstId.Contains(x.ResourceID))
                    .ToListAsync();
                if (iquery.Count > 0)
                {
                    result = await this.UnitOfWork.DeleteToListAsync(iquery, true);
                }
                else { result = true; }
            }
            catch (Exception ex) { LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return result;
        }

        #endregion
    }
}
