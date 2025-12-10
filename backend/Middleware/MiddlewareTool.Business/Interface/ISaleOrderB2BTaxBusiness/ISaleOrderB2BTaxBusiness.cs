using MiddlewareTool.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MiddlewareTool.Dto.SaleDto;

namespace MiddlewareTool.Business.Interface
{
    public interface ISaleOrderB2BTaxBusiness
    {
        Task<B2BTaxDto> GetAsync(Guid id);
        Task<B2BTaxDto> GetBySKUAsync(string sku);
        Task<Tuple<int, List<B2BTaxDto>>> GetPagingAsync(B2BTaxFilterDto filter, bool isAdmin, string userName);
        Task<Tuple<int, List<B2BTaxExportDto>>> GetExportAsync(B2BTaxFilterDto filter, bool isAdmin, string userName);
        Task<Tuple<bool, string>> InsertListAsync(List<B2BTaxDto> dtos);
        Task<Tuple<bool, B2BTaxDto>> UpdateAsync(B2BTaxDto dto);
        Task<bool> DeleteAsync(B2BTaxDto dto);
        bool CheckExist(string sku);
        bool CheckSKUExist(string sku);
        double getNormalTax(string sku);
    }
}
