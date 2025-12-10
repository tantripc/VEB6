using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static MiddlewareTool.Dto.CategoryMgmtDto;

namespace MiddlewareTool.Business.Interface
{
    public interface ICategoryBusiness
    {
        List<CategoryDto> GetAll();
        Task<Tuple<int, List<CategoryDto>>> GetPagingAsync(string userName, string keyWord, Guid? parentId, int pageIndex, int pageSize);
        Tuple<int, List<CategoryDto>> GetNode(string userName, string keyword, Guid currentNodeId);
        Tuple<List<CategoryDto>> GetChildNode(Guid currentNodeId);
        Task<CategoryDto> GetByIdAsync(Guid id);
        Task<CategoryDto> GetByCodeAsync(string code);
        Task<CategoryDto> InsertAsync(string user, CategoryDto dto);
        Task<bool> UpdateAsync(string user, CategoryDto dto);
        Task<bool> HasChildAsync(CategoryDto dto);
        Task<bool> DeleteAsync(string user, Guid id);
        bool CheckRef(Guid id);
        List<Guid> GetParentNodes(Guid id);
        Task<bool> AddToMappingAsync(MappingDto dto);
        Task<bool> RemoveMappingAsync(Guid categoryId, string masterId);
        byte[] GetTransfer(byte[] template, int timeOut);
        bool UpdateTransferred(DateTime dateTime, int timeOut);
        List<CategoryCompactDto> GetCategories(List<Guid> parentIds);

    }
}
