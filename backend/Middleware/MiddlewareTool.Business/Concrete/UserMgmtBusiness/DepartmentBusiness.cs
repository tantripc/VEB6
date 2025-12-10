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
using static MiddlewareTool.Dto.UserMgmtDto;

namespace MiddlewareTool.Business.Concrete
{
    public class DepartmentBusiness : BaseBusiness, IDepartmentBusiness
    {
        private readonly IUserInfoBusiness _userInfoBusiness;
        public DepartmentBusiness(IUnitOfWork unitOfWork, IUserInfoBusiness userInfoBusiness) : base(unitOfWork) { _userInfoBusiness = userInfoBusiness; }
        public async Task<Tuple<int, List<DepartmentDto>>> GetPagingAsync(string userName, string keyWord, int pageIndex, int pageSize)
        {
            int total = 0;
            try
            {
                var iquery = this.UnitOfWork.GetAll<Department>().Where(x => x.ActiveFlag == STATUS_ACTIVE);
                if (!string.IsNullOrEmpty(userName))
                {
                    iquery = iquery.Where(x => !string.IsNullOrEmpty(x.CreateBy) && x.CreateBy.ToUpper() == userName.ToUpper());
                }
                if (!string.IsNullOrEmpty(keyWord))
                {
                    var search = AppValue.ToUnsignString(keyWord);
                    iquery = iquery.Where(x => x.Name.Contains(search) || x.URL.Contains(search) || x.Code.Contains(search));
                }
                total = iquery.Count();
                var data = await iquery.OrderBy(x => x.Level).ThenByDescending(x => x.UpdateDate)
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .Select(x => new DepartmentDto
                    {
                        Id = x.Id,
                        ParentId = x.ParentId,
                        Code = x.Code,
                        Name = x.Name,
                        CreateBy = x.CreateBy,
                        UpdateBy = x.UpdateBy,
                        UpdateDate = x.UpdateDate
                    })
                    .ToListAsync();
                if (data.Count > 0)
                {
                    var lstParentID = data.Select(x => x.ParentId).ToList();
                    var lstParentName = this.UnitOfWork.GetAll<Department>()
                        .Where(x => lstParentID.Contains(x.Id))
                        .Select(x => new { x.Id, x.Name }).ToList();
                    var lstUserUpdateByIds = data.Select(x => x.UpdateBy).ToList();
                    var lstUpdateBys = _userInfoBusiness.GetUserByListUserId(lstUserUpdateByIds);
                    foreach (var item in data)
                    {
                        item.ParentName = lstParentName.Where(x => x.Id == item.ParentId)
                            .Select(x => x.Name)
                            .FirstOrDefault();
                        item.UpdateByFullName = lstUpdateBys.Where(x => x.UserName.ToLower().Equals(item.UpdateBy.ToLower()))
                        .Select(x => x.FullName)
                        .FirstOrDefault();
                    }
                }
                return new Tuple<int, List<DepartmentDto>>(total, data);
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return new Tuple<int, List<DepartmentDto>>(total, new List<DepartmentDto>());
        }
        public IQueryable<Department> GetAll()
        {
            return this.UnitOfWork.GetAll<Department>().Where(x => x.ActiveFlag == STATUS_ACTIVE);
        }
        public IQueryable<Department> GetAllByUser(string user)
        {
            return this.UnitOfWork.GetAll<Department>().Where(x => x.CreateBy == user && x.ActiveFlag == STATUS_ACTIVE);
        }
        public Dictionary<int, List<DictDeptDto>> GetLevelByUser(string user)
        {
            var dict = new Dictionary<int, List<DictDeptDto>>();
            try
            {
                #region Get data
                var iquery = this.UnitOfWork.GetAll<Department>()
                    .Where(x => x.ActiveFlag == STATUS_ACTIVE);
                if (!string.IsNullOrEmpty(user))
                {
                    var thisDept = this.UnitOfWork.GetAll<UserInfo>()
                        .Where(x => x.UserId == user && x.ActiveFlag == STATUS_ACTIVE)
                        .Join(this.UnitOfWork.GetAll<UserDepartment>()
                                .Where(x => x.ActiveFlag == STATUS_ACTIVE),
                                u => u.Id,
                                d => d.UserId,
                                (u, d) => new { d.DeptId }.DeptId)
                        .ToList();
                    iquery = iquery.Where(x => x.CreateBy == user || thisDept.Contains(x.Id));
                }
                var iqueryData = iquery.OrderBy(x => x.Level).ThenBy(x => x.OrderNumber)
                    .Select(x => new DictDeptDto
                    {
                        Id = x.Id,
                        ParentId = x.ParentId,
                        Name = x.Name,
                        DisplayName = x.Name,
                        Level = x.Level
                    })
                    .ToList();
                #endregion
                #region Add level
                if (iqueryData != null && iqueryData?.Count > 0)
                {
                    var maxLevel = iqueryData.Max(x => x.Level);
                    var deptRoot = iqueryData.Where(x => x.ParentId == null).ToList();
                    dict.Add(0, deptRoot);
                    for (int i = 1; i <= maxLevel; i++)
                    {
                        var items = iqueryData.Where(x => x.Level == i)
                                .Select(x => new DictDeptDto
                                {
                                    Id = x.Id,
                                    ParentId = x.ParentId,
                                    Name = x.Name,
                                    DisplayName = string.Concat(new string('-', i), " ", x.Name),
                                    Level = x.Level
                                })
                                .ToList();
                        foreach (var sub in items)
                        {
                            sub.LstDept = iqueryData.Where(x => x.ParentId == sub.Id)
                                .Select(x => new DictDeptDto
                                {
                                    Id = x.Id,
                                    ParentId = x.ParentId,
                                    Name = x.Name,
                                    DisplayName = string.Concat(new string('-', (i + 1)), x.Name),
                                    Level = x.Level
                                })
                                .ToList();
                        }
                        if (items.Count > 0) { dict.Add(i, items); }
                    }
                }
                #endregion
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return dict;
        }
        public Dictionary<int, List<DictDeptDto>> GetAllByUserDeparment(Guid userId)
        {
            var dict = new Dictionary<int, List<DictDeptDto>>();
            try
            {
                #region Get data
                var iquery = this.UnitOfWork.GetAll<Department>()
                    .Where(x => x.ActiveFlag == STATUS_ACTIVE)
                    .Join(this.UnitOfWork.GetAll<UserDepartment>()
                            .Where(x => x.UserId == userId && x.ActiveFlag == STATUS_ACTIVE),
                            dept => dept.Id,
                            ud => ud.DeptId,
                            (dept, ud) => new { dept }.dept);
                var iqueryData = iquery.OrderBy(x => x.Level).ThenBy(x => x.OrderNumber)
                       .Select(x => new DictDeptDto
                       {
                           Id = x.Id,
                           ParentId = x.ParentId,
                           Name = x.Name,
                           DisplayName = x.Name,
                           Level = x.Level
                       })
                       .ToList();
                #endregion
                #region Add level
                if (iqueryData != null && iqueryData?.Count > 0)
                {
                    var maxLevel = iqueryData.Max(x => x.Level);
                    var deptRoot = iqueryData.Where(x => x.ParentId == null).ToList();
                    dict.Add(0, deptRoot);
                    for (int i = 1; i <= maxLevel; i++)
                    {
                        var items = iqueryData.Where(x => x.Level == i)
                                .Select(x => new DictDeptDto
                                {
                                    Id = x.Id,
                                    ParentId = x.ParentId,
                                    Name = x.Name,
                                    DisplayName = string.Concat(new string('-', i), " ", x.Name),
                                    Level = x.Level
                                })
                                .ToList();
                        foreach (var sub in items)
                        {
                            sub.LstDept = iqueryData.Where(x => x.ParentId == sub.Id)
                                .Select(x => new DictDeptDto
                                {
                                    Id = x.Id,
                                    ParentId = x.ParentId,
                                    Name = x.Name,
                                    DisplayName = string.Concat(new string('-', (i + 1)), x.Name),
                                    Level = x.Level
                                })
                                .ToList();
                        }
                        if (items.Count > 0) { dict.Add(i, items); }
                    }
                }
                #endregion
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return dict;
        }
        public async Task<List<DepartmentTreeDto>> GetAllRecursiveOfLevel(string userName)
        {
            try
            {
                var sqlData = $@"WITH DepartmentTree (Id,Name, [Level], location,CreateBy)
                                AS (SELECT  Id,
			                                CAST(Name AS NVARCHAR(MAX)),          
                                           0 AS [Level],
                                           CAST(Id AS NVARCHAR(MAX)) AS Location,
                                           CreateBy
                                    FROM Departments
                                    WHERE ParentId IS NULL
                                    UNION ALL
                                    SELECT child.Id,
			                               CAST(CONCAT(SPACE(parent.[Level] * 5), '--', child.Name) AS NVARCHAR(MAX)), 
                                           parent.Level + 1,
                                           CAST(CONCAT(parent.location, ',', child.Id) AS NVARCHAR(MAX)) AS Location,
                                           child.CreateBy
                                    FROM Departments child
                                        INNER JOIN DepartmentTree parent
                                            ON child.ParentId = parent.Id)
                                SELECT *
                                FROM DepartmentTree
                                ORDER BY location;";
                var iquery = this.UnitOfWork.SqlQuery<DepartmentTreeDto>(sqlData);
                if (!string.IsNullOrEmpty(userName))
                {
                    var lstDept = await this.UnitOfWork.GetAll<UserInfo>()
                        .Where(x => x.UserId == userName && x.ActiveFlag == STATUS_ACTIVE)
                        .Join(this.UnitOfWork.GetAll<UserDepartment>()
                                .Where(x => x.ActiveFlag == STATUS_ACTIVE),
                                u => u.Id,
                                d => d.UserId,
                                (u, d) => new { d.DeptId }.DeptId)
                        .ToListAsync();
                    iquery = iquery.Where(x => x.CreateBy == userName || lstDept.Contains(x.Id));
                }
                if (iquery != null)
                {
                    return iquery.ToList();
                }
            }
            catch (Exception ex) { LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return new List<DepartmentTreeDto>();
        }
        public List<DepartmentTreeDto> GetAllRecursiveOfLevelByDeptId(Guid deptId)
        {
            try
            {
                var sqlData = @"WITH DepartmentTree (Id,Name, [Level], location,CreateBy)
                                AS (SELECT  Id,
			                                CAST(Name AS NVARCHAR(MAX)),          
                                           0 AS [Level],
                                           CAST(Id AS NVARCHAR(MAX)) AS Location,
                                           CreateBy
                                    FROM Departments
                                    WHERE Id='{0}'
                                    UNION ALL
                                    SELECT child.Id,
			                               CAST(CONCAT(SPACE(parent.[Level] * 5), '--', child.Name) AS NVARCHAR(MAX)),                   parent.Level + 1,
                                           CAST(CONCAT(parent.location, ',', child.Id) AS NVARCHAR(MAX)) AS Location,
                                           child.CreateBy
                                    FROM Departments child
                                        INNER JOIN DepartmentTree parent
                                            ON child.ParentId = parent.Id)
                                SELECT *
                                FROM DepartmentTree
                                ORDER BY location;";
                var iquery = this.UnitOfWork.SqlQuery<DepartmentTreeDto>(string.Format(sqlData, deptId));
                if (iquery != null)
                {
                    return iquery.ToList();
                }
            }
            catch (Exception ex) { LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return new List<DepartmentTreeDto>();
        }
        #region Recursive dept
        public List<RecursiveDeptDto> GetRecursiveDeptID(List<Guid> lstId)
        {
            var records = new List<RecursiveDeptDto>();
            try
            {
                #region get data
                var iquery = this.UnitOfWork.GetAll<Department>()
                    .Where(x => x.ActiveFlag == STATUS_ACTIVE)
                    .Select(x => new RecursiveDeptDto
                    {
                        id = x.Id,
                        ParentId = x.ParentId,
                        text = x.Name,
                        Level = x.Level
                    });
                var dataRoot = iquery.ToList();
                #endregion
                #region Recursive
                records = iquery.Where(x => lstId.Contains(x.id))
                    .ToList()
                    .OrderBy(x => x.Level)
                    .ThenBy(x => x.OrderNumber)
                    .Select(x => new RecursiveDeptDto
                    {
                        id = x.id,
                        ParentId = x.ParentId,
                        text = x.text,
                        @checked = x.@checked,
                        children = GetChildren(dataRoot.ToList(), x.id)
                    })
                    .ToList();
                #endregion
            }
            catch (Exception ex) { LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return records;
        }
        public List<Guid> GetChildrenByDeptID(List<Guid> lstId)
        {
            var results = new List<Guid>();
            try
            {
                var iquery = this.UnitOfWork.GetAll<Department>()
                    .Where(x => lstId.Contains(x.Id) || (x.ParentId.HasValue && lstId.Any(r => r == x.ParentId)))
                    .OrderBy(x => x.Level)
                    .ToList();
                if (iquery.Count > 0)
                {
                    var parentID = iquery.Where(x => lstId.Contains(x.Id)).Select(x => x.Id).ToList();
                    results.AddRange(parentID);
                    var childList = iquery.Where(x => x.ParentId.HasValue && lstId.Any(r => r == x.ParentId)).ToList();
                    foreach (var t in childList)
                    {
                        results.Add(t.Id);
                        results = results.Union(GetChildrenByDeptID(new List<Guid> { t.Id })).ToList();
                    }
                    return results;
                }
            }
            catch (Exception ex) { LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return results;
        }
        public List<RecursiveDeptDto> GetRecursiveDeptByUser(string user, Guid? selId)
        {
            var records = new List<RecursiveDeptDto>();
            try
            {
                #region get data
                var iquery = this.UnitOfWork.GetAll<Department>().Where(x => x.ActiveFlag == STATUS_ACTIVE);
                if (!string.IsNullOrEmpty(user))
                {
                    iquery = iquery.Where(x => x.CreateBy == user);
                }
                var dataIquery = iquery.Select(x => new RecursiveDeptDto
                {
                    id = x.Id,
                    ParentId = x.ParentId,
                    text = x.Name,
                    Level = x.Level,
                    @checked = (x.Id == selId),
                }).ToList();
                #endregion
                #region Recursive
                records = dataIquery.Where(x => x.ParentId == null)
                    .OrderBy(x => x.Level)
                    .ThenBy(x => x.OrderNumber)
                    .Select(x => new RecursiveDeptDto
                    {
                        id = x.id,
                        ParentId = x.ParentId,
                        text = x.text,
                        @checked = x.@checked,
                        children = GetChildren(dataIquery, x.id)
                    })
                    .ToList();
                #endregion
            }
            catch (Exception ex) { LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return records;
        }
        private List<RecursiveDeptDto> GetChildren(List<RecursiveDeptDto> departments, Guid parentId)
        {
            return departments.Where(x => x.ParentId == parentId)
                .Select(x => new RecursiveDeptDto
                {
                    id = x.id,
                    ParentId = x.ParentId,
                    text = x.text,
                    @checked = x.@checked,
                    children = GetChildren(departments, x.id)
                }).ToList();
        }
        #endregion
        public async Task<DepartmentDto> GetByIdAsync(Guid id)
        {
            try
            {
                var iquery = await this.UnitOfWork.GetSingleAsync<Department>(x => x.Id == id && x.ActiveFlag == STATUS_ACTIVE);
                if (iquery != null)
                {
                    return Mapper.Map<DepartmentDto>(iquery);
                }
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return null;
        }
        public async Task<DepartmentDto> GetByCodeAsync(string code)
        {
            try
            {
                if (!string.IsNullOrEmpty(code))
                {
                    var iquery = await this.UnitOfWork.GetSingleAsync<Department>(x => x.Code.ToUpper() == code.ToUpper() && x.ActiveFlag == STATUS_ACTIVE);
                    if (iquery != null)
                    {
                        return Mapper.Map<DepartmentDto>(iquery);
                    }
                }
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return null;
        }
        public async Task<bool> CheckCodeAsync(string code)
        {
            bool result = true;
            try
            {
                if (!string.IsNullOrEmpty(code))
                {
                    var iquery = await this.UnitOfWork.GetSingleAsync<Department>(x => x.Code.ToUpper() == code.ToUpper() && x.ActiveFlag == STATUS_ACTIVE);
                    if (iquery == null) { result = false; }
                }
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return result;
        }
        public async Task<bool> InsertAsync(DepartmentDto dto)
        {
            bool result = false;
            try
            {
                var entity = Mapper.Map<Department>(dto);
                entity.Id = Guid.NewGuid();
                entity.CreateDate = dto.CreateDate;
                entity.CreateBy = dto.CreateBy;
                entity.UpdateBy = dto.CreateBy;
                entity.UpdateDate = DateTime.Now;
                if (entity.ParentId == null) { entity.Level = 1; }
                else
                {
                    entity.Level = (this.UnitOfWork.GetSingle<Department>(x => x.Id == entity.ParentId && x.ActiveFlag == STATUS_ACTIVE).Level + 1);
                }
                var add = await this.UnitOfWork.InsertAsync(entity);
                if (add != null) { result = true; }
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return result;
        }
        public async Task<bool> UpdateAsync(DepartmentDto dto)
        {
            bool result = false;
            try
            {
                var entity = this.UnitOfWork.GetSingle<Department>(x => x.Id == dto.Id && x.ActiveFlag == STATUS_ACTIVE);
                if (entity != null)
                {
                    entity.Name = dto.Name;
                    //entity.Code = dto.Code;
                    entity.ParentId = dto.ParentId;
                    entity.UpdateDate = DateTime.Now;
                    entity.UpdateBy = dto.UpdateBy;
                    if (entity.ParentId == null) { entity.Level = 1; }
                    else
                    {
                        entity.Level = (this.UnitOfWork.GetSingle<Department>(x => x.Id == entity.ParentId && x.ActiveFlag == STATUS_ACTIVE).Level + 1);
                    }
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
                var entity = this.UnitOfWork.GetSingle<Department>(x => x.Id == id && x.ActiveFlag == STATUS_ACTIVE);
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
        public async Task<string> GetCodeByIdAsync(Guid id)
        {
            return await this.UnitOfWork.GetAll<Department>()
                .Where(x => x.Id == id && x.ActiveFlag == STATUS_ACTIVE)
                .OrderByDescending(x => x.UpdateDate)
                .Select(x => x.Code)
                .FirstOrDefaultAsync();
        }
    }
}
