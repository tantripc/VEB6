using MiddlewareTool.Dto;
using System;
using System.Collections.Generic;
using static MiddlewareTool.Common.AppType;

namespace MiddlewareTool.Business.Interface
{
    public interface IPaymentTypeMappingBusiness
    {
        PaymentTypeMappingDto Get(Guid id);
        Tuple<int, List<PaymentTypeMappingDto>> GetPaging(PaymentTypeMappingFilterDto filter);
        PaymentTypeMappingDto Insert(PaymentTypeMappingDto dto);
        PaymentTypeMappingDto Update(PaymentTypeMappingDto dto);
        Tuple<string, bool> Delete(PaymentTypeMappingDto dto);
        List<SelectSetting> GetScopeSettings();
        List<SelectSetting> GetCustomerTypeSettings();
        List<SelectSetting> GetMethodSettings();
        List<PaymentTypeMappingDto> GetForSale();
        List<PaymentTypeMappingDto> GetForRefund();
    }
}
