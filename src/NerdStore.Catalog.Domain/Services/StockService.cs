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
            var productResult = await _productRepository.GetById(productId);
            if (!productResult.IsSuccess)
            {
                return productResult;
            }

            var product = productResult.Value!;
            if (!product.HasStock(quantity))
            {
                return Result<Product>.Failure("Insufficient stock.");
            }

            var updatedProductResult = product.DebitStock(quantity);
            if (!updatedProductResult.IsSuccess)
            {
                return updatedProductResult;
            }

            var updatedProduct = updatedProductResult.Value!;
            await PublishLowStockEventIfNeeded(updatedProduct);
            return await PersistStockUpdate(updatedProduct);
        }

        public async Task<Result<Product>> ReplenishStock(ProductId productId, int quantity)
        {
            var productResult = await _productRepository.GetById(productId);
            if (!productResult.IsSuccess)
            {
                return productResult;
            }

            var product = productResult.Value!;
            var updatedProductResult = product.ReplenishStock(quantity);
            if (!updatedProductResult.IsSuccess)
            {
                return updatedProductResult;
            }

            return await PersistStockUpdate(updatedProductResult.Value!);
        }

        private async Task PublishLowStockEventIfNeeded(Product product)
        {
            if (product.Stock.Amount < 10)
            {
                var lowStockEvent = ProductBelowStockEvent<ProductId>.Create(product.Id, product.Stock);
                await _mediatRHandler.PublishEvent(lowStockEvent);
            }
        }

        private async Task<Result<Product>> PersistStockUpdate(Product product)
        {
            await _productRepository.Update(product);
            return await _productRepository.UnitOfWork.Commit()
                ? Result<Product>.Success(product)
                : Result<Product>.Failure("Failed to commit stock update.");
        }
    }
}
