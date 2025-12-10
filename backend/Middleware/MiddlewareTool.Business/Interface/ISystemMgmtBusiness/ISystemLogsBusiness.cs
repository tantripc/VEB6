using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static MiddlewareTool.Dto.SystemMgmtDto;

namespace MiddlewareTool.Business.Interface
{
    public interface ISystemLogsBusiness
    {
        Tuple<int, List<SystemLogDto>> GetPaging(string userName, string keyWord, string module, int userFunction, int eventResult, string dateFrom, string dateTo, int pageIndex, int pageSize, string source);
        List<SystemLogDto> Export(string userName, string keyWord, string module, int userFunction, int eventResult, string dateFrom, string dateTo);
        bool Insert(SystemLogDto dto);
        Task<bool> InsertAsync(SystemLogDto dto);
        SystemLogAttachmentDto GetLogAttachment(Guid id);
        bool InsertSystemLogAttachment(SystemLogAttachmentDto systemLogAttachmentDto);
    }
}
