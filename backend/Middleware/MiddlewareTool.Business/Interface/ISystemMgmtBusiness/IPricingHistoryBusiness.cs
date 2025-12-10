using MiddlewareTool.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MiddlewareTool.Business.Interface
{
    public interface IPricingHistoryBusiness
    {
        Task<Tuple<int, List<PricingHistoryDto>>> GetPagingAsync(int pageIndex, string keyWord, string searchStore,
            string dateFrom, string dateTo, string createdBy, string updatedBy, int action, int pageSize);
        List<PricingHistoryDto> Export(string keyWord, int historyType, string searchStore, string dateFrom, string dateTo, Guid createdBy, Guid updatedBy, int action);
        bool Insert(PricingHistoryDto dto);
        Task<bool> InsertAsync(PricingHistoryDto dto);
        Task<bool> InsertListAsync(List<PricingHistoryDto> list);
    }
}
