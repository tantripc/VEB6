using MiddlewareTool.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static MiddlewareTool.Dto.RefundDto;
using static MiddlewareTool.Dto.SaleDto;

namespace MiddlewareTool.Business.Interface
{
    public interface ISaleOrderRefundBusiness
    {
        Task<SaleOrderRefundDto> GetAsync(Guid id);
        Task<SaleOrderRefundDto> GetCODAsync(Guid id);
        Task<List<SaleOrderRefundDto>> GetAllBySaleOrderIdAsync(Guid id);
        Task<List<SaleOrderRefundDto>> GetAllBySaleOrderIdCODAsync(Guid id);
        bool CheckExist(Guid id);
        bool CheckView(Guid id, string userId);
        bool CheckEdit(Guid id, string userId);
        Task<Tuple<int, List<SaleOrderRefundDto>>> GetPagingAsync(SaleOrderFilterDto filter, bool isAdmin, string userName);
        Task<Tuple<int, List<SaleOrderRefundCODDto>>> GetPagingCODAsync(SaleOrderFilterDto filter, bool isAdmin, string userName);
        Task<Tuple<int, List<RefundOrderExportDto>>> GetExportAsync(SaleOrderFilterDto filter, bool isAdmin, string userName);
        Task<Tuple<int, List<RefundOrderExportDto>>> GetExportCODAsync(SaleOrderFilterDto filter, bool isAdmin, string userName);
        Task<bool> InsertAsync(SaleOrderRefundDto dto);
        Task<bool> InsertCODAsync(SaleOrderRefundDto dto);
        List<RefundReasonDto> GetReasons();
        Task<Tuple<bool, string>> InsertListAsync(List<SaleOrderRefundDto> dtos);
        Task<Tuple<bool, string>> InsertListCODAsync(List<SaleOrderRefundDto> dtos);
        Task<Tuple<bool, SaleOrderRefundDto>> checkRefundedItem(SaleOrderRefundDto dto);
        Task<bool> UpdateAsync(SaleOrderRefundDto dto);
        Task<bool> UpdateCODAsync(SaleOrderRefundDto dto);
        Task<bool> DeleteAsync(SaleOrderRefundDto dto);
        Task<bool> DeleteCODAsync(SaleOrderRefundDto dto);
        bool InsertUploadFile(UploadFileDto dto);
        Task<UploadFileDto> GetUploadFileAsync(Guid uploadId);
        Task<string> WriteRefundCsvAsync(Guid headerId);
        Task<string> WriteRefundCODCsvAsync(Guid headerId);
        Task<RefundHeaderDto> WriteRefundCsvFileAsync(Guid headerId);
        Task<RefundHeaderDto> WriteRefundCODCsvFileAsync(Guid headerId);
        Task<Tuple<bool, SaleOrderRefundDto>> ReValidate(SaleOrderRefundDto dto);
        Task<Tuple<bool, SaleOrderRefundDto>> ReValidateCOD(SaleOrderRefundDto dto);
    }
}
