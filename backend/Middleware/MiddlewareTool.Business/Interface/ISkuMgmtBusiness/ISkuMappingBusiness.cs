using MiddlewareTool.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static MiddlewareTool.Dto.SkuMappingMgmtDto;

namespace MiddlewareTool.Business.Interface
{
    public interface ISkuMappingBusiness
    {
        List<SkuMappingDto> GetAllNoTracking();
        Task<Tuple<int, List<SkuMappingDto>>> GetPagingAsync(string keyWord, string mallCode, string expressLocationGroupName, string parcelLocationGroupName, int pageIndex, int pageSize);
        Task<SkuMappingDto> GetByIdAsync(Guid id);
        Task<bool> InsertAsync(string user, SkuMappingDto dto);
        bool Insert(string user, SkuMappingDto dto);
        Task<bool> UpdateAsync(string user, SkuMappingDto dto);
        bool Update(string user, SkuMappingDto dto);
        Task<bool> DeleteAsync(string user, Guid id);
        Task<bool> CheckExistAsync(Guid? id, string mallCode, string expressLocationGroupName, string parcelLocationGroupName);
        Task<List<SkuMappingExportDto>> ExportAsync(string keyWord, string mallCode, string expressLocationGroupName, string parcelLocationGroupName);
        bool InsertToList(string user, List<SkuMappingDto> dtos);
        bool UpdateToList(string user, List<SkuMappingDto> dtos);
        SkuUploadMonitor InsertUploadMonitor(string user, SkuUploadMonitorDto dto);
        bool InsertUploadError(string user, SkuUploadErrorDto dto);
        bool UpdateCurrentUploadMonitor(string user, SkuUploadMonitor uploadMonitor, int current);
        SkuUploadMonitor GetUploadMonitor(Guid id);
        int GetCurrentUploadError(Guid uploadId);
        List<SkuUploadError> GetUploadErrors(Guid uploadId);
        //byte[] GetTransfer(byte[] template);
        //bool UpdateTransferred(DateTime dateTime);
    }
}
