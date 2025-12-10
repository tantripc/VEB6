using AutoMapper;
using MiddlewareTool.Business.Interface;
using MiddlewareTool.Common;
using MiddlewareTool.Dto;
using MiddlewareTool.Entities;
using MiddlewareTool.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using static MiddlewareTool.Common.AppType;
using static MiddlewareTool.Dto.UserMgmtDto;

namespace MiddlewareTool.Business.Concrete
{
    public class PaymentTypeMappingBusiness : BaseBusiness, IPaymentTypeMappingBusiness
    {
        public PaymentTypeMappingBusiness(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public PaymentTypeMappingDto Get(Guid id)
        {
            try
            {
                var iquery = this.UnitOfWork.GetAllNoTracking<PaymentTypeMapping>()
                    .Where(x => x.Id == id && x.ActiveFlag != STATUS_DELETE).FirstOrDefault();

                var item = Mapper.Map<PaymentTypeMappingDto>(iquery);
                if (item != null)
                {
                    var lstScope = GetScopeSettings();
                    var lstCustomerType = GetCustomerTypeSettings();
                    var lstMethod = GetMethodSettings();

                    if (item.Scope.HasValue)
                        item.ScopeName = lstScope.FirstOrDefault(x => x.Value == ((byte)item.Scope).ToString())?.Name;
                    if (!string.IsNullOrEmpty(item.CustomerType))
                        item.CustomerTypeName = lstCustomerType.FirstOrDefault(x => x.Value == item.CustomerType)?.Name;
                    if (item.Method.HasValue)
                        item.MethodName = lstMethod.FirstOrDefault(x => x.Value == ((byte)item.Method.Value).ToString())?.Name;

                    return item;
                }
            }
            catch (Exception ex)
            {
                LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
            }
            return null;
        }

        public Tuple<int, List<PaymentTypeMappingDto>> GetPaging(PaymentTypeMappingFilterDto filter)
        {
            List<PaymentTypeMappingDto> dtos = new List<PaymentTypeMappingDto>();
            int totalItem = 0;
            try
            {
                var iquery = this.UnitOfWork.GetAllNoTracking<PaymentTypeMapping>()
                    .Where(x => x.ActiveFlag != STATUS_DELETE);

                if (!string.IsNullOrEmpty(filter.Keyword))
                {
                    var keyword = filter.Keyword.Trim();
                    iquery = iquery.Where(x => x.Type.Contains(keyword)
                    || x.Description.Contains(keyword)
                    || x.DeliveryCode.Contains(keyword)
                    || x.PaymentCodeOutput.Contains(keyword)
                    || x.SaleToRefund.Contains(keyword)
                    );
                }
                if (!string.IsNullOrEmpty(filter.CustomerType))
                {
                    var customerType = filter.CustomerType.Trim();
                    iquery = iquery.Where(x => x.CustomerType == customerType);
                }
                if (filter.IsMapping.HasValue)
                {
                    iquery = iquery.Where(x => x.IsMapping == filter.IsMapping.Value);
                }
                if (filter.AllowRefund.HasValue)
                {
                    iquery = iquery.Where(x => x.AllowRefund == filter.AllowRefund.Value);
                }

                totalItem = iquery.Count();
                var entities = iquery
                    .OrderByDescending(x => x.UpdateDate)
                    .Skip((filter.PageIndex - 1) * filter.PageSize)
                    .Take(filter.PageSize)
                    .ToList();
                dtos = Mapper.Map<List<PaymentTypeMappingDto>>(entities);
                if (dtos != null)
                {
                    var lstUserUpdateByIds = dtos.Select(x => x.UpdateBy).ToList();
                    var lstUpdateBys = this.UnitOfWork.GetAll<UserInfo>()
                                    .Where(x => lstUserUpdateByIds.Contains(x.UserId) && x.ActiveFlag == STATUS_ACTIVE)
                                    .Select(x => new UsersDto()
                                    {
                                        Id = x.Id,
                                        UserName = x.UserId,
                                        Email = x.Email,
                                        FullName = x.FullName
                                    })
                                    .ToList();

                    var lstScope = GetScopeSettings();
                    var lstCustomerType = GetCustomerTypeSettings();
                    var lstMethod = GetMethodSettings();

                    foreach (var item in dtos)
                    {
                        item.UpdateByFullName = lstUpdateBys.Where(x => x.UserName.ToLower().Equals(item.UpdateBy.ToLower()))
                       .Select(x => x.FullName)
                       .FirstOrDefault();

                        if (item.Scope.HasValue)
                            item.ScopeName = lstScope.FirstOrDefault(x => x.Value == ((byte)item.Scope).ToString())?.Name;
                        if (!string.IsNullOrEmpty(item.CustomerType))
                            item.CustomerTypeName = lstCustomerType.FirstOrDefault(x => x.Value == item.CustomerType)?.Name;
                        if (item.Method.HasValue)
                            item.MethodName = lstMethod.FirstOrDefault(x => x.Value == ((byte)item.Method.Value).ToString())?.Name;
                    }
                }
            }
            catch (Exception ex)
            {
                LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
            }
            return new Tuple<int, List<PaymentTypeMappingDto>>(totalItem, dtos);
        }
        private bool CheckExist(PaymentTypeMappingDto dto)
        {
            var result = new List<PaymentTypeMapping>();
            // Kiểm tra trùng mã Type
            var checkExist = UnitOfWork.GetAllNoTracking<PaymentTypeMapping>()
                .Where(x => x.Id != dto.Id
                    && x.Type == dto.Type
                    && (x.Scope == dto.Scope || dto.Scope == null || x.Scope == null)
                    && (x.CustomerType == dto.CustomerType || dto.CustomerType == null || x.CustomerType == null)
                    && x.ActiveFlag == STATUS_ACTIVE
                    ).ToList();
            if (dto.IsMapping)
            {
                var deliveryCodes = (dto.DeliveryCode ?? "")
    .Split(';', (char)StringSplitOptions.RemoveEmptyEntries)
    .Select(x => x.Trim())
    .Where(x => !string.IsNullOrEmpty(x))
    .ToArray();

                checkExist.ForEach(x =>
                {
                    var dbCodes = (x.DeliveryCode ?? "")
                        .Split(';', (char)StringSplitOptions.RemoveEmptyEntries)
                        .Select(c => c.Trim());

                    // Kiểm tra có ít nhất 1 phần tử giao nhau
                    if (x.IsMapping
                        && dbCodes.Any(c => deliveryCodes.Contains(c)))
                    {

                        result.Add(x);
                    }
                });

            }
            else
            {
                result = checkExist.Where(x => !x.IsMapping
                ).ToList();
            }
            return result.Any();
        }
        public PaymentTypeMappingDto Insert(PaymentTypeMappingDto dto)
        {
            try
            {
                var checkExist = CheckExist(dto);
                if (!checkExist)
                {
                    var entity = Mapper.Map<PaymentTypeMapping>(dto);

                    entity = UnitOfWork.Insert(entity);
                    dto = Mapper.Map<PaymentTypeMappingDto>(entity);
                    var lstScope = GetScopeSettings();
                    var lstCustomerType = GetCustomerTypeSettings();
                    var lstMethod = GetMethodSettings();

                    if (dto.Scope.HasValue)
                        dto.ScopeName = lstScope.FirstOrDefault(x => x.Value == ((byte)dto.Scope).ToString())?.Name;
                    if (!string.IsNullOrEmpty(dto.CustomerType))
                        dto.CustomerTypeName = lstCustomerType.FirstOrDefault(x => x.Value == dto.CustomerType)?.Name;
                    if (dto.Method.HasValue)
                        dto.MethodName = lstMethod.FirstOrDefault(x => x.Value == ((byte)dto.Method.Value).ToString())?.Name;
                    return dto;
                }
                else
                {
                    dto.URL = "PaymentType is exists";
                    return dto;
                }
            }
            catch (Exception ex)
            {
                LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
            }
            return null;
        }

        public PaymentTypeMappingDto Update(PaymentTypeMappingDto dto)
        {
            try
            {
                var entity = UnitOfWork.GetAll<PaymentTypeMapping>(x => x.Id == dto.Id && x.ActiveFlag == STATUS_ACTIVE).FirstOrDefault();
                if (entity == null)
                {
                    dto.URL = "PaymentType is not exists";
                    return dto;
                }
                // Kiểm tra trùng mã Type
                var checkExist = CheckExist(dto);
                if (!checkExist)
                {
                    entity.Type = dto.Type;
                    entity.Description = dto.Description;
                    entity.Scope = dto.Scope;
                    entity.CustomerType = dto.CustomerType;
                    entity.IsMapping = dto.IsMapping;
                    entity.DeliveryCode = dto.DeliveryCode;
                    entity.PaymentCodeOutput = dto.PaymentCodeOutput;
                    entity.Method = dto.Method;
                    entity.AllowRefund = dto.AllowRefund;
                    entity.SaleToRefund = dto.SaleToRefund;

                    entity.UpdateBy = dto.UpdateBy;
                    entity.UpdateDate = dto.UpdateDate;

                    var rs = UnitOfWork.Update(entity);
                    if (rs)
                    {
                        var lstScope = GetScopeSettings();
                        var lstCustomerType = GetCustomerTypeSettings();
                        var lstMethod = GetMethodSettings();

                        if (dto.Scope.HasValue)
                            dto.ScopeName = lstScope.FirstOrDefault(x => x.Value == ((byte)dto.Scope).ToString())?.Name;
                        if (!string.IsNullOrEmpty(dto.CustomerType))
                            dto.CustomerTypeName = lstCustomerType.FirstOrDefault(x => x.Value == dto.CustomerType)?.Name;
                        if (dto.Method.HasValue)
                            dto.MethodName = lstMethod.FirstOrDefault(x => x.Value == ((byte)dto.Method.Value).ToString())?.Name;
                        return dto;
                    }
                    else
                    {
                        dto.URL = "Error when saving";
                        return dto;
                    }
                }
                else
                {
                    dto.URL = "PaymentType is exists";
                    return dto;
                }
            }
            catch (Exception ex)
            {
                LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
            }
            return null;
        }
        public Tuple<string, bool> Delete(PaymentTypeMappingDto dto)
        {
            var rs = new Tuple<string, bool>("", false);
            try
            {
                var entity = UnitOfWork.GetAll<PaymentTypeMapping>(x => x.Id == dto.Id && x.ActiveFlag == STATUS_ACTIVE).FirstOrDefault();
                if (entity == null)
                {
                    rs = new Tuple<string, bool>("PaymentType is not exists", false);
                    return rs;
                }

                entity.ActiveFlag = (byte)dto.ActiveFlag;
                entity.UpdateDate = dto.UpdateDate;
                entity.UpdateBy = dto.UpdateBy;

                var iquery = UnitOfWork.Update(entity);
                rs = new Tuple<string, bool>("", true);
            }
            catch (Exception ex)
            {
                LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
                rs = new Tuple<string, bool>(ex.Message, false);
            }
            return rs;
        }

        public List<SelectSetting> GetScopeSettings()
        {
            var lstSetting = new List<SelectSetting>();
            try
            {
                var setting = this.UnitOfWork.GetAllNoTracking<SystemSetting>().Where(x => x.Code == "PaymentScopeType").FirstOrDefault();
                if (setting?.Value != null)
                {
                    var lst = setting.Value.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList().Select(x => x.Trim()).ToList();
                    lstSetting = new List<SelectSetting>();
                    foreach (var item in lst)
                    {
                        var obj = item.Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries);

                        lstSetting.Add(new SelectSetting { Value = obj[0], Name = obj[1] });
                    }
                }
            }
            catch (Exception ex)
            {
                LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
            }
            return lstSetting;
        }

