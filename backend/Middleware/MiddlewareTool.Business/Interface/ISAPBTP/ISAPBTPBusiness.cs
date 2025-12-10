using MiddlewareTool.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static MiddlewareTool.Dto.RefundDto;
using static MiddlewareTool.Dto.SaleDto;

namespace MiddlewareTool.Business.Interface
{
    public interface ISAPBTPBusiness
    {
        bool CallMonthlyMemberAPI(int month, int year, out string error);
        bool CallMonthlyMemberAPI(int month, int year, out string error, out int countResult);
        /// <summary>
        /// Gọi API đến S4HANA
        /// </summary>
        /// <param name="headers">Danh sách sale order</param>
        /// <returns>Số record gọi thành công</returns>
        int CallS4HANAAPI(List<HeaderByStoreDto> headers, out List<HeaderByStoreDto> errors);
        /// <summary>
        /// Gọi API đến S4HANA
        /// </summary>
        /// <param name="headers">Danh sách refund order</param>
        /// <returns>Số record gọi thành công</returns>
        int CallS4HANAAPI(List<RefundHeaderDto> headers, out List<RefundHeaderDto> errors);
        Task<Tuple<int, List<MonthlyMemberSaleDto>>> GetPagingAsync(MonthlyMemberSaleFilterDto filter);
    }
}
