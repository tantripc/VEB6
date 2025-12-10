using MiddlewareTool.Business.Interface;
using MiddlewareTool.Common;
using MiddlewareTool.Entities;
using MiddlewareTool.OpenXML;
using MiddlewareTool.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using static MiddlewareTool.Dto.CoreDto;
using static MiddlewareTool.Dto.ProductMgmtDto;

namespace MiddlewareTool.Business.Concrete
{
    public class InventoryDeltaBusiness : BaseBusiness, IInventoryDeltaBusiness
    {
        public InventoryDeltaBusiness(IUnitOfWork unitOfWork) : base(unitOfWork) { }
        public byte[] GetTransfer(byte[] template, int timeOut)
        {
            try
            {
                if (template != null && template.Length > 0)
                {
                    var sql = "Exec " + BaseBusiness.SP_InventoryDelta_GetTransfer;
                    var lstData = this.UnitOfWork.SqlQuery<InventoryDeltaBoxedDto>(sql, timeOut).ToList();
                    if(lstData != null && lstData.Count > 0)
                    {
                        var excel = new Excel();
                        excel.TemplateFileData = template;
                        excel.ParameterData.Add("MallCode", "MallCode");
                        excel.ParameterData.Add("MallName", "MallName");
                        excel.ParameterData.Add("StoreCode", "StoreCode");
                        excel.ParameterData.Add("StoreName", "StoreName");
                        excel.ParameterData.Add("Sku", "Sku");
                        excel.ParameterData.Add("QuantityDelta", "QuantityDelta");
                        excel.ParameterData.Add("Is_Published", "Is_Published");
                        var data = excel.Export(lstData);
                        return data;
                    }
                }
            }
            catch (Exception ex)
            {
                LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
            }
            return null;
        }
        public bool UpdateTransferred(DateTime dateTime, int timeOut)
        {
            try
            {
                var sql = BaseBusiness.SP_InventoryDelta_UpdateTransferred;
                var parameters = new Dictionary<string, object>();
                parameters.Add("@datetime", dateTime);
                return this.UnitOfWork.ExecuteNonQuery(sql, parameters, timeOut);
            }
            catch (Exception ex)
            {
                LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
            }
            return false;
        }
        public bool UpdateIsTransfer(string sku)
        {
            bool result = false;
            try
            {
                var lstEntity = this.UnitOfWork.GetAll<InventoryDelta>(x => x.Sku.Equals(sku) && x.ActiveFlag == STATUS_ACTIVE);
                if (lstEntity != null)
                {
                    foreach (var item in lstEntity)
                    {
                        item.IsTransfer = false;
                        item.UpdateDate = DateTime.Now;
                        result = this.UnitOfWork.Update(item);
                    }
                }
            }
            catch (Exception ex) { LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return result;
        }
    }
}
