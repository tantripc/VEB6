using MiddlewareTool.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MiddlewareTool.Business.Interface
{
    public interface IInventoryHistoryBusiness
    {
        Task<Tuple<int, List<InventoryHistoryDto>>> GetPagingAsync(int pageIndex, string keyWord, string searchStore,
            string dateFrom, string dateTo, string createdBy, string updatedBy, int action, int pageSize);
        List<InventoryHistoryDto> Export(string keyWord, int historyType, string searchStore, string dateFrom, string dateTo, Guid createdBy, Guid updatedBy, int action);
        bool Insert(InventoryHistoryDto dto);
        Task<bool> InsertAsync(InventoryHistoryDto dto);
        bool InsertList(List<InventoryHistoryDto> list);
        Task<bool> InsertListAsync(List<InventoryHistoryDto> list);
    }
}
