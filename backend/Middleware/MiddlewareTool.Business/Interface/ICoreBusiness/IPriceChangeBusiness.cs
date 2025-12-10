using MiddlewareTool.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using static MiddlewareTool.Dto.CoreDto;

namespace MiddlewareTool.Business.Interface
{
    public interface IPriceChangeBusiness
    {
        Task<Tuple<int, List<PriceChangeDto>>> GetPagingAsync(string userName, string keyWord, int pageIndex, int pageSize);
        List<PriceChangeCompactDto> GetAll(string storeCode = "");
        PriceChangeDto GetById(Guid id);
        Task<PriceChangeDto> GetByIdAsync(Guid id);
        PriceChangeDto GetByCode(string code);
        //PriceChangeDto GetByKeyCode(string code, string storeCode, string PRC_NO);
        //bool AnyByKeyCode(string code, string storeCode, string PRC_NO);
        bool Insert(PriceChangeDto dto);
        Task<bool> InsertAsync(PriceChangeDto dto);
        bool Update(PriceChangeDto dto);
        Task<bool> UpdateAsync(PriceChangeDto dto);
        bool IsExist(Guid id);
        Task<bool> IsExistAsync(Guid id);
        bool IsExistByCode(string code);
        bool Import(DataTable dt, string fileName, int timeOut, out string error);
    }
}
