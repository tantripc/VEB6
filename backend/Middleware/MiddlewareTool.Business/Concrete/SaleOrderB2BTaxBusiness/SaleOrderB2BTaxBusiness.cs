using AutoMapper;
using MiddlewareTool.Business.Interface;
using MiddlewareTool.Common;
using MiddlewareTool.Dto;
using MiddlewareTool.Entities;
using MiddlewareTool.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using static MiddlewareTool.Common.AppValue;
using static MiddlewareTool.Dto.UserMgmtDto;

namespace MiddlewareTool.Business.Concrete
{
    public class SaleOrderB2BTaxBusiness : BaseBusiness, ISaleOrderB2BTaxBusiness
    {
        public SaleOrderB2BTaxBusiness(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<bool> DeleteAsync(B2BTaxDto dto)
        {
            var result = true;
            var msg = "";
            var _appEventResult = AppSystemLog.EventResult.Fail;
            string _transData = $"ERROR! Don't Delete B2BTaxCode SKU {dto.SKU} by user: {dto.UpdateBy}.";
            using (var trans = this.UnitOfWork.BeginTransaction())
            {
                try
                {

                    var entity = await this.UnitOfWork.GetAllNoTracking<B2BTax>()
                    .FirstOrDefaultAsync(x => x.Id == dto.Id && x.ActiveFlag != STATUS_DELETE);

                    if (entity.ActiveFlag == (byte)ActiveFlag.Active || entity.ActiveFlag == (byte)ActiveFlag.Deactive)
                    {
                        entity.UpdateBy = dto.UpdateBy;
                        entity.UpdateDate = DateTime.Now;
                        entity.ActiveFlag = (byte)ActiveFlag.Delete;
                        result = await this.UnitOfWork.UpdateAsync(entity);
                        if (result)
                        {
                            dto = Mapper.Map<B2BTaxDto>(entity);
                            _appEventResult = AppSystemLog.EventResult.Success;
                            _transData = $"SUCCESSFUL! Delete B2BTaxCode SKU {dto.SKU} by user: {dto.UpdateBy}."; ;
                            trans.Commit();
                        }
                        else
                            this.UnitOfWork.Rollback(trans);
                    }
                }

                catch (Exception ex)
                {
                    this.UnitOfWork.Rollback(trans);
                    result = false;
                    LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
                    _transData += " " + ex.StackTrace + "---" + ex.Message;
                }
            }
            dto.Comment = _transData;
            InsertHistory(dto, (int)AppSystemLog.Action.Delete, (int)_appEventResult);
            return result;
        }

        public async Task<B2BTaxDto> GetAsync(Guid id)
        {
            try
            {
                var entity = await this.UnitOfWork.GetAllNoTracking<B2BTax>()
                    .FirstOrDefaultAsync(x => x.Id == id && x.ActiveFlag != STATUS_DELETE);
                if (entity != null)
                {
                    var product = this.UnitOfWork.GetAllNoTracking<Product>()
                    .FirstOrDefault(x => x.Sku == entity.SKU && x.ActiveFlag == STATUS_ACTIVE);
                    if (product != null)
                    {
                        entity.ProductName = product.Name;
                        entity.TaxCode_Normal = product.TaxRate;
                    }
                }
                return Mapper.Map<B2BTaxDto>(entity);
            }
            catch (Exception ex)
            {
                LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
            }
            return null;
        }

        public async Task<B2BTaxDto> GetBySKUAsync(string sku)
        {
            try
            {
                var entity = await this.UnitOfWork.GetAllNoTracking<B2BTax>()
                    .FirstOrDefaultAsync(x => x.SKU == sku && x.ActiveFlag != STATUS_DELETE);
                return Mapper.Map<B2BTaxDto>(entity);
            }
            catch (Exception ex)
            {
                LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
            }
            return null;
        }


        public async Task<Tuple<int, List<B2BTaxExportDto>>> GetExportAsync(B2BTaxFilterDto filter, bool isAdmin, string userName)
        {
            List<B2BTaxExportDto> dto = new List<B2BTaxExportDto>();
            try
            {
                var iquery = this.UnitOfWork.GetAllNoTracking<B2BTax>()
                    .Where(x => x.ActiveFlag != STATUS_DELETE);

                if (!string.IsNullOrEmpty(filter.Keyword))
                {
                    var keyword = filter.Keyword.Trim();
                    iquery = iquery.Where(x => x.ProductName.Contains(keyword)
                    || x.SKU == keyword
                    );
                }
                dto = iquery
                    .OrderByDescending(x => x.CreateDate)
                    .ThenBy(x => x.ProductName)
                    .AsEnumerable()
                    .Select((x, index) => new B2BTaxExportDto
                    {
                        SKU = x.SKU,
                        ProductName = x.ProductName,
                        TaxCode_B2B = x.TaxCode_B2B.ToString(),
                        Status = x.ActiveFlag == STATUS_ACTIVE ? "Active" : "Deactive",
                    })
                    .ToList();
                if (!dto.Any())
                    dto.Add(new B2BTaxExportDto());
            }
            catch (Exception ex)
            {
                LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
            }
            return new Tuple<int, List<B2BTaxExportDto>>(0, dto);
        }

        public async Task<Tuple<int, List<B2BTaxDto>>> GetPagingAsync(B2BTaxFilterDto filter, bool isAdmin, string userName)
        {
            List<B2BTaxDto> dtos = new List<B2BTaxDto>();
            int totalItem = 0;
            try
            {
                var iquery = this.UnitOfWork.GetAllNoTracking<B2BTax>()
                    .Where(x => x.ActiveFlag != STATUS_DELETE);
                var productQuery = this.UnitOfWork.GetAllNoTracking<Product>()
                    .Where(x => x.ActiveFlag == STATUS_ACTIVE);
                if (!string.IsNullOrEmpty(filter.Keyword))
                {
                    var keyword = filter.Keyword.Trim();
                    iquery = iquery.Where(x => x.ProductName.Contains(keyword)
                    || x.SKU == keyword
                    );
                }
                totalItem = await iquery.CountAsync();
                dtos = await iquery
                    .OrderByDescending(x => x.UpdateDate)
                    .ThenBy(x => x.SKU)
                    .ThenBy(x => x.ProductName)
                    .Skip((filter.PageIndex - 1) * filter.PageSize)
                    .Take(filter.PageSize)
                    .Join(productQuery, x => x.SKU, y => y.Sku, (x, y) => new { x, y })
                    .Select(x => new B2BTaxDto()
                    {
                        Id = x.x.Id,
                        SKU = x.x.SKU,
                        ProductName = x.x.ProductName,
                        TaxCode_Normal = x.y.TaxRate,
                        TaxCode_B2B = x.x.TaxCode_B2B,
                        No = x.x.No,
                        ActiveFlag = (ActiveFlag)x.x.ActiveFlag,
                        CreateBy = x.x.CreateBy,
                        UpdateBy = x.x.UpdateBy,
                        CreateDate = x.x.CreateDate,
                        UpdateDate = x.x.UpdateDate
                    })
                    .ToListAsync();
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

                    foreach (var item in dtos)
                    {
                        item.UpdateByFullName = lstUpdateBys.Where(x => x.UserName.ToLower().Equals(item.UpdateBy.ToLower()))
                       .Select(x => x.FullName)
                       .FirstOrDefault();
                    }
                }
            }
            catch (Exception ex)
            {
                LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
            }
            return new Tuple<int, List<B2BTaxDto>>(totalItem, dtos);
        }

