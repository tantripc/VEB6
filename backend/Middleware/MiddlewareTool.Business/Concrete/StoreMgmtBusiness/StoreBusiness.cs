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
using System.Security.Cryptography;
using System.Threading.Tasks;
using static MiddlewareTool.Common.AppType;
using static MiddlewareTool.Dto.StoreMgmtDto;

namespace MiddlewareTool.Business.Concrete
{
    public class StoreBusiness : BaseBusiness, IStoreBusiness
    {
        public StoreBusiness(IUnitOfWork unitOfWork) : base(unitOfWork) { }
        public async Task<Tuple<int, List<StoreDto>>> GetPagingAsync(string mallCode, string cityCode, string districtCode, string wardCode, string userName, string keyWord, int? storeType, bool isAdmin, int pageIndex, int pageSize)
        {
            int total = 0;
            try
            {
                var iquery = this.UnitOfWork.GetAll<Store>().Where(x => x.ActiveFlag == STATUS_ACTIVE);
                var iqueryMall = this.UnitOfWork.GetAll<Mall>().Where(x => x.ActiveFlag == STATUS_ACTIVE);

                var join = from store in iquery
                           join mall in iqueryMall on store.MallCode.Trim() equals mall.Code.Trim() into rs1
                           from leftJoin in rs1.DefaultIfEmpty()
                           select new { store = store, mall = leftJoin };

                if (!string.IsNullOrEmpty(keyWord))
                {
                    var keyTrim = keyWord.Trim().ToLower();

                    join = join.Where(x => x.mall.Code.Trim().ToLower().Equals(keyTrim)
                        || x.mall.Name.ToLower().Contains(keyTrim)
                        || x.store.Code.ToLower().Contains(keyTrim)
                        || x.store.Name.ToLower().Contains(keyTrim)
                        );
                }

                if (!string.IsNullOrEmpty(mallCode))
                {
                    join = join.Where(x => x.mall.Code.Trim() == mallCode.Trim());
                }

                if (storeType.HasValue)
                {
                    join = join.Where(x => x.store.StoreType == storeType.Value);
                }
                if (!isAdmin)
                {
                    var userStores = this.UnitOfWork.GetAllNoTracking<UserStore>().Where(x => x.ActiveFlag == STATUS_ACTIVE && x.UserName == userName).Select(x => x.StoreCode).ToList();
                    if (!(userStores?.Count > 0))
                        userStores.Add("NotAdmin");
                    join = join.Where(x => userStores.Contains(x.store.Code));
                }

                total = join.Count();
                var data = await join.OrderBy(x => x.store.Code).ThenBy(x => x.store.Name)
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .Select(x => new StoreDto
                    {
                        Id = x.store.Id,
                        Code = x.store.Code,
                        Name = x.store.Name,
                        Description = x.store.Description,
                        CreateBy = x.store.CreateBy,
                        UpdateBy = x.store.UpdateBy,
                        CreateDate = x.store.CreateDate,
                        UpdateDate = x.store.UpdateDate,
                        MallCode = x.store.MallCode ?? string.Empty,
                        POSNumber1 = x.store.POSNumber1,
                        POSNumber2 = x.store.POSNumber2,
                        TaxName = x.store.TaxName,
                        MerchantTax = x.store.MerchantTax,
                        TaxAddress = x.store.TaxAddress,
                        MallName = x.mall.Name,
                        StoreType = x.store.StoreType,
                        ApplyPromotion = (bool)(x.store.ApplyPromotion == null ? false : x.store.ApplyPromotion),
                    })
                    .ToListAsync();
                return new Tuple<int, List<StoreDto>>(total, data);
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return new Tuple<int, List<StoreDto>>(total, new List<StoreDto>());
        }
        public async Task<List<StoreDto>> GetAllStoreMaster()
        {
            try
            {
                var iquery = await this.UnitOfWork.GetAllAsync<Store>(x => x.ActiveFlag == STATUS_ACTIVE);
                if (iquery != null)
                {
                    return Mapper.Map<List<StoreDto>>(iquery);
                }
            }
            catch (Exception ex) { LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return null;
        }
        public async Task<StoreDto> GetByIdAsync(Guid id)
        {
            try
            {
                var iquery = await this.UnitOfWork.GetSingleAsync<Store>(x => x.Id == id && x.ActiveFlag == STATUS_ACTIVE);
                if (iquery != null)
                {
                    return Mapper.Map<StoreDto>(iquery);
                }
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return null;
        }
        public async Task<StoreDto> GetAsync(string code)
        {
            try
            {
                var iquery = await this.UnitOfWork.GetSingleAsync<Store>(x => x.Code == code && x.ActiveFlag == STATUS_ACTIVE);
                if (iquery != null)
                {
                    return Mapper.Map<StoreDto>(iquery);
                }
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return null;
        }
        public StoreDto Get(string code)
        {
            try
            {
                var iquery = this.UnitOfWork.GetSingle<Store>(x => x.Code == code && x.ActiveFlag == STATUS_ACTIVE);
                if (iquery != null)
                {
                    return Mapper.Map<StoreDto>(iquery);
                }
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return null;
        }
        public List<StoreCompactDto> Get(List<string> codes)
        {
            try
            {
                return this.UnitOfWork.GetItems<StoreCompactDto, Store>(x => codes.Contains(x.Code) && x.ActiveFlag == STATUS_ACTIVE).ToList();
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return new List<StoreCompactDto>();
        }
        public async Task<Guid> InsertAsync(StoreDto dto)
        {
            Guid result = Guid.NewGuid();
            try
            {
                var mallId = this.UnitOfWork.GetSingle<Mall>(x => x.Code == dto.MallCode)?.Id;

                var entity = new Store()
                {
                    Id = Guid.NewGuid(),
                    MallCode = dto.MallCode,
                    MallId = mallId,
                    Code = dto.Code,
                    Name = dto.Name,
                    POSNumber1 = dto.POSNumber1,
                    POSNumber2 = dto.POSNumber2,
                    TaxName = dto.TaxName,
                    MerchantTax = dto.MerchantTax,
                    TaxAddress = dto.TaxAddress,
                    StoreType = dto.StoreType,
                    //AddressLine = dto.AddressLine,
                    //City = dto.City,
                    //District = dto.District,
                    //Ward = dto.Ward,
                    //MerchantId = dto.MerchantId,
                    //MerchantTax = dto.MerchantTax,
                    //Ranking = dto.Ranking,
                    URL = AppValue.ToUnsignString(dto.Description),
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    CreateBy = dto.CreateBy,
                    UpdateBy = dto.CreateBy,
                    ApplyPromotion = dto.ApplyPromotion,
                };

                var add = await this.UnitOfWork.InsertAsync(entity);
                if (add != null) { result = entity.Id; }
            }
            catch (Exception ex)
            {
                this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
            }
            return result;
        }

        public async Task<bool> InsertPromotionAsync(PromotionStoreDto dto)
        {
            bool result = false;
            try
            {
                var entity = new Promotion1()
                {
                    Id = Guid.NewGuid(),
                    StoreCode = dto.StoreCode,
                    CasePromotion = dto.CasePromotion,
                    PNLAllocation = dto.PNLAllocation,
                    TransactionType = dto.TransactionType,
                    ActiveFlag = 0
                };

                var add = await this.UnitOfWork.InsertAsync(entity);
                if (add != null) { result = true; }
            }
            catch (Exception ex)
            {
                this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
            }
            return result;
        }
        public async Task<bool> UpdateAsync(StoreDto dto)
        {
            bool result = false;
            try
            {
                var entity = this.UnitOfWork.GetSingle<Store>(x => x.Id.Equals(dto.Id));
                var mallId = this.UnitOfWork.GetSingle<Mall>(x => x.Code == dto.MallCode)?.Id;
                if (entity != null)
                {
                    entity.MallCode = dto.MallCode;
                    //entity.Code = dto.Code;
                    entity.Name = dto.Name;
                    entity.POSNumber1 = dto.POSNumber1;
                    entity.POSNumber2 = dto.POSNumber2;
                    entity.TaxName = dto.TaxName;
                    entity.MerchantTax = dto.MerchantTax;
                    entity.TaxAddress = dto.TaxAddress;
                    entity.StoreType = dto.StoreType;
                    entity.MallId = mallId;
                    //entity.AddressLine = dto.AddressLine;
                    //entity.City = dto.City;
                    //entity.District = dto.District;
                    //entity.Ward = dto.Ward;
                    //entity.MerchantId = dto.MerchantId;
                    //entity.MerchantTax = dto.MerchantTax;
                    //entity.Ranking = dto.Ranking;
                    entity.URL = AppValue.ToUnsignString(dto.Description);
                    entity.UpdateBy = dto.UpdateBy;
                    entity.UpdateDate = DateTime.Now;
                    entity.ApplyPromotion = dto.ApplyPromotion;
                    result = await this.UnitOfWork.UpdateAsync(entity);
                }
            }
            catch (Exception ex) { LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return result;
        }
        public async Task<bool> DeleteAsync(Guid id, string userName)
        {
            bool result = false;
            try
            {
                var entity = this.UnitOfWork.GetSingle<Store>(x => x.Id == id && x.ActiveFlag == STATUS_ACTIVE);
                if (entity != null)
                {
                    entity.UpdateDate = DateTime.Now;
                    entity.UpdateBy = userName;
                    result = await this.UnitOfWork.DeleteAsync(entity);
                    var rs = await this.UnitOfWork.GetAll<Promotion1>().Where(x => x.StoreCode == entity.Code).ToListAsync();
                    if (rs.Count > 0)
                    {
                        await this.UnitOfWork.DeleteToListAsync(rs);
                    }
                }
            }
            catch (Exception ex) { LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return result;
        }
        public async Task<bool> IsExistAsync(Guid id)
        {
            bool result = true;
            try
            {
                if (id != Guid.Empty)
                {
                    var iquery = await this.UnitOfWork.GetSingleAsync<Store>(x => x.Id == id);
                    if (iquery == null) { result = false; }
                }
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return result;
        }
        //public async Task<int> CheckRankingByMallId(Guid? mallId, int ranking)
        //{
        //    int result = 0;
        //    try
        //    {
        //        if (mallId != Guid.Empty)
        //        {
        //            result = await this.UnitOfWork.CountAsync<Store>(x => x.MallId == mallId && x.Ranking == ranking);
        //        }
        //    }
        //    catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
        //    return result;
        //}
        public async Task<int> IsExistStoreID(Guid? id, string mallCode, string code)
        {
            int duplicatedcode = 0;
            try
            {
                if (code != null)
                {
                    duplicatedcode = await this.UnitOfWork.CountAsync<Store>(x =>
                        (!id.HasValue || (id.HasValue && x.Id != id.Value))
                        && (x.Code == code && x.MallCode.Trim() == mallCode.Trim())
                    );
                }
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return duplicatedcode;
        }
        public List<StoreCreationDto> ExportStoreCreation(string keyword, string mallCode, int? storeType, bool isAdmin, string userName)
        {
            try
            {
                var mallQuery = this.UnitOfWork.GetAllNoTracking<Mall>().Where(x => x.ActiveFlag == (int)STATUS_ACTIVE
                    && (!string.IsNullOrEmpty(x.Code))
                    && (!string.IsNullOrEmpty(x.Name))
                    && (!string.IsNullOrEmpty(x.Phone))
                    && (!string.IsNullOrEmpty(x.Email))
                    && (!string.IsNullOrEmpty(x.AddressLine))
                    && (!string.IsNullOrEmpty(x.CityName))
                    && (!string.IsNullOrEmpty(x.DistrictName))
                    && (!string.IsNullOrEmpty(x.WardName))
                //&& (!string.IsNullOrEmpty(x.MerchantId))
                );

                if (!string.IsNullOrEmpty(keyword))
                {
                    var keyTrim = keyword.Trim().ToLower();
                    mallQuery = mallQuery.Where(x => x.Code.ToLower().Equals(keyTrim)
                        || x.Name.ToLower().Contains(keyTrim)
                        );
                }

                if (!string.IsNullOrEmpty(mallCode))
                {
                    var keyTrim = mallCode.Trim().ToLower();
                    mallQuery = mallQuery.Where(x => x.Code.Trim().ToLower().Equals(mallCode)
                        );
                }

                List<string> userStores = new List<string>();
                if (!isAdmin)
                {
                    userStores = this.UnitOfWork.GetAllNoTracking<UserStore>().Where(x => x.ActiveFlag == STATUS_ACTIVE && x.UserName == userName).Select(x => x.StoreCode).ToList();
                    if (!(userStores?.Count > 0))
                        userStores.Add("NotAdmin");
                }
                var mallData = mallQuery.ToList();
                var lstData = new List<StoreCreationDto>();

                mallData.ForEach(x =>
                {
                    var data = new StoreCreationDto()
                    {
                        MallCode = x.Code.Trim(),
                        MallName = x.Name,
                        MallPhone = x.Phone,
                        MallEmail = x.Email,
                        MallAddressLine = x.AddressLine,
                        MallCity = x.CityName,
                        MallDistrict = x.DistrictName,
                        MallWard = x.WardName,
                        MallMerchantId = x.MerchantId
                    };
                    var storeQuery = this.UnitOfWork.GetAllNoTracking<Store>().Where(s => s.ActiveFlag == (int)STATUS_ACTIVE);
                    if (!isAdmin)
                        storeQuery = storeQuery.Where(s => userStores.Contains(s.Code));
                    storeQuery = storeQuery.Where(s => s.MallCode.Trim() == x.Code.Trim()
                    && (!string.IsNullOrEmpty(s.Code))
                    && (!string.IsNullOrEmpty(s.Name))
                    && (!string.IsNullOrEmpty(s.TaxName))
                    && (!string.IsNullOrEmpty(s.TaxAddress))
                    && (!string.IsNullOrEmpty(s.MerchantTax))
                );

                    if (!string.IsNullOrEmpty(keyword))
                    {
                        var keyTrim = keyword.Trim().ToLower();
                        storeQuery = storeQuery.Where(s => s.Code.Trim().ToLower().Equals(keyTrim)
                            || s.Name.ToLower().Contains(keyTrim)
                            || s.TaxAddress.ToLower().Contains(keyTrim)
                            || s.TaxName.ToLower().Contains(keyTrim)
                            || s.MerchantTax.ToLower().Contains(keyTrim)
                            );
                    }

                    if (storeType.HasValue)
                    {
                        storeQuery = storeQuery.Where(s => s.StoreType == storeType.Value);
                    }

                    var stores = storeQuery
                    .GroupBy(s => s.Code)
                    .Select(s => s.FirstOrDefault())
                    .Take(4)
                    .ToList();

                    for (int i = 0; i < stores.Count; i++)
                    {
                        switch (i)
                        {
                            case 0:
                                data.StoreCode1 = stores[i].Code.Trim();
                                data.StoreName1 = stores[i].Name;
                                data.StoreTaxName1 = stores[i].TaxName;
                                data.StoreTaxAddress1 = stores[i].TaxAddress;
                                data.StoreMerchantTaxId1 = stores[i].MerchantTax;
                                break;
                            case 1:
                                data.StoreCode2 = stores[i].Code.Trim();
                                data.StoreName2 = stores[i].Name;
                                data.StoreTaxName2 = stores[i].TaxName;
                                data.StoreTaxAddress2 = stores[i].TaxAddress;
                                data.StoreMerchantTaxId2 = stores[i].MerchantTax;
                                break;
                            case 2:
                                data.StoreCode3 = stores[i].Code.Trim();
                                data.StoreName3 = stores[i].Name;
                                data.StoreTaxName3 = stores[i].TaxName;
                                data.StoreTaxAddress3 = stores[i].TaxAddress;
                                data.StoreMerchantTaxId3 = stores[i].MerchantTax;
                                break;
                            case 3:
                                data.StoreCode4 = stores[i].Code.Trim();
                                data.StoreName4 = stores[i].Name;
                                data.StoreTaxName4 = stores[i].TaxName;
                                data.StoreTaxAddress4 = stores[i].TaxAddress;
                                data.StoreMerchantTaxId4 = stores[i].MerchantTax;
                                break;
                        }

                    }
                    if (stores.Count > 0)
                        lstData.Add(data);
                });

                return lstData;
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return new List<StoreCreationDto>();
        }

        public List<StoreCompactDto> GetAllStoreActive()
        {
            try
            {
                var iquery = this.UnitOfWork.GetAll<Store>().Where(x => x.ActiveFlag == STATUS_ACTIVE);
                var iqueryMall = this.UnitOfWork.GetAll<Mall>().Where(x => x.ActiveFlag == STATUS_ACTIVE);

                var query = from store in iquery
                            join mall in iqueryMall on store.MallCode.Trim() equals mall.Code.Trim()
                            into rs1
                            from leftJoin in rs1.DefaultIfEmpty()
                            where (store.ActiveFlag == STATUS_ACTIVE && leftJoin.ActiveFlag == STATUS_ACTIVE && !string.IsNullOrEmpty(store.MerchantTax))
                            select new StoreCompactDto { Code = store.Code, Name = store.Name, MallName = leftJoin.Name };
                return query.OrderBy(x => x.Code).ToList();
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return new List<StoreCompactDto>();
        }
        public bool CheckStoreValid(string storeCode)
        {
            try
            {
                if (!string.IsNullOrEmpty(storeCode))
                {
                    var iquery = this.UnitOfWork.GetSingle<Store>(x => x.Code.Trim() == storeCode.Trim() && !string.IsNullOrEmpty(x.MerchantTax) && x.MallId.HasValue && x.ActiveFlag == STATUS_ACTIVE);
                    if (iquery != null)
                    {
                        var iqueryMall = this.UnitOfWork.GetSingle<Mall>(m => m.Id == iquery.MallId.Value && m.ActiveFlag == STATUS_ACTIVE);
                        if (iqueryMall != null)
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return false;
        }

        public List<StoreDto> GetValidStores()
        {
            var dto = this.UnitOfWork.GetAll<Store>()
                .Where(x => !string.IsNullOrEmpty(x.MerchantTax)
                            && x.MallId.HasValue
                            && x.ActiveFlag == STATUS_ACTIVE)
                .Join(
                    this.UnitOfWork.GetAll<Mall>().Where(m => m.ActiveFlag == STATUS_ACTIVE),
                    store => store.MallId,
                    mall => mall.Id,
                    (store, mall) => store
                )
                .ToList();

            return Mapper.Map<List<StoreDto>>(dto);
        }
        public async Task<List<StoreDto>> GetStoreB2BAsync(bool isAdmin, string userName)
        {
            try
            {
                var iquery = this.UnitOfWork.GetAllNoTracking<Store>().Where(x => x.ActiveFlag == STATUS_ACTIVE && (x.StoreType == (int)StoreTypes.B2B || x.StoreType == (int)StoreTypes.All));
                if (!isAdmin)
                {
                    List<string> userStores = new List<string>();
                    if (!isAdmin)
                    {
                        userStores = this.UnitOfWork.GetAllNoTracking<UserStore>().Where(x => x.ActiveFlag == STATUS_ACTIVE && x.UserName == userName).Select(x => x.StoreCode).ToList();
                        if (!(userStores?.Count > 0))
                            userStores.Add("NotAdmin");
                    }
                    iquery = iquery.Where(x => userStores.Contains(x.Code));
                }
                if (iquery != null)
                {
                    var dtos = await iquery.ToListAsync();
                    return Mapper.Map<List<StoreDto>>(dtos);
                }
            }
            catch (Exception ex) { LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return null;
        }
        public async Task<List<StoreDto>> GetStoreB2CAsync(bool isAdmin, string userName)
        {
            try
            {
                var iquery = this.UnitOfWork.GetAllNoTracking<Store>().Where(x => x.ActiveFlag == STATUS_ACTIVE && (x.StoreType == (int)StoreTypes.Boxed || x.StoreType == (int)StoreTypes.All));
                if (!isAdmin)
                {
                    List<string> userStores = new List<string>();
                    if (!isAdmin)
                    {
                        userStores = this.UnitOfWork.GetAllNoTracking<UserStore>().Where(x => x.ActiveFlag == STATUS_ACTIVE && x.UserName == userName).Select(x => x.StoreCode).ToList();
                        if (!(userStores?.Count > 0))
                            userStores.Add("NotAdmin");
                    }
                    iquery = iquery.Where(x => userStores.Contains(x.Code));
                }
                if (iquery != null)
                {
                    var dtos = await iquery.OrderBy(x => x.Code).ToListAsync();
                    return Mapper.Map<List<StoreDto>>(dtos);
                }
            }
            catch (Exception ex) { LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return null;
        }
        public async Task<Tuple<int, List<StoreDto>>> GetPagingSearchStorePopupAsync(string mallCode, string cityCode, string districtCode, string wardCode, string userName, string keyWord, int? storeType)
        {
            int total = 0;
            try
            {
                var iquery = this.UnitOfWork.GetAll<Store>().Where(x => x.ActiveFlag == STATUS_ACTIVE);
                var iqueryMall = this.UnitOfWork.GetAll<Mall>().Where(x => x.ActiveFlag == STATUS_ACTIVE);

                var join = from store in iquery
                           join mall in iqueryMall on store.MallCode.Trim() equals mall.Code.Trim() into rs1
                           from leftJoin in rs1.DefaultIfEmpty()
                           select new { store = store, mall = leftJoin };

                if (!string.IsNullOrEmpty(keyWord))
                {
                    var keyTrim = keyWord.Trim().ToLower();

                    join = join.Where(x => x.mall.Code.Trim().ToLower().Equals(keyTrim)
                        || x.mall.Name.ToLower().Contains(keyTrim)
                        || x.store.Code.ToLower().Contains(keyTrim)
                        || x.store.Name.ToLower().Contains(keyTrim)
                        );
                }

                if (!string.IsNullOrEmpty(mallCode))
                {
                    join = join.Where(x => x.mall.Code.Trim() == mallCode.Trim());
                }

                if (storeType.HasValue)
                {
                    join = join.Where(x => x.store.StoreType == storeType.Value);
                }
                total = join.Count();
                var data = await join.OrderBy(x => x.store.Code).ThenBy(x => x.store.Name)

                    .Select(x => new StoreDto
                    {
                        Id = x.store.Id,
                        Code = x.store.Code,
                        Name = x.store.Name,
                        Description = x.store.Description,
                        CreateBy = x.store.CreateBy,
                        UpdateBy = x.store.UpdateBy,
                        CreateDate = x.store.CreateDate,
                        UpdateDate = x.store.UpdateDate,
                        MallCode = x.store.MallCode ?? string.Empty,
                        POSNumber1 = x.store.POSNumber1,
                        POSNumber2 = x.store.POSNumber2,
                        TaxName = x.store.TaxName,
                        MerchantTax = x.store.MerchantTax,
                        TaxAddress = x.store.TaxAddress,
                        MallName = x.mall.Name,
                        StoreType = x.store.StoreType
                    })
                    .ToListAsync();
                return new Tuple<int, List<StoreDto>>(total, data);
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return new Tuple<int, List<StoreDto>>(total, new List<StoreDto>());
        }

        public async Task<List<PromotionStoreDto>> GetPromotionByStoreCodeAsync(string storeCode)
        {
            try
            {
                var iquery = await this.UnitOfWork.GetAll<Promotion1>().Where(x => x.StoreCode == storeCode).ToListAsync();
                if (iquery != null)
                {
                    return Mapper.Map<List<PromotionStoreDto>>(iquery);
                }
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return null;
        }
        public List<PromotionStoreDto> GetPromotionByStoreCode(string storeCode)
        {
            try
            {
                var iquery = this.UnitOfWork.GetAll<Promotion1>().Where(x => x.StoreCode == storeCode).ToList();
                if (iquery != null)
                {
                    return Mapper.Map<List<PromotionStoreDto>>(iquery);
                }
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return null;
        }

        public async Task<bool> UpdatePromotionAsync(PromotionStoreDto dto)
        {
            bool result = false;
            try
            {
                var entity = this.UnitOfWork.GetSingle<Promotion1>(x => x.StoreCode.Equals(dto.StoreCode) && x.CasePromotion.Equals(dto.CasePromotion));
                if (entity != null)
                {
                    entity.PNLAllocation = dto.PNLAllocation;
                    entity.TransactionType = dto.TransactionType;
                    result = await this.UnitOfWork.UpdateAsync(entity);
                }
                else
                {
                    result = await InsertPromotionAsync(dto);
                }
            }
            catch (Exception ex) { LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return result;
        }

        public List<string> GetListValuePaymentType(PaymentTypeScopes paymentTypeScopes)
        {
            try
            {
                var iquery = this.UnitOfWork.GetAll<Entities.PaymentType>().Where(s => s.Scope == (int)paymentTypeScopes || s.Scope == null || s.Scope == (int)PaymentTypeScopes.All);
                if (iquery != null)
                {
                    return iquery.Select(s => s.Type).ToList();
                }
            }
            catch (Exception ex) { LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return new List<string>();
        }
        public List<string> GetListValueDeliveryCode()
        {
            try
            {
                var iquery = this.UnitOfWork.GetAll<Entities.DeliveryCode>();
                if (iquery != null)
                {
                    return iquery.Select(s => s.Code).ToList();
                }
            }
            catch (Exception ex) { LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return new List<string>();
        }
    }
}
