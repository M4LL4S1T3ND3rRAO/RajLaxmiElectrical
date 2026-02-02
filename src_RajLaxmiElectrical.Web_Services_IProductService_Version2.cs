using RajLaxmiElectrical.Web.Models;

namespace RajLaxmiElectrical.Web.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> SearchAsync(string? q, Guid? categoryId, decimal? minPrice, decimal? maxPrice, int page = 1, int pageSize = 20);
        Task<Product?> GetByIdAsync(Guid id);
        Task CreateAsync(Product product);
        Task UpdateAsync(Product product);
        Task DeleteAsync(Guid id);
    }
}