        public async Task<Tuple<bool, string>> InsertListAsync(List<B2BTaxDto> dtos)
        {
            var result = true;
            var msg = "";

            using (var trans = this.UnitOfWork.BeginTransaction())
            {
                try
                {
                    foreach (var dto in dtos)
                    {
                        var action = AppSystemLog.Action.Insert;

                        if (this.UnitOfWork.GetAllNoTracking<B2BTax>().Any(x => x.ActiveFlag != STATUS_DELETE && x.SKU == dto.SKU))
                            action = AppSystemLog.Action.Update;

                        if (action == AppSystemLog.Action.Insert)
                        {
                            var entity = Mapper.Map<B2BTax>(dto);
                            dto.SetDefaultValueInsert();
                            entity = this.UnitOfWork.Insert(entity);
                            result = entity != null;
                        }
                        else
                        {
                            var entity = this.UnitOfWork.GetAllNoTracking<B2BTax>().FirstOrDefault(x => x.ActiveFlag != STATUS_DELETE && x.SKU == dto.SKU);
                            entity.UpdateDate = DateTime.Now;
                            entity.UpdateBy = dto.UpdateBy;

                            entity.TaxCode_B2B = dto.TaxCode_B2B;
                            entity.ProductName = dto.ProductName;

                            result = this.UnitOfWork.Update(entity);
                        }
                        if (result)
                        {
                            dto.Comment = $"SUCCESSFUL! {action.ToString()} B2BTaxCode SKU {dto.SKU} by user: {dto.UpdateBy}.";
                            InsertHistory(dto, (int)action, 1);
                        }
                        else
                        {
                            msg = "Error when importing Sheet: " + dto.ProductName;
                            break;
                        }
                    }
                    if (result)
                        trans.Commit();
                    else
                        this.UnitOfWork.Rollback(trans);

                }
                catch (Exception ex)
                {
                    this.UnitOfWork.Rollback(trans);
                    LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
                    result = false;
                    msg = ex.StackTrace + "---" + ex.Message;
                }
            }
            return new Tuple<bool, string>(result, msg);
        }

