using MiddlewareTool.Dto;
using System;
using System.Collections.Generic;

namespace MiddlewareTool.Business.Interface
{
    public interface IDiscountTypeBusiness
    {
        Tuple<int, List<DiscountTypeDto>> GetPaging(DiscountTypeFilterDto filter);
        List<DiscountTypeDto> GetActive();
        DiscountTypeDto Get(Guid id);
        DiscountTypeDto Insert(DiscountTypeDto dto);
        DiscountTypeDto Update(DiscountTypeDto dto);
        bool Delete(Guid id);
    }
}