        public List<SelectSetting> GetCustomerTypeSettings()
        {
            var lstSetting = new List<SelectSetting>();
            try
            {
                var setting = this.UnitOfWork.GetAllNoTracking<SystemSetting>().Where(x => x.Code == "PaymentCustomerType").FirstOrDefault();
                if (setting?.Value != null)
                {
                    var lst = setting.Value.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList().Select(x => x.Trim()).ToList();
                    lstSetting = new List<SelectSetting>();
                    foreach (var item in lst)
                    {
                        var obj = item.Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries);

                        lstSetting.Add(new SelectSetting { Value = obj[0], Name = obj[1] });
                    }
                }
            }
            catch (Exception ex)
            {
                LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
            }
            return lstSetting;
        }

        public List<SelectSetting> GetMethodSettings()
        {
            var lstSetting = new List<SelectSetting>();
            try
            {
                var setting = this.UnitOfWork.GetAllNoTracking<SystemSetting>().Where(x => x.Code == "PaymentMethod").FirstOrDefault();
                if (setting?.Value != null)
                {
                    var lst = setting.Value.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList().Select(x => x.Trim()).ToList();
                    lstSetting = new List<SelectSetting>();
                    foreach (var item in lst)
                    {
                        var obj = item.Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries);

                        lstSetting.Add(new SelectSetting { Value = obj[0], Name = obj[1] });
                    }
                }
            }
            catch (Exception ex)
            {
                LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
            }
            return lstSetting;
        }

        public List<PaymentTypeMappingDto> GetForSale()
        {
            List<PaymentTypeMappingDto> dtos = new List<PaymentTypeMappingDto>();
            try
            {
                var iquery = this.UnitOfWork.GetAllNoTracking<PaymentTypeMapping>()
                    .Where(x => x.ActiveFlag != STATUS_DELETE
                    && (x.Scope == (byte)PaymentTypeScopes.RecordSale || !x.Scope.HasValue)
                    );

                var entities = iquery
                    .ToList();
                dtos = Mapper.Map<List<PaymentTypeMappingDto>>(entities);
            }
            catch (Exception ex)
            {
                LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
            }
            return dtos;
        }
        public List<PaymentTypeMappingDto> GetForRefund()
        {
            List<PaymentTypeMappingDto> dtos = new List<PaymentTypeMappingDto>();
            try
            {
                var iquery = this.UnitOfWork.GetAllNoTracking<PaymentTypeMapping>()
                    .Where(x => x.ActiveFlag != STATUS_DELETE
                    && (x.Scope == (byte)PaymentTypeScopes.RecordRefund || !x.Scope.HasValue)
                    );

                var entities = iquery
                    .ToList();
                dtos = Mapper.Map<List<PaymentTypeMappingDto>>(entities);
            }
            catch (Exception ex)
            {
                LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
            }
            return dtos;
        }
    }
}
