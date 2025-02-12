using NerdStore.Catalog.Domain.Entities;
using NerdStore.Core.Data;
using NerdStore.Core.Patterns;

namespace NerdStore.Catalog.Domain.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<IEnumerable<Product>> GetAll();
        Task<Option<Product>> GetById(ProductId id);
        Task<IEnumerable<Product>> GetByCategory(CategoryCode code);
        Task<IEnumerable<Category>> GetCategories();
        Task Add(Product product);
        Task Update(Product product);
        Task AddCategory(Category category);
        Task UpdateCategory(Category category);
    }
}
