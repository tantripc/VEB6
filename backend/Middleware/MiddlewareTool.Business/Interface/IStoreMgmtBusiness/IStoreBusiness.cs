using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static MiddlewareTool.Common.AppType;
using static MiddlewareTool.Dto.StoreMgmtDto;

namespace MiddlewareTool.Business.Interface
{
    public interface IStoreBusiness
    {
        Task<Tuple<int, List<StoreDto>>> GetPagingAsync(string mallCode, string cityCode, string districtCode, string wardCode, string userName, string keyWord, int? storeType, bool isAdmin, int pageIndex, int pageSize);
        Task<List<StoreDto>> GetAllStoreMaster();
        List<StoreCompactDto> GetAllStoreActive();
        Task<StoreDto> GetByIdAsync(Guid id);
        Task<StoreDto> GetAsync(string code);
        StoreDto Get(string code);
        List<StoreCompactDto> Get(List<string> codes);
        Task<Guid> InsertAsync(StoreDto dto);
        Task<bool> UpdateAsync(StoreDto dto);
        Task<bool> DeleteAsync(Guid id, string userName);
        Task<bool> IsExistAsync(Guid id);
        //Task<int> CheckRankingByMallId(Guid? mallId, int ranking);
        Task<int> IsExistStoreID(Guid? id, string mallCode, string code);
        List<StoreCreationDto> ExportStoreCreation(string keyword, string mallCode, int? storeType, bool isAdmin, string userName);
        bool CheckStoreValid(string storeCode);
        List<StoreDto> GetValidStores();
        Task<List<StoreDto>> GetStoreB2BAsync(bool isAdmin, string userName);
        Task<List<StoreDto>> GetStoreB2CAsync(bool isAdmin, string userName);
        Task<Tuple<int, List<StoreDto>>> GetPagingSearchStorePopupAsync(string mallCode, string cityCode, string districtCode, string wardCode, string userName, string keyWord, int? storeType);
        Task<bool> InsertPromotionAsync(PromotionStoreDto dto);
        Task<List<PromotionStoreDto>> GetPromotionByStoreCodeAsync(string storeCode);
        List<PromotionStoreDto> GetPromotionByStoreCode(string storeCode);
        Task<bool> UpdatePromotionAsync(PromotionStoreDto dto);
        List<string> GetListValuePaymentType(PaymentTypeScopes paymentTypeScopes);
        List<string> GetListValueDeliveryCode();
    }
}
