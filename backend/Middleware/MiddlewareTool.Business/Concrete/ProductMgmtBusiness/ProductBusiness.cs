using AutoMapper;
using MiddlewareTool.Business.Interface;
using MiddlewareTool.Common;
using MiddlewareTool.Dto;
using MiddlewareTool.Entities;
using MiddlewareTool.OpenXML;
using MiddlewareTool.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using static MiddlewareTool.Common.AppType;
using static MiddlewareTool.Dto.ProductMgmtDto;
using static MiddlewareTool.Dto.StoreMgmtDto;
using static MiddlewareTool.Dto.UserMgmtDto;

namespace MiddlewareTool.Business.Concrete
{
    public class ProductBusiness : BaseBusiness, IProductBusiness
    {
        private readonly IInventoryDeltaBusiness _inventoryDeltaBusiness;
        private readonly IInventoryBusiness _inventoryBusiness;
        private readonly IPricingBusiness _pricingBusiness;
        private readonly IProductHistoryBusiness _productHistoryBusiness;
        private readonly IProductInfoHistoryBusiness _productInfoHistoryBusiness;
        public ProductBusiness(IUnitOfWork unitOfWork, IInventoryDeltaBusiness inventoryDeltaBusiness, IInventoryBusiness inventoryBusiness, IPricingBusiness pricingBusiness, IProductHistoryBusiness productHistoryBussiness, IProductInfoHistoryBusiness productInfoHistoryBusiness) : base(unitOfWork)
        {
            _inventoryDeltaBusiness = inventoryDeltaBusiness;
            _inventoryBusiness = inventoryBusiness;
            _pricingBusiness = pricingBusiness;
            _productHistoryBusiness = productHistoryBussiness;
            _productInfoHistoryBusiness = productInfoHistoryBusiness;
        }
        public byte[] GetTransfer(byte[] template, string transData, int timeOut)
        {
            try
            {
                if (template != null && template.Length > 0)
                {
                    var sql = "Exec " + BaseBusiness.SP_Product_GetTransfer;
                    sql += " @transData = '" + transData + "';";
                    var lstData = this.UnitOfWork.SqlQuery<ProductBoxedDto>(sql, timeOut).ToList();
                    if (lstData != null && lstData.Count > 0)
                    {
                        //lstData.ForEach(item =>
                        //{
                        //    item.Description = item.Description
                        //    .Replace("\r\n", "")
                        //    .Replace("\n", "");
                        //});
                        var excel = new Excel();
                        excel.TemplateFileData = template;
                        //excel.ParameterData.Add("Code", "Code");
                        //excel.ParameterData.Add("Name", "Name");
                        //excel.ParameterData.Add("VariantOption", "VariantOption");
                        //excel.ParameterData.Add("Sku", "Sku");
                        //excel.ParameterData.Add("VariantName", "VariantName");
                        //excel.ParameterData.Add("ExtendedName", "ExtendedName");
                        //excel.ParameterData.Add("BrandName", "BrandName");
                        //excel.ParameterData.Add("Description", "Description");
                        //excel.ParameterData.Add("Ingredients", "Ingredients");
                        //excel.ParameterData.Add("CategoryCode", "CategoryCode");
                        //excel.ParameterData.Add("Is_Chill", "Is_Chill");
                        //excel.ParameterData.Add("Is_Frozen", "Is_Frozen");
                        //excel.ParameterData.Add("Weight", "Weight");
                        //excel.ParameterData.Add("Length", "Length");
                        //excel.ParameterData.Add("Width", "Width");
                        //excel.ParameterData.Add("Height", "Height");
                        //excel.ParameterData.Add("Volume", "Volume");
                        //excel.ParameterData.Add("Is_Age_Gated", "Is_Age_Gated");
                        //excel.ParameterData.Add("MaxCartQuantity", "MaxCartQuantity");
                        //excel.ParameterData.Add("UnitCount", "UnitCount");
                        //excel.ParameterData.Add("UnitType", "UnitType");
                        //excel.ParameterData.Add("Is_Perishable", "Is_Perishable");
                        //excel.ParameterData.Add("Origin", "Origin");
                        //excel.ParameterData.Add("Grade", "Grade");
                        //excel.ParameterData.Add("Size", "Size");
                        //excel.ParameterData.Add("Upc", "Upc");
                        //excel.ParameterData.Add("Barcode", "Barcode");
                        //excel.ParameterData.Add("TaxRate", "TaxRate");
                        //excel.ParameterData.Add("Fulfillment_Method", "Fulfillment_Method");
                        //excel.ParameterData.Add("ImageLinks", "ImageLinks");
                        //excel.ParameterData.Add("seo_title", "seo_title");
                        //excel.ParameterData.Add("seo_description", "seo_description");
                        //excel.ParameterData.Add("seo_keyword", "seo_keyword");
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
        public bool UpdateTransferred(DateTime dateTime, string source, int action, string transData, int timeOut)
        {
            try
            {
                var sql = BaseBusiness.SP_Product_UpdateTransferred;
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
        public async Task<Tuple<int, List<ProductDto>>> GetPagingAsync(string keyWord, int pageIndex, int pageSize, List<string> categoryIds, string categoryIdIsNull, bool? isPublished, bool? isAgeGated, bool? isChilled, bool? isFrozen, bool? isPerishable, bool? isManual, string brand, List<string> storeCodes, List<byte?> fulfillment, bool? isSyncProfit, bool? concess, bool? outright, string variantType, string customerScope, string slug, bool? showInProductList, bool? displayAsOos, bool? mommyItem, bool? aeonCardItem, bool? quickDelivery)
        {
            int total = 0;
            try
            {
                string sql = "EXEC [prod].[SP_PRODUCT_GETPAGING]";
                List<SqlParameter> parameters = new List<SqlParameter>();
                #region Build query
                if (!string.IsNullOrEmpty(keyWord))
                {
                    keyWord = keyWord.Replace("'", "''");
                    sql += $"@keyword = N'{keyWord.Trim()}',";
                }

                if (categoryIds?.Count > 0)
                {
                    categoryIds = categoryIds.Where(x => !string.IsNullOrEmpty(x)).ToList();
                    if (categoryIds.Count > 0)
                    {
                        sql += $" @categoryIds = '{string.Join(",", categoryIds)}',";
                    }
                }
                if (isPublished.HasValue)
                {
                    sql += $" @isPublished = '{(isPublished.Value ? "1" : "0")}',";
                }
                if (isAgeGated.HasValue && isAgeGated.Value)
                {
                    sql += $" @isAgeGated = '{(isAgeGated.Value ? "1" : "0")}',";
                }
                if (isChilled.HasValue && isChilled.Value)
                {
                    sql += $" @isChilled = '{(isChilled.Value ? "1" : "0")}',";
                }
                if (isFrozen.HasValue && isFrozen.Value)
                {
                    sql += $" @isFrozen = '{(isFrozen.Value ? "1" : "0")}',";
                }
                if (isPerishable.HasValue && isPerishable.Value)
                {
                    sql += $" @isPerishable = '{(isPerishable.Value ? "1" : "0")}',";
                }
                if (!string.IsNullOrEmpty(brand))
                {
                    brand = brand.Replace("'", "''");
                    sql += $@" @brand = N'{brand}',";
                }
                if (storeCodes?.Count > 0)
                {
                    storeCodes = storeCodes.Where(x => !string.IsNullOrEmpty(x)).ToList();
                    if (storeCodes.Count > 0)
                    {
                        sql += $" @storeCodes = '{string.Join(",", storeCodes)}',";
                    }
                }
                if (fulfillment?.Count > 0 && !fulfillment.Contains(null))
                {
                    sql += $" @fulfillment = '{string.Join(",", fulfillment)}',";
                }
                if (isSyncProfit.HasValue)
                {
                    sql += $" @isSyncProfit = '{(isSyncProfit.Value ? "1" : "0")}',";
                }
                if (concess.HasValue && outright.HasValue)
                {
                    sql += $" @product_types = '{(byte)ProductType.Consignment},{(byte)ProductType.Outright}',";

                }
                else if (concess.HasValue)
                {
                    sql += $" @product_types = '{(byte)ProductType.Consignment}',";

                }
                else if (outright.HasValue)
                {
                    sql += $" @product_types = '{(byte)ProductType.Outright}',";
                }

                if (!string.IsNullOrEmpty(variantType))
                {
                    sql += $" @variantType = N'{variantType}',";
                }
                if (!string.IsNullOrEmpty(customerScope))
                {
                    sql += $" @customerScope = N'{customerScope}',";
                }
                if (!string.IsNullOrEmpty(slug))
                {
                    sql += $" @slug = N'{slug}',";
                }
                if (showInProductList.HasValue)
                {
                    sql += $" @showInProductList = '{(showInProductList.Value ? "1" : "0")}',";
                }
                if (displayAsOos.HasValue)
                {
                    sql += $" @displayAsOos = '{(displayAsOos.Value ? "1" : "0")}',";
                }
                if (mommyItem.HasValue)
                {
                    sql += $" @mommyItem = '{(mommyItem.Value ? "1" : "0")}',";
                }
                if (aeonCardItem.HasValue)
                {
                    sql += $" @aeonCardItem = '{(aeonCardItem.Value ? "1" : "0")}',";
                }
                if (quickDelivery.HasValue)
                {
                    sql += $" @quickDelivery = '{(quickDelivery.Value ? "1" : "0")}',";
                }

                sql += $" @pageIndex='{pageIndex}', @pageSize='{pageSize}';";
                #endregion

                var data = this.UnitOfWork.SqlQuery<ProductDto>(sql).ToList();
                if (data.Count > 0)
                    total = data[0].Total;
                return new Tuple<int, List<ProductDto>>(total, data);
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return new Tuple<int, List<ProductDto>>(total, new List<ProductDto>());
        }
        #region Old code
        //public async Task<Tuple<int, List<ProductDto>>> GetPagingAsync(string keyWord, int pageIndex, int pageSize, List<string> categoryIds, string categoryIdIsNull, bool? isPublished, bool? isAgeGated, bool? isChilled, bool? isFrozen, bool? isPerishable, bool? isManual, string brand, List<string> storeCodes, List<byte?> fulfillment, bool? isSyncProfit, bool? concess, bool? outright)
        //{
        //    int total = 0;
        //    try
        //    {
        //        var iquery = this.UnitOfWork.GetAll<Product>().Where(x => x.ActiveFlag == STATUS_ACTIVE);
        //        var category = this.UnitOfWork.GetAll<Category>().Where(x => x.ActiveFlag == STATUS_ACTIVE);
        //        var productInfos = this.UnitOfWork.GetAll<ProductInfo>().Where(x => x.ActiveFlag == STATUS_ACTIVE);
        //        if (!string.IsNullOrEmpty(keyWord))
        //        {
        //            var keyTrim = keyWord.Trim().ToLower();
        //            iquery = iquery.Where(x => x.Code.ToLower().Equals(keyTrim)
        //                || x.Name.ToLower().Contains(keyTrim)
        //                //|| x.Description.ToLower().Contains(keyTrim)
        //                //|| x.Description.ToLower().Contains(keytrimEncode)
        //                || x.CompanyCode.ToLower().Contains(keyTrim)
        //                || x.BrandName.ToLower().Contains(keyTrim)
        //                || x.ExtendedName.ToLower().Contains(keyTrim)
        //                || x.VariantName.ToLower().Contains(keyTrim)
        //                || x.Sku.ToLower().Equals(keyTrim)
        //                );
        //        }
        //        if (categoryIds?.Count > 0)
        //        {
        //            categoryIds = categoryIds.Where(x => !string.IsNullOrEmpty(x)).ToList();
        //            if (categoryIds.Count > 0)
        //            {
        //                if (categoryIds.Contains("null"))
        //                    iquery = iquery.Where(x => string.IsNullOrEmpty(x.CategoryCode));
        //                else if (categoryIds.Contains("notnull"))
        //                    iquery = iquery.Where(x => !string.IsNullOrEmpty(x.CategoryCode));
        //                else
        //                    iquery = iquery.Where(x => categoryIds.Contains(x.CategoryCode));
        //            }
        //        }
        //        if (isPublished.HasValue)
        //        {
        //            if (isPublished == true)
        //            {
        //                //productInfos = productInfos.Where(x => x.IsPublished == true);
        //                var productInfo = productInfos.Where(x => x.IsPublished == true).Select(x => x.Sku).Distinct();
        //                iquery = iquery.Where(x => productInfo.Contains(x.Sku));
        //            }
        //            else
        //            {
        //                //productInfos = productInfos.Where(x => x.IsPublished != true);
        //                var productInfo = productInfos.Where(x => x.IsPublished != true).Select(x => x.Sku).Distinct();
        //                iquery = iquery.Where(x => productInfo.Contains(x.Sku));
        //            }
        //        }
        //        if (isAgeGated.HasValue && isAgeGated.Value)
        //        {
        //            iquery = iquery.Where(x => x.IsAgeGated == isAgeGated.Value);
        //        }
        //        if (isChilled.HasValue && isChilled.Value)
        //        {
        //            iquery = iquery.Where(x => x.IsChilled == isChilled.Value);
        //        }
        //        if (isFrozen.HasValue && isFrozen.Value)
        //        {
        //            iquery = iquery.Where(x => x.IsFrozen == isFrozen.Value);
        //        }
        //        if (isPerishable.HasValue && isPerishable.Value)
        //        {
        //            iquery = iquery.Where(x => x.IsPerishable == isPerishable.Value);
        //        }
        //        if (!string.IsNullOrEmpty(brand))
        //        {
        //            iquery = iquery.Where(x => x.BrandName == brand);
        //        }
        //        if (storeCodes?.Count > 0)
        //        {
        //            storeCodes = storeCodes.Where(x => !string.IsNullOrEmpty(x)).ToList();
        //            if (storeCodes.Count > 0)
        //            {
        //                //productInfos = productInfos.Where(x => storeCodes.Contains(x.StoreCode));
        //                var productInfo = productInfos.Where(x => storeCodes.Contains(x.StoreCode)).Select(x => x.Sku).Distinct();
        //                iquery = iquery.Where(x => productInfo.Contains(x.Sku));
        //            }
        //        }
        //        if (fulfillment?.Count > 0 && !fulfillment.Contains(null))
        //        {
        //            iquery = iquery.Where(x => fulfillment.Contains(x.Fulfillment.Value));
        //        }
        //        if (isSyncProfit.HasValue)
        //        {
        //            //if (isSyncProfit == true)
        //            //{
        //            //    //productInfos = productInfos.Where(x => x.IsSyncProfit == true);
        //            //    var productInfo = productInfos.Where(x => x.IsSyncProfit == true).Select(x => x.Sku).Distinct();
        //            //    iquery = iquery.Where(x => productInfo.Contains(x.Sku));
        //            //}
        //            //else
        //            //{
        //            //    //productInfos = productInfos.Where(x => x.IsSyncProfit != true);
        //            //    var productInfo = productInfos.Where(x => x.IsSyncProfit != true).Select(x => x.Sku).Distinct();
        //            //    iquery = iquery.Where(x => productInfo.Contains(x.Sku));
        //            //}
        //            var productInfo = productInfos.Where(x => x.IsSyncProfit == isSyncProfit).Select(x => x.Sku).Distinct();
        //            iquery = iquery.Where(x => productInfo.Contains(x.Sku));
        //        }
        //        if (concess.HasValue && outright.HasValue)
        //        {
        //            iquery = iquery.Where(x => x.Type == (byte)ProductType.Consignment || x.Type == (byte)ProductType.Outright);
        //        }
        //        else if (concess.HasValue)
        //        {
        //            iquery = iquery.Where(x => x.Type == (byte)ProductType.Consignment);
        //        }
        //        else if (outright.HasValue)
        //        {
        //            iquery = iquery.Where(x => x.Type == (byte)ProductType.Outright);
        //        }

        //        var data = await iquery.OrderByDescending(x => x.OrderNumber).ThenByDescending(x => x.UpdateDate)
        //            .Skip((pageIndex - 1) * pageSize)
        //            .Take(pageSize)
        //            .Select(x => new ProductDto
        //            {
        //                Id = x.Id,
        //                CompanyCode = x.CompanyCode,
        //                StoreCode = x.StoreCode,
        //                Code = x.Code,
        //                Name = x.Name,
        //                Sku = x.Sku,
        //                VariantName = x.VariantName,
        //                VariantOption = x.VariantOption,
        //                ExtendedName = x.ExtendedName,
        //                BrandName = x.BrandName,
        //                Description = x.Description,
        //                Ingredients = x.Ingredients,
        //                CategoryCode = x.CategoryCode + "-" + category.Where(cat => cat.Code == x.CategoryCode).FirstOrDefault().Name,
        //                Upc = x.Upc,
        //                Barcode = x.Barcode,
        //                Size = x.Size,
        //                Weight = x.Weight,
        //                Length = x.Length,
        //                Width = x.Width,
        //                Height = x.Height,
        //                Volume = x.Volume,
        //                MaxCartQuantity = x.MaxCartQuantity,
        //                UnitCount = x.UnitCount,
        //                UnitType = x.UnitType,
        //                Origin = x.Origin,
        //                Grade = x.Grade,
        //                TaxRate = x.TaxRate,
        //                ImageLinks = x.ImageLinks,
        //                IsAgeGated = x.IsAgeGated,
        //                IsChilled = x.IsChilled,
        //                IsFrozen = x.IsFrozen,
        //                IsPerishable = x.IsPerishable,
        //                IsTransfer = x.IsTransfer,
        //                IsPublished = x.IsPublished,
        //                //IsSyncProfit = x.IsSyncProfit,
        //                IsSyncProfit = productInfos.Any(productInfo => productInfo.Sku == x.Sku && productInfo.IsSyncProfit == true),
        //                Type = x.Type,
        //                Fulfillment = x.Fulfillment,
        //                URL = x.URL,
        //                CreateBy = x.CreateBy,
        //                UpdateBy = x.UpdateBy,
        //                CreateDate = x.CreateDate,
        //                UpdateDate = x.UpdateDate
        //            })
        //            .ToListAsync();
        //        total = await iquery.CountAsync();

        //        foreach (var item in data)
        //        {
        //            item.IsPublished = productInfos.Any(x => x.Sku.Equals(item.Sku) && x.IsPublished && x.ActiveFlag == STATUS_ACTIVE);
        //        }
        //        return new Tuple<int, List<ProductDto>>(total, data);
        //    }
        //    catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
        //    return new Tuple<int, List<ProductDto>>(total, new List<ProductDto>());
        //}
        #endregion

        public async Task<ProductDto> GetByIdAsync(Guid id)
        {
            try
            {
                var iquery = await this.UnitOfWork.GetSingleAsync<Product>(x => x.Id == id && x.ActiveFlag == STATUS_ACTIVE);
                if (iquery != null)
                {
                    return Mapper.Map<ProductDto>(iquery);
                }
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return null;
        }
        public async Task<ESLDto> GetESLBySkuAsync(string sku)
        {
            try
            {
                string sql = $"exec [prod].[SP_Get_ESL] @sku='{sku}'";

                var rs = this.UnitOfWork.SqlQuery<ESLDto>(sql).FirstOrDefault();
                return rs ?? new ESLDto();
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return new ESLDto();
        }
        public async Task<double?> GetManualStockBySkuAsync(string sku)
        {
            try
            {
                var iquery = await this.UnitOfWork.GetSingleAsync<ProductInfo>(x => x.Sku == sku);
                if (iquery != null)
                {
                    return iquery.Inventory;
                }
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return null;
        }
        public bool CheckExist(string sku)
        {
            var result = false;
            try
            {
                result = this.UnitOfWork.GetAllNoTracking<Product>().Any(x => x.Sku == sku && x.ActiveFlag == STATUS_ACTIVE);
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return result;
        }
        public bool CheckExist(string sku, string storeCode)
        {
            var result = false;
            try
            {
                result = this.UnitOfWork.GetAllNoTracking<ProductInfo>().Any(x => x.Sku == sku && x.StoreCode == storeCode && x.ActiveFlag == STATUS_ACTIVE);
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return result;
        }
        public List<ProductDto> GetAllSKUAndStoreCode(List<string> storeCodes = null)
        {
            List<ProductDto> result = new List<ProductDto>();
            try
            {
                if (storeCodes?.Count > 0)
                {
                    var hs = new HashSet<string>(storeCodes);
                    result = this.UnitOfWork.GetAllNoTracking<ProductInfo>()
                        .Where(x => x.ActiveFlag == STATUS_ACTIVE
                        && hs.Contains(x.StoreCode)
                        )
                        .Select(y => new ProductDto() { StoreCode = y.StoreCode, Sku = y.Sku })
                        .ToList();
                }
                else

                    result = this.UnitOfWork.GetAllNoTracking<ProductInfo>().Where(x => x.ActiveFlag == STATUS_ACTIVE).Select(y => new ProductDto() { StoreCode = y.StoreCode, Sku = y.Sku }).ToList();


            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return result;
        }
        public bool CheckExistProductInfor(string sku)
        {
            try
            {
                var iquery = this.UnitOfWork.GetSingle<Product>(x => x.Sku == sku && x.ActiveFlag == STATUS_ACTIVE);
                if (iquery != null)
                {
                    return true;
                }
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return false;
        }
        public async Task<bool> InsertAsync(ProductDto dto)
        {
            bool result = false;
            try
            {
                var entity = Mapper.Map<Product>(dto);
                entity.Id = Guid.NewGuid();
                entity.URL = AppValue.ToUnsignString(dto.Description);
                entity.CreateDate = DateTime.Now;
                entity.UpdateDate = DateTime.Now;
                entity.CreateBy = dto.CreateBy;
                entity.UpdateBy = dto.CreateBy;
                entity.IsTransfer = false;
                entity.IsNew = true;
                entity.IsTransferESL = false;
                var add = await this.UnitOfWork.InsertAsync(entity);
                if (add != null)
                {
                    //_inventoryDeltaBusiness.UpdateIsTransfer(entity.Sku);
                    var productHistory = Mapper.Map<ProductHistoryDto>(entity);
                    productHistory.Id = Guid.NewGuid();
                    productHistory.Action = (int)Common.History.Action.Insert;
                    productHistory.Source = "ProductMgmt/Insert";
                    _productHistoryBusiness.Insert(productHistory);
                    _inventoryBusiness.UpdateIsTransfer(entity.Sku, Common.History.Action.Insert);
                    _pricingBusiness.UpdateIsTransfer(entity.Sku);

                    result = true;
                }
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return result;
        }
        //public async Task<bool> InsertOrUpdsateManualStockAsync(ProductDto dto, double manualStockQuantity)
        //{
        //    bool result = false;
        //    try
        //    {
        //        var entity = this.UnitOfWork.GetSingle<ProductInfo>(x => x.Sku.Equals(dto.Sku));
        //        if (entity != null)
        //        {
        //            entity.Inventory = manualStockQuantity;
        //            entity.OrderNumber = dto.OrderNumber;
        //            entity.URL = string.Empty;
        //            entity.UpdateBy = dto.UpdateBy;
        //            entity.UpdateDate = DateTime.Now;
        //            result = await this.UnitOfWork.UpdateAsync(entity);
        //        }
        //        else
        //        {
        //            var entityManualStock = new ProductInfo
        //            {
        //                Id = Guid.NewGuid(),
        //                Sku = dto.Sku,
        //                ProductName = dto.Name,
        //                Inventory = manualStockQuantity,
        //                IsPublished = true,
        //                OrderNumber = 0,
        //                URL = AppValue.ToUnsignString(dto.Description),
        //                CreateDate = DateTime.Now,
        //                UpdateDate = DateTime.Now,
        //                CreateBy = dto.CreateBy,
        //                UpdateBy = dto.CreateBy,
        //                ActiveFlag = (byte)AppValue.ActiveFlag.Active
        //            };
        //            var insertResult = await this.UnitOfWork.InsertAsync(entityManualStock);
        //            if (insertResult != null) { result = true; }
        //        }
        //    }
        //    catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
        //    return result;
        //}
        public bool InsertOrUpdateManualStock(string user, string sku, string storeCode, double manualStockQuantity, int? stockBuffer, bool? published, bool? syncProfit, int? fulfillment, bool? quickDelivery, string fileName)
        {
            bool result = false;
            try
            {
                var entity = this.UnitOfWork.GetAll<ProductInfo>().FirstOrDefault(x => x.Sku.Equals(sku) && x.StoreCode.Equals(storeCode) && x.ActiveFlag == STATUS_ACTIVE);
                if (entity != null)
                {
                    if (manualStockQuantity >= 0)
                        entity.Inventory = manualStockQuantity;
                    entity.IsTransfer = entity.IsPublished ? false : entity.IsTransfer;

                    //Giá trị: n > 0: cập nhật. 
                    //Rỗng(Empty cell): không cập nhật field này.
                    if (stockBuffer.HasValue)
                    {
                        if (stockBuffer >= 0)
                        {
                            entity.StockBuffer = stockBuffer.Value;
                        }
                    }
                    // Rỗng (Empty cell): không cập nhật field này.
                    if (published.HasValue)
                    {
                        if (entity.IsPublished != true && published == true)
                        {
                            entity.IsNew = true;
                            entity.IsTransfer = false;
                        }
                        else if (entity.IsPublished == true && published == false)
                        {
                            var oldInventory = this.UnitOfWork.GetAll<Inventory>(x => x.Sku.Trim() == sku.Trim() && x.StoreCode == storeCode && x.ActiveFlag == STATUS_ACTIVE).FirstOrDefault();
                            if (oldInventory != null)
                            {
                                #region hotfix trường hợp từ Publish - > Unpublished chỉ gửi 1 lần
                                oldInventory.IsTransfer = false;
                                oldInventory.UpdateBy = user;
                                oldInventory.UpdateDate = DateTime.Now;
                                this.UnitOfWork.Update(oldInventory);

                                var inventoryHistory = Mapper.Map<InventoryHistory>(oldInventory);
                                inventoryHistory.Id = Guid.NewGuid();
                                inventoryHistory.Action = (int)Common.History.Action.ImportManualStock;
                                inventoryHistory.Source = fileName;
                                this.UnitOfWork.Insert(inventoryHistory);
                                #endregion
                            }
                        }
                        entity.IsPublished = published.Value;
                    }
                    // Rỗng (Empty cell): không cập nhật field này.
                    if (syncProfit.HasValue)
                    {
                        entity.IsSyncProfit = syncProfit.Value;
                    }
                    // Rỗng (Empty cell): không cập nhật field này.
                    if (fulfillment.HasValue)
                    {
                        /*
                         Rule: Nếu update/change trường Fulfillment_method với bầt ký cách nào (trên UI hoặc upload excel) thì MW sẽ nhận diện và xuất file zip, theo logic tương tự new SKU.
                         */
                        if (entity.Fulfillment != fulfillment)
                        {
                            entity.IsNew = true;
                        }
                        entity.Fulfillment = (byte)fulfillment.Value;

                        // chỉ cập nhật field Quick Delivery với điều kiện field Fulfillment (cột trước đó trong file) có giá trị là "0,1" (2) hoặc "1"
                        // rỗng: không update field này, giữ nguyên hiện trạng ở DB
                        // Nếu field Fulfillment có giá trị là "0" thì field Quick Delivery sẽ tự động được set về "FALSE" (dù có nhập gì trong file đi nữa)
                        if (quickDelivery.HasValue && (fulfillment == 1 || fulfillment == 2))
                        {
                            entity.QuickDelivery = quickDelivery.Value;
                        }
                        else if (!(fulfillment == 1 || fulfillment == 2))
                        {
                            entity.QuickDelivery = false;
                        }
                    }

                    entity.UpdateBy = user;
                    entity.UpdateDate = DateTime.Now;
                    result = this.UnitOfWork.Update(entity);
                    if (result)
                    {
                        var productHistory = Mapper.Map<ProductInfoHistoryDto>(entity);
                        productHistory.Id = Guid.NewGuid();
                        productHistory.Action = (int)Common.History.Action.ImportManualStock;
                        productHistory.Source = fileName;
                        _productInfoHistoryBusiness.Insert(productHistory);
                    }
                }

                //else
                //{
                //    var entityManualStock = new ProductInfo
                //    {
                //        Id = Guid.NewGuid(),
                //        Sku = sku,
                //        StoreCode = storeCode,
                //        ProductName = string.Empty,
                //        Inventory = manualStockQuantity,
                //        IsPublished = true,
                //        StockBuffer = stockBuffer ?? 0,
                //        Fulfillment = fulfillment.HasValue ? (byte)fulfillment.Value : (byte)1,
                //        IsSyncProfit = syncProfit ?? true,
                //        IsNew = true,
                //        IsTransfer = false,
                //        OrderNumber = 0,
                //        URL = string.Empty,
                //        CreateDate = DateTime.Now,
                //        UpdateDate = DateTime.Now,
                //        CreateBy = user,
                //        UpdateBy = user,
                //        ActiveFlag = (byte)AppValue.ActiveFlag.Active
                //    };
                //    var insertResult = this.UnitOfWork.Insert(entityManualStock);
                //    if (insertResult != null)
                //    {
                //        var productInfoHistory = Mapper.Map<ProductInfoHistoryDto>(entity);
                //        productInfoHistory.Id = Guid.NewGuid();
                //        productInfoHistory.Action = (int)Common.History.Action.Insert;
                //        productInfoHistory.Source = "ProductImport/InsertManualStock";
                //        _productInfoHistoryBusiness.Insert(productInfoHistory);
                //        result = true;
                //    }
                //}
                //if (result)
                //{
                //    var action = entity != null ? Common.History.Action.Insert : Common.History.Action.Update;
                //_inventoryBusiness.UpdateIsTransferBySkuAndStore(sku, storeCode, entity.IsTransfer.GetValueOrDefault(), action);
                //}
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return result;
        }
        public bool Insert(ProductDto dto, string fileName)
        {
            bool result = false;
            try
            {
                using (var trans = this.UnitOfWork.BeginTransaction())
                {
                    var entity = Mapper.Map<Product>(dto);
                    entity.Id = Guid.NewGuid();
                    entity.URL = AppValue.ToUnsignString(dto.Description);
                    entity.CreateDate = DateTime.Now;
                    entity.UpdateDate = DateTime.Now;
                    entity.CreateBy = dto.CreateBy;
                    entity.UpdateBy = dto.CreateBy;
                    entity.IsTransfer = false;
                    entity.IsNew = true;
                    entity.IsTransferESL = false;
                    var add = this.UnitOfWork.Insert(entity);
                    if (add != null)
                    {
                        //_inventoryDeltaBusiness.UpdateIsTransfer(entity.Sku);
                        var productHistory = Mapper.Map<ProductHistoryDto>(entity);
                        productHistory.Action = (int)Common.History.Action.Insert;
                        productHistory.Id = Guid.NewGuid();
                        productHistory.Source = fileName;
                        _productHistoryBusiness.Insert(productHistory);
                        _inventoryBusiness.UpdateIsTransfer(entity.Sku, Common.History.Action.Import);
                        _pricingBusiness.UpdateIsTransfer(entity.Sku);
                        result = true;
                    }

                    if (result)
                        trans.Commit();
                    else
                        this.UnitOfWork.Rollback(trans);
                }
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return result;
        }
        public async Task<bool> IsExistAsync(string sku)
        {
            bool result = true;
            try
            {
                if (!string.IsNullOrEmpty(sku))
                {
                    var iquery = await this.UnitOfWork.GetSingleAsync<Product>(x => x.Sku.Equals(sku) && x.ActiveFlag == STATUS_ACTIVE);
                    if (iquery == null) { result = false; }
                }
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return result;
        }
        public bool IsExist(string sku)
        {
            bool result = true;
            try
            {
                if (!string.IsNullOrEmpty(sku))
                {
                    var iquery = this.UnitOfWork.GetSingle<Product>(x => x.Sku.Equals(sku) && x.ActiveFlag == STATUS_ACTIVE);
                    if (iquery == null) { result = false; }
                }
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return result;
        }
        public async Task<bool> UpdateAsync(ProductDto dto)
        {
            bool result = false;
            try
            {
                var entity = this.UnitOfWork.GetSingle<Product>(x => x.Sku.Equals(dto.Sku) && x.ActiveFlag == STATUS_ACTIVE);
                if (entity != null)
                {
                    entity.IsTransferESL = (entity.Origin != dto.Origin || entity.BrandName != dto.BrandName) ? false : entity.IsTransferESL;
                    entity.Code = dto.Code;
                    entity.CompanyCode = dto.CompanyCode;
                    entity.Barcode = dto.Barcode;
                    entity.Sku = dto.Sku;
                    entity.Upc = dto.Upc;
                    entity.StoreCode = dto.StoreCode;
                    entity.Name = dto.Name;
                    entity.VariantName = dto.VariantName;
                    entity.VariantOption = dto.VariantOption;
                    entity.ExtendedName = dto.ExtendedName;
                    entity.BrandName = dto.BrandName;
                    entity.Description = dto.Description;
                    entity.Ingredients = dto.Ingredients;
                    entity.CategoryCode = !string.IsNullOrEmpty(dto.CategoryCode) ? dto.CategoryCode : null;
                    entity.Size = dto.Size;
                    entity.Weight = dto.Weight;
                    entity.Length = dto.Length;
                    entity.Width = dto.Width;
                    entity.Height = dto.Height;
                    entity.Volume = dto.Volume;
                    entity.MaxCartQuantity = dto.MaxCartQuantity;
                    entity.UnitCount = dto.UnitCount;
                    entity.UnitType = dto.UnitType != null ? dto.UnitType : string.Empty;
                    entity.Origin = dto.Origin;
                    entity.Grade = dto.Grade;
                    entity.TaxRate = dto.TaxRate;
                    entity.ImageLinks = dto.ImageLinks != null ? dto.ImageLinks : string.Empty;
                    entity.IsAgeGated = dto.IsAgeGated;
                    entity.IsChilled = dto.IsChilled;
                    entity.IsFrozen = dto.IsFrozen;
                    entity.IsPerishable = dto.IsPerishable;
                    //if (dto.Fulfillment.HasValue)
                    //    entity.Fulfillment = dto.Fulfillment;
                    entity.Type = dto.Type;
                    //entity.IsSyncProfit = dto.IsSyncProfit;
                    entity.Description = dto.Description;
                    entity.OrderNumber = dto.OrderNumber;
                    entity.URL = AppValue.ToUnsignString(dto.Description);
                    entity.UpdateBy = dto.UpdateBy;
                    entity.UpdateDate = DateTime.Now;
                    entity.IsTransfer = false;
                    entity.IsPublished = dto.IsPublished;
                    entity.SEODescription = dto.SEODescription;
                    entity.SEOKeywords = dto.SEOKeywords;
                    entity.SEOTitle = dto.SEOTitle;
                    entity.InternalNote = dto.InternalNote;
                    entity.VariantType = dto.VariantType;
                    entity.CustomerScope = dto.CustomerScope;
                    entity.Slug = dto.Slug;
                    entity.B2BTaxRate = dto.B2BTaxRate;
                    entity.ShowInProductList = dto.ShowInProductList;
                    entity.DisplayAsOos = dto.DisplayAsOos;
                    result = await this.UnitOfWork.UpdateAsync(entity);
                    //_inventoryDeltaBusiness.UpdateIsTransfer(entity.Sku);
                    if (result)
                    {
                        var productHistory = Mapper.Map<ProductHistoryDto>(entity);
                        productHistory.Action = (int)Common.History.Action.Update;
                        productHistory.Id = Guid.NewGuid();
                        productHistory.Source = "ProductMgmt/Update";
                        _productHistoryBusiness.Insert(productHistory);
                    }
                    _inventoryBusiness.UpdateIsTransfer(entity.Sku, Common.History.Action.Update, entity.IsSyncProfit ?? true);
                    _pricingBusiness.UpdateIsTransfer(entity.Sku);
                }
            }
            catch (Exception ex) { LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return result;
        }
        public bool Update(ProductDto dto, string fileName)
        {
            bool result = false;
            try
            {
                var entity = this.UnitOfWork.GetSingle<Product>(x => x.Sku.Equals(dto.Sku) && x.ActiveFlag == STATUS_ACTIVE);
                if (entity != null)
                {
                    using (var trans = this.UnitOfWork.BeginTransaction())
                    {
                        entity.IsTransferESL = (entity.Origin != dto.Origin || entity.BrandName != dto.BrandName) ? false : entity.IsTransferESL;
                        entity.CompanyCode = dto.CompanyCode;
                        entity.Code = dto.Code;
                        entity.StoreCode = dto.StoreCode;
                        entity.Name = dto.Name;
                        entity.VariantName = dto.VariantName;
                        entity.VariantOption = dto.VariantOption;
                        entity.ExtendedName = dto.ExtendedName;
                        entity.BrandName = dto.BrandName;
                        entity.Description = dto.Description;
                        entity.Ingredients = dto.Ingredients;
                        entity.CategoryCode = !string.IsNullOrEmpty(dto.CategoryCode) ? dto.CategoryCode : null;
                        entity.Size = dto.Size;
                        entity.Weight = dto.Weight;
                        entity.Length = dto.Length;
                        entity.Width = dto.Width;
                        entity.Height = dto.Height;
                        entity.Volume = dto.Volume;
                        entity.MaxCartQuantity = dto.MaxCartQuantity;
                        entity.UnitCount = dto.UnitCount;
                        entity.UnitType = dto.UnitType != null ? dto.UnitType : string.Empty;
                        entity.Origin = dto.Origin;
                        entity.Grade = dto.Grade;
                        entity.ImageLinks = dto.ImageLinks != null ? dto.ImageLinks : string.Empty;
                        entity.IsAgeGated = dto.IsAgeGated;
                        entity.IsChilled = dto.IsChilled;
                        entity.IsFrozen = dto.IsFrozen;
                        entity.IsPerishable = dto.IsPerishable;
                        //if (dto.Fulfillment.HasValue)
                        //    entity.Fulfillment = dto.Fulfillment;
                        entity.SEODescription = dto.SEODescription;
                        entity.SEOKeywords = dto.SEOKeywords;
                        entity.SEOTitle = dto.SEOTitle;
                        entity.Description = dto.Description;
                        entity.OrderNumber = dto.OrderNumber;
                        entity.URL = string.Empty;
                        entity.UpdateBy = dto.UpdateBy;
                        entity.UpdateDate = DateTime.Now;
                        entity.InternalNote = dto.InternalNote;
                        entity.VariantType = dto.VariantType;
                        entity.CustomerScope = dto.CustomerScope;
                        entity.Slug = dto.Slug;
                        entity.ShowInProductList = dto.ShowInProductList;
                        entity.DisplayAsOos = dto.DisplayAsOos;
                        entity.IsTransfer = false;
                        entity.B2BTaxRate = dto.B2BTaxRate;
                        result = this.UnitOfWork.Update(entity);
                        //_inventoryDeltaBusiness.UpdateIsTransfer(entity.Sku);
                        if (result)
                        {
                            var productHistory = Mapper.Map<ProductHistoryDto>(entity);
                            productHistory.Action = (int)Common.History.Action.Import;
                            productHistory.Id = Guid.NewGuid();
                            productHistory.Source = fileName;
                            _productHistoryBusiness.Insert(productHistory);
                        }
                        _inventoryBusiness.UpdateIsTransfer(entity.Sku, Common.History.Action.Update);
                        _pricingBusiness.UpdateIsTransfer(entity.Sku);

                        if (result)
                        {
                            trans.Commit();
                        }
                        else
                            this.UnitOfWork.Rollback(trans);
                    }
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
                var entity = this.UnitOfWork.GetSingle<Product>(x => x.Id.Equals(id) && x.ActiveFlag == STATUS_ACTIVE);
                if (entity != null)
                {
                    entity.UpdateDate = DateTime.Now;
                    entity.UpdateBy = userName;
                    entity.ActiveFlag = STATUS_DEACTIVE;
                    result = await this.UnitOfWork.UpdateAsync(entity);
                    if (result)
                    {
                        var productHistory = Mapper.Map<ProductHistoryDto>(entity);
                        productHistory.Action = (int)Common.History.Action.Delete;
                        productHistory.Id = Guid.NewGuid();
                        productHistory.Source = "ProductMgmt/Delete";
                        _productHistoryBusiness.Insert(productHistory);
                    }
                }
            }
            catch (Exception ex) { LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return result;
        }
        public List<ProductExportDto> Export(string keyWord, List<string> categoryIds, string categoryIdIsNull, bool? isPublished, bool? isAgeGated, bool? isChilled, bool? isFrozen, bool? isPerishable, string brand, List<string> storeCodes, List<byte?> fulfillment, bool? isSyncProfit, bool? concess, bool? outright, string variantType, string customerScope, string slug, bool? showInProductList, bool? displayAsOos, bool? mommyItem, bool? aeonCardItem, bool? quickDelivery)
        {
            string sql = "";
            try
            {
                sql = "EXEC [prod].[SP_PRODUCT_EXPORT]";
                #region Build query
                if (!string.IsNullOrEmpty(keyWord))
                {
                    keyWord = keyWord.Replace("'", "''");
                    sql += $" @keyword = N'{keyWord.Trim()}',";
                }
                if (categoryIds?.Count > 0)
                {
                    categoryIds = categoryIds.Where(x => !string.IsNullOrEmpty(x)).ToList();
                    if (categoryIds.Count > 0)
                    {
                        sql += $" @categoryIds = '{string.Join(",", categoryIds)}',";
                    }
                }
                if (isPublished.HasValue)
                {
                    sql += $" @isPublished = '{(isPublished.Value ? "1" : "0")}',";
                }
                if (isAgeGated.HasValue && isAgeGated.Value)
                {
                    sql += $" @isAgeGated = '{(isAgeGated.Value ? "1" : "0")}',";
                }
                if (isChilled.HasValue && isChilled.Value)
                {
                    sql += $" @isChilled = '{(isChilled.Value ? "1" : "0")}',";
                }
                if (isFrozen.HasValue && isFrozen.Value)
                {
                    sql += $" @isFrozen = '{(isFrozen.Value ? "1" : "0")}',";
                }
                if (isPerishable.HasValue && isPerishable.Value)
                {
                    sql += $" @isPerishable = '{(isPerishable.Value ? "1" : "0")}',";
                }
                if (!string.IsNullOrEmpty(brand))
                {
                    brand = brand.Replace("'", "''");
                    sql += $" @brand = N'{brand}',";
                }
                if (storeCodes?.Count > 0)
                {
                    storeCodes = storeCodes.Where(x => !string.IsNullOrEmpty(x)).ToList();
                    if (storeCodes.Count > 0)
                    {
                        sql += $" @storeCodes = '{string.Join(",", storeCodes)}',";
                    }
                }
                if (fulfillment?.Count > 0 && !fulfillment.Contains(null))
                {
                    sql += $" @fulfillment = '{string.Join(",", fulfillment)}',";
                }
                if (isSyncProfit.HasValue)
                {
                    sql += $" @isSyncProfit = '{(isSyncProfit.Value ? "1" : "0")}',";
                }
                if (concess.HasValue && outright.HasValue)
                {
                    sql += $" @product_types = '{(byte)ProductType.Consignment},{(byte)ProductType.Outright}',";

                }
                else if (concess.HasValue)
                {
                    sql += $" @product_types = '{(byte)ProductType.Consignment}',";

                }
                else if (outright.HasValue)
                {
                    sql += $" @product_types = '{(byte)ProductType.Outright}',";
                }

                if (!string.IsNullOrEmpty(variantType))
                {
                    sql += $" @variantType = N'{variantType}',";
                }
                if (!string.IsNullOrEmpty(customerScope))
                {
                    sql += $" @customerScope = N'{customerScope}',";
                }
                if (!string.IsNullOrEmpty(slug))
                {
                    sql += $" @slug = N'{slug}',";
                }
                if (showInProductList.HasValue)
                {
                    sql += $" @showInProductList = '{(showInProductList.Value ? "1" : "0")}',";
                }
                if (displayAsOos.HasValue)
                {
                    sql += $" @displayAsOos = '{(displayAsOos.Value ? "1" : "0")}',";
                }
                if (mommyItem.HasValue)
                {
                    sql += $" @mommyItem = '{(mommyItem.Value ? "1" : "0")}',";
                }
                if (aeonCardItem.HasValue)
                {
                    sql += $" @aeonCardItem = '{(aeonCardItem.Value ? "1" : "0")}',";
                }
                if (quickDelivery.HasValue)
                {
                    sql += $" @quickDelivery = '{(quickDelivery.Value ? "1" : "0")}',";
                }

                sql = sql.TrimEnd(',');
                #endregion
                var data = this.UnitOfWork.SqlQuery<ProductExportDto>(sql).ToList();
                return data;
            }
            catch (Exception ex)
            {
                LogError(MethodBase.GetCurrentMethod().DeclaringType.Name + $" | sql = {sql}| ", ex);
            }
            return new List<ProductExportDto>();
        }
        #region Old code

        //public async Task<List<ProductExportDto>> ExportAsync(string keyWord, List<string> categoryIds, string categoryIdIsNull, bool? isPublished, bool? isAgeGated, bool? isChilled, bool? isFrozen, bool? isPerishable, string brand, List<string> storeCodes, List<byte?> fulfillment, bool? isSyncProfit, bool? concess, bool? outright)
        //{
        //    try
        //    {
        //        var iquery = this.UnitOfWork.GetAll<Product>().Where(x => x.ActiveFlag == STATUS_ACTIVE);
        //        var productInfos = this.UnitOfWork.GetAll<ProductInfo>().Where(x => x.ActiveFlag == STATUS_ACTIVE);
        //        #region Build query
        //        if (!string.IsNullOrEmpty(keyWord))
        //        {
        //            var keyTrim = keyWord.Trim().ToLower();
        //            iquery = iquery.Where(x => x.Code.ToLower().Equals(keyTrim)
        //                || x.Name.ToLower().Contains(keyTrim)
        //                || x.Description.ToLower().Contains(keyTrim)
        //                || x.CompanyCode.ToLower().Contains(keyTrim)
        //                || x.BrandName.ToLower().Contains(keyTrim)
        //                || x.ExtendedName.ToLower().Contains(keyTrim)
        //                || x.VariantName.ToLower().Contains(keyTrim)
        //                || x.Sku.ToLower().Equals(keyTrim)
        //                );
        //        }
        //        if (categoryIds?.Count > 0)
        //        {
        //            categoryIds = categoryIds.Where(x => !string.IsNullOrEmpty(x)).ToList();
        //            if (categoryIds.Count > 0)
        //            {
        //                if (categoryIds.Contains("null"))
        //                    iquery = iquery.Where(x => string.IsNullOrEmpty(x.CategoryCode));
        //                else if (categoryIds.Contains("notnull"))
        //                    iquery = iquery.Where(x => !string.IsNullOrEmpty(x.CategoryCode));
        //                else
        //                    iquery = iquery.Where(x => categoryIds.Contains(x.CategoryCode));
        //            }
        //        }
        //        if (isPublished.HasValue)
        //        {
        //            if (isPublished.Value)
        //            {
        //                var productInfo = productInfos.Where(x => x.IsPublished == isPublished).Select(x => x.Sku).Distinct();
        //                iquery = iquery.Where(x => productInfo.Contains(x.Sku));
        //            }
        //            else
        //            {
        //                var productInfo = productInfos.Where(x => x.IsPublished).Select(x => x.Sku).Distinct();
        //                iquery = iquery.Where(x => !productInfo.Contains(x.Sku));
        //            }

        //        }
        //        if (isAgeGated.HasValue && isAgeGated.Value)
        //        {
        //            iquery = iquery.Where(x => x.IsAgeGated == isAgeGated.Value);
        //        }
        //        if (isChilled.HasValue && isChilled.Value)
        //        {
        //            iquery = iquery.Where(x => x.IsChilled == isChilled.Value);
        //        }
        //        if (isFrozen.HasValue && isFrozen.Value)
        //        {
        //            iquery = iquery.Where(x => x.IsFrozen == isFrozen.Value);
        //        }
        //        if (isPerishable.HasValue && isPerishable.Value)
        //        {
        //            iquery = iquery.Where(x => x.IsPerishable == isPerishable.Value);
        //        }
        //        if (!string.IsNullOrEmpty(brand))
        //        {
        //            iquery = iquery.Where(x => x.BrandName == brand);
        //        }
        //        if (storeCodes?.Count > 0)
        //        {
        //            storeCodes = storeCodes.Where(x => !string.IsNullOrEmpty(x)).ToList();
        //            if (storeCodes.Count > 0)
        //            {
        //                var productInfo = productInfos.Where(x => storeCodes.Contains(x.StoreCode)).Select(x => x.Sku).Distinct();
        //                iquery = iquery.Where(x => productInfo.Contains(x.Sku));
        //            }
        //        }
        //        if (fulfillment?.Count > 0 && !fulfillment.Contains(null))
        //        {
        //            iquery = iquery.Where(x => fulfillment.Contains(x.Fulfillment.Value));
        //        }
        //        if (isSyncProfit.HasValue)
        //        {
        //            if (isSyncProfit == true)
        //            {
        //                var productInfo = productInfos.Where(x => x.IsSyncProfit == true).Select(x => x.Sku).Distinct();
        //                iquery = iquery.Where(x => productInfo.Contains(x.Sku));
        //            }
        //            else
        //            {
        //                var productInfo = productInfos.Where(x => x.IsSyncProfit != true).Select(x => x.Sku).Distinct();
        //                iquery = iquery.Where(x => productInfo.Contains(x.Sku));
        //            }
        //        }
        //        if (concess.HasValue && outright.HasValue)
        //        {
        //            iquery = iquery.Where(x => x.Type == (byte)ProductType.Consignment || x.Type == (byte)ProductType.Outright);
        //        }
        //        else if (concess.HasValue)
        //        {
        //            iquery = iquery.Where(x => x.Type == (byte)ProductType.Consignment);
        //        }
        //        else if (outright.HasValue)
        //        {
        //            iquery = iquery.Where(x => x.Type == (byte)ProductType.Outright);
        //        }
        //        #endregion

        //        var data = await iquery
        //            .OrderByDescending(x => x.UpdateDate)
        //            .Select(x => new ProductExportDto()
        //            {
        //                company_code = x.CompanyCode,
        //                product_id = x.Code,
        //                product_name_vi = x.Name,
        //                variant_options_vi = x.VariantOption,
        //                sku = x.Sku,
        //                variant_name_vi = x.VariantName,
        //                extended_name_vi = x.ExtendedName,
        //                brand_name_vi = x.BrandName,
        //                long_description_vi = x.Description,
        //                ingredients_vi = x.Ingredients,
        //                category_id = x.CategoryCode,
        //                upcs = x.Upc,
        //                barcode = x.Barcode,
        //                weight = x.Weight,
        //                product_size = x.Size,
        //                length = x.Length,
        //                width = x.Width,
        //                height = x.Height,
        //                volume = x.Volume,
        //                max_cart_quantity = x.MaxCartQuantity,
        //                unit_count = x.UnitCount,
        //                unit_type = x.UnitType,
        //                product_origin = x.Origin,
        //                product_grade = x.Grade,
        //                tax_rate = x.TaxRate,
        //                image_links = x.ImageLinks,
        //                is_age_gated = x.IsAgeGated ? "1" : "0",
        //                is_chilled = x.IsChilled ? "1" : "0",
        //                is_frozen = x.IsFrozen ? "1" : "0",
        //                is_published = x.IsPublished.HasValue && x.IsPublished.Value ? "1" : "0",
        //                fulfillment_method = x.Fulfillment.HasValue ? SqlFunctions.StringConvert((decimal)x.Fulfillment.Value).Trim() : string.Empty,
        //                is_perishable = x.IsPerishable.HasValue ? (x.IsPerishable.Value ? "1" : "0") : "0",
        //                //is_sync_profit = x.IsSyncProfit.HasValue ? (x.IsSyncProfit.Value ? "1" : "0") : "0",
        //                is_sync_profit = productInfos.Any(productInfo => productInfo.Sku == x.Sku && productInfo.IsSyncProfit == true) ? "1" : "0",
        //                seo_title = x.SEOTitle,
        //                seo_description = x.SEODescription,
        //                seo_keyword = x.SEOKeywords,

        //                product_status = productInfos.Any(productInfo => productInfo.Sku == x.Sku && productInfo.IsPublished == true) ? "Published" : "Unpublished",

        //                _category_code = x.CategoryCode,
        //                _type = x.Type ?? (byte)ProductType.Consignment
        //            })
        //            .ToListAsync();

        //        #region Build result
        //        //if (data?.Count > 0)
        //        //{
        //        //    var _category_codes = data.Select(product => product._category_code).ToList();
        //        //    var categories = this.UnitOfWork.GetAllNoTracking<Category>().Where(x => x.ActiveFlag == STATUS_ACTIVE && _category_codes.Contains(x.Code)).ToList();

        //        //    var _categoryIds = categories.Select(x => x.Id).ToList();
        //        //    var categoryMappings = this.UnitOfWork.GetAllNoTracking<Mapping>().Where(x => x.ActiveFlag == STATUS_ACTIVE && _categoryIds.Contains(x.CategoryId)).ToList();

        //        //    var categoryMasterIds = categoryMappings.Select(x => x.CategoryMasterId).ToList();
        //        //    var categoryMasters = this.UnitOfWork.GetAllNoTracking<CategoryMaster>().Where(x => x.ActiveFlag == STATUS_ACTIVE && categoryMasterIds.Contains(x.Id)
        //        //    )
        //        //        .Include(x => x.DepartmentMaster)
        //        //            .Include(x => x.DepartmentMaster.GroupMaster)
        //        //            .Include(x => x.DepartmentMaster.GroupMaster.DivisionMaster)
        //        //            .Include(x => x.DepartmentMaster.GroupMaster.DivisionMaster.LineMaster)
        //        //        .ToList();

        //        //    foreach (var item in data)
        //        //    {
        //        //        #region Category
        //        //        var _category = categories.Where(x => x.Code == item._category_code)
        //        //            .Select(x => new CategoryMgmtDto.CategoryCompactDto()
        //        //            {
        //        //                Id = x.Id
        //        //            }).FirstOrDefault();
        //        //        if (_category == null) continue;
        //        //        #endregion

        //        //        #region Mapping
        //        //        var _categoryMasterIds = categoryMappings.Where(x => x.CategoryId == _category.Id).Select(x => x.CategoryMasterId).ToList();
        //        //        #endregion

        //        //        #region CategoryMaster
        //        //        var _categoryMasters = categoryMasters.Where(x => _categoryMasterIds.Contains(x.Id))
        //        //            .ToList();

        //        //        item.category = string.Join(";", _categoryMasters.Select(x => x.Description).Distinct());
        //        //        #endregion

        //        //        #region DepartmentMasters
        //        //        item.department = string.Join(";", _categoryMasters.Select(x => x.DepartmentMaster.Description).Distinct());
        //        //        #endregion

        //        //        #region GroupMaster
        //        //        item.group = string.Join(";", _categoryMasters.Select(x => x.DepartmentMaster.GroupMaster.Description).Distinct());
        //        //        #endregion

        //        //        #region DivisionMaster
        //        //        item.division = string.Join(";", _categoryMasters.Select(x => x.DepartmentMaster.GroupMaster.DivisionMaster.Description).Distinct());
        //        //        #endregion

        //        //        #region LineMaster
        //        //        item.line = string.Join(";", _categoryMasters.Select(x => x.DepartmentMaster.GroupMaster.DivisionMaster.LineMaster.Description).Distinct());
        //        //        #endregion

        //        //        #region ProductType
        //        //        item.product_type = AppValue.ProductTypeToString(item._type.HasValue ? (ProductType)item._type : ProductType.Consignment);
        //        //        #endregion

        //        //        #region StoreCode
        //        //        item.store = string.Join(";", productInfos.Where(productInfo => productInfo.Sku == item.sku).OrderBy(productInfo => productInfo.StoreCode).Select(x => x.StoreCode).Distinct());
        //        //        #endregion
        //        //    }
        //        //}
        //        #endregion

        //        return data;
        //    }
        //    catch (Exception ex)
        //    {
        //        LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
        //    }
        //    return new List<ProductExportDto>();
        //}
        #endregion

        public List<InventoryExportDto> ExportInventory(string keyWord, List<string> categoryIds, string categoryIdIsNull, bool? isPublished, bool? isAgeGated, bool? isChilled, bool? isFrozen, bool? isPerishable, string brand, List<string> storeCodes, List<byte?> fulfillment, bool? isSyncProfit, bool? concess, bool? outright, string variantType, string customerScope, string slug, bool? showInProductList, bool? displayAsOos, bool? mommyItem, bool? aeonCardItem, bool? quickDelivery)
        {
            try
            {
                string sql = "EXEC [prod].[SP_Inventory_EXPORT]";
                #region Build query
                if (!string.IsNullOrEmpty(keyWord))
                {
                    keyWord = keyWord.Replace("'", "''");
                    sql += $" @keyword = N'{keyWord.Trim()}',";
                }
                if (categoryIds?.Count > 0)
                {
                    categoryIds = categoryIds.Where(x => !string.IsNullOrEmpty(x)).ToList();
                    if (categoryIds.Count > 0)
                    {
                        sql += $" @categoryIds = '{string.Join(",", categoryIds)}',";
                    }
                }
                if (isPublished.HasValue)
                {
                    sql += $" @isPublished = '{(isPublished.Value ? "1" : "0")}',";
                }
                if (isAgeGated.HasValue && isAgeGated.Value)
                {
                    sql += $" @isAgeGated = '{(isAgeGated.Value ? "1" : "0")}',";
                }
                if (isChilled.HasValue && isChilled.Value)
                {
                    sql += $" @isChilled = '{(isChilled.Value ? "1" : "0")}',";
                }
                if (isFrozen.HasValue && isFrozen.Value)
                {
                    sql += $" @isFrozen = '{(isFrozen.Value ? "1" : "0")}',";
                }
                if (isPerishable.HasValue && isPerishable.Value)
                {
                    sql += $" @isPerishable = '{(isPerishable.Value ? "1" : "0")}',";
                }
                if (!string.IsNullOrEmpty(brand))
                {
                    brand = brand.Replace("'", "''");
                    sql += $" @brand = N'{brand}',";
                }
                if (storeCodes?.Count > 0)
                {
                    storeCodes = storeCodes.Where(x => !string.IsNullOrEmpty(x)).ToList();
                    if (storeCodes.Count > 0)
                    {
                        sql += $" @storeCodes = '{string.Join(",", storeCodes)}',";
                    }
                }
                if (fulfillment?.Count > 0 && !fulfillment.Contains(null))
                {
                    sql += $" @fulfillment = '{string.Join(",", fulfillment)}',";
                }
                if (isSyncProfit.HasValue)
                {
                    sql += $" @isSyncProfit = '{(isSyncProfit.Value ? "1" : "0")}',";
                }
                if (concess.HasValue && outright.HasValue)
                {
                    sql += $" @product_types = '{(byte)ProductType.Consignment},{(byte)ProductType.Outright}',";

                }
                else if (concess.HasValue)
                {
                    sql += $" @product_types = '{(byte)ProductType.Consignment}',";

                }
                else if (outright.HasValue)
                {
                    sql += $" @product_types = '{(byte)ProductType.Outright}',";
                }

                if (!string.IsNullOrEmpty(variantType))
                {
                    sql += $" @variantType = N'{variantType}',";
                }
                if (!string.IsNullOrEmpty(customerScope))
                {
                    sql += $" @customerScope = N'{customerScope}',";
                }
                if (!string.IsNullOrEmpty(slug))
                {
                    sql += $" @slug = N'{slug}',";
                }
                if (showInProductList.HasValue)
                {
                    sql += $" @showInProductList = '{(showInProductList.Value ? "1" : "0")}',";
                }
                if (displayAsOos.HasValue)
                {
                    sql += $" @displayAsOos = '{(displayAsOos.Value ? "1" : "0")}',";
                }
                if (mommyItem.HasValue)
                {
                    sql += $" @mommyItem = '{(mommyItem.Value ? "1" : "0")}',";
                }
                if (aeonCardItem.HasValue)
                {
                    sql += $" @aeonCardItem = '{(aeonCardItem.Value ? "1" : "0")}',";
                }
                if (quickDelivery.HasValue)
                {
                    sql += $" @quickDelivery = '{(quickDelivery.Value ? "1" : "0")}',";
                }

                sql = sql.TrimEnd(',');
                #endregion
                var data = this.UnitOfWork.SqlQuery<InventoryExportDto>(sql).ToList();
                return data;
            }
            catch (Exception ex)
            {
                LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
            }
            return new List<InventoryExportDto>();
        }

        public async Task<Tuple<int, List<ProductInventoryAndPricingDto>>> GetInventoryPagingAsync(string sku, bool isAdmin, List<string> storeCodes, int pageIndex, int pageSize)
        {
            int total = 0;
            try
            {
                var iqueryPricing = this.UnitOfWork.GetAll<Pricing>().Where(x => x.Sku == sku && x.ActiveFlag == STATUS_ACTIVE);
                var iqueryInventory = this.UnitOfWork.GetAll<Inventory>().Where(x => x.Sku == sku && x.ActiveFlag == STATUS_ACTIVE);
                var iqueryMall = this.UnitOfWork.GetAll<Mall>().Where(x => x.ActiveFlag == STATUS_ACTIVE);
                var iqueryStore = this.UnitOfWork.GetAll<Store>().Where(x => x.ActiveFlag == STATUS_ACTIVE);
                var iqueryProductInfo = this.UnitOfWork.GetAll<ProductInfo>().Where(x => x.Sku == sku
                && x.ActiveFlag == STATUS_ACTIVE);

                var joinquery = from pr in iqueryPricing
                                join iv in iqueryInventory
                                on new { sku = pr.Sku, storeCode = pr.StoreCode } equals new { sku = iv.Sku, storeCode = iv.StoreCode }
                                select new ProductInventoryAndPricingDto
                                {
                                    StoreID = pr.StoreCode,
                                    StoreName = pr.StoreName,
                                    StockOnHandQty = iv.Quantity,
                                    Price = pr.Price,
                                    SellingPrice = (double)pr.SalePrice
                                };
                if (!isAdmin)
                {
                    iqueryProductInfo = iqueryProductInfo.Where(x => storeCodes.Contains(x.StoreCode));
                    joinquery = joinquery.Where(x => storeCodes.Contains(x.StoreID));
                }

                var data = await joinquery
                                 .OrderBy(x => x.StoreID)
                                 .Skip((pageIndex - 1) * pageSize)
                                 .Take(pageSize)
                                 .ToListAsync();

                var lst = new List<ProductInventoryAndPricingDto>();
                foreach (var item in data)
                {
                    var store = iqueryStore.Where(st => st.Code == item.StoreID).FirstOrDefault();
                    if (store != null)
                    {
                        var mall = iqueryMall.Where(m => m.Code == store.MallCode).FirstOrDefault();
                        var productInfo = iqueryProductInfo.Where(m => m.StoreCode == store.Code).FirstOrDefault();
                        item.MallCode = mall?.Code;
                        item.MallName = mall?.Name;
                        item.StoreName = store?.Name;
                        if (productInfo != null)
                        {
                            item.ManualQuantity = productInfo.Inventory.HasValue ? productInfo.Inventory.Value : 0;
                            item.StockBuffer = productInfo.StockBuffer;
                            item.IsPublished = productInfo.IsPublished ? productInfo.IsPublished : false;
                            item.IsSyncProfit = productInfo.IsSyncProfit.GetValueOrDefault();
                            item.QuickDelivery = productInfo.QuickDelivery;
                        }
                    }
                }
                total = data.Count;

                return new Tuple<int, List<ProductInventoryAndPricingDto>>(total, data);
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return new Tuple<int, List<ProductInventoryAndPricingDto>>(total, new List<ProductInventoryAndPricingDto>());
        }
        public List<ProductDto> GetAllNoTracking()
        {
            try
            {
                var iquery = this.UnitOfWork.GetAllNoTracking<Product>()
                    .Where(x => x.ActiveFlag == STATUS_ACTIVE)
                    .ToList();

                var mapped = Mapper.Map<List<ProductDto>>(iquery);
                return mapped;
            }
            catch (Exception ex)
            {
                LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
            }
            return new List<ProductDto>();
        }
        public List<string> GetAllSKU()
        {
            try
            {
                return this.UnitOfWork.GetAllNoTracking<Product>()
                    .Where(x => x.ActiveFlag == STATUS_ACTIVE).Select(x => x.Sku)
                    .ToList();
            }
            catch (Exception ex)
            {
                LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
            }
            return new List<string>();
        }
        public bool UpdateCurrentUploadMonitor(string user, UploadMonitor uploadMonitor, int current)
        {
            try
            {
                uploadMonitor.Curent = current.ToString();

                bool updated = this.UnitOfWork.Update(uploadMonitor, new List<System.Linq.Expressions.Expression<Func<UploadMonitor, object>>>() { x => x.Curent });
                return updated;
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return false;
        }
        public bool UpdateCurrentManualStockUploadMonitor(string user, ProductInfoUploadMonitor uploadMonitor, int current)
        {
            try
            {
                uploadMonitor.Curent = current.ToString();

                bool updated = this.UnitOfWork.Update(uploadMonitor, new List<System.Linq.Expressions.Expression<Func<ProductInfoUploadMonitor, object>>>() { x => x.Curent });
                return updated;
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return false;
        }
        public bool InsertUploadError(string user, ProductUploadErrorDto dto)
        {
            try
            {
                dto.SetDefaultValueInsert();
                dto.CreateBy = user;
                dto.UpdateBy = user;

                var entity = new UploadError()
                {
                    Id = dto.Id,
                    UploadId = dto.UploadId,
                    ProductId = dto.ProductId,
                    Infor = dto.Infor,
                    CreateBy = dto.CreateBy,
                    UpdateBy = dto.UpdateBy,
                    CreateDate = dto.CreateDate,
                    UpdateDate = dto.UpdateDate,
                    Description = dto.Description,
                    OrderNumber = dto.OrderNumber,
                    URL = dto.URL,
                    ActiveFlag = (byte)dto.ActiveFlag,
                    Current = dto.Current,
                };

                var add = this.UnitOfWork.Insert(entity);
                if (add != null) { return true; }
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return false;
        }
        public bool InsertManualStockUploadError(string user, ProductInfoUploadErrorDto dto)
        {
            try
            {
                dto.SetDefaultValueInsert();
                dto.CreateBy = user;
                dto.UpdateBy = user;

                var entity = new ProductInfoUploadError()
                {
                    Id = dto.Id,
                    UploadId = dto.UploadId,
                    Sku = dto.Sku,
                    Infor = dto.Infor,
                    CreateBy = dto.CreateBy,
                    UpdateBy = dto.UpdateBy,
                    CreateDate = dto.CreateDate,
                    UpdateDate = dto.UpdateDate,
                    Description = dto.Description,
                    OrderNumber = dto.OrderNumber,
                    URL = dto.URL,
                    ActiveFlag = (byte)dto.ActiveFlag
                };

                var add = this.UnitOfWork.Insert(entity);
                if (add != null) { return true; }
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return false;
        }
        public UploadMonitor InsertUploadMonitor(string user, ProductUploadMonitorDto dto)
        {
            try
            {
                dto.SetDefaultValueInsert();
                dto.CreateBy = user;
                dto.UpdateBy = user;

                var entity = new UploadMonitor()
                {
                    Id = dto.Id,
                    FileName = dto.FileName,
                    FileContent = dto.FileContent,
                    FileExt = dto.FileExt,
                    TotalRow = dto.TotalRow,
                    Curent = dto.Curent,
                    CreateBy = dto.CreateBy,
                    UpdateBy = dto.UpdateBy,
                    CreateDate = dto.CreateDate,
                    UpdateDate = dto.UpdateDate,
                    Description = dto.Description,
                    OrderNumber = dto.OrderNumber,
                    URL = dto.URL,
                    ActiveFlag = (byte)dto.ActiveFlag
                };

                var add = this.UnitOfWork.Insert(entity);
                if (add != null) { return add; }
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return null;
        }
        public ProductInfoUploadMonitor InsertManualStockUploadMonitor(string user, ProductInfoUploadMonitorDto dto)
        {
            try
            {
                dto.SetDefaultValueInsert();
                dto.CreateBy = user;
                dto.UpdateBy = user;

                var entity = new ProductInfoUploadMonitor()
                {
                    Id = dto.Id,
                    FileName = dto.FileName,
                    FileContent = dto.FileContent,
                    FileExt = dto.FileExt,
                    TotalRow = dto.TotalRow,
                    Curent = dto.Curent,
                    CreateBy = dto.CreateBy,
                    UpdateBy = dto.UpdateBy,
                    CreateDate = dto.CreateDate,
                    UpdateDate = dto.UpdateDate,
                    Description = dto.Description,
                    OrderNumber = dto.OrderNumber,
                    URL = dto.URL,
                    ActiveFlag = (byte)dto.ActiveFlag
                };

                var add = this.UnitOfWork.Insert(entity);
                if (add != null) { return add; }
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return null;
        }
        public bool Publish(List<ProductInfo> listInfo, bool isPublished, string userName)
        {
            bool result = false;
            try
            {
                if (listInfo != null && listInfo.Count > 0)
                {
                    List<ProductInfoHistoryDto> productHistorylst = new List<ProductInfoHistoryDto>();
                    foreach (var entity in listInfo)
                    {
                        var inventoryEntityOld = this.UnitOfWork.GetAll<Inventory>(x => x.Sku.Trim() == entity.Sku && x.StoreCode == entity.StoreCode && x.ActiveFlag == STATUS_ACTIVE).FirstOrDefault();
                        entity.IsTransfer = false;
                        entity.UpdateDate = DateTime.Now;
                        entity.UpdateBy = userName;
                        if (isPublished && entity.IsPublished == false)
                        {
                            entity.IsNew = true;
                        }

                        #region hotfix trường hợp từ Publish - > Unpublished chỉ gửi 1 lần
                        if (inventoryEntityOld != null)
                        {
                            if (entity.IsPublished && !isPublished)
                                inventoryEntityOld.IsTransfer = false;
                        }
                        #endregion
                        if (entity.IsPublished != isPublished)
                        {
                            var pInfo = Mapper.Map<ProductInfoHistoryDto>(entity);
                            pInfo.IsPublished = isPublished;
                            productHistorylst.Add(pInfo);
                        }
                        entity.IsPublished = isPublished;

                    }
                    result = this.UnitOfWork.UpdateToList(listInfo);
                    if (result)
                    {
                        //var productHistorylst = Mapper.Map<List<ProductInfoHistoryDto>>(listInfo);
                        productHistorylst.ForEach(x =>
                        {
                            x.Action = isPublished ? (int)Common.History.Action.Publish : (int)Common.History.Action.UnPublish;
                            x.Source = "ProductMgmt/publish";
                            x.Id = Guid.NewGuid();
                            x.UpdateDate = DateTime.Now;
                            x.UpdateBy = userName;
                        });
                        _productInfoHistoryBusiness.InsertList(productHistorylst);
                    }
                    var action = isPublished ? Common.History.Action.Publish : Common.History.Action.UnPublish;
                    _inventoryBusiness.UpdateIsTransfer(listInfo[0].Sku, action);
                }
            }
            catch (Exception ex) { LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return result;
        }
        public bool PublishSku(string sku, bool isPublished)
        {
            bool result = false;
            try
            {
                var entity = this.UnitOfWork.GetSingle<ProductInfo>(x => x.Sku.Equals(sku) && x.ActiveFlag == STATUS_ACTIVE);
                if (entity != null)
                {
                    entity.IsPublished = isPublished;
                    entity.IsTransfer = false;
                    result = this.UnitOfWork.Update(entity);
                    if (result)
                    {
                        var productHistory = Mapper.Map<ProductInfoHistoryDto>(entity);
                        productHistory.Id = Guid.NewGuid();
                        productHistory.UpdateDate = DateTime.Now;
                        productHistory.Action = isPublished ? (int)Common.History.Action.Publish : (int)Common.History.Action.UnPublish;
                        productHistory.Source = "ProductMgmt/Publish";
                        _productInfoHistoryBusiness.Insert(productHistory);
                    }
                }
            }
            catch (Exception ex) { LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return result;
        }
        public List<CategoriesDto> GetCategories()
        {
            var category = this.UnitOfWork.GetAll<Category>().Where(x => x.ActiveFlag == STATUS_ACTIVE);
            var data = category.Select(x => new CategoriesDto
            {
                Code = x.Code,
                Name = x.Name
            }).OrderBy(x => x.Code).ToList();
            return data;
        }
        public UploadMonitor GetUploadMonitor(Guid id)
        {
            try
            {
                var entity = this.UnitOfWork.GetSingle<UploadMonitor>(x => x.Id == id);
                return entity;
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return null;
        }
        public ProductInfoUploadMonitor GetManualStockUploadMonitor(Guid id)
        {
            try
            {
                var entity = this.UnitOfWork.GetSingle<ProductInfoUploadMonitor>(x => x.Id == id);
                return entity;
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return null;
        }
        public int GetCurrentUploadError(Guid uploadId)
        {
            try
            {
                var iquery = this.UnitOfWork.GetAll<UploadError>(x => x.UploadId == uploadId);
                if (iquery == null)
                    return 0;
                return iquery.Count();
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return 0;
        }
        public int GetCurrentManualStockUploadError(Guid uploadId)
        {
            try
            {
                var iquery = this.UnitOfWork.GetAll<ProductInfoUploadError>(x => x.UploadId == uploadId);
                if (iquery == null) return 0;
                return iquery.Count();
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return 0;
        }
        public List<UploadError> GetUploadErrors(Guid uploadId)
        {
            try
            {
                var uploadErrors = this.UnitOfWork.GetAll<UploadError>(x => x.UploadId == uploadId);
                return uploadErrors.ToList();
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return new List<UploadError>();
        }
        public bool UpdateIsManualStatus(string userName, string sku)
        {
            bool result = false;
            try
            {
                var entity = this.UnitOfWork.GetSingle<Product>(x => x.Sku.Equals(sku) && x.ActiveFlag == STATUS_ACTIVE);
                if (entity != null)
                {
                    entity.UpdateBy = userName;
                    entity.UpdateDate = DateTime.Now;
                    result = this.UnitOfWork.Update(entity);
                    if (result)
                    {
                        var productHistory = Mapper.Map<ProductHistoryDto>(entity);
                        productHistory.Action = (int)Common.History.Action.Update;
                        productHistory.Source = "ProductMgmt/UpdateIsManualStatus";
                        _productHistoryBusiness.Insert(productHistory);
                    }
                }
            }
            catch (Exception ex) { LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return result;
        }
        public bool UpdateProductTypeAsync(string userName, string sku, byte productType, string fileName)
        {
            bool result = false;
            try
            {
                var entity = this.UnitOfWork.GetSingle<Product>(x => x.Sku.Equals(sku) && x.ActiveFlag == STATUS_ACTIVE);
                if (entity != null)
                {
                    entity.Type = productType;

                    var productInfos = this.UnitOfWork.GetAll<ProductInfo>(x => x.Sku == entity.Sku).ToList();
                    productInfos.ForEach(x =>
                    {
                        var oldIsSyncProfit = x.IsSyncProfit;
                        x.IsSyncProfit = productType != (byte)ProductType.Consignment;
                        if (oldIsSyncProfit != x.IsSyncProfit)
                            x.IsTransfer = false;
                        x.UpdateDate = DateTime.Now;
                        x.UpdateBy = userName;
                    });
                    result = this.UnitOfWork.UpdateToList(productInfos);
                    if (result)
                    {
                        var productHistorylst = Mapper.Map<List<ProductInfoHistoryDto>>(productInfos);
                        productHistorylst.ForEach(x => { x.Action = (int)Common.History.Action.ImportProductType; x.Source = fileName; x.Id = Guid.NewGuid(); });
                        _productInfoHistoryBusiness.InsertList(productHistorylst);
                    }
                    //if (productType == (byte)ProductType.Consignment)
                    //{
                    //    entity.IsSyncProfit = false;                        
                    //}
                    //else
                    //{
                    //    entity.IsSyncProfit = true;
                    //}
                    entity.UpdateBy = userName;
                    entity.UpdateDate = DateTime.Now;
                    result = this.UnitOfWork.Update(entity);
                    if (result)
                    {
                        var productHistory = Mapper.Map<ProductHistoryDto>(entity);
                        productHistory.Action = (int)Common.History.Action.ImportProductType;
                        productHistory.Id = Guid.NewGuid();
                        productHistory.Source = fileName;
                        _productHistoryBusiness.Insert(productHistory);
                    }
                }
            }
            catch (Exception ex) { LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return result;
        }
        public async Task<bool> UpdateInventoryAndPublish(string user, string sku, List<InventoryAndPublishDto> listInventoryAndPublish, bool isAdmin, List<string> storeCodes)
        {
            var dbtransaction = this.UnitOfWork.BeginTransaction();
            bool result = false;
            try
            {
                var listEntityOld = this.UnitOfWork.GetAll<ProductInfo>(x => x.Sku.Trim() == sku.Trim() && x.ActiveFlag == STATUS_ACTIVE).ToList();
                var listInventoryEntityOld = this.UnitOfWork.GetAll<Inventory>(x => x.Sku.Trim() == sku.Trim() && x.ActiveFlag == STATUS_ACTIVE).ToList();

                var lstEntityNew = new List<ProductInfo>();
                foreach (var dto in listInventoryAndPublish)
                {
                    var oldEntity = listEntityOld.Find(x => x.Sku.Trim() == sku.Trim() && x.StoreCode.Trim() == dto.StoreCode.Trim() && x.ActiveFlag == STATUS_ACTIVE);

                    var oldInventory = listInventoryEntityOld.FirstOrDefault(x => x.Sku.Trim() == sku.Trim() && x.StoreCode.Trim() == dto.StoreCode.Trim());

                    var entityNew = new ProductInfo()
                    {
                        Id = Guid.NewGuid(),
                        StoreCode = dto.StoreCode,
                        MallCode = dto.MallCode,
                        Sku = dto.Sku,
                        ProductName = dto.ProductName,
                        Inventory = !dto.IsSyncProfit ? (dto.Inventory ?? 0) : (oldEntity?.Inventory ?? 0),
                        StockBuffer = dto.StockBuffer >= 0 ? dto.StockBuffer : (oldEntity?.StockBuffer ?? 0),
                        Pricing = dto.Pricing,
                        URL = string.Empty,
                        UpdateDate = DateTime.Now,
                        IsPublished = dto.IsPublished,
                        IsNew = true,
                        IsSyncProfit = dto.IsSyncProfit,
                        IsTransfer = true,
                        UpdateBy = user,
                        ActiveFlag = (int)AppValue.ActiveFlag.Active,
                        Fulfillment = dto.Fulfillment,
                        QuickDelivery = oldEntity.QuickDelivery
                    };

                    // chỉ cập nhật field Quick Delivery với điều kiện field Fulfillment (cột trước đó trong file) có giá trị là "0,1" (2) hoặc "1". Nếu dto.Fulfillment != 1,2 => Quick Delivery = false
                    // Quick Delivery rỗng: không update field này, giữ nguyên hiện trạng ở DB
                    if (dto.Fulfillment.HasValue)
                    {
                        if (!(dto.Fulfillment == 1 || dto.Fulfillment == 2))
                            entityNew.QuickDelivery = false;
                        else
                        {
                            if (dto.QuickDelivery.HasValue)
                                entityNew.QuickDelivery = dto.QuickDelivery.Value;
                        }
                    }

                    if (oldEntity != null)
                    {
                        if ((oldEntity.IsPublished != dto.IsPublished
                            || oldEntity.Inventory != dto.Inventory
                            || oldEntity.StockBuffer != dto.StockBuffer
                            || oldEntity.IsSyncProfit != dto.IsSyncProfit
                            || oldEntity.Fulfillment != dto.Fulfillment
                            ) && oldEntity.IsPublished)
                        {
                            entityNew.IsTransfer = false;
                        }
                        else
                            entityNew.IsTransfer = oldEntity.IsTransfer;

                        if ((oldEntity.IsPublished == false && dto.IsPublished == true)
                            || oldEntity.Fulfillment != dto.Fulfillment)
                        {
                            entityNew.IsNew = true;
                            entityNew.IsTransfer = false;
                        }
                        else
                        {
                            entityNew.IsNew = oldEntity.IsNew;
                        }
                        entityNew.IsSyncProfit = dto.IsSyncProfit;
                        entityNew.CreateBy = oldEntity.CreateBy ?? user;
                        entityNew.CreateDate = oldEntity.CreateDate;

                        #region hotfix trường hợp từ Publish - > Unpublished chỉ gửi 1 lần
                        if (oldEntity.IsPublished && !dto.IsPublished)
                            oldInventory.IsTransfer = false;
                        #endregion
                    }
                    else
                    {
                        entityNew.IsTransfer = false;
                        entityNew.CreateBy = user;
                        entityNew.CreateDate = DateTime.Now;
                    }
                    lstEntityNew.Add(entityNew);
                }

                if (!isAdmin)
                {
                    listEntityOld = listEntityOld.Where(x => storeCodes.Contains(x.StoreCode)).ToList();
                    lstEntityNew = lstEntityNew.Where(x => storeCodes.Contains(x.StoreCode)).ToList();
                }
                result = await this.UnitOfWork.DeleteToListAsync(listEntityOld, true);
                if (result)
                {
                    var data = await this.UnitOfWork.InsertToListAsync(lstEntityNew);
                    var changedEntities = lstEntityNew.Where(newEntity =>
                        {
                            var oldEntity = listEntityOld.FirstOrDefault(x => x.Sku == newEntity.Sku && x.StoreCode == newEntity.StoreCode);
                            return oldEntity == null || IsProductInfoDifferent(oldEntity, newEntity);
                        })
                        .ToList();
                    if (data != null && data?.Count > 0)
                    {
                        var productHistorylst = Mapper.Map<List<ProductInfoHistoryDto>>(changedEntities);
                        productHistorylst.ForEach(x => { x.Action = (int)Common.History.Action.Update; x.Source = "ProductMgmt/UpdateInventory"; x.Id = Guid.NewGuid(); });
                        _productInfoHistoryBusiness.InsertList(productHistorylst);
                        dbtransaction.Commit();
                        result = true;
                    }
                    else
                    {
                        dbtransaction.Rollback();
                    }
                }
                else
                {
                    dbtransaction.Rollback();
                }
            }
            catch (Exception ex)
            {
                LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
                this.UnitOfWork.Rollback(dbtransaction);
            }
            return result;
        }
        public List<ProductInfoDto> GetInventory(string sku, bool isAdmin, List<string> storeCodes)
        {
            try
            {
                var stores = this.UnitOfWork.GetAll<Store>().Where(x => x.ActiveFlag == STATUS_ACTIVE).ToList();
                var lstProductInfo = this.UnitOfWork.GetAll<ProductInfo>(x => x.Sku.Equals(sku) && x.ActiveFlag == STATUS_ACTIVE).AsEnumerable();
                if (!isAdmin)
                    lstProductInfo = lstProductInfo.Where(x => storeCodes.Contains(x.StoreCode));
                var data = lstProductInfo.Select(
                   x =>
                   {
                       var store = stores.Find(st => st.Code == x.StoreCode);
                       var inventory = GetInventoryByStoreCode(x.StoreCode, x.Sku);
                       return new ProductInfoDto()
                       {
                           StoreCode = store?.Code,
                           StoreName = store?.Name,
                           MallCode = store?.MallCode,
                           Inventory = x.Inventory ?? 0,
                           IsPublished = x.IsPublished,
                           InventoryQuantity = inventory != null ? inventory.Quantity : 0,
                           IsSyncProfit = x.IsSyncProfit.GetValueOrDefault(),
                           Fulfillment = x.Fulfillment ?? 1,
                           StockBuffer = x.StockBuffer,
                           QuickDelivery = x.QuickDelivery
                       };
                   })
               .OrderBy(x => x.StoreCode)
               .ToList();
                return data;
            }
            catch (Exception ex)
            {
                LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
            }
            return null;
        }
        public Inventory GetInventoryByStoreCode(string storeCode, string sku)
        {
            try
            {
                var entity = this.UnitOfWork.GetSingle<Inventory>(x => x.Sku.Equals(sku) && x.StoreCode.Trim() == storeCode.Trim() && x.ActiveFlag == STATUS_ACTIVE);
                return entity;
            }
            catch (Exception ex)
            {
                LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
            }
            return null;
        }
        public double? GetSellingPriceByStoreCode(string storeCode, string sku)
        {
            try
            {
                var entity = this.UnitOfWork.GetSingle<Pricing>(x => x.Sku.Equals(sku) && x.StoreCode.Trim() == storeCode.Trim() && x.ActiveFlag == STATUS_ACTIVE);
                return entity?.SalePrice;
            }
            catch (Exception ex)
            {
                LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
            }
            return null;
        }
        public async Task<Tuple<int, List<UpdateResultsDto>>> UpdateMonitorGetPagingAsync(string keyWord, int pageIndex, int pageSize, DateTime? fromDate, DateTime? toDate, string uploadBy)
        {
            try
            {
                string importProductType = ImportProductTypes.ProductType.ToString();
                var iquery = UnitOfWork.GetAllNoTracking<UploadMonitor>().Where(x => x.Description.ToUpper() == importProductType.ToUpper());
                if (!string.IsNullOrEmpty(keyWord))
                    iquery = iquery.Where(x => x.FileName.Contains(keyWord));
                if (fromDate.HasValue)
                {
                    var date = fromDate.Value.Date;
                    iquery = iquery.Where(x => x.CreateDate >= date);
                }
                if (toDate.HasValue)
                {
                    var date = toDate.Value.Date.AddHours(23).AddMinutes(59).AddSeconds(59);
                    iquery = iquery.Where(x => x.CreateDate <= date);
                }
                if (!string.IsNullOrEmpty(uploadBy))
                    iquery = iquery.Where(x => x.CreateBy == uploadBy);

                var count = iquery.Count();
                var result = await iquery.OrderByDescending(x => x.CreateDate)
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
                var mapped = Mapper.Map<List<UpdateResultsDto>>(result);
                if (mapped.Count > 0)
                {
                    var lstUserCreateByIds = mapped.Select(x => x.CreateBy).ToList();
                    var lstCreateBys = this.UnitOfWork.GetAll<UserInfo>()
                                     .Where(x => lstUserCreateByIds.Contains(x.UserId) && x.ActiveFlag == STATUS_ACTIVE)
                                     .Select(x => new UsersDto()
                                     {
                                         Id = x.Id,
                                         UserName = x.UserId,
                                         Email = x.Email,
                                         FullName = x.FullName
                                     })
                                     .ToList();
                    foreach (var item in mapped)
                    {
                        item.CreateByFullName = lstCreateBys.Where(x => x.UserName.ToLower().Equals(item.CreateBy.ToLower()))
                            .Select(x => x.FullName)
                            .FirstOrDefault();
                    }
                }
                return new Tuple<int, List<UpdateResultsDto>>(count, mapped);
            }
            catch (Exception ex)
            {
                LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
            }
            return new Tuple<int, List<UpdateResultsDto>>(0, new List<UpdateResultsDto>());
        }
        public async Task<Tuple<int, List<UpdateResultsDto>>> UpdateProductInfoMonitorGetPagingAsync(string keyWord, int pageIndex, int pageSize, DateTime? fromDate, DateTime? toDate, string uploadBy)
        {
            try
            {
                string type = AppType.ImportProductTypes.ManualStock.ToString();
                var iquery = UnitOfWork.GetAllNoTracking<ProductInfoUploadMonitor>().Where(x => x.Description == type);
                if (!string.IsNullOrEmpty(keyWord))
                    iquery = iquery.Where(x => x.FileName.Contains(keyWord));
                if (fromDate.HasValue)
                {
                    var date = fromDate.Value.Date;
                    iquery = iquery.Where(x => x.CreateDate >= date);
                }
                if (toDate.HasValue)
                {
                    var date = toDate.Value.Date.AddHours(23).AddMinutes(59).AddSeconds(59);
                    iquery = iquery.Where(x => x.CreateDate <= date);
                }
                if (!string.IsNullOrEmpty(uploadBy))
                    iquery = iquery.Where(x => x.CreateBy == uploadBy);

                var count = iquery.Count();
                var result = await iquery.OrderByDescending(x => x.CreateDate)
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
                var mapped = Mapper.Map<List<UpdateResultsDto>>(result);
                if (mapped.Count > 0)
                {
                    var lstUserCreateByIds = mapped.Select(x => x.CreateBy).ToList();
                    var lstCreateBys = this.UnitOfWork.GetAll<UserInfo>()
                                     .Where(x => lstUserCreateByIds.Contains(x.UserId) && x.ActiveFlag == STATUS_ACTIVE)
                                     .Select(x => new UsersDto()
                                     {
                                         Id = x.Id,
                                         UserName = x.UserId,
                                         Email = x.Email,
                                         FullName = x.FullName
                                     })
                                     .ToList();
                    foreach (var item in mapped)
                    {
                        item.CreateByFullName = lstCreateBys.Where(x => x.UserName.ToLower().Equals(item.CreateBy.ToLower()))
                            .Select(x => x.FullName)
                            .FirstOrDefault();
                    }
                }
                return new Tuple<int, List<UpdateResultsDto>>(count, mapped);
            }
            catch (Exception ex)
            {
                LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
            }
            return new Tuple<int, List<UpdateResultsDto>>(0, new List<UpdateResultsDto>());
        }
        public bool SyncProfit(Guid productId, bool isSync, string userName)
        {
            bool result = false;
            try
            {
                var entity = this.UnitOfWork.GetSingle<Product>(x => x.Id.Equals(productId));
                if (entity != null)
                {
                    List<ProductInfoHistoryDto> historylst = new List<ProductInfoHistoryDto>();
                    var productInfos = this.UnitOfWork.GetAll<ProductInfo>(x => x.Sku == entity.Sku).ToList();
                    productInfos.ForEach(x =>
                    {
                        if (x.IsSyncProfit != isSync)
                        {
                            historylst.Add(Mapper.Map<ProductInfoHistoryDto>(x));
                        }
                        x.IsSyncProfit = isSync;
                        x.UpdateDate = DateTime.Now;
                        x.UpdateBy = userName;
                    });
                    result = this.UnitOfWork.UpdateToList(productInfos);
                    if (result)
                    {
                        string method = MethodBase.GetCurrentMethod().Name;
                        historylst.ForEach(x => { x.Action = isSync ? (int)Common.History.Action.SyncProfit : (int)Common.History.Action.UnSyncProfit; x.Source = "ProductMgmt/syncprofit"; x.Id = Guid.NewGuid(); });
                        _productInfoHistoryBusiness.InsertList(historylst);
                    }
                    //entity.IsSyncProfit = isSync;
                    //entity.IsTransfer = true;
                    if (result)
                    {
                        entity.CreateDate = DateTime.Now;
                        entity.UpdateDate = DateTime.Now;
                        entity.UpdateBy = userName;
                        result = this.UnitOfWork.Update(entity);
                        if (result)
                        {
                            //var productHistory = Mapper.Map<ProductHistoryDto>(entity);
                            //productHistory.Id = Guid.NewGuid();
                            //productHistory.Action = isSync ? (int)Common.History.Action.SyncProfit : (int)Common.History.Action.UnSyncProfit;
                            //productHistory.Source = "ProductMgmt/SyncProfit";
                            //_productHistoryBusiness.Insert(productHistory);
                        }
                    }
                }
            }
            catch (Exception ex) { LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return result;
        }
        public bool CheckIsSyncProfit(string sku, string storeCode)
        {
            bool result = false;
            try
            {
                var entity = this.UnitOfWork.GetSingle<ProductInfo>(x => x.Sku.Equals(sku)
                && x.StoreCode.Equals(storeCode)
                && x.ActiveFlag == STATUS_ACTIVE);
                if (entity != null)
                {
                    result = entity.IsSyncProfit ?? false;
                }
            }
            catch (Exception ex) { LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return result;
        }
        public bool UpdateIsPublished(string sku)
        {
            bool result = false;
            try
            {
                var lstEntity = this.UnitOfWork.GetAllNoTracking<ProductInfo>().Where(x => x.Sku.Equals(sku) && x.ActiveFlag == STATUS_ACTIVE).ToList();
                List<ProductInfo> modifiedEntities = new List<ProductInfo>();
                if (lstEntity != null && lstEntity.Count > 0)
                {
                    lstEntity.ForEach(x =>
                    {
                        if (x.IsNew)
                        {
                            x.IsPublished = true;
                            modifiedEntities.Add(x);
                        }
                    });
                }
                this.UnitOfWork.UpdateToList(lstEntity);
                var lstHistory = Mapper.Map<List<ProductInfoHistoryDto>>(modifiedEntities);
                lstHistory.ForEach(x =>
                {
                    x.Action = (int)Common.History.Action.Publish;
                    x.Id = Guid.NewGuid(); x.Source = "Productmgmt/Publish";
                    x.UpdateDate = DateTime.Now;
                });
                _productInfoHistoryBusiness.InsertList(lstHistory);
            }
            catch (Exception ex) { LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return result;
        }
        public bool UpdateProductInfos(string sku, byte? Fulfillment, string userName, string fileName)
        {
            bool result = false;

            try
            {
                var lstEntity = this.UnitOfWork.GetAllNoTracking<ProductInfo>().Where(x => x.Sku.Equals(sku) && x.ActiveFlag == STATUS_ACTIVE).ToList();
                if (lstEntity != null && lstEntity.Count > 0)
                {
                    lstEntity.ForEach(x =>
                    {
                        if (Fulfillment.HasValue)
                        {
                            if (x.Fulfillment != Fulfillment && x.IsPublished == true)
                            {
                                x.IsNew = true;
                                x.IsTransfer = false;
                            }
                            x.Fulfillment = Fulfillment;
                        }
                        if (!x.Fulfillment.HasValue)
                            x.Fulfillment = (byte)FulfillmentTypes.Express;
                        if (x.IsNew && x.IsPublished == true)
                        {
                            x.IsPublished = true;
                        }
                    });
                }
                this.UnitOfWork.UpdateToList(lstEntity);
                var lstHistory = Mapper.Map<List<ProductInfoHistoryDto>>(lstEntity);
                //// Đây là hàm update Info, nhma được xài trong hàm Import nên action là import
                lstHistory.ForEach(x =>
                {
                    x.Action = (int)Common.History.Action.Import;
                    x.Id = Guid.NewGuid(); x.Source = fileName;
                    x.UpdateDate = DateTime.Now;
                    x.UpdateBy = userName;
                });
                _productInfoHistoryBusiness.InsertList(lstHistory);
            }
            catch (Exception ex)
            {
                LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
            }
            return result;
        }
        public bool ResetManualStock(DateTime dateTime, int timeOut)
        {
            try
            {
                var sql = BaseBusiness.SP_ProductInfo_ResetManualStock;
                var parameters = new Dictionary<string, object>();
                parameters.Add("@datetime", dateTime);
                return this.UnitOfWork.ExecuteNonQuery(sql, parameters, timeOut);
            }
            catch (Exception ex)
            {
                LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
            }
            return false;
        }
        public List<ProductInfo> GetProductInfoBySku(string sku)
        {
            try
            {
                var lstProductInfo = this.UnitOfWork.GetAll<ProductInfo>(x => x.Sku.Equals(sku) && x.ActiveFlag == STATUS_ACTIVE).ToList();
                return lstProductInfo;
            }
            catch (Exception ex)
            {
                LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
            }
            return null;
        }
        public InventoryAndPublishDto GetProductInfoBySkuAndStoreCode(string sku, string storeCode)
        {
            try
            {
                var productInfo = this.UnitOfWork.GetItem<InventoryAndPublishDto, ProductInfo>(x => x.Sku.Equals(sku) && x.StoreCode.Equals(storeCode) && x.ActiveFlag == STATUS_ACTIVE);
                return productInfo;
            }
            catch (Exception ex)
            {
                LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
            }
            return null;
        }
        public List<ProductInventoryPricingTaxDto> GetProductInfoByStoreCode(string storeCode)
        {
            try
            {
                var sql = $@"Exec {BaseBusiness.SP_B2B_GET_PRODUCT_BY_STORE}
                @storeCode = '{storeCode}'";
                var rs = this.UnitOfWork.SqlQuery<ProductInventoryPricingTaxDto>(sql, 120).ToList();
                if (rs != null)
                {
                    rs.ForEach(x =>
                    {
                        x.Sku = x.Sku?.Trim();
                        x.ProductName = x.ProductName?.Trim();
                        x.UnitType = x.UnitType?.Trim();
                    });
                }
                return rs;
            }
            catch (Exception ex)
            {
                LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
            }
            return new List<ProductInventoryPricingTaxDto>();
        }
        public ProductCompactDto GetProductBySku(string sku)
        {
            try
            {
                var product = this.UnitOfWork.GetItem<ProductCompactDto, Product>(x => x.Sku.Equals(sku) && x.ActiveFlag == STATUS_ACTIVE);
                return product;
            }
            catch (Exception ex)
            {
                LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
            }
            return null;
        }
        public bool UpdateManualExportZip(string userName)
        {
            bool result = true;
            try
            {
                var parameters = new Dictionary<string, object>();
                parameters.Add("@userName", userName);
                return this.UnitOfWork.ExecuteNonQuery(SP_ManualExportZip, parameters, 3600);
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return result;
        }

        public bool CheckExistNewProduct(string userName)
        {
            bool result = false;
            try
            {
                var parameters = new Dictionary<string, object>();
                var ds = this.UnitOfWork.ExecuteQuery(SP_CheckProductNew, parameters);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    var rowData = ds.Tables[0].Rows[0];
                    var count = rowData.ItemArray.GetValue(0);
                    if (int.Parse(count.ToString()) > 0)
                        result = true;
                }
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return result;
        }
        public List<string> GetVariantTypes()
        {
            var lst = new List<string>() { VariantTypes.dry_good, VariantTypes.fresh_food };
            //try
            //{
            //    var setting = this.UnitOfWork.GetAllNoTracking<SystemSetting>().Where(x => x.Code == "VariantType").FirstOrDefault();
            //    if (setting?.Value != null)
            //    {
            //        lst = setting.Value.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList().Select(x => x.Trim()).ToList();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
            //}
            return lst;
        }
        public List<FulfillmentTypesSetting> GetFulfillmentSettings()
        {
            var lstSetting = new List<FulfillmentTypesSetting>();
            try
            {
                var setting = this.UnitOfWork.GetAllNoTracking<SystemSetting>().Where(x => x.Code == "FulfillmentType").FirstOrDefault();
                if (setting?.Value != null)
                {
                    var lst = setting.Value.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList().Select(x => x.Trim()).ToList();
                    lstSetting = new List<FulfillmentTypesSetting>();
                    foreach (var item in lst)
                    {
                        var obj = item.Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries);

                        lstSetting.Add(new FulfillmentTypesSetting { Value = obj[0], Name = obj[1] });
                    }
                }
            }
            catch (Exception ex)
            {
                this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
            }
            return lstSetting;
        }
        public List<ProductCompactDto> GetAllCompact()
        {
            var lst = new List<ProductCompactDto>();
            try
            {
                lst = this.UnitOfWork.GetAllNoTracking<Product>().Where(x => x.ActiveFlag == STATUS_ACTIVE).Select(x => new ProductCompactDto
                {
                    Sku = x.Sku,
                    Name = x.Name,
                    TaxRate = x.TaxRate,
                    IsSyncProfit = x.IsSyncProfit,
                    UnitType = x.UnitType,
                    VariantName = x.VariantName
                }).ToList();
            }
            catch (Exception ex)
            {
                LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
            }
            return lst;
        }
        public ProductInfo GetProductInfoBySKUAndStoreCode(string sku, string storeCode)
        {
            try
            {
                var entity = this.UnitOfWork.GetSingle<ProductInfo>(x => x.Sku.Equals(sku) && x.StoreCode.Equals(storeCode) && x.ActiveFlag == STATUS_ACTIVE);
                return entity;
            }
            catch (Exception ex)
            {
                LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
            }
            return null;
        }
        public List<StoreCompactDto> GetStoreListBySKU(string sku)
        {
            try
            {
                var stores = this.UnitOfWork.GetAll<Store>().Where(x => x.ActiveFlag == STATUS_ACTIVE).ToList();
                var lstProductInfo = this.UnitOfWork.GetAll<ProductInfo>(x => x.Sku.Equals(sku) && x.ActiveFlag == STATUS_ACTIVE).AsEnumerable();
                var data = lstProductInfo.Select(
                   x =>
                   {
                       var store = stores.Find(st => st.Code == x.StoreCode);
                       return new StoreCompactDto()
                       {
                           Code = store?.Code,
                           Name = store?.Name,
                           MallName = store?.MallCode,
                       };
                   })
               .ToList();
                return data;
            }
            catch (Exception ex)
            {
                LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
            }
            return null;
        }
        private bool IsProductInfoDifferent(ProductInfo oldEntity, ProductInfo newEntity)
        {
            return oldEntity.ProductName != newEntity.ProductName ||
                   oldEntity.Inventory != newEntity.Inventory ||
                   (oldEntity?.IsTransfer != newEntity?.IsTransfer) ||
                   oldEntity.IsNew != newEntity.IsNew ||
                   oldEntity.OrderNumber != newEntity.OrderNumber ||
                   oldEntity.URL != newEntity.URL ||
                   oldEntity.ActiveFlag != newEntity.ActiveFlag ||
                   oldEntity.StoreCodeSku != newEntity.StoreCodeSku ||
                   oldEntity.StockBuffer != newEntity.StockBuffer ||
                   oldEntity.Pricing != newEntity.Pricing ||
                   oldEntity.Fulfillment != newEntity.Fulfillment ||
                   oldEntity.IsSyncProfit != newEntity.IsSyncProfit ||
                   oldEntity.IsPublished != newEntity.IsPublished ||
                   oldEntity.QuickDelivery != newEntity.QuickDelivery
                   ;
        }
    }
}
