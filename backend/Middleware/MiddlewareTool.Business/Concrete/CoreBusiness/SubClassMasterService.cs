using MiddlewareTool.Business.Interface.ICoreBusiness;
using MiddlewareTool.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareTool.Business.Concrete.CoreBusiness
{
    public class SubClassMasterService : BaseBusiness, ISubClassMasterService
    {
        public SubClassMasterService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
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
                    return this.UnitOfWork.ExecuteNonQuery(SP_SubClassMaster_Import, m_Param, timeOut);
                }
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return false;
        }
    }
}
