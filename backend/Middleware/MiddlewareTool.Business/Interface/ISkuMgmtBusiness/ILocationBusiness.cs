using MiddlewareTool.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static MiddlewareTool.Dto.SkuMappingMgmtDto;

namespace MiddlewareTool.Business.Interface
{
    public interface ILocationBusiness
    {
        Task<List<LocationDto>> GetAllAsync();
        List<LocationDto> GetAllNoTracking();
        Task<Tuple<int, List<LocationDto>>> GetPagingAsync(string keyWord, string locationGroupName, int pageIndex, int pageSize);
        Task<LocationDto> GetByIdAsync(Guid id);
        Task<bool> InsertAsync(string user, LocationDto dto);
        bool Insert(string user, LocationDto dto);
        Task<bool> UpdateAsync(string user, LocationDto dto);
        bool Update(string user, LocationDto dto);
        Task<bool> DeleteAsync(string user, Guid id);
        Task<List<LocationDto>> ExportAsync(string keyWord, string locationGroupName);
        LocationUploadMonitor InsertUploadMonitor(string user, LocationUploadMonitorDto dto);
        bool InsertUploadError(string user, LocationUploadErrorDto dto);
        bool UpdateCurrentUploadMonitor(string user, LocationUploadMonitor uploadMonitor, int current);
        LocationUploadMonitor GetUploadMonitor(Guid id);
        int GetCurrentUploadError(Guid uploadId);
        List<LocationUploadError> GetUploadErrors(Guid uploadId);
    }
}
