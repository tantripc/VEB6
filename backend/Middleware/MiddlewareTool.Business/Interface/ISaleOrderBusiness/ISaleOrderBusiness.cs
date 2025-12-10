using MiddlewareTool.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static MiddlewareTool.Dto.SaleDto;

namespace MiddlewareTool.Business.Interface
{
    public interface ISaleOrderBusiness
    {
        Task<SaleOrderDto> GetAsync(Guid id);
        Task<bool> checkRefund(Guid id);
        bool CheckView(Guid id, string userId);
        Task<Tuple<int, List<SaleOrderDto>>> GetPagingAsync(SaleOrderFilterDto filter, bool isAdmin, string userName);
        Task<Tuple<int, List<SaleOrderExportDto>>> GetExportAsync(SaleOrderFilterDto filter, bool isAdmin, string userName);
        Task<List<SaleOrderCompactDto>> GetOrderNumbersAsync(SaleOrderFilterDto filter, bool isAdmin, string userName);
        Task<Tuple<bool, string>> InsertListAsync(List<SaleOrderDto> dtos);
        Task<Tuple<bool, SaleOrderDto>> UpdateAsync(SaleOrderDto dto);
        Task<bool> DeleteAsync(SaleOrderDto dto);
        bool InsertUploadFile(UploadFileDto dto);
        Task<UploadFileDto> GetUploadFileAsync(Guid uploadId);
        Task<string> WriteSaleCsvAsync(Guid headerId);
        Task<HeaderDto> WriteSaleCsvFileAsync(Guid headerId);
        List<PromotionDto> GetPromotionByItemId(Guid id);
    }
}
