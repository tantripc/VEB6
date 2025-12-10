using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using static MiddlewareTool.Dto.CoreDto;

namespace MiddlewareTool.Business.Interface
{
   public interface IStockBusiness
   {
      Task<Tuple<int, List<StockDto>>> GetPagingAsync(string userName, string keyWord, int pageIndex, int pageSize);
      StockDto GetById(Guid id);
      Task<StockDto> GetByIdAsync(Guid id);
      StockDto GetByCode(string code);
      bool Insert(StockDto dto);
      Task<bool> InsertAsync(StockDto dto);
      bool Update(StockDto dto);
      Task<bool> UpdateAsync(StockDto dto);
      bool IsExist(Guid id);
      Task<bool> IsExistAsync(Guid id);
      bool IsExistByCode(string code);
      bool Import(DataTable dt, int timeOut);
      bool ImportHourly(DataTable dt, string fileName, int timeOut, out string error);
      //List<StockDto> GetStockUpdate(DataTable dt);
      bool IsExistBySkuAndStoreCode(string sku, string storeCode);
   }
}
