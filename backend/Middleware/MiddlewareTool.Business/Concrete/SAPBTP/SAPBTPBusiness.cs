using AutoMapper;
using MiddlewareTool.Business.Interface;
using MiddlewareTool.Common;
using MiddlewareTool.Dto;
using MiddlewareTool.Dto.S4;
using MiddlewareTool.Entities;
using MiddlewareTool.Repository;
using MiddlewareTool.Utility;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using static MiddlewareTool.Common.AppSystemLog;
using static MiddlewareTool.Common.AppValue;
using static MiddlewareTool.Dto.RefundDto;
using static MiddlewareTool.Dto.SaleDto;

namespace MiddlewareTool.Business.Concrete
{
    public class SAPBTPBusiness : BaseBusiness, ISAPBTPBusiness
    {
        private readonly ISaleBusiness _saleBusiness;
        private readonly IRefundBusiness _refundBusiness;
        public SAPBTPBusiness(IUnitOfWork unitOfWork, ISaleBusiness saleBusiness, IRefundBusiness refundBusiness) : base(unitOfWork)
        {
            _saleBusiness = saleBusiness;
            _refundBusiness = refundBusiness;
        }
        public bool CallMonthlyMemberAPI(int month, int year, out string error)
        {
            return CallMonthlyMemberAPI(month, year, out error, out int countResult);
        }

        public bool CallMonthlyMemberAPI(int month, int year, out string error, out int countResult)
        {
            var rs = false;
            error = string.Empty;
            string apiURL = string.Empty;
            var lstResult = new List<MonthlyMemberSaleDto>();
            try
            {
                #region 1. Lấy thông tin API từ Setting
                var lstSetting = this.UnitOfWork.GetAllNoTracking<SystemSetting>().Where(x => x.Type == (byte)AppType.Setting.SAP_BTP_MonthlyMember).ToList();

                apiURL = lstSetting.FirstOrDefault(x => x.Name == AppType.Setting.SAP_BTP_MonthlyMember.ToString() + "_URL")?.Value;
                var apiUser = lstSetting.FirstOrDefault(x => x.Name == AppType.Setting.SAP_BTP_MonthlyMember.ToString() + "_User")?.Value;
                var apiPassword = lstSetting.FirstOrDefault(x => x.Name == AppType.Setting.SAP_BTP_MonthlyMember.ToString() + "_Password")?.Value;

                #endregion

                #region 2. Gọi API
                var strMonth = month.ToString("0#");
                var strYear = year.ToString("000#");
                var filterParams = $"?$format=json&$filter=Transactionmonth eq '{strMonth}' and Transactionyear eq '{strYear}'";
                apiURL += filterParams;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(apiURL);
                request.Method = "GET";
                request.Credentials = new NetworkCredential(apiUser, apiPassword);
                request.PreAuthenticate = true;
                string responseData = string.Empty;
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    responseData = reader.ReadToEnd();
                    error = responseData;
                    try
                    {
                        RootMemberSalesObject data = JsonConvert.DeserializeObject<RootMemberSalesObject>(responseData);

                        foreach (var member in data.d.results)
                        {
                            var memberDto = MonthlyMemberSaleDto.Clone(member);
                            lstResult.Add(memberDto);
                        }
                        LogCallMonthlyMemberAPI(apiURL, (int)EventResult.Success, responseData);
                    }
                    catch (Exception ex)
                    {
                        LogCallMonthlyMemberAPI(apiURL, (int)EventResult.Fail, responseData);
                        throw (ex);
                    }
                }
                #endregion

                #region 3. Insert/Update Member
                var lstOld = this.UnitOfWork.GetAllNoTracking<MonthlyMemberSale>().Where(x => x.ActiveFlag == STATUS_ACTIVE
                && x.Transactionyear == strYear
                && x.Transactionmonth == strMonth)
                    .ToList();
                var oldKeys = new HashSet<string>(lstOld.Select(x => $"{x.Membercode}_{x.Transactionyear}_{x.Transactionmonth}"));
                var newItems = lstResult.Where(x =>
    !oldKeys.Contains($"{x.Membercode}_{x.Transactionyear}_{x.Transactionmonth}")
).ToList();
                var updateItems = lstResult.Where(x =>
    oldKeys.Contains($"{x.Membercode}_{x.Transactionyear}_{x.Transactionmonth}")
).ToList();
                updateItems.ForEach(x =>
                {
                    var old = lstOld.FirstOrDefault(o => o.Membercode == x.Membercode
                    && o.Transactionyear == x.Transactionyear
                    && o.Transactionmonth == x.Transactionmonth);
                    x.Id = old.Id;
                    x.CreateDate = old.CreateDate;
                    x.CreateBy = old.CreateBy;
                });

                var newRecordsMapped = Mapper.Map<List<MonthlyMemberSale>>(newItems);
                var updateRecordsMapped = Mapper.Map<List<MonthlyMemberSale>>(updateItems);
                countResult = newRecordsMapped.Count + updateRecordsMapped.Count;
                var trans = this.UnitOfWork.BeginTransaction();
                try
                {
                    this.UnitOfWork.UpdateToList(updateRecordsMapped);
                    this.UnitOfWork.InsertToList(newRecordsMapped);
                    trans.Commit();
                    rs = true;
                }
                catch (Exception ex)
                {
                    error = apiURL;
                    this.UnitOfWork.Rollback(trans);
                    throw (ex);
                }

                #endregion
            }
            catch (Exception ex)
            {
                error = apiURL;
                throw (ex);
            }
            return rs;
        }

