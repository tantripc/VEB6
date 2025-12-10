using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static MiddlewareTool.Dto.RefundDto;

namespace MiddlewareTool.Business.Interface
{
    public interface IRecordRefundBusiness
    {
        bool Insert(RecordRefundFileDto dto);
        bool Update(RecordRefundFileDto dto);
        bool IsExist(string storeCode, string name);
        Tuple<int, List<RecordRefundDto>> GetPaging(string keyWord, string dateFrom, string dateTo, int pageIndex, int pageSize, List<string> storeCodes, string customerType, List<string> paymentType, List<string> deliveryCode);
        List<RecordRefundDto> Export(string keyWord, string dateFrom, string dateTo, string storeCodes = null, string customerType = null, string paymentType = null, string deliveryCode = null);
        byte[] GetTransfer(byte[] template, DateTime dateTime);
        bool UpdateTransferred(DateTime dateTime);
        List<RecordRefundBIDto> GetPagingRefundHistories(int pageIndex, int pageSize, string actualOrderNumber);
        RecordRefundBIDto GetById(Guid id);
        RefundItemsDto GetPagingItems(int pageIndex, int pageSize, string headerId, string storeCode);
        Task<string> WriteProfitSaleCsvAsync(Guid recordRefundId);
        Task<string> WriteBoxedSaleCsvAsync(Guid recordRefundId);
    }
}
