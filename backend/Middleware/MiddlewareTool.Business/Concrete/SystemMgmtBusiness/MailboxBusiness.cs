using AutoMapper;
using MiddlewareTool.Business.Interface;
using MiddlewareTool.Common;
using MiddlewareTool.Entities;
using MiddlewareTool.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using static MiddlewareTool.Dto.SystemMgmtDto;
using static MiddlewareTool.Dto.UserMgmtDto;

namespace MiddlewareTool.Business.Concrete
{
    public class MailboxBusiness : BaseBusiness, IMailboxBusiness
    {
        #region Variables
        #endregion

        #region Constructors
        public MailboxBusiness(IUnitOfWork unitOfWork) : base(unitOfWork) { }
        #endregion

        #region Methods

        public bool Insert(MailboxDto dto)
        {
            bool result = false;
            try
            {
                Dictionary<string, object> m_Param = new Dictionary<string, object>()
                {
                    {"@subject", dto.Subject},
                    {"@body", dto.Body},
                    {"@mailto", dto.MailTo},
                    {"@mailcc", dto.MailCc},
                    {"@createby", dto.CreateBy}
                };
                result = this.UnitOfWork.ExecuteNonQueryWithNull(BaseBusiness.SP_Mailbox_Insert, m_Param);
            }
            catch (Exception ex)
            {
                this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
            }
            return result;
        }
        public async Task<bool> InsertAsync(MailboxDto dto)
        {
            bool result = false;
            try
            {
                Dictionary<string, object> m_Param = new Dictionary<string, object>()
                {
                    {"@id", dto.Id},
                    {"@subject", dto.Subject},
                    {"@body", dto.Body},
                    {"@mailto", dto.MailTo},
                    {"@mailcc", dto.MailCc},
                    {"@createby", dto.CreateBy}
                };
                result = await this.UnitOfWork.ExecuteNonQueryWithNullAsync(BaseBusiness.SP_Mailbox_Insert, m_Param);
            }
            catch (Exception ex)
            {
                this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
            }
            return result;
        }
        public List<MailboxDto> GetNotSent()
        {
            var lstResult = new List<MailboxDto>();
            try
            {
                Dictionary<string, object> m_Param = new Dictionary<string, object>() { };
                var ds = this.UnitOfWork.ExecuteQuery(BaseBusiness.SP_Mailbox_GetNotSent, m_Param);
                if (ds != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        var result = new MailboxDto();
                        if (result.ParseData(ds.Tables[0].Rows[i]))
                        {
                            lstResult.Add(result);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
            }
            return lstResult;
        }
        public async Task<List<MailboxDto>> GetNotSentAsync()
        {
            var lstResult = new List<MailboxDto>();
            try
            {
                var m_Param = new Dictionary<string, object>() { };
                var ds = await this.UnitOfWork.ExecuteQueryAsync(BaseBusiness.SP_Mailbox_GetNotSent, m_Param);
                if (ds != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        var result = new MailboxDto();
                        if (result.ParseData(ds.Tables[0].Rows[i])) lstResult.Add(result);
                    }
                }
            }
            catch (Exception ex)
            {
                this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
            }
            return lstResult;
        }
        public bool UpdateNumSend(string id, string sent, string numsend)
        {
            bool result = false;
            try
            {
                Dictionary<string, object> m_Param = new Dictionary<string, object>()
                {
                    {"@id", id},
                    {"@sent", sent},
                    {"@numsend", numsend},
                };
                result = this.UnitOfWork.ExecuteNonQuery(BaseBusiness.SP_Mailbox_UpdateNumSend, m_Param);
            }
            catch (Exception ex)
            {
                this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
            }
            return result;
        }
        public async Task<Tuple<int, IList<MailboxDto>>> GetPagingAsync(int pageIndex, int pageSize, string keyWord, string fromDate, string toDate, string isSeen, string username)
        {
            IList<MailboxDto> lstResult = new List<MailboxDto>();
            int total = 0;
            try
            {
                Dictionary<string, object> m_Param = new Dictionary<string, object>()
                {
                    {"@keyWord", keyWord},
                    {"@fromdate", fromDate},
                    {"@todate", toDate},
                    {"@isseen", isSeen},
                    {"@username", username},
                    {"@pageIndex", pageIndex},
                    {"@pageSize", pageSize}
                };
                var ds = await this.UnitOfWork.ExecuteQueryAsync(BaseBusiness.SP_Mailbox_GetPaging, m_Param);
                if (ds != null)
                {
                    total = (int)ds.Tables[1].Rows[0][0];
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        var result = new MailboxDto();
                        if (result.ParseData(ds.Tables[0].Rows[i]))
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
            return new Tuple<int, IList<MailboxDto>>(total, lstResult);
        }
        public async Task<Tuple<int, List<MailboxDto>>> GetNewFeedAsync(string username, int pageSize)
        {
            var result = new List<MailboxDto>();
            int count = 0;
            try
            {
                if (!string.IsNullOrEmpty(username))
                {
                    var user = await this.GetByUserIdAsync(username);
                    if (user != null && !string.IsNullOrEmpty(user.Email))
                    {
                        var iquery = this.UnitOfWork.GetAll<Mailbox>()
                            .Where(x => x.MailTo.Contains(user.Email) && !x.IsSeen && x.Sent == true && x.ActiveFlag == STATUS_ACTIVE)
                            .OrderByDescending(x => x.CreateDate);
                        count = await iquery.CountAsync();
                        if (pageSize > 0 && pageSize <= count)
                        {
                            var lstEntity = await iquery.Take(pageSize).ToListAsync();
                            result = Mapper.Map<List<MailboxDto>>(lstEntity);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
                result = null;
            }
            return new Tuple<int, List<MailboxDto>>(count, result);
        }
        public async Task<MailboxDto> UpdateStatusAsync(Guid id, string username)
        {
            var result = new MailboxDto();
            var dbtransaction = this.UnitOfWork.BeginTransaction();
            bool check = true;
            var entity = new Mailbox();
            try
            {
                if (id != null && id != Guid.Empty)
                {
                    var user = await this.GetByUserIdAsync(username);
                    if (user != null && !string.IsNullOrEmpty(user.Email))
                    {
                        entity = await this.UnitOfWork.GetSingleAsync<Mailbox>(x => x.Id == id && x.ActiveFlag == STATUS_ACTIVE);
                        if (entity != null && entity.MailTo.Contains(user.Email) && !entity.IsSeen)
                        {
                            entity.IsSeen = true;
                            check = await this.UnitOfWork.UpdateAsync(entity);
                        }
                    }
                    else
                    {
                        check = false;
                    }
                }
                if (check)
                {
                    dbtransaction.Commit();
                    result = Mapper.Map<MailboxDto>(entity);
                }
                else { dbtransaction.Rollback(); result = null; }
            }
            catch (Exception ex)
            {
                this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
                dbtransaction.Rollback();
                result = null;
            }
            return result;
        }
        public async Task<bool> MarkAllAsReadAsync(string username)
        {
            bool result = false;
            var dbtransaction = this.UnitOfWork.BeginTransaction();
            try
            {
                if (!string.IsNullOrEmpty(username))
                {
                    var user = await this.GetByUserIdAsync(username);
                    if (user != null && !string.IsNullOrEmpty(user.Email))
                    {
                        var mailBox = this.UnitOfWork.GetAll<Mailbox>()
                            .Where(x => x.MailTo.Contains(user.Email) && x.Sent == true && !x.IsSeen && x.ActiveFlag == STATUS_ACTIVE)
                            .ToList();
                        if (mailBox.Count > 0)
                        {
                            foreach (var item in mailBox)
                            {
                                item.IsSeen = true;
                                item.UpdateBy = username;
                                item.UpdateDate = DateTime.Now;
                            }
                            result = await this.UnitOfWork.UpdateToListAsync(mailBox);
                        }
                    }
                }
                if (result == true) { dbtransaction.Commit(); }
                else { dbtransaction.Rollback(); }
            }
            catch (Exception ex)
            {
                this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
                dbtransaction.Rollback();
                result = false;
            }
            return result;
        }
        private async Task<UserInfoDto> GetByUserIdAsync(string userId)
        {
            return await this.UnitOfWork.GetAll<UserInfo>()
                .Where(x => x.UserId.ToLower().EndsWith(userId.ToLower()) && x.ActiveFlag == STATUS_ACTIVE)
                .Select(x => new UserInfoDto
                {
                    Id = x.Id,
                    UserId = x.UserId,
                    IsActive = x.IsActive,
                    FullName = x.FullName,
                    Email = x.Email,
                    Mobile = x.Mobile,
                    Address = x.Address,
                    HomePhone = x.HomePhone,
                    LanguageCode = x.LanguageCode,
                    Birthday = x.Birthday
                })
                .FirstOrDefaultAsync();
        }

        #endregion
    }
}
