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
    public class BarcodeMasterBusiness : BaseBusiness, IBarcodeMasterBusiness
    {
        public BarcodeMasterBusiness(IUnitOfWork unitOfWork) : base(unitOfWork) { }
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
                    return this.UnitOfWork.ExecuteNonQuery(SP_BarcodeMaster_Import, m_Param, timeOut);
                }
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return false;
        }
    }
}
