using MiddlewareTool.Business.Interface;
using MiddlewareTool.OpenXML;
using MiddlewareTool.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using static MiddlewareTool.Dto.CategoryMgmtDto;
using static MiddlewareTool.Dto.CoreDto;
using static MiddlewareTool.Dto.ProductMgmtDto;

namespace MiddlewareTool.Business.Concrete
{
    public class NewProductBusiness : BaseBusiness, INewProductBusiness
    {
        public NewProductBusiness(IUnitOfWork unitOfWork) : base(unitOfWork) { }
        public Tuple<byte[], byte[], byte[], byte[]> GetTransfer(Tuple<byte[], byte[], byte[], byte[]> template, string transData, int timeOut)
        {
            byte[] category = null;
            byte[] catalogEnrichment = null;
            byte[] inventory = null;
            byte[] pricing = null;
            try
            {
                #region For Category
                if (template.Item1 != null && template.Item1.Length > 0)
                {
                    var sql = "Exec " + BaseBusiness.SP_Category_GetTransferFirstTime;
                    var lstData = this.UnitOfWork.SqlQuery<CategoryBoxedDto>(sql, timeOut).ToList();
                    if (lstData != null && lstData.Count > 0)
                    {
                        var excel = new Excel();
                        excel.TemplateFileData = template.Item1;
                        excel.ParameterData.Add("category_id", "category_id");
                        excel.ParameterData.Add("category_path", "category_path");
                        var data = excel.Export(lstData);
                        category = data;
                    }
                }
                #endregion

                #region For CatalogEnrichment
                if (template.Item2 != null && template.Item2.Length > 0)
                {
                    var sql = "Exec " + BaseBusiness.SP_Product_GetTransferFirstTime;
                    sql += " @transData = '" + transData + "'";
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
                        excel.TemplateFileData = template.Item2;
                        excel.ParameterData.Add("Code", "Code");
                        excel.ParameterData.Add("Name", "Name");
                        excel.ParameterData.Add("VariantOption", "VariantOption");
                        excel.ParameterData.Add("Sku", "Sku");
                        excel.ParameterData.Add("VariantName", "VariantName");
                        excel.ParameterData.Add("ExtendedName", "ExtendedName");
                        excel.ParameterData.Add("BrandName", "BrandName");
                        excel.ParameterData.Add("Description", "Description");
                        excel.ParameterData.Add("Ingredients", "Ingredients");
                        excel.ParameterData.Add("CategoryCode", "CategoryCode");
                        excel.ParameterData.Add("Is_Chill", "Is_Chill");
                        excel.ParameterData.Add("Is_Frozen", "Is_Frozen");
                        excel.ParameterData.Add("Weight", "Weight");
                        excel.ParameterData.Add("Length", "Length");
                        excel.ParameterData.Add("Width", "Width");
                        excel.ParameterData.Add("Height", "Height");
                        excel.ParameterData.Add("Volume", "Volume");
                        excel.ParameterData.Add("Is_Age_Gated", "Is_Age_Gated");
                        excel.ParameterData.Add("MaxCartQuantity", "MaxCartQuantity");
                        excel.ParameterData.Add("UnitCount", "UnitCount");
                        excel.ParameterData.Add("UnitType", "UnitType");
                        excel.ParameterData.Add("Is_Perishable", "Is_Perishable");
                        excel.ParameterData.Add("Origin", "Origin");
                        excel.ParameterData.Add("Grade", "Grade");
                        excel.ParameterData.Add("Size", "Size");
                        excel.ParameterData.Add("Upc", "Upc");
                        excel.ParameterData.Add("Barcode", "Barcode");
                        excel.ParameterData.Add("TaxRate", "TaxRate");
                        excel.ParameterData.Add("Fulfillment_Method", "Fulfillment_Method");
                        excel.ParameterData.Add("ImageLinks", "ImageLinks");
                        excel.ParameterData.Add("seo_title", "seo_title");
                        excel.ParameterData.Add("seo_description", "seo_description");
                        excel.ParameterData.Add("seo_keyword", "seo_keyword");
                        var data = excel.Export(lstData);
                        catalogEnrichment = data;
                    }
                }
                #endregion

                #region For Inventory
                if (template.Item3 != null && template.Item3.Length > 0)
                {
                    var sql = "Exec " + BaseBusiness.SP_Inventory_GetTransferFirstTime;
                    sql += " @transData = '" + transData + "'";
                    var lstData = this.UnitOfWork.SqlQuery<InventoryBoxedDto>(sql, timeOut).ToList();
                    if (lstData != null && lstData.Count > 0)
                    {
                        var excel = new Excel();
                        excel.TemplateFileData = template.Item3;
                        excel.ParameterData.Add("MallCode", "MallCode");
                        excel.ParameterData.Add("MallName", "MallName");
                        excel.ParameterData.Add("StoreCode", "StoreCode");
                        excel.ParameterData.Add("StoreName", "StoreName");
                        excel.ParameterData.Add("Sku", "Sku");
                        excel.ParameterData.Add("Quantity", "Quantity");
                        excel.ParameterData.Add("Is_Published", "Is_Published");
                        var data = excel.Export(lstData);
                        inventory = data;
                    }
                }
                #endregion

                #region For Pricing
                if (template.Item4 != null && template.Item4.Length > 0)
                {
                    var sql = "Exec " + BaseBusiness.SP_Pricing_GetTransferFirstTime;
                    sql += " @transData = '" + transData + "'";
                    var lstData = this.UnitOfWork.SqlQuery<PricingBoxedDto>(sql, timeOut).ToList();
                    if (lstData != null && lstData.Count > 0)
                    {
                        var excel = new Excel();
                        excel.TemplateFileData = template.Item4;
                        excel.ParameterData.Add("MallCode", "MallCode");
                        excel.ParameterData.Add("MallName", "MallName");
                        excel.ParameterData.Add("StoreCode", "StoreCode");
                        excel.ParameterData.Add("StoreName", "StoreName");
                        excel.ParameterData.Add("Sku", "Sku");
                        excel.ParameterData.Add("Price", "Price");
                        excel.ParameterData.Add("DisplaySalePrice", "DisplaySalePrice");
                        var data = excel.Export(lstData);
                        pricing = data;
                    }
                }
                #endregion

                return new Tuple<byte[], byte[], byte[], byte[]>(category, catalogEnrichment, inventory, pricing);
            }
            catch (Exception ex)
            {
                LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
            }
            return null;
        }
        public bool UpdateTransferredAndNotNew(DateTime dateTime, string source, int action, string transData, int timeOut)
        {
            try
            {
                var sql = BaseBusiness.SP_Product_UpdateTransferredAndNotNew;
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
    }
}