        public async Task<Tuple<bool, B2BTaxDto>> UpdateAsync(B2BTaxDto dto)
        {
            var result = false;
            AppSystemLog.Action _actionType = AppSystemLog.Action.Update;
            var trans = UnitOfWork.BeginTransaction();
            try
            {
                var entity = this.UnitOfWork.GetAllNoTracking<B2BTax>()
                            .SingleOrDefault(x => x.Id == dto.Id);
                entity.UpdateDate = DateTime.Now;
                entity.UpdateBy = dto.UpdateBy;
                entity.TaxCode_B2B = dto.TaxCode_B2B;
                entity.ProductName = dto.ProductName;
                entity.ActiveFlag = (byte)dto.ActiveFlag;
                result = await this.UnitOfWork.UpdateAsync(entity);
                if (result)
                {
                    trans.Commit();
                }
                else
                    this.UnitOfWork.Rollback(trans);
            }
            catch (Exception ex)
            {
                this.UnitOfWork.Rollback(trans);
                LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
                dto.Comment = ex.StackTrace + "---" + ex.Message;
                result = false;
            }

            InsertHistory(dto, (int)_actionType, result ? 1 : 0);
            return new Tuple<bool, B2BTaxDto>(result, dto);
        }

        public bool CheckExist(string sku)
        {
            bool result = false;
            try
            {
                result = this.UnitOfWork.GetAllNoTracking<B2BTax>().Any(x => x.SKU == sku && x.ActiveFlag != STATUS_DELETE);
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return result;
        }
        public bool CheckSKUExist(string sku)
        {
            bool result = false;
            try
            {
                result = this.UnitOfWork.GetAllNoTracking<Product>().Any(x => x.Sku == sku && x.ActiveFlag == STATUS_ACTIVE);
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return result;
        }
        public double getNormalTax(string sku)
        {
            double tax = 0;
            try
            {
                var product = this.UnitOfWork.GetAllNoTracking<Product>().Where(x => x.Sku == sku && x.ActiveFlag == STATUS_ACTIVE).FirstOrDefault();
                tax = product?.TaxRate ?? 5;
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return tax;
        }
        private void InsertHistory(B2BTaxDto dto, int action, int sucess)
        {
            var rs = this.UnitOfWork.Insert(new SystemLog
            {
                LogId = Guid.NewGuid(),
                Module = AppModule.B2BTax.ToString(),
                UserId = dto.UpdateBy,
                UserFunction = action,
                EventResult = sucess,
                FuncDateTime = DateTime.Now,
                Source = dto.SKU,
                Transdata = dto.Comment,
                WSName = ""
            });
        }
    }
}
