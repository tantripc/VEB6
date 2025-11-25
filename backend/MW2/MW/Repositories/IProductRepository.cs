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
    }
}
