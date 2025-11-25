using Microsoft.EntityFrameworkCore;
using MW.Data;
using MW.Entities;

namespace MW.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _db;

        public ProductRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<List<Product>> GetAll()
            => await _db.Products.AsNoTracking().ToListAsync();
        public async Task<List<Product>> GetPaging()
        {
            var iquery = await _db.Products.AsNoTracking().Skip(0).Take(10).ToListAsync();

            return iquery;
        }

        public async Task<Product?> GetById(Guid id)
            => await _db.Products.FirstOrDefaultAsync(x => x.Id == id);

        public async Task<Product> Add(Product product)
        {
            _db.Products.Add(product);
            await _db.SaveChangesAsync();
            return product;
        }

        public async Task Update(Product product)
        {
            _db.Products.Update(product);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var entity = await _db.Products.FindAsync(id);
            if (entity != null)
            {
                _db.Products.Remove(entity);
                await _db.SaveChangesAsync();
            }
        }
    }
}
