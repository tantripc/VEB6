using MW.DTO;
using MW.Entities;

namespace MW.Repositories
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAll();
        Task<List<Product>> GetPaging();
        Task<Product?> GetById(Guid id);
        Task<Product> Add(Product product);
        Task Update(Product product);
        Task Delete(int id);
        Task<List<SaleOrderCompactDto>> GetSaleOrderNumbersAsync(SaleOrderFilterDto filter, bool isAdmin, string userName);
        Task<bool> CheckRefundAsync(Guid saleId, string storeCode);
        Task<List<RefundHeader>> GetAllBySaleOrderIdCODAsync(Guid id);
    }
}
