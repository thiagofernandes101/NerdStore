using Microsoft.EntityFrameworkCore;
using NerdStore.Catalog.Data.Contexts;
using NerdStore.Catalog.Domain.Entities;
using NerdStore.Catalog.Domain.Repositories;
using NerdStore.Core.Data;

namespace NerdStore.Catalog.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly CatalogContext _catalogContext;

        public ProductRepository(CatalogContext catalogContext)
        {
            _catalogContext = catalogContext;
        }

        public IUnitOfWork UnitOfWork => _catalogContext;

        public async Task Add(Product product)
        {
            await _catalogContext.Products.AddAsync(product);
        }

        public async Task AddCategory(Category category)
        {
            await _catalogContext.Categories.AddAsync(category);
        }

        public async Task<IEnumerable<Product>> GetAll() =>
            await _catalogContext.Products.AsNoTracking().ToListAsync();

        public async Task<IEnumerable<Product>> GetByCategory(CategoryCode code) =>
            await _catalogContext.Products.AsNoTracking()
                .Include(p => p.Category)
                .Where(p => p.Category.Code == code)
                .ToListAsync();

        public async Task<Product?> GetById(ProductId id)
        {
            return await _catalogContext.Products
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Category>> GetCategories() =>
            await _catalogContext.Categories.AsNoTracking().ToListAsync();

        public async Task Update(Product product)
        {
            _catalogContext.Products.Update(product);
        }

        public async Task UpdateCategory(Category category)
        {
            _catalogContext.Categories.Update(category);
        }
    }
}
