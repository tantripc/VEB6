using MiddlewareTool.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareTool.Business.Interface.ICoreBusiness
{
    public interface ISubClassMasterService
    {
        bool Import(DataTable dt, int timeOut);
    }
}
