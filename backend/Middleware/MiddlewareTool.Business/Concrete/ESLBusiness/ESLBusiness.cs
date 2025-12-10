using MiddlewareTool.Business.Interface;
using MiddlewareTool.Dto;
using MiddlewareTool.OpenXML;
using MiddlewareTool.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;

namespace MiddlewareTool.Business.Concrete
{
    public class ESLBusiness : BaseBusiness, IESLBusiness
    {
        public ESLBusiness(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public List<string> M_GetStores(DateTime datetime, int timeout = 120, string storeCodes = null)
        {
            var lst = new List<string>();
            try
            {
                string sql = "EXEC [core].[SP_ESL_M_GetStores] @storeCodes, @datetime ";
                SqlParameter[] parameters =
                {
                    new SqlParameter("@dateTime", SqlDbType.DateTime) { Value = datetime },
                    new SqlParameter("@storeCodes", SqlDbType.NVarChar)
                        {
                            Value = storeCodes ?? (object)DBNull.Value
                        }
                };
                var ds = this.UnitOfWork.SqlQuery<ESL_Store_Dto>(sql, timeout, parameters).ToList();
                lst = ds.Select(x => x.StoreCode).Distinct().OrderBy(x => x).ToList();
            }
            catch (Exception ex)
            {
                LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
            }
            return lst;
        }

        public List<string> INV_GetStores(bool _daily = false, int timeout = 120, string storeCodes = null)
        {
            var lst = new List<string>();
            try
            {
                string sql = $"EXEC {(_daily ? "[core].[SP_ESL_INV_GetStores_Daily]" : "[core].[SP_ESL_INV_GetStores_Hourly]")}";
                if (!string.IsNullOrEmpty(storeCodes))
                    sql += $" @storeCodes = '{storeCodes}'";
                var ds = this.UnitOfWork.SqlQuery<ESL_Store_Dto>(sql, timeout).ToList();
                lst = ds.Select(x => x.StoreCode).Distinct().OrderBy(x => x).ToList();
            }
            catch (Exception ex)
            {
                LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
            }
            return lst;
        }

        public byte[] INV_GetTransfer(byte[] template, string storeCode, bool _daily = false, int timeout = 120)
        {
            byte[] bytes = null;
            try
            {
                string sql = $"EXEC {(_daily ? "[core].[SP_ESL_INV_GetTransfer_Daily]" : "[core].[SP_ESL_INV_GetTransfer_Hourly]")}";
                sql += $" @storeCode = '{storeCode}'";
                var lstData = this.UnitOfWork.SqlQuery<ESL_INV_Dto>(sql, timeout).ToList();
                if (lstData?.Count > 0)
                {
                    var excel = new Excel();
                    excel.TemplateFileData = template;
                    bytes = excel.Export(lstData);
                }
            }
            catch (Exception ex)
            {
                LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
            }
            return bytes;
        }

        public bool INV_UpdateTransferred(DateTime dateTime, int timeout = 120)
        {
            bool isSuccess = false;
            try
            {
                string sql = $"[core].[SP_ESL_INV_UpdateTransferred]";
                var prs = new Dictionary<string, object>();
                prs.Add("@datetime", dateTime);
                isSuccess = this.UnitOfWork.ExecuteNonQuery(sql, prs, timeout);
            }
            catch (Exception ex)
            {
                LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
            }
            return isSuccess;
        }

        public byte[] M_GetTransfer(byte[] template, string storeCode, int timeout = 120)
        {
            byte[] bytes = null;
            try
            {
                string sql = $"EXEC [core].[SP_ESL_M_GetTransfer]";
                sql += $" @storeCode = '{storeCode}'";

                var lstData = this.UnitOfWork.SqlQuery<ESL_M_Dto>(sql, timeout).ToList();
                if (lstData?.Count > 0)
                {
                    var excel = new Excel();
                    excel.TemplateFileData = template;
                    bytes = excel.Export(lstData);
                }
            }
            catch (Exception ex)
            {
                LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
            }
            return bytes;
        }

        public bool M_UpdateTransferred(DateTime dateTime, int timeout = 120)
        {
            bool isSuccess = false;
            try
            {
                string sql = $"[core].[SP_ESL_M_UpdateTransferred]";
                var prs = new Dictionary<string, object>();
                prs.Add("@datetime", dateTime);
                isSuccess = this.UnitOfWork.ExecuteNonQuery(sql, prs, timeout);
            }
            catch (Exception ex)
            {
                LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
            }
            return isSuccess;
        }
        public bool UpdatePromotionEDLPFlag(DateTime dateTime, int timeout = 120)
        {
            bool isSuccess = false;
            try
            {
                string sql = $"[core].[SP_PromotionESL_Update_ELDPFlag]";
                var prs = new Dictionary<string, object>();
                prs.Add("@datetime", dateTime);
                isSuccess = this.UnitOfWork.ExecuteNonQuery(sql, prs, timeout);
            }
            catch (Exception ex)
            {
                LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
            }
            return isSuccess;
        }
    }
}
