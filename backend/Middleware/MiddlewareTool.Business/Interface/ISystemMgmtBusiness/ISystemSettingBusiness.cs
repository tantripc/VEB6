using MiddlewareTool.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static MiddlewareTool.Dto.SystemMgmtDto;

namespace MiddlewareTool.Business.Interface
{
    public interface ISystemSettingBusiness
    {
        Task<Tuple<int, List<SystemSettingDto>>> GetPagingAsync(string userName, string keyWord, int layout, int pageIndex, int pageSize);
        Task<SystemSettingDto> GetByIdAsync(Guid id);
        Task<SystemSettingDto> GetByTypeAsync(AppType.Setting type);
        List<SystemSettingDto> GetByType(AppType.Setting type);
        Task<SystemSettingDto> GetByCodeAsync(string code);
        SystemSettingDto GetByCode(string code);
        bool CheckExistByCode(string code);
        Task<bool> InsertAsync(SystemSettingDto dto);
        Task<bool> UpdateAsync(SystemSettingDto dto);
        Task<bool> DeleteAsync(Guid id, string userName);
        Task<List<SystemSettingDto>> GetListByLayoutAsync(AppType.Layout layout);
        List<SystemSettingDto> GetListByLayout(AppType.Layout layout);
        bool Update(SystemSettingDto dto);
    }
}
