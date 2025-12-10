using AutoMapper;
using MiddlewareTool.Business.Interface;
using MiddlewareTool.Dto;
using MiddlewareTool.Entities;
using MiddlewareTool.OpenXML;
using MiddlewareTool.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using static MiddlewareTool.Dto.CoreDto;

namespace MiddlewareTool.Business.Concrete
{
    public class InventoryBusiness : BaseBusiness, IInventoryBusiness
    {
        private readonly IInventoryHistoryBusiness _inventoryHistoryBusiness;
        public InventoryBusiness(IUnitOfWork unitOfWork, IInventoryHistoryBusiness inventoryHistoryBusiness) : base(unitOfWork)
        {
            _inventoryHistoryBusiness = inventoryHistoryBusiness;
        }
        public byte[] GetTransfer(byte[] template, string transData, int timeOut)
        {
            try
            {
                if (template != null && template.Length > 0)
                {
                    var sql = "Exec " + BaseBusiness.SP_Inventory_GetTransfer;
                    sql += " @transData = '" + transData + "';";

                    var lstData = this.UnitOfWork.SqlQuery<InventoryBoxedDto>(sql, timeOut).ToList();
                    if (lstData != null && lstData.Count > 0)
                    {
                        var excel = new Excel();
                        excel.TemplateFileData = template;
                        //excel.ParameterData.Add("MallCode", "MallCode");
                        //excel.ParameterData.Add("MallName", "MallName");
                        //excel.ParameterData.Add("StoreCode", "StoreCode");
                        //excel.ParameterData.Add("StoreName", "StoreName");
                        //excel.ParameterData.Add("Sku", "Sku");
                        //excel.ParameterData.Add("Quantity", "Quantity");
                        //excel.ParameterData.Add("Is_Published", "Is_Published");
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
                var sql = BaseBusiness.SP_Inventory_UpdateTransferred;
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
        public bool UpdateIsTransfer(string sku, Common.History.Action historyAction, bool IsSyncProfit = true)
        {
            bool result = false;
            try
            {
                var lstEntity = this.UnitOfWork.GetAll<Inventory>(x => x.Sku.Equals(sku) && x.ActiveFlag == STATUS_ACTIVE);
                if (lstEntity != null)
                {
                    foreach (var item in lstEntity)
                    {

                        var isTransfer = this.UnitOfWork.GetSingle<ProductInfo>(x => x.Sku.Equals(sku) && x.StoreCode.Equals(item.StoreCode) && x.ActiveFlag == STATUS_ACTIVE);

                        if (!IsSyncProfit)
                        {
                            if (isTransfer?.IsPublished == true)
                                item.IsTransfer = isTransfer?.IsTransfer ?? false;
                        }
                        else
                        {
                            if (isTransfer?.IsPublished == true)
                                item.IsTransfer = false;
                        }
                        item.UpdateDate = DateTime.Now;
                        result = this.UnitOfWork.Update(item);
                        if (result)
                        {
                            var history = Mapper.Map<InventoryHistoryDto>(item);
                            history.Id = Guid.NewGuid();
                            history.Action = (int)historyAction;
                            _inventoryHistoryBusiness.Insert(history);
                        }
                    }
                }
            }
            catch (Exception ex) { LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return result;
        }
        public bool UpdateIsTransferBySkuAndStore(string sku, string storeCode, bool isTransfer, Common.History.Action historyAction)
        {
            bool result = false;
            try
            {
                var lstEntity = this.UnitOfWork.GetAll<Inventory>(x => x.Sku.Equals(sku) && x.StoreCode.Equals(storeCode) && x.ActiveFlag == STATUS_ACTIVE);
                if (lstEntity != null)
                {
                    foreach (var item in lstEntity)
                    {
                        item.IsTransfer = isTransfer;
                        item.UpdateDate = DateTime.Now;
                        result = this.UnitOfWork.Update(item);
                    }
                    var lstHistory = Mapper.Map<List<InventoryHistoryDto>>(lstEntity);
                    foreach (var item in lstHistory)
                    {
                        item.Id = Guid.NewGuid();
                        item.Action = (int)historyAction;
                        item.Source = "ProductMgmt/" + historyAction.ToString();
                    }
                    _inventoryHistoryBusiness.InsertList(lstHistory);
                }
            }
            catch (Exception ex) { LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return result;
        }
    }
}
