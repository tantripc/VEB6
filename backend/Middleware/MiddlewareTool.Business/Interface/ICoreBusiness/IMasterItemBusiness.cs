using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using static MiddlewareTool.Dto.CoreDto;

namespace MiddlewareTool.Business.Interface
{
    public interface IMasterItemBusiness
    {
        Task<Tuple<int, List<MasterItemDto>>> GetPagingAsync(string userName, string keyWord, int pageIndex, int pageSize);
        MasterItemDto GetById(Guid id);
        Task<MasterItemDto> GetByIdAsync(Guid id);
        MasterItemDto GetByCode(string code);
        bool Insert(MasterItemDto dto);
        Task<bool> InsertAsync(MasterItemDto dto);
        bool Update(MasterItemDto dto);
        Task<bool> UpdateAsync(MasterItemDto dto);
        bool IsExist(Guid id);
        Task<bool> IsExistAsync(Guid id);
        bool IsExistByCode(string code);
        bool Import(DataTable dt, string storeCode, string fileName, int timeOut, out string error);
    }
}
