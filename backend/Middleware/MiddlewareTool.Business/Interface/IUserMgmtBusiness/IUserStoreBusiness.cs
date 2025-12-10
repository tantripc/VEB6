using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static MiddlewareTool.Dto.StoreMgmtDto;

namespace MiddlewareTool.Business.Interface
{
    public interface IUserStoreBusiness
    {
        Task<List<StoreDto>> GetListByUserId(Guid userId);
        List<StoreCompactDto> GetListByUserName(string userName);
        Task<bool> InsertOrDeleteAsync(Guid userId, string userName, List<string> lstStore);
    }
}
