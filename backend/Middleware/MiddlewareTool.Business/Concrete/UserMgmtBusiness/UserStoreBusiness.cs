using MiddlewareTool.Business.Interface;
using MiddlewareTool.Entities;
using MiddlewareTool.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using static MiddlewareTool.Dto.StoreMgmtDto;

namespace MiddlewareTool.Business.Concrete
{
    public class UserStoreBusiness : BaseBusiness, IUserStoreBusiness
    {
        public UserStoreBusiness(IUnitOfWork unitOfWork) : base(unitOfWork) { }
        public async Task<List<StoreDto>> GetListByUserId(Guid userId)
        {
            try
            {
                return await this.UnitOfWork.GetAll<UserStore>()
                    .Where(x => x.UserId == userId && x.ActiveFlag == STATUS_ACTIVE)
                    .Join(this.UnitOfWork.GetAll<Store>().Where(x => x.ActiveFlag == STATUS_ACTIVE),
                        ud => ud.StoreId,
                        d => d.Id,
                        (ud, d) => new { ud, d })
                    .OrderBy(x => x.d.Code)
                    .Select(x => new StoreDto
                    {
                        Id = x.d.Id,
                        Code = x.d.Code,
                        Name = x.d.Name,
                        Description = x.d.Description,
                        CreateBy = x.d.CreateBy,
                        UpdateBy = x.d.UpdateBy,
                        CreateDate = x.d.CreateDate,
                        UpdateDate = x.d.UpdateDate,
                        MallCode = x.d.MallCode ?? string.Empty,
                        POSNumber1 = x.d.POSNumber1,
                        POSNumber2 = x.d.POSNumber2,
                        TaxName = x.d.TaxName,
                        MerchantTax = x.d.MerchantTax,
                        TaxAddress = x.d.TaxAddress,
                        MallName = x.d.Name,
                        StoreType = x.d.StoreType
                    })
                    .ToListAsync();
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return new List<StoreDto>();
        }
        public List<StoreCompactDto> GetListByUserName(string userName)
        {
            try
            {
                return this.UnitOfWork.GetAll<UserStore>()
                    .Where(x => x.UserName == userName && x.ActiveFlag == STATUS_ACTIVE)
                    .Join(this.UnitOfWork.GetAll<Store>().Where(x => x.ActiveFlag == STATUS_ACTIVE),
                        ud => ud.StoreId,
                        d => d.Id,
                        (ud, d) => new { ud, d })
                    .OrderBy(x => x.d.Id)
                    .Select(x => new StoreCompactDto
                    {
                        Code = x.d.Code,
                        Name = x.d.Name,
                    })
                    .ToList();
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return new List<StoreCompactDto>();
        }
        public async Task<bool> InsertOrDeleteAsync(Guid userId, string userName, List<string> lstStoreId)
        {
            bool result = false;
            try
            {
                result = await this.DeleteAsync(userId);
                if (result && lstStoreId != null) { result = await this.InsertAsync(userId, userName, lstStoreId); }
            }
            catch (Exception ex)
            {
                result = false;
                this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
            }
            return result;
        }
        public async Task<bool> InsertAsync(Guid userId, string userName, List<string> lstStoreId)
        {
            bool result = false;
            try
            {
                var lstEntity = new List<UserStore>();
                foreach (var item in lstStoreId)
                {
                    Guid storeId = Guid.Parse(item);
                    var store = this.UnitOfWork.GetAll<Store>().Where(x => x.Id == storeId).FirstOrDefault();
                    if (store != null)
                    {
                        lstEntity.Add(new UserStore
                        {
                            Id = Guid.NewGuid(),
                            UserId = userId,
                            UserName = userName,
                            StoreId = storeId,
                            StoreCode = store.Code,
                            StoreName = store.Name,
                            CreateDate = DateTime.Now,
                            UpdateDate = DateTime.Now
                        });
                    }
                }
                var add = await this.UnitOfWork.InsertToListAsync(lstEntity);
                if (add != null) { result = true; }
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return result;
        }
        public async Task<bool> DeleteAsync(Guid userId)
        {
            bool result = false;
            try
            {
                var lstDel = this.UnitOfWork.GetAll<UserStore>()
                    .Where(x => x.UserId == userId && x.ActiveFlag == STATUS_ACTIVE)
                    .ToList();
                if (lstDel.Count > 0)
                {
                    result = await this.UnitOfWork.DeleteToListAsync(lstDel, true);
                }
                else { result = true; }
            }
            catch (Exception ex) { LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return result;
        }
    }
}
