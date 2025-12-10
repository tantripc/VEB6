using AutoMapper;
using MiddlewareTool.Business.Interface;
using MiddlewareTool.Common;
using MiddlewareTool.Dto;
using MiddlewareTool.Entities;
using MiddlewareTool.Repository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace MiddlewareTool.Business.Concrete
{
    public class PromotionESLBusiness : BaseBusiness, IPromotionESLBusiness
    {
        public PromotionESLBusiness(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
        public async Task<Tuple<PromotionESLDto, string>> AddPromotionESL(PromotionESLDto promotionESLDto, string user)
        {
            try
            {
                if (IsExist(promotionESLDto.SKU, promotionESLDto.StoreCode, promotionESLDto.StartDateTime, promotionESLDto.EndDateTime))
                {
                    return new Tuple<PromotionESLDto, string>(null, $@"Already exist a promotion for this SKU in Store {promotionESLDto.StoreCode}");
                }
                var entity = Mapper.Map<PromotionESL>(promotionESLDto);
                entity.Id = Guid.NewGuid();
                entity.EndTime = new TimeSpan(entity.EndTime.Hours, entity.EndTime.Minutes, 59);
                entity.CreateBy = user;
                entity.UpdateBy = user;
                entity.UpdateDate = DateTime.Now;
                entity.CreateDate = DateTime.Now;
                entity.ActiveFlag = (byte)STATUS_ACTIVE;
                entity.EDLPFlag = (byte)promotionESLDto.EDLPFlag;
                entity.IsTransferESL = promotionESLDto.IsTransferESL;

                var result = await this.UnitOfWork.InsertAsync(entity);
                if (result != null)
                {
                    // Insert history
                    var history = new PromotionESLHistoryDto
                    {
                        Id = Guid.NewGuid(),
                        SKU = promotionESLDto.SKU,
                        StoreCode = promotionESLDto.StoreCode,
                        StartDate = promotionESLDto.StartDate,
                        EndDate = promotionESLDto.EndDate,
                        StartTime = promotionESLDto.StartTime,
                        EndTime = promotionESLDto.EndTime,
                        CreateBy = user,
                        UpdateBy = user,
                        UpdateDate = DateTime.Now,
                        CreateDate = DateTime.Now,
                        ActiveFlag = (byte)STATUS_ACTIVE,
                        Action = (byte)Common.History.Action.Insert,
                        Source = "PromotionESL/Insert"
                    };
                    var rs = await this.InsertHistory(history);
                }
                return new Tuple<PromotionESLDto, string>(promotionESLDto, "");

            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return null;
        }

        public List<PromotionESLPaging> GetPaging(PromotionESLSearchModel searchModel)
        {
            try
            {
                var startDate = searchModel.StartDate.Value.Date;
                var endDate = searchModel.EndDate.Value.Date.AddDays(1).AddTicks(-1);
                string sql = $@"
    EXEC core.SP_PromotionESL_GetPaging 
        @StartDate = '{startDate:yyyy-MM-dd HH:mm:ss}', 
        @EndDate = '{endDate:yyyy-MM-dd HH:mm:ss}', 
        @Keyword = {(string.IsNullOrEmpty(searchModel.Keyword) ? "NULL" : $"'{searchModel.Keyword}'")}, 
        @StoreCode = {((searchModel.StoreList != null && searchModel.StoreList.Count > 0) ? $"'{string.Join(",", searchModel.StoreList.Where(x => !string.IsNullOrEmpty(x)).ToList())}'" : "NULL")}, 
        @ELDPFlag = {(searchModel.ELDPFlag == 0 ? "NULL" : searchModel.ELDPFlag.ToString())}, 
        @LineId = {(searchModel.LineId.Count > 0 ? $"'{string.Join(",", searchModel.LineId)}'" : "NULL")}, 
        @DivisionId = {(searchModel.DivisionId.Count > 0 ? $"'{string.Join(",", searchModel.DivisionId)}'" : "NULL")}, 
        @GroupId = {(searchModel.GroupId.Count > 0 ? $"'{string.Join(",", searchModel.GroupId)}'" : "NULL")}, 
        @PageIndex = {searchModel.PageIndex}, 
        @PageSize = {searchModel.PageSize}";


                var rs = this.UnitOfWork.SqlQuery<PromotionESLPaging>(sql).OrderByDescending(x => x.UpdateDate).ToList();
                return rs;
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return new List<PromotionESLPaging>();
        }

        public async Task<List<PromotionESLDto>> GetDetailBySKU(PromotionESLSearchModel searchModel)
        {
            try
            {
                var currentDateTime = DateTime.Now;
                var entity = this.UnitOfWork.GetAllNoTracking<PromotionESL>()
                    .Where(x => x.SKU == searchModel.SKU
                             && x.ActiveFlag == STATUS_ACTIVE
                             && x.StartDate <= searchModel.EndDate
                             && x.EndDate >= searchModel.StartDate
                             && x.EndDateTime >= currentDateTime
                             );

                if (searchModel.StoreCode != null)
                {
                    entity = entity.Where(x => x.StoreCode == searchModel.StoreCode);
                }

                if (searchModel.ELDPFlag == 0) // All
                {
                }
                else if (searchModel.ELDPFlag == 1) // Active
                {
                    entity = entity.Where(x =>
                        x.StartDateTime <= currentDateTime &&
                        x.EndDateTime >= currentDateTime);
                }
                else if (searchModel.ELDPFlag == 2) // Upcoming
                {
                    entity = entity.Where(x => x.StartDateTime > currentDateTime);
                }
                //else // Expired
                //{
                //    entity = entity.Where(x => x.EndDateTime < currentDateTime && x.StartDateTime < currentDateTime);
                //}
                var test = entity.ToList();
                var result = await entity
                    .Join(this.UnitOfWork.GetAllNoTracking<Store>().Where(x => x.ActiveFlag == STATUS_ACTIVE),
                        promo => promo.StoreCode,
                        store => store.Code,
                        (promo, store) => new { promo, store })
                    .Join(this.UnitOfWork.GetAllNoTracking<Product>().Where(x => x.ActiveFlag == STATUS_ACTIVE),
                        combined => combined.promo.SKU,
                        product => product.Sku,
                        (combined, product) => new PromotionESLDto
                        {
                            Id = combined.promo.Id,
                            SKU = combined.promo.SKU,
                            StoreName = combined.store.Name,
                            StoreCode = combined.promo.StoreCode,
                            StartDate = combined.promo.StartDate,
                            EndDate = combined.promo.EndDate,
                            StartTime = combined.promo.StartTime,
                            EndTime = combined.promo.EndTime,
                            ProductName = product.Name
                        })
                    .ToListAsync();
                result.ForEach(x => x.TotalCount = result.Count);
                return result;
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return new List<PromotionESLDto>();
        }

        public async Task<Tuple<PromotionESLDto, string>> UpdatePromotionESL(PromotionESLDto promotionESLDto, string user)
        {
            try
            {
                var entity = await this.UnitOfWork.GetSingleAsync<PromotionESL>(x => x.Id == promotionESLDto.Id);
                if (entity == null) return new Tuple<PromotionESLDto, string>(null, "Cannot find promotion!");
                if (IsExist(promotionESLDto.SKU, promotionESLDto.StoreCode, promotionESLDto.StartDateTime, promotionESLDto.EndDateTime, entity.Id))
                    return new Tuple<PromotionESLDto, string>(null, $@"Already exist a promotion for this SKU in Store {promotionESLDto.StoreCode}");
                entity.StoreCode = promotionESLDto.StoreCode;
                entity.StartDate = promotionESLDto.StartDate;
                entity.EndDate = promotionESLDto.EndDate;
                entity.StartTime = promotionESLDto.StartTime;
                entity.EndTime = new TimeSpan(promotionESLDto.EndTime.Hours, promotionESLDto.EndTime.Minutes, 59);
                entity.UpdateBy = user;
                entity.UpdateDate = DateTime.Now;
                entity.EDLPFlag = (byte)promotionESLDto.EDLPFlag;
                entity.IsTransferESL = false;
                entity.ActiveFlag = (byte)STATUS_ACTIVE;
                var result = await this.UnitOfWork.UpdateAsync(entity);
                if (result)
                {
                    // Insert history
                    var history = new PromotionESLHistoryDto
                    {
                        Id = Guid.NewGuid(),
                        SKU = promotionESLDto.SKU,
                        StoreCode = promotionESLDto.StoreCode,
                        StartDate = promotionESLDto.StartDate,
                        EndDate = promotionESLDto.EndDate,
                        StartTime = promotionESLDto.StartTime,
                        EndTime = promotionESLDto.EndTime,
                        CreateBy = entity.CreateBy,
                        UpdateBy = user,
                        UpdateDate = DateTime.Now,
                        CreateDate = entity.CreateDate,
                        ActiveFlag = (byte)STATUS_ACTIVE,
                        Action = (byte)Common.History.Action.Update,
                        Source = "PromotionESL/Update"
                    };
                    var rs = await this.InsertHistory(history);
                    return new Tuple<PromotionESLDto, string>(promotionESLDto, "Success");
                }

            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return null;
        }
        public async Task<Tuple<string, bool>> DeletePromotion(Guid Id, string user)
        {
            try
            {
                var entity = await this.UnitOfWork.GetSingleAsync<PromotionESL>(x => x.Id == Id);
                if (entity == null) return new Tuple<string, bool>("Cannot find promotion!", false);
                entity.UpdateDate = DateTime.Now;
                entity.UpdateBy = user;
                entity.ActiveFlag = (byte)STATUS_DELETE;
                bool rs = await this.UnitOfWork.UpdateAsync(entity);
                if (rs)
                {
                    // Insert history
                    var history = new PromotionESLHistoryDto
                    {
                        Id = Guid.NewGuid(),
                        SKU = entity.SKU,
                        StoreCode = entity.StoreCode,
                        StartDate = entity.StartDate,
                        EndDate = entity.EndDate,
                        StartTime = entity.StartTime,
                        EndTime = entity.EndTime,
                        CreateBy = entity.CreateBy,
                        UpdateBy = user,
                        UpdateDate = DateTime.Now,
                        CreateDate = entity.CreateDate,
                        ActiveFlag = (byte)STATUS_ACTIVE,
                        Action = (byte)Common.History.Action.Delete,
                        Source = "PromotionESL/Delete"
                    };
                    _ = await this.InsertHistory(history);
                    return new Tuple<string, bool>("Success", true);
                }
                else return new Tuple<string, bool>("Fail", false);
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return new Tuple<string, bool>("Fail", false);
        }
        public bool InsertExcel(List<PromotionESLImport> promotionESLDtos, string user, string fileName, string tempFile)
        {
            try
            {
                bool result = false;
                const int batchSize = 10000;
                var totalRecords = promotionESLDtos.Count;
                Guid logID = Guid.NewGuid();
                // Tính số batch cần chia
                int numberOfBatches = (int)Math.Ceiling((double)totalRecords / batchSize);
                for (int i = 0; i < numberOfBatches; i++)
                {
                    var batch = promotionESLDtos.Skip(i * batchSize).Take(batchSize).ToList();
                    var dt = ToDataTable(batch);
                    Dictionary<string, object> m_Param = new Dictionary<string, object>()
                    {
                        {"@PromotionESLData", dt},
                        {"@UserName", user},
                        {"@Source",  fileName},
                        {"@Action", Common.History.Action.Insert },
                        {"@LogId", logID.ToString() }
                    };
                    result = this.UnitOfWork.ExecuteNonQuery(SP_PromotionESL_Import, m_Param);
                }
                if (result)
                {
                    var rs = this.UnitOfWork.Insert(new SystemLog
                    {
                        LogId = Guid.NewGuid(),
                        Module = Common.AppModule.ESL.ToString(),
                        UserId = user,
                        UserFunction = (int)AppSystemLog.Action.Import,
                        EventResult = 1,
                        FuncDateTime = DateTime.Now,
                        Source = "PromotionESL/Import",
                        Transdata = fileName,
                        WSName = ""
                    });
                    // Lưu log file import
                    var TempFolderPath = ConfigurationSettings.AppSettings["TempFolder_Key"] ?? Common.AppValue.TempFolder_Default;
                    if (!System.IO.Directory.Exists(TempFolderPath))
                        System.IO.Directory.CreateDirectory(TempFolderPath);
                    var zipFilePath = Path.Combine(TempFolderPath, tempFile);
                    var fileContent = File.ReadAllBytes(zipFilePath);
                    var sysAtt = new SystemLogAttachment
                    {
                        Id = logID,
                        LogId = rs.LogId,
                        Name = fileName,
                        FileName = fileName,
                        FileContent = fileContent,

                        URL = "",
                        CreateBy = user,
                        CreateDate = DateTime.Now,
                        UpdateBy = user,
                        UpdateDate = DateTime.Now,
                        ActiveFlag = STATUS_ACTIVE
                    };
                    sysAtt = this.UnitOfWork.Insert(sysAtt);
                    if (sysAtt != null)
                        File.Delete(zipFilePath);
                }
                return result;
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return false;
        }
        public bool IsExist(string sku, string storeCode, DateTime startDateTime, DateTime endDateTime, Guid? id = null)
        {
            try
            {
                ///
                var now = DateTime.Now;
                bool ELDPFlag = startDateTime <= now && endDateTime >= now;
                var checkExist = UnitOfWork.GetSingle<PromotionESL>(x =>
                    x.SKU == sku &&
                    x.StoreCode == storeCode &&
                    x.Id != id &&
                    x.ActiveFlag == (byte)STATUS_ACTIVE &&
                    (
                        (ELDPFlag && x.StartDateTime <= now && x.EndDateTime > now) || // Active
                        (!ELDPFlag && x.StartDateTime > now)  // Upcoming
                    ));

                return checkExist != null;
            }
            catch (Exception ex)
            {
                this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
                return false;
            }
        }

        public Tuple<List<PromotionESLHistoryDto>, int> GetHistoryPaging(PromotionHistorySearchModel searchModel)
        {
            var result = new List<PromotionESLHistoryDto>();
            try
            {
                if (searchModel.EndDate.HasValue)
                {
                    searchModel.EndDate = searchModel.EndDate.Value.AddDays(1).Date.AddMilliseconds(-1);
                }
                var entity = this.UnitOfWork.GetAllNoTracking<PromotionESLHistory>()
                                            .Where(x => x.UpdateDate >= searchModel.StartDate
                                                     && x.UpdateDate <= searchModel.EndDate);
                if (!String.IsNullOrEmpty(searchModel.SKU))
                {
                    entity = entity.Where(x => x.SKU == searchModel.SKU);
                }
                if (searchModel.StoreCode != null)
                {
                    entity = entity.Where(x => x.StoreCode == searchModel.StoreCode);
                }
                var currentDateTime = DateTime.Now;
                if (searchModel.ELDPFlag == 0) // All
                {
                }
                else if (searchModel.ELDPFlag == 1) // Upcoming
                {
                    entity = entity.Where(x =>
                        x.StartDateTime <= currentDateTime &&
                        x.EndDateTime >= currentDateTime);
                }
                else if (searchModel.ELDPFlag == 2) // Upcoming
                {
                    entity = entity.Where(x => x.StartDateTime > currentDateTime);
                }
                else // Expired
                {
                    entity = entity.Where(x => x.EndDateTime < currentDateTime && x.StartDateTime < currentDateTime);
                }

                if (searchModel.Action.HasValue)
                {
                    entity = entity.Where(x => x.Action == searchModel.Action);
                }
                int total = entity.Count();
                var test = entity.ToList();
                var rs = entity.OrderByDescending(x => x.UpdateDate)
                    .Join(this.UnitOfWork.GetAllNoTracking<UserInfo>().Where(x => x.ActiveFlag == STATUS_ACTIVE),
                        promo => promo.UpdateBy,
                        user => user.UserId,
                        (promo, user) => new PromotionESLHistoryDto
                        {
                            Id = promo.Id,
                            URL = promo.URL,
                            SKU = promo.SKU,
                            StoreCode = promo.StoreCode,
                            StartDate = promo.StartDate,
                            EndDate = promo.EndDate,
                            StartTime = promo.StartTime,
                            EndTime = promo.EndTime,
                            UpdateBy = user.FullName,
                            UpdateDate = promo.UpdateDate,
                            Action = promo.Action,
                            Source = promo.Source,
                            IsTransferESL = promo.IsTransferESL,
                            EDLPFLagHistory = promo.EDLPFlag == 1 ? "Active" : promo.EDLPFlag == 2 ? "Upcoming" : "Expried",
                        })
                    .OrderByDescending(x => x.UpdateDate)
                    .ThenByDescending(x => x.StartDate)
                    .ThenByDescending(x => x.EndDate)
                    .ThenByDescending(x => x.EndDate)
                    .ThenByDescending(x => x.EndTime)
                    .Skip((searchModel.PageIndex - 1) * searchModel.PageSize)
                    .Take(searchModel.PageSize)
                    .ToList();
                return new Tuple<List<PromotionESLHistoryDto>, int>(rs, total);
            }
            catch (Exception ex)
            {
                this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
            }
            return new Tuple<List<PromotionESLHistoryDto>, int>(result, 0);
        }
        public async Task<bool> InsertHistory(PromotionESLHistoryDto history)
        {
            try
            {
                var entity = Mapper.Map<PromotionESLHistory>(history);
                entity.Id = Guid.NewGuid();
                entity.CreateBy = history.CreateBy;
                entity.UpdateBy = history.UpdateBy;
                entity.UpdateDate = DateTime.Now;
                entity.CreateDate = history.CreateDate;
                var result = await this.UnitOfWork.InsertAsync(entity);
                if (result != null)
                {
                    return true;
                }
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return false;
        }

        public Dictionary<Tuple<string, string, bool>, Tuple<DateTime, DateTime>> GetAllEDLP(List<string> skus)
        {
            try
            {
                var dict = this.UnitOfWork.GetAllNoTracking<PromotionESL>().AsNoTracking()
                        .Where(x => skus.Contains(x.SKU)
                                    && x.ActiveFlag == STATUS_ACTIVE
                                    && x.EndDateTime >= DateTime.Now)
                        .Select(x => new
                        {
                            x.SKU,
                            x.StoreCode,
                            IsStarted = x.StartDateTime <= DateTime.Now,
                            x.StartDate,
                            x.EndDate
                        })
                        .ToList() // EF sẽ thực thi truy vấn tại đây
                        .ToDictionary(
                            x => Tuple.Create(x.SKU, x.StoreCode, x.IsStarted),
                            x => Tuple.Create(x.StartDate, x.EndDate)
                        );
                return dict;
            }
            catch (Exception ex)
            {
                this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
                return new Dictionary<Tuple<string, string, bool>, Tuple<DateTime, DateTime>>();
            }
        }
    }
}
