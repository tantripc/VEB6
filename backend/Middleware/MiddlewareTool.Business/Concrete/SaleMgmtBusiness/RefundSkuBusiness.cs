using AutoMapper;
using MiddlewareTool.Entities;
using MiddlewareTool.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using static MiddlewareTool.Dto.SaleDto;

namespace MiddlewareTool.Business.Concrete
{
    public class RefundSkuBusiness : BaseBusiness, IRefundSkuBusiness
    {
        public RefundSkuBusiness(IUnitOfWork unitOfWork) : base(unitOfWork) { }
        public List<string> GetAllSku()
        {
            try
            {
                var iquery = this.UnitOfWork.GetAll<RefundSku>(x => x.ActiveFlag == STATUS_ACTIVE);
                if (iquery != null)
                {
                    return iquery.Select(x => x.Sku).Distinct().ToList();
                }
            }
            catch (Exception ex) { LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return null;
        }
    }
}
