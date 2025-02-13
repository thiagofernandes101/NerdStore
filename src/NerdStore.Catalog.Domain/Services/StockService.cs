using NerdStore.Catalog.Domain.Entities;
using NerdStore.Catalog.Domain.Repositories;
using NerdStore.Core.Exceptions;
using System.Reflection.Metadata.Ecma335;
using System.Text.RegularExpressions;

namespace NerdStore.Catalog.Domain.Services
{
    public interface IStockService
    {
        Task<bool> DebitStock(ProductId productId, int quantity);
        Task<bool> ReplenishStock(ProductId productId, int quantity);
    }

    public class StockService : IStockService
    {
        private readonly IProductRepository _productRepository;

        public StockService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<bool> DebitStock(ProductId productId, int quantity)
        {
            var product = await _productRepository.GetById(productId);
            if (product is not null && product.HasStock(quantity))
            {
                product.DebitStock(quantity);
                await _productRepository.Update(product);
                return await _productRepository.UnitOfWork.Commit();
            }
            return false;
        }

        public async Task<bool> ReplenishStock(ProductId productId, int quantity)
        {
            var product = await _productRepository.GetById(productId);
            if ( product is not null)
            {
                product.ReplenishStock(quantity);
                await _productRepository.Update(product);
                return await _productRepository.UnitOfWork.Commit();
            }
            return false;
        }
    }
}
