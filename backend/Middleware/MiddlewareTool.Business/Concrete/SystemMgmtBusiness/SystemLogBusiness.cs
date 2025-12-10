using AutoMapper;
using MiddlewareTool.Business.Interface;
using MiddlewareTool.Entities;
using MiddlewareTool.Repository;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using static MiddlewareTool.Dto.SystemMgmtDto;

namespace MiddlewareTool.Business.Concrete
{
    internal class SystemLogBusiness : BaseBusiness, ISystemLogBusiness
    {
        #region Variables
        #endregion

        #region Constructors
        public SystemLogBusiness(IUnitOfWork unitOfWork) : base(unitOfWork) { }
        #endregion

        #region Methods
        public Tuple<int, List<SystemLogDto>> GetPaging(string userName, string keyWord, string module, int userFunction, int eventResult, string dateFrom, string dateTo, int pageIndex, int pageSize, string source)
        {
            int total = 0;
            var lstSystemDTO = new List<SystemLogDto>();
            Tuple<int, List<SystemLogDto>> lstResult = new Tuple<int, List<SystemLogDto>>(total, lstSystemDTO);
            try
            {
                if (!string.IsNullOrEmpty(keyWord))
                {
                    keyWord = keyWord.Replace("'", "''");
                }
                Dictionary<string, object> m_Param = new Dictionary<string, object>()
                {
                    {"@userId", userName},
                    {"@keyWord", keyWord},
                    {"@source", source},
                    {"@module", module},
                    {"@userFunction", userFunction},
                    {"@eventResult", eventResult},
                    {"@dateFrom", dateFrom},
                    {"@dateTo", dateTo},
                    {"@pageIndex", pageIndex},
                    {"@pageSize", pageSize}
                };
                var ds = this.UnitOfWork.ExecuteQuery(BaseBusiness.SP_SystemLog_GetPaging, m_Param);
                if (ds != null)
                {
                    total = 0;
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        var result = new SystemLogDto();
                        if (ds.Tables[0].Rows[i] != null && result.ParseData(ds.Tables[0].Rows[i]))
                        {
                            lstSystemDTO.Add(result);
                        }
                    }
                    if (lstSystemDTO?.Count > 0)
                        total = lstSystemDTO[0].TotalRecord;
                    lstResult = new Tuple<int, List<SystemLogDto>>(total, lstSystemDTO);
                }
            }
            catch (Exception ex)
            {
                this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
                lstResult = null;
            }
            return lstResult;
        }
        public List<SystemLogDto> Export(string userName, string keyWord, string module, int userFunction, int eventResult, string dateFrom, string dateTo)
        {
            var lstResult = new List<SystemLogDto>();
            try
            {
                Dictionary<string, object> m_Param = new Dictionary<string, object>()
                {
                    {"@userId", userName},
                    {"@keyWord", keyWord},
                    {"@module", module},
                    {"@userFunction", userFunction},
                    {"@eventResult", eventResult},
                    {"@dateFrom", dateFrom},
                    {"@dateTo", dateTo}
                };
                var ds = this.UnitOfWork.ExecuteQuery(BaseBusiness.SP_SystemLog_Export, m_Param);
                if (ds != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        var result = new SystemLogDto();
                        if (ds.Tables[0].Rows[i] != null && result.ParseData(ds.Tables[0].Rows[i]))
                        {
                            lstResult.Add(result);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
                lstResult = null;
            }
            return lstResult;
        }
        public bool Insert(SystemLogDto dto)
        {
            bool result = false;
            try
            {
                var entity = Mapper.Map<SystemLog>(dto);
                var res = this.UnitOfWork.Insert(entity);
                if (res != null) { result = true; }
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return result;
        }
        public async Task<bool> InsertAsync(SystemLogDto dto)
        {
            bool result = false;
            try
            {
                Dictionary<string, object> m_Param = new Dictionary<string, object>()
                {
                    {"@logId", dto.LogId},
                    {"@module", dto.Module},
                    {"@userId", dto.UserId},
                    {"@userFunction", dto.UserFunction},
                    {"@eventResult", dto.EventResult},
                    {"@funcDateTime",dto.FuncDateTime},
                    {"@source",dto.Source},
                    {"@transdata", dto.Transdata},
                    {"@WSName", dto.WSName}
                };
                result = await this.UnitOfWork.ExecuteNonQueryWithNullAsync(BaseBusiness.SP_SystemLog_Insert, m_Param);
            }
            catch (Exception ex)
            {
                this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
            }
            return result;
        }
        public SystemLogAttachmentDto GetLogAttachment(Guid id)
        {
            SystemLogAttachmentDto dto = null;
            try
            {
                dto = this.UnitOfWork.GetItem<SystemLogAttachmentDto, SystemLogAttachment>(x => x.Id == id);
            }
            catch (Exception ex)
            {
                this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
            }
            return dto;
        }
        #endregion
    }
}
