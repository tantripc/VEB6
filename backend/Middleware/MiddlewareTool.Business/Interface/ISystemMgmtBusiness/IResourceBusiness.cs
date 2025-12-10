using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using static MiddlewareTool.Dto.SystemMgmtDto;

namespace MiddlewareTool.Business.Interface
{
    public interface IResourceBusiness
    {
        Task<Tuple<int, List<ResourceDto>>> GetPagingAsync(string keyWord, int pageIndex, int pageSize);
        Dictionary<string, ResourceDto> GetAll();
        bool Import(DataTable resource);
        Task<ResourceDto> GetByIdAsync(string id);
        bool Insert(ResourceDto dto);
        Task<bool> InsertAsync(ResourceDto dto);
        Task<bool> InsertToListAsync(List<ResourceDto> LstDTO);
        bool Update(ResourceDto dto);
        Task<bool> UpdateAsync(ResourceDto dto);
        Task<bool> DeleteAsync(string id);
        Task<bool> DeleteToListAsync(List<string> lstId);
    }
}
