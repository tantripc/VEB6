using MiddlewareTool.Dto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using static MiddlewareTool.Dto.SaleDto;

namespace MiddlewareTool.Business.Interface
{
    public interface ISaleBusiness
    {
        bool Import(DataTable dtHeader, DataTable dtItem, DataTable dtPayment, DataTable dtInvoice, DataTable dtDelivery, DataTable dtPromotion, DataTable dtItemForDelivery, DataTable dtItemForRefund, DataTable dtCustomerData);
        HeaderByStoreDto GetTransfer(string storeCode, Guid headerId, bool isSAP = false);
        List<SaleByStoreDto> GetStores();
        List<SaleByStoreDto> GetSAPStores();
        List<SaleByStoreDto> GetS4Stores();
        bool UpdateTransferred(string storeCode, Guid headerId, bool success = true);
        bool UpdateSAPTransferred(string storeCode, Guid headerId);
        bool UpdateS4Transferred(string storeCode, Guid headerId, bool success = true);
        List<SaleByStoreDto> GetHotFixPaymentByStore();
        Task<SaleCODDto> GetAsync(Guid id, string storeCode);
    }
}
