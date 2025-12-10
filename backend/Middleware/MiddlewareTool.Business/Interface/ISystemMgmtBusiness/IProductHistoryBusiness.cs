using MiddlewareTool.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MiddlewareTool.Business.Interface
{
    public interface IProductHistoryBusiness
    {
        Task<Tuple<int, List<ProductHistoryDto>>> GetPagingAsync(int pageIndex, string keyWord, string searchStore,
            string dateFrom, string dateTo, string createdBy, string updatedBy, int action, int pageSize);
        List<ProductHistoryDto> Export(string keyWord, int historyType, string searchStore, string dateFrom, string dateTo, Guid createdBy, Guid updatedBy, int action);
        bool Insert(ProductHistoryDto dto);
        bool InsertList(List<ProductHistoryDto> list);
        Task<bool> InsertAsync(ProductHistoryDto dto);
        Task<bool> InsertListAsync(List<ProductHistoryDto> list);
    }
}
