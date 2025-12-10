using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using static MiddlewareTool.Dto.CoreDto;

namespace MiddlewareTool.Business.Interface
{
    public interface IGroupPriceChangeBusiness
    {
        Task<Tuple<int, List<GroupPriceChangeDto>>> GetPagingAsync(string userName, string keyWord, int pageIndex, int pageSize);
        GroupPriceChangeDto GetById(Guid id);
        Task<GroupPriceChangeDto> GetByIdAsync(Guid id);
        GroupPriceChangeDto GetByCode(string code); 
        bool Insert(GroupPriceChangeDto dto);
        Task<bool> InsertAsync(GroupPriceChangeDto dto);
        bool Update(GroupPriceChangeDto dto);
        Task<bool> UpdateAsync(GroupPriceChangeDto dto);
        bool IsExist(Guid id);
        Task<bool> IsExistAsync(Guid id);
        bool IsExistByCode(string code);
        bool Import(DataTable dt, int timeOut);
    }
}
