using MiddlewareTool.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareTool.Business.Interface
{
    public interface IPromotionESLBusiness
    {
        Task<Tuple<PromotionESLDto, string>> AddPromotionESL(PromotionESLDto promotionESLDto, string user);
        List<PromotionESLPaging> GetPaging(PromotionESLSearchModel searchModel);
        Task<List<PromotionESLDto>> GetDetailBySKU(PromotionESLSearchModel searchModel);
        Task<Tuple<PromotionESLDto, string>> UpdatePromotionESL(PromotionESLDto promotionESLDto, string user);
        Task<Tuple<string, bool>> DeletePromotion(Guid Id, string user);
        bool InsertExcel(List<PromotionESLImport> promotionESLDtos, string user, string fileName, string tempFile);
        bool IsExist(string sku, string storeCode, DateTime startDateTime, DateTime endDateTime, Guid? id = null);
        Tuple<List<PromotionESLHistoryDto>, int> GetHistoryPaging(PromotionHistorySearchModel searchModel);
        Task<bool> InsertHistory(PromotionESLHistoryDto history);
        Dictionary<Tuple<string, string, bool>, Tuple<DateTime, DateTime>> GetAllEDLP(List<string> skus);

    }
}
