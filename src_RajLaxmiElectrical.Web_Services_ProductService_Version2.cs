using Microsoft.EntityFrameworkCore;
using RajLaxmiElectrical.Web.Data;
using RajLaxmiElectrical.Web.Models;

namespace RajLaxmiElectrical.Web.Services
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _db;
        public ProductService(ApplicationDbContext db) => _db = db;

        public async Task CreateAsync(Product product)
        {
            _db.Products.Add(product);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var p = await _db.Products.FindAsync(id);
            if (p != null)
            {
                _db.Products.Remove(p);
                await _db.SaveChangesAsync();
            }
        }

        public async Task<Product?> GetByIdAsync(Guid id)
        {
            return await _db.Products
                .Include(x => x.Inventory)
                .Include(x => x.Category)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Product>> SearchAsync(string? q, Guid? categoryId, decimal? minPrice, decimal? maxPrice, int page = 1, int pageSize = 20)
        {
            var query = _db.Products.Include(x => x.Inventory).AsQueryable();

            if (!string.IsNullOrWhiteSpace(q))
            {
                query = query.Where(x => x.Name.Contains(q) || (x.Description ?? "").Contains(q) || (x.SKU ?? "").Contains(q));
            }
            if (categoryId.HasValue)
                query = query.Where(x => x.CategoryId == categoryId);

            if (minPrice.HasValue)
                query = query.Where(x => x.Price >= minPrice.Value);

            if (maxPrice.HasValue)
                query = query.Where(x => x.Price <= maxPrice.Value);

            return await query
                .OrderBy(x => x.Name)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task UpdateAsync(Product product)
        {
            _db.Products.Update(product);
            await _db.SaveChangesAsync();
        }
    }
}