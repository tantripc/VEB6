using MiddlewareTool.Business.Interface;
using MiddlewareTool.Entities;
using MiddlewareTool.OpenXML;
using MiddlewareTool.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using static MiddlewareTool.Dto.CoreDto;

namespace MiddlewareTool.Business.Concrete
{
    public class PricingBusiness : BaseBusiness, IPricingBusiness
    {
        public PricingBusiness(IUnitOfWork unitOfWork) : base(unitOfWork) { }
        public byte[] GetTransfer(byte[] template, string transData, int timeOut)
        {
            try
            {
                if (template != null && template.Length > 0)
                {
                    var sql = "Exec " + BaseBusiness.SP_Pricing_GetTransfer;
                    sql += " @transData = '" + transData + "';";

                    var lstData = this.UnitOfWork.SqlQuery<PricingBoxedDto>(sql, timeOut).ToList();
                    if (lstData != null && lstData.Count > 0)
                    {
                        var excel = new Excel();
                        excel.TemplateFileData = template;
                        excel.ParameterData.Add("MallCode", "MallCode");
                        excel.ParameterData.Add("MallName", "MallName");
                        excel.ParameterData.Add("StoreCode", "StoreCode");
                        excel.ParameterData.Add("StoreName", "StoreName");
                        excel.ParameterData.Add("Sku", "Sku");
                        excel.ParameterData.Add("Price", "Price");
                        excel.ParameterData.Add("DisplaySalePrice", "DisplaySalePrice");
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
        public byte[] GetTransferRealTime(byte[] template, string transData, int timeOut)
        {
            try
            {
                if (template != null && template.Length > 0)
                {
                    var sql = "Exec " + BaseBusiness.SP_Pricing_GetTransferRealTime;
                    sql += " @transData = '" + transData + "';";

                    var lstData = this.UnitOfWork.SqlQuery<PricingBoxedDto>(sql, timeOut).ToList();
                    if (lstData != null && lstData.Count > 0)
                    {
                        var m_Excel = new Excel();
                        m_Excel.TemplateFileData = template;
                        m_Excel.ParameterData.Add("MallCode", "MallCode");
                        m_Excel.ParameterData.Add("MallName", "MallName");
                        m_Excel.ParameterData.Add("StoreCode", "StoreCode");
                        m_Excel.ParameterData.Add("StoreName", "StoreName");
                        m_Excel.ParameterData.Add("Sku", "Sku");
                        m_Excel.ParameterData.Add("Price", "Price");
                        m_Excel.ParameterData.Add("DisplaySalePrice", "DisplaySalePrice");
                        var m_Data = m_Excel.Export(lstData);
                        return m_Data;
                    }
                }
            }
            catch (Exception ex)
            {
                LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
            }
            return null;
        }
        public bool UpdateTransferred(DateTime dateTime, string source, int action, string transData, int timeOut)
        {
            try
            {
                var sql = BaseBusiness.SP_Pricing_UpdateTransferred;
                var parameters = new Dictionary<string, object>();
                parameters.Add("@datetime", dateTime);
                parameters.Add("@source", source);
                parameters.Add("@action", action);
                parameters.Add("@transData", transData);
                return this.UnitOfWork.ExecuteNonQuery(sql, parameters, timeOut * 5);
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
                var lstEntity = this.UnitOfWork.GetAll<Pricing>(x => x.Sku.Equals(sku) && x.ActiveFlag == STATUS_ACTIVE);
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
