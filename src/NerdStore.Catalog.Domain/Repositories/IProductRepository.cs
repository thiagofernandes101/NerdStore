using NerdStore.Catalog.Domain.Entities;
using NerdStore.Catalog.Domain.ValueObjects;
using NerdStore.Core.Data;

namespace NerdStore.Catalog.Domain.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<IEnumerable<Product>> GetAll();
        Task<Product?> GetById(ProductId id);
        Task<IEnumerable<Product>> GetByCategory(CategoryCode code);
        Task<IEnumerable<Category>> GetCategories();
        Task Add(Product product);
        Task Update(Product product);
        Task AddCategory(Category category);
        Task UpdateCategory(Category category);
    }
}
