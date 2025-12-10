using System.Collections.Generic;
using System;
using static MiddlewareTool.Dto.SaleDto;
using System.Threading.Tasks;

namespace MiddlewareTool.Business.Interface
{
    public interface IRecordSaleBusiness
    {
        bool Insert(RecordSaleFileDto dto);
        bool Update(RecordSaleFileDto dto);
        Tuple<string, InvoiceDto> ManualInvoice(InvoiceDto dto);
        bool IsExist(string storeCode, string name);
        Tuple<int, List<RecordSaleDto>> GetPaging(string keyWord, string dateFrom, string dateTo, int pageIndex, int pageSize, bool B2B = false, List<string> storeCodes = null, string customerType = null, List<string> paymentType = null, List<string> deliveryCode = null);
        List<RecordSaleDto> Export(string keyWord, string dateFrom, string dateTo, bool B2B = false, string storeCodes = null, string customerType = null, string paymentType = null, string deliveryCode = null);
        byte[] GetTransfer(byte[] template, DateTime dateTime);
        bool UpdateTransferred(DateTime dateTime);
        RecordSaleBIDto GetById(Guid id);
        ItemsDto GetPagingItems(int pageIndex, int pageSize, string headerId, string storeCode);
        Task<string> WriteBoxedSaleCsvAsync(Guid recordSaleId);
        Task<string> WriteProfitSaleCsvAsync(Guid recordSaleId);
    }
}