        public int CallS4HANAAPI(List<HeaderByStoreDto> headers, out List<HeaderByStoreDto> errors)
        {
            errors = new List<HeaderByStoreDto>();
            if (!(headers?.Count > 0))
            {
                return 0;
            }
            var rs = 0;
            string apiURL = string.Empty;
            try
            {
                #region 1. Lấy thông tin API từ Setting
                var lstSetting = this.UnitOfWork.GetAllNoTracking<SystemSetting>().Where(x => x.Type == (byte)AppType.Setting.SAP_S4HAHA).ToList();

                apiURL = lstSetting.FirstOrDefault(x => x.Name == AppType.Setting.SAP_S4HAHA.ToString() + "_URL")?.Value;
                var apiUser = lstSetting.FirstOrDefault(x => x.Name == AppType.Setting.SAP_S4HAHA.ToString() + "_User")?.Value;
                var apiPassword = lstSetting.FirstOrDefault(x => x.Name == AppType.Setting.SAP_S4HAHA.ToString() + "_Password")?.Value;
                #endregion

                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                foreach (var header in headers)
                {
                    string requestData = string.Empty;
                    try
                    {
                        #region Get record Sale
                        var record = this.UnitOfWork.GetAllNoTracking<RecordSale>().FirstOrDefault(x => x.HeaderId == header.Id && x.StoreCode == header.StoreCode);
                        #endregion
                        var s4header = new S4Dto(header);
                        s4header.SetUUID(record.Id);
                        if (string.IsNullOrEmpty(s4header.extCustomerNo))
                        {
                            #region Update IsS4Transfrred = Null nếu gửi request lỗi
                            _saleBusiness.UpdateS4Transferred(header.StoreCode, header.Id, false);
                            #endregion
                            errors.Add(header);
                            LogCallS4HAHAAPI(apiURL, (int)EventResult.Fail, "", "", header);
                            continue;
                        }
                        requestData = JsonConvert.SerializeObject(s4header);
                        string responseData = string.Empty;
                        using (HttpClient client = new HttpClient())
                        {
                            // Thiết lập Basic Authentication
                            var byteArray = Encoding.ASCII.GetBytes($"{apiUser}:{apiPassword}");
                            client.DefaultRequestHeaders.Authorization =
                                new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

                            // Thêm Header tùy chỉnh
                            client.DefaultRequestHeaders.Add("X-Requested-With", "X");

                            // Chuẩn bị nội dung gửi
                            var content = new StringContent(requestData, Encoding.UTF8, "application/json");

                            // Gửi yêu cầu POST
                            var res = client.PostAsync(apiURL, content).GetAwaiter();

                            HttpResponseMessage response = res.GetResult();
                            // Đọc phản hồi
                            var responseDataTask = response.Content.ReadAsStringAsync().GetAwaiter();
                            responseData = responseDataTask.GetResult();
                            try
                            {
                                var serializer = new XmlSerializer(typeof(Entry));
                                var headerResponse = S4Dto.Deserialize(responseData);
                                #region Đánh dấu đã gửi S4HANA
                                if (headerResponse.Status == AppType.S4HANAStatuses.Success)
                                {
                                    _saleBusiness.UpdateS4Transferred(header.StoreCode, header.Id);
                                    LogCallS4HAHAAPI(apiURL, (int)EventResult.Success, requestData, responseData, header);
                                    rs++;
                                }
                                else
                                {
                                    #region Update IsS4Transfrred = Null nếu gửi request lỗi
                                    _saleBusiness.UpdateS4Transferred(header.StoreCode, header.Id, false);
                                    #endregion
                                    errors.Add(header);
                                    LogCallS4HAHAAPI(apiURL, (int)EventResult.Fail, requestData, responseData, header);
                                }
                                #endregion
                            }
                            catch (Exception ex)
                            {
                                #region Update IsS4Transfrred = Null nếu gửi request lỗi
                                _saleBusiness.UpdateS4Transferred(header.StoreCode, header.Id, false);
                                #endregion
                                errors.Add(header);
                                LogCallS4HAHAAPI(apiURL, (int)EventResult.Fail, requestData, responseData, header);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        errors.Add(header);
                        LogCallS4HAHAAPI(apiURL, (int)EventResult.Fail, requestData, ex.Message, header);
                        if (ex?.InnerException?.Message == "Unable to connect to the remote server")
                        {
                            throw (ex);
                        }
                        #region Update IsS4Transfrred = Null nếu gửi request lỗi
                        _saleBusiness.UpdateS4Transferred(header.StoreCode, header.Id, false);
                        #endregion
                        LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
                    }
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }
            return rs;
        }

        public int CallS4HANAAPI(List<RefundDto.RefundHeaderDto> headers, out List<RefundHeaderDto> errors)
        {
            errors = new List<RefundHeaderDto>();
            if (!(headers?.Count > 0))
                return 0;
            var rs = 0;
            string apiURL = string.Empty;
            try
            {
                #region 1. Lấy thông tin API từ Setting
                var lstSetting = this.UnitOfWork.GetAllNoTracking<SystemSetting>().Where(x => x.Type == (byte)AppType.Setting.SAP_S4HAHA).ToList();

                apiURL = lstSetting.FirstOrDefault(x => x.Name == AppType.Setting.SAP_S4HAHA.ToString() + "_URL")?.Value;
                var apiUser = lstSetting.FirstOrDefault(x => x.Name == AppType.Setting.SAP_S4HAHA.ToString() + "_User")?.Value;
                var apiPassword = lstSetting.FirstOrDefault(x => x.Name == AppType.Setting.SAP_S4HAHA.ToString() + "_Password")?.Value;
                #endregion

                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                foreach (var header in headers)
                {

                    string requestData = string.Empty;
                    try
                    {
                        #region Get record Sale
                        var record = this.UnitOfWork.GetAllNoTracking<RecordRefund>().FirstOrDefault(x => x.HeaderId == header.Id && x.StoreCode == header.MallCode);
                        #endregion
                        var s4header = new S4Dto(header);
                        s4header.SetUUID(record.Id);
                        if (string.IsNullOrEmpty(s4header.extCustomerNo))
                        {
                            #region Update IsS4Transfrred = Null nếu gửi request lỗi
                            _refundBusiness.UpdateS4Transferred(header.MallCode, header.Id, false);
                            #endregion
                            errors.Add(header);
                            LogCallS4HAHAAPI(apiURL, (int)EventResult.Fail, "", "", header);
                            continue;
                        }

                        requestData = JsonConvert.SerializeObject(s4header);
                        string responseData = string.Empty;
                        using (HttpClient client = new HttpClient())
                        {
                            // Thiết lập Basic Authentication
                            var byteArray = Encoding.ASCII.GetBytes($"{apiUser}:{apiPassword}");
                            client.DefaultRequestHeaders.Authorization =
                                new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

                            // Thêm Header tùy chỉnh
                            client.DefaultRequestHeaders.Add("X-Requested-With", "X");

                            // Chuẩn bị nội dung gửi
                            var content = new StringContent(requestData, Encoding.UTF8, "application/json");

                            // Gửi yêu cầu POST
                            var res = client.PostAsync(apiURL, content).GetAwaiter();

                            HttpResponseMessage response = res.GetResult();
                            // Đọc phản hồi
                            var responseDataTask = response.Content.ReadAsStringAsync().GetAwaiter();
                            responseData = responseDataTask.GetResult();
                            try
                            {
                                XmlSerializer serializer = new XmlSerializer(typeof(Entry));
                                var headerResponse = S4Dto.Deserialize(responseData);
                                #region Đánh dấu đã gửi S4HANA
                                if (headerResponse.Status == AppType.S4HANAStatuses.Success)
                                {
                                    _refundBusiness.UpdateS4Transferred(header.MallCode, header.Id);
                                    LogCallS4HAHAAPI(apiURL, (int)EventResult.Success, requestData, responseData, header);
                                    rs++;
                                }
                                else
                                {
                                    #region Update IsS4Transfrred = Null nếu gửi request lỗi
                                    _refundBusiness.UpdateS4Transferred(header.MallCode, header.Id, false);
                                    #endregion
                                    errors.Add(header);
                                    LogCallS4HAHAAPI(apiURL, (int)EventResult.Fail, requestData, responseData, header);
                                }
                                #endregion
                            }
                            catch (Exception ex)
                            {
                                #region Update IsS4Transfrred = Null nếu gửi request lỗi
                                _refundBusiness.UpdateS4Transferred(header.MallCode, header.Id, false);
                                #endregion
                                errors.Add(header);
                                LogCallS4HAHAAPI(apiURL, (int)EventResult.Fail, requestData, responseData, header);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        errors.Add(header);
                        LogCallS4HAHAAPI(apiURL, (int)EventResult.Fail, "", ex.Message, header);
                        if (ex?.InnerException?.Message == "Unable to connect to the remote server")
                        {
                            throw (ex);
                        }
                        #region Update IsS4Transfrred = Null nếu gửi request lỗi
                        _refundBusiness.UpdateS4Transferred(header.MallCode, header.Id, false);
                        #endregion
                        LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
                    }
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }
            return rs;
        }

        [Obsolete]
        private void LogCallMonthlyMemberAPI(string source, int sucess, string responseData)
        {
            var rs = this.UnitOfWork.Insert(new SystemLog
            {
                LogId = Guid.NewGuid(),
                Module = AppModule.SAP_BTP_MonthlyMember.ToString(),
                UserId = "system",
                UserFunction = (int)AppSystemLog.Action.SendRequest,
                EventResult = sucess,
                Transdata = "",
                Source = source,
                FuncDateTime = DateTime.Now,
                WSName = ""
            });
            if (rs != null)
            {
                var filePath = ConfigurationSettings.AppSettings[TempFolder_Key] ?? TempFolder_Default;
                filePath += "\\SAP\\" + rs.LogId.ToString();
                if (!Directory.Exists(filePath))
                    Directory.CreateDirectory(filePath);
                var fileName = string.Concat(filePath, "\\response.json");
                if (File.Exists(fileName))
                    File.Delete(fileName);
                var byteRes = Encoding.Unicode.GetBytes(responseData);
                string res = Encoding.Unicode.GetString(byteRes);
                File.WriteAllText(fileName, res);
                byte[] fileContent = File.ReadAllBytes(fileName);
                var sysAtt = new SystemLogAttachment
                {
                    Id = Guid.NewGuid(),
                    LogId = rs.LogId,
                    Name = "response.json",
                    FileName = "response.json",
                    FileContent = fileContent,

                    URL = "",
                    CreateBy = "system",
                    CreateDate = DateTime.Now,
                    UpdateBy = "system",
                    UpdateDate = DateTime.Now,
                    ActiveFlag = STATUS_ACTIVE
                };
                sysAtt = this.UnitOfWork.Insert(sysAtt);
                if (sysAtt != null)
                {
                    File.Delete(fileName);
                    Directory.Delete(filePath);
                }
            }
        }

        [Obsolete]
        private void LogCallS4HAHAAPI(string source, int sucess, string requestData, string responseData, HeaderByStoreDto header)
        {
            var log = new SystemLog
            {
                LogId = Guid.NewGuid(),
                Module = AppModule.SAP_S4HAHA.ToString(),
                UserId = "system",
                UserFunction = (int)AppSystemLog.Action.SendRequest,
                EventResult = sucess,
                //Transdata = $"{header.Id}_{header.OrderNumber}_{header.Invoices[0].Number}_{header.SalesDate}",
                Source = source,
                FuncDateTime = DateTime.Now,
                WSName = ""
            };
            if (header.Invoices?.Count > 0)
                log.Transdata = $"{header.Id}_{header.OrderNumber}_{header.Invoices[0].Number}_{header.SalesDate}_{header.BillNumber}";
            else
                log.Transdata = $"{header.Id}_{header.OrderNumber}_{header.SalesDate}_{header.BillNumber}| Wrong format: miss Invoices";

            if (string.IsNullOrEmpty(header.CustomerID))
            {
                log.Transdata += " | Wrong format: miss CustomerID";
            }

            var rs = this.UnitOfWork.Insert(log);
            if (rs != null && !string.IsNullOrEmpty(requestData))
            {
                var folderPath = ConfigurationSettings.AppSettings[TempFolder_Key] ?? TempFolder_Default;
                folderPath += "\\SAPS4HANA\\" + GuidToUUID(header.Id);
                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);
                #region Lưu file request
                var fileName = string.Concat(folderPath, "\\sales_request.json");
                if (File.Exists(fileName))
                    File.Delete(fileName);
                var byteRes = Encoding.Unicode.GetBytes(requestData);
                string res = Encoding.Unicode.GetString(byteRes);
                File.WriteAllText(fileName, res);
                #endregion
                #region Lưu file response
                fileName = string.Concat(folderPath, "\\sales_response.xml");
                if (File.Exists(fileName))
                    File.Delete(fileName);
                byteRes = Encoding.Unicode.GetBytes(responseData);
                res = Encoding.Unicode.GetString(byteRes);
                File.WriteAllText(fileName, res);
                #endregion
                #region Zip
                var fileContent = FileUtility.ZipFolder(folderPath);
                #endregion

                var sysAtt = new SystemLogAttachment
                {
                    Id = Guid.NewGuid(),
                    LogId = rs.LogId,
                    Name = $"{header.Id}.zip",
                    FileName = $"{header.Id}.zip",
                    FileContent = fileContent,

                    URL = "",
                    CreateBy = "system",
                    CreateDate = DateTime.Now,
                    UpdateBy = "system",
                    UpdateDate = DateTime.Now,
                    ActiveFlag = STATUS_ACTIVE
                };
                sysAtt = this.UnitOfWork.Insert(sysAtt);
            }
        }
        [Obsolete]
        private void LogCallS4HAHAAPI(string source, int sucess, string requestData, string responseData, RefundDto.RefundHeaderDto header)
        {
            var log = new SystemLog
            {
                LogId = Guid.NewGuid(),
                Module = AppModule.SAP_S4HAHA.ToString(),
                UserId = "system",
                UserFunction = (int)AppSystemLog.Action.SendRequest,
                EventResult = sucess,
                //Transdata = $"{header.Id}_{header.OrderNumber}_{header.RefundInvoices[0].Number}_{header.RefundDate}",
                Source = source,
                FuncDateTime = DateTime.Now,
                WSName = ""
            };
            if (header.RefundInvoices?.Count > 0)
                log.Transdata = $"{header.Id}_{header.OrderNumber}_{header.RefundInvoices[0].Number}_{header.SalesDate}_{header.ReceiptNumber}";
            else
                log.Transdata = $"{header.Id}_{header.OrderNumber}_{header.RefundDate}_{header.ReceiptNumber}| Wrong format: miss Invoices";

            if (string.IsNullOrEmpty(header.CustomerID))
            {
                log.Transdata += " | Wrong format: miss CustomerID";
            }

            var rs = this.UnitOfWork.Insert(log);
            if (rs != null && !string.IsNullOrEmpty(requestData))
            {
                var folderPath = ConfigurationSettings.AppSettings[TempFolder_Key] ?? TempFolder_Default;
                folderPath += "\\SAPS4HANA\\" + GuidToUUID(header.Id);
                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);
                #region Lưu file request
                var fileName = string.Concat(folderPath, "\\refund_request.json");
                if (File.Exists(fileName))
                    File.Delete(fileName);
                var byteRes = Encoding.Unicode.GetBytes(requestData);
                string res = Encoding.Unicode.GetString(byteRes);
                File.WriteAllText(fileName, res);
                #endregion
                #region Lưu file response
                fileName = string.Concat(folderPath, "\\refund_response.xml");
                if (File.Exists(fileName))
                    File.Delete(fileName);
                byteRes = Encoding.Unicode.GetBytes(responseData);
                res = Encoding.Unicode.GetString(byteRes);
                File.WriteAllText(fileName, res);
                #endregion
                #region Zip
                var fileContent = FileUtility.ZipFolder(folderPath);
                #endregion

                var sysAtt = new SystemLogAttachment
                {
                    Id = Guid.NewGuid(),
                    LogId = rs.LogId,
                    Name = $"{header.Id}.zip",
                    FileName = $"{header.Id}.zip",
                    FileContent = fileContent,

                    URL = "",
                    CreateBy = "system",
                    CreateDate = DateTime.Now,
                    UpdateBy = "system",
                    UpdateDate = DateTime.Now,
                    ActiveFlag = STATUS_ACTIVE
                };
                sysAtt = this.UnitOfWork.Insert(sysAtt);
            }
        }

        public async Task<Tuple<int, List<MonthlyMemberSaleDto>>> GetPagingAsync(MonthlyMemberSaleFilterDto filter)
        {
            var query = this.UnitOfWork.GetAllNoTracking<MonthlyMemberSale>();
            if (!string.IsNullOrEmpty(filter.KeyWord))
            {
                query = query.Where(x => x.Membercode.Contains(filter.KeyWord)
                || x.Companycode.Contains(filter.KeyWord)
                || x.Memberlevel.Contains(filter.KeyWord)
                );
            }
            if (filter.FromDate.HasValue)
            {
                var year_month = string.Concat(filter.FromDate.Value.Year.ToString("D4"), filter.FromDate.Value.Month.ToString("D2"));
                query = query.Where(x => x.YearMonth.CompareTo(year_month) >= 0);
            }
            if (filter.ToDate.HasValue)
            {
                var year_month = string.Concat(filter.ToDate.Value.Year.ToString("D4"), filter.ToDate.Value.Month.ToString("D2"));
                query = query.Where(x => x.YearMonth.CompareTo(year_month) <= 0);
            }

            var count = await query.CountAsync();
            var result = await query.OrderByDescending(x => x.UpdateDate)
                                    .ThenByDescending(x => x.YearMonth)
                                    .Skip((filter.PageIndex - 1) * filter.PageSize)
                                    .Take(filter.PageSize)
                                    .ToListAsync();
            var mapped = Mapper.Map<List<MonthlyMemberSaleDto>>(result);
            var userNames = new HashSet<string>(mapped.Select(x => x.UpdateBy).Concat(mapped.Select(x => x.CreateBy)).Distinct().ToList());
            var userDictionary = this.UnitOfWork.GetAllNoTracking<UserInfo>()
                .Where(x => userNames.Contains(x.UserId))
                .ToDictionary(x => x.UserId, x => x.FullName);
            foreach (var item in mapped)
            {
                item.UpdateBy = userDictionary.TryGetValue(item.UpdateBy, out var fullName)
                                ? fullName
                                : item.UpdateBy;
                item.CreateBy = userDictionary.TryGetValue(item.CreateBy, out var CreateBy)
                ? CreateBy
                : item.CreateBy;
            }
            return new Tuple<int, List<MonthlyMemberSaleDto>>(count, mapped);
        }
    }
}
