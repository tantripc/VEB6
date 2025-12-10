using MiddlewareTool.Business.Interface;
using MiddlewareTool.Entities;
using MiddlewareTool.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace MiddlewareTool.Business.Concrete
{
    public class NPriceChangeBusiness : BaseBusiness, INPriceChangeBusiness
    {
        public NPriceChangeBusiness(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public bool Exist(string prc_no, string storeCode)
        {
            try
            {
                return this.UnitOfWork.GetAllNoTracking<NPriceChange>().Any(x => x.ActiveFlag == STATUS_ACTIVE && x.REC_ID != "C" && x.PRC_NO == prc_no && x.StoreCode == storeCode);
            }
            catch (Exception ex)
            {
                this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
            }
            return false;
        }

        public bool Import(DataTable dt, int timeOut)
        {
            try
            {
                if (dt != null)
                {
                    Dictionary<string, object> m_Param = new Dictionary<string, object>()
                    {
                        {"@dt", dt}
                    };
                    return this.UnitOfWork.ExecuteNonQuery(SP_NPriceChange_Import, m_Param, timeOut);
                }
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return false;
        }
    }
}
