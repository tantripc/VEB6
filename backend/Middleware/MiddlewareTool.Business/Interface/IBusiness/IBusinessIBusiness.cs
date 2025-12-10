using MiddlewareTool.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MiddlewareTool.Business.Interface
{
    public interface IBusinessBusiness
    {
        Task<List<BusinessDto>> GetAllAsync();
        Task<BusinessDto> GetAsync(Guid id);
        Task<Tuple<int, List<BusinessDto>>> GetPagingAsync(SaleOrderFilterDto filter);
        Task<BusinessDto> InsertAsync(BusinessDto dto);
        Task<bool> UpdateAsync(BusinessDto dto);
        Task<bool> DeleteAsync(Guid id, string userId);
        Task<bool> CheckExist(Guid id);
        bool CheckDelete(Guid id);
    }
}
