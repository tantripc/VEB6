using MiddlewareTool.Dto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using static MiddlewareTool.Dto.RefundDto;
using static MiddlewareTool.Dto.SaleDto;

namespace MiddlewareTool.Business.Interface
{
    public interface IRefundBusiness
    {
        bool Import(DataTable dtHeader, DataTable dtItem, DataTable dtPayment, DataTable dtInvoice, DataTable dtPromotion);
        RefundHeaderDto GetTransfer(string storeCode, Guid refundHeaderId, bool isSAP = false);
        List<RefundByStoreDto> GetStores();
        List<RefundByStoreDto> GetSAPStores();
        List<RefundByStoreDto> GetS4Stores();
        bool UpdateTransferred(string storeCode, Guid refundHeaderId);
        bool UpdateSAPTransferred(string storeCode, Guid refundHeaderId);
        bool UpdateS4Transferred(string storeCode, Guid refundHeaderId, bool success = true);
        List<RefundByStoreDto> GetHotFixPaymentByStore();
        Task<bool> CheckRefundAsync(Guid saleId, string storeCode);
        Task<List<SaleOrderCompactDto>> GetSaleOrderNumbersAsync(SaleOrderFilterDto filter, bool isAdmin, string userName);
        List<SaleOrderCompactDto> GetSaleOrderNumbers(SaleOrderFilterDto filter, bool isAdmin, string userName);
    }
}
