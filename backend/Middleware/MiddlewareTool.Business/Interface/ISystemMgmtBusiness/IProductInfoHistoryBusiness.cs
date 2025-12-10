using MiddlewareTool.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MiddlewareTool.Business.Interface
{
    public interface IProductInfoHistoryBusiness
    {
        Task<Tuple<int, List<ProductInfoHistoryDto>>> GetPagingAsync(int pageIndex, string keyWord, string searchStore,
            string dateFrom, string dateTo, string createdBy, string updatedBy, int action, int pageSize);
        List<ProductInfoHistoryDto> Export(string keyWord, int historyType, string searchStore, string dateFrom, string dateTo, Guid createdBy, Guid updatedBy, int action);
        bool Insert(ProductInfoHistoryDto dto);
        bool InsertList(List<ProductInfoHistoryDto> list);
        Task<bool> InsertAsync(ProductInfoHistoryDto dto);
        Task<bool> InsertListAsync(List<ProductInfoHistoryDto> list);
    }
}
