using NerdStore.Catalog.Domain.Entities;
using NerdStore.Catalog.Domain.Events;
using NerdStore.Catalog.Domain.Repositories;
using NerdStore.Core.Bus;
using NerdStore.Core.DomainObjects;

namespace NerdStore.Catalog.Domain.Services
{
    public interface IStockService
    {
        Task<Result<Product>> DebitStock(ProductId productId, int quantity);
        Task<Result<Product>> ReplenishStock(ProductId productId, int quantity);
    }

    public class StockService : IStockService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMediatRHandler _mediatRHandler;

        public StockService(IProductRepository productRepository, IMediatRHandler mediatRHandler)
        {
            _productRepository = productRepository;
            _mediatRHandler = mediatRHandler;
        }

        public async Task<Result<Product>> DebitStock(ProductId productId, int quantity)
        {
            var product = await _productRepository.GetById(productId);

            if (product is null)
            {
                return Result<Product>.Failure("Product not found.");
            }

            if (!product.HasStock(quantity))
            {
                return Result<Product>.Failure("Insufficient stock.");
            }

            product.DebitStock(quantity);

            await HandleLowStockEvent(product);

            await _productRepository.Update(product);
            var commitSuccess = await _productRepository.UnitOfWork.Commit();
            if (!commitSuccess)
            {
                return Result<Product>.Failure("Failed to commit stock replenishment.");
            }

            return Result<Product>.Success(product);
        }

        private async Task HandleLowStockEvent(Product product)
        {
            if (product.StockQuantity.Value >= 10)
                return;

            var lowStockEvent = ProductBelowStockEvent<ProductId>.Create(product.Id, product.StockQuantity);
            await _mediatRHandler.PublishEvent(lowStockEvent);
        }

        public async Task<Result<Product>> ReplenishStock(ProductId productId, int quantity)
        {
            var product = await _productRepository.GetById(productId);

            if (product is null)
            {
                return Result<Product>.Failure("Product not found.");
            }

            product.ReplenishStock(quantity);
            await _productRepository.Update(product);
            var commitSuccess = await _productRepository.UnitOfWork.Commit();
            if (!commitSuccess)
            {
                return Result<Product>.Failure("Failed to commit stock replenishment.");
            }

            return Result<Product>.Success(product);
        }
    }
}
