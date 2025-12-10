using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static MiddlewareTool.Dto.SystemMgmtDto;

namespace MiddlewareTool.Business.Interface
{
    public interface IMailboxBusiness
    {
        bool Insert(MailboxDto dto);
        Task<bool> InsertAsync(MailboxDto dto);
        List<MailboxDto> GetNotSent();
        Task<List<MailboxDto>> GetNotSentAsync();
        bool UpdateNumSend(string id, string sent, string numsend);
        Task<Tuple<int, IList<MailboxDto>>> GetPagingAsync(int pageIndex, int pageSize, string keyWord, string fromDate, string toDate, string isSeen, string username);
        Task<Tuple<int, List<MailboxDto>>> GetNewFeedAsync(string username, int pageSize);
        Task<MailboxDto> UpdateStatusAsync(Guid id, string username);
        Task<bool> MarkAllAsReadAsync(string username);
    }
}
