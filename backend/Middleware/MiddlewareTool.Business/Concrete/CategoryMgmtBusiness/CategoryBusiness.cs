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
using static MiddlewareTool.Dto.CategoryMgmtDto;

namespace MiddlewareTool.Business.Concrete
{
    public class CategoryBusiness : BaseBusiness, ICategoryBusiness
    {
        private readonly ICategoryMasterBusiness _categoryMasterBusiness;
        public CategoryBusiness(IUnitOfWork unitOfWork, ICategoryMasterBusiness categoryMasterBusiness) : base(unitOfWork)
        {
            _categoryMasterBusiness = categoryMasterBusiness;
        }
        public List<CategoryDto> GetAll()
        {
            try
            {
                var iquery = this.UnitOfWork.GetAll<Category>()
                    .Where(x => x.ActiveFlag == STATUS_ACTIVE)
                    .OrderBy(x => x.Path)
                    .ToList();
                var mapped = Mapper.Map<List<CategoryDto>>(iquery);
                return mapped;
            }
            catch (Exception ex)
            {
                LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
            }
            return new List<CategoryDto>();
        }
        public async Task<Tuple<int, List<CategoryDto>>> GetPagingAsync(string userName, string keyWord, Guid? parentId, int pageIndex, int pageSize)
        {
            int total = 0;
            try
            {
                var iquery = this.UnitOfWork.GetAll<Category>()
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
                if (parentId.HasValue)
                {
                    iquery = iquery.Where(x => x.ParentId == parentId.Value);
                }
                else
                {
                    iquery = iquery.Where(x => x.ParentId == null || parentId == Guid.Empty);
                }

                total = await iquery.CountAsync();

                var data = await iquery
                    .Select(x => Mapper.Map<CategoryDto>(x))
                    .ToListAsync();
                return new Tuple<int, List<CategoryDto>>(total, data);
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return new Tuple<int, List<CategoryDto>>(total, new List<CategoryDto>());
        }
        public Tuple<int, List<CategoryDto>> GetNode(string userName, string keyword, Guid currentNodeId)
        {
            int total = 0;
            try
            {
                var iquery = this.UnitOfWork.GetAll<Category>()
                .Where(x => x.ActiveFlag == STATUS_ACTIVE);

                if (!string.IsNullOrEmpty(keyword))
                {
                    iquery = iquery.Where(x => x.Code.Contains(keyword) || x.Name.Contains(keyword));
                }
                else
                {
                    iquery = iquery.Where(x => x.ParentId == null);
                }

                total = iquery.Count();

                var data = iquery.OrderBy(x => x.CreateDate).ToList();

                var mapped = Mapper.Map<List<CategoryDto>>(data);

                #region Thêm cấp cha gần nhất cho trường hợp tìm kiếm
                if (!string.IsNullOrEmpty(keyword))
                {
                    var ids = data.Select(x => x.Id).ToList();
                    var parentIds = data.Where(x => x.ParentId != null).Select(x => x.ParentId).Distinct().ToList();
                    var parentLv1Ids = parentIds.Where(x => !ids.Contains(x.Value)).ToList();
                    var parentsLv1 = this.UnitOfWork.GetAll<Category>()
                        .Where(x => parentLv1Ids.Contains(x.Id)).ToList() ?? new List<Category>();
                    var parentsLv1Dto = Mapper.Map<List<CategoryDto>>(parentsLv1);
                    parentsLv1Dto.ForEach(x => x.ParentId = null);
                    mapped.AddRange(parentsLv1Dto);
                }
                #endregion

                return new Tuple<int, List<CategoryDto>>(total, mapped);
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return new Tuple<int, List<CategoryDto>>(total, new List<CategoryDto>());
        }
        public Tuple<List<CategoryDto>> GetChildNode(Guid currentNodeId)
        {
            try
            {
                var iquery = this.UnitOfWork.GetAll<Category>()
                    .Where(x => x.ActiveFlag == STATUS_ACTIVE
                    && x.ParentId == currentNodeId);


                var data = iquery
                    .ToList();
                var mapped = Mapper.Map<List<CategoryDto>>(data);
                return new Tuple<List<CategoryDto>>(mapped);
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return new Tuple<List<CategoryDto>>(new List<CategoryDto>());
        }
        public async Task<CategoryDto> GetByIdAsync(Guid id)
        {
            try
            {
                var iquery = await this.UnitOfWork
                    .GetAll<Category>()
                    .Include(x => x.Mappings)
                    .FirstOrDefaultAsync(x => x.Id == id && x.ActiveFlag == STATUS_ACTIVE);
                if (iquery != null)
                {
                    var dto = Mapper.Map<CategoryDto>(iquery);
                    if (dto.Mappings != null)
                    {
                        var mappingIds = dto.Mappings.Select(x => x.CategoryMasterId).ToList();
                        dto.Masters = await _categoryMasterBusiness.GetByIdsAsync(mappingIds);
                    }
                    return dto;
                }
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return null;
        }
        public async Task<CategoryDto> GetByCodeAsync(string code)
        {
            try
            {
                var iquery = await this.UnitOfWork.GetSingleAsync<Category>(x => x.Code == code && x.ActiveFlag == STATUS_ACTIVE);
                if (iquery != null)
                {
                    return Mapper.Map<CategoryDto>(iquery);
                }
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return null;
        }
        public async Task<CategoryDto> InsertAsync(string user, CategoryDto dto)
        {
            try
            {
                var entity = Mapper.Map<Category>(dto);
                entity.Id = Guid.NewGuid();
                entity.CreateDate = DateTime.Now;
                entity.UpdateDate = DateTime.Now;
                entity.CreateBy = user;
                entity.UpdateBy = user;
                entity.Mappings.ToList().ForEach(x =>
                {
                    x.Id = Guid.NewGuid();
                    x.CategoryId = entity.Id;
                });
                entity.IsTransfer = false;
                entity.IsNew = true;
                var add = await this.UnitOfWork.InsertAsync(entity);
                if (add != null) { return Mapper.Map<CategoryDto>(add); }
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return null;
        }
        public async Task<bool> UpdateAsync(string user, CategoryDto dto)
        {
            bool result = false;
            var trans = this.UnitOfWork.BeginTransaction();
            try
            {
                var entity = await this.UnitOfWork.GetSingleAsync<Category>(x => x.Id == dto.Id);
                entity.UpdateDate = DateTime.Now;
                entity.UpdateBy = user;
                entity.Code = dto.Code;

                var isChangeName = entity.Name != dto.Name || entity.ParentId != dto.ParentId;
                if (isChangeName)
                {
                    if (dto.ParentId.HasValue)
                    {
                        #region Cập nhật Path chính xác theo Category cha (nhiều cấp)
                        var parentNames = GetParentNodesName(dto.ParentId.Value);
                        parentNames.Reverse();
                        if (parentNames.Count <= 0)
                            entity.Path = dto.Name;
                        else
                        {
                            entity.Path = string.Join(" > ", parentNames);
                        }
                        #endregion
                    }
                    entity.Path += (string.IsNullOrEmpty(entity.Path) ? "" : " > ") + dto.Name;
                }

                entity.ParentId = dto.ParentId;
                entity.Name = dto.Name;
                entity.IsTransfer = false;
                result = await this.UnitOfWork.UpdateAsync(entity);
                this.UnitOfWork.Commit(trans);

                if (isChangeName)
                {
                    #region Cập nhật Path của category con (nhiều cấp) nếu có thay đổi Path
                    var sql = "[cat].[SP_Category_UpdatePath]";
                    var parameters = new Dictionary<string, object>();
                    parameters.Add("@categoryId", dto.Id);
                    result = this.UnitOfWork.ExecuteNonQuery(sql, parameters);
                    #endregion
                }
            }
            catch (Exception ex)
            {
                LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
                this.UnitOfWork.Rollback(trans);
            }
            return result;
        }
        public async Task<bool> DeleteAsync(string user, Guid id)
        {
            bool result = false;
            try
            {
                var entity = await this.UnitOfWork.GetSingleAsync<Category>(x => x.Id == id && x.ActiveFlag == STATUS_ACTIVE);

                if (entity != null)
                {
                    var sql = "[cat].[SP_Category_Delete]";
                    var parameters = new Dictionary<string, object>();
                    parameters.Add("@categoryId", id);
                    parameters.Add("@userId", user);
                    result = this.UnitOfWork.ExecuteNonQuery(sql, parameters);
                }
            }
            catch (Exception ex) { LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return result;
        }
        public bool CheckRef(Guid id)
        {
            bool result = false;
            try
            {
                var entity = this.UnitOfWork.GetSingle<Category>(x => x.Id == id && x.ActiveFlag == STATUS_ACTIVE);

                if (entity != null)
                {
                    var sql = "[cat].[SP_Category_CheckRef]";
                    var parameters = new Dictionary<string, object>();
                    parameters.Add("@categoryId", id);
                    var iquery = this.UnitOfWork.ExecuteScalar(sql, parameters);
                    if (iquery != null)
                        bool.TryParse(iquery.ToString(), out result);
                }
            }
            catch (Exception ex) { LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return result;
        }
        public async Task<bool> HasChildAsync(CategoryDto dto)
        {
            var count = await this.UnitOfWork.CountAsync<Category>(x => x.ActiveFlag == STATUS_ACTIVE
            && x.ParentId == dto.Id
            );
            return count > 0;
        }
        public async Task<bool> AddToMappingAsync(MappingDto dto)
        {
            try
            {
                var entity = Mapper.Map<Mapping>(dto);
                entity.Id = Guid.NewGuid();

                var rs = await this.UnitOfWork.InsertAsync(entity);
                return rs != null;
            }
            catch (Exception ex)
            {
                LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
            }
            return false;
        }
        public async Task<bool> RemoveMappingAsync(Guid categoryId, string masterId)
        {
            try
            {
                var rs = await this.UnitOfWork.DeleteAsync<Mapping>(x => x.CategoryId == categoryId && x.CategoryMasterId == masterId, null, true);
                return rs;
            }
            catch (Exception ex)
            {
                LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
            }
            return false;
        }
        public List<Guid> GetParentNodes(Guid currentNodeId)
        {
            var parentNodes = new List<Guid>();
            var category = this.UnitOfWork.GetSingle<Category>(x => x.Id == currentNodeId && x.ActiveFlag == STATUS_ACTIVE);
            if (category != null)
            {
                if (!category.ParentId.HasValue)
                {
                    return parentNodes;
                }
                parentNodes.Add(category.ParentId.Value);
                parentNodes.AddRange(GetParentNodes(category.ParentId.Value));
            }
            return parentNodes;
        }
        private List<string> GetParentNodesName(Guid currentNodeId)
        {
            var parentNodes = new List<string>();
            var category = this.UnitOfWork.GetSingle<Category>(x => x.Id == currentNodeId && x.ActiveFlag == STATUS_ACTIVE);
            if (category != null)
            {
                parentNodes.Add(category.Name);

                if (!category.ParentId.HasValue)
                {
                    return parentNodes;
                }
                parentNodes.AddRange(GetParentNodesName(category.ParentId.Value));
            }
            return parentNodes;
        }
        public byte[] GetTransfer(byte[] template, int timeOut)
        {
            try
            {
                if (template != null && template.Length > 0)
                {
                    var sql = "Exec " + BaseBusiness.SP_Category_GetTransfer;
                    var lstData = this.UnitOfWork.SqlQuery<CategoryBoxedDto>(sql, timeOut).ToList();
                    if (lstData != null && lstData.Count > 0)
                    {
                        var excel = new Excel();
                        excel.TemplateFileData = template;
                        excel.ParameterData.Add("category_id", "category_id");
                        excel.ParameterData.Add("category_path", "category_path");
                        var data = excel.Export(lstData);
                        return data;
                    }
                }
            }
            catch (Exception ex)
            {
                LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
            }
            return null;
        }
        public bool UpdateTransferred(DateTime dateTime, int timeOut)
        {
            try
            {
                var sql = BaseBusiness.SP_Category_UpdateTransferred;
                var parameters = new Dictionary<string, object>();
                parameters.Add("@datetime", dateTime);
                return this.UnitOfWork.ExecuteNonQuery(sql, parameters, timeOut);
            }
            catch (Exception ex)
            {
                LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
            }
            return false;
        }
        public bool UpdateTransferredAndNotNew(DateTime dateTime, int timeOut)
        {
            try
            {
                var sql = BaseBusiness.SP_Category_UpdateTransferredAndNotNew;
                var parameters = new Dictionary<string, object>();
                parameters.Add("@datetime", dateTime);
                return this.UnitOfWork.ExecuteNonQuery(sql, parameters, timeOut);
            }
            catch (Exception ex)
            {
                LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
            }
            return false;
        }
        public List<CategoryCompactDto> GetCategories(List<Guid> parentIds)
        {
            try
            {
                if (parentIds == null || parentIds?.Count == 0)
                {
                    parentIds = UnitOfWork.GetAll<Category>()
                    .Where(x => x.ActiveFlag == STATUS_ACTIVE && !x.ParentId.HasValue)
                    .Select(x => x.Id).ToList();
                }
                var iquery = UnitOfWork.GetAll<Category>()
                    .Where(x => x.ActiveFlag == STATUS_ACTIVE && x.ParentId.HasValue && parentIds.Contains(x.ParentId.Value))
                    .OrderBy(x => x.Name)
                    .Select(x => new CategoryCompactDto
                    {
                        Id = x.Id,
                        Code = x.Code,
                        Name = x.Name
                    }).ToList();
                return iquery;
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return new List<CategoryCompactDto>();
        }
    }
}
