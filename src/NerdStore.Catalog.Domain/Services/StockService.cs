using NerdStore.Catalog.Domain.Entities;
using NerdStore.Catalog.Domain.Events;
using NerdStore.Catalog.Domain.Repositories;
using NerdStore.Core.Bus;
using NerdStore.Core.Exceptions;
using NerdStore.Core.Messages;
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
        private readonly IMediatRHandler _mediatRHandler;

        public StockService(IProductRepository productRepository, IMediatRHandler mediatRHandler)
        {
            _productRepository = productRepository;
            _mediatRHandler = mediatRHandler;
        }

        public async Task<bool> DebitStock(ProductId productId, int quantity)
        {
            var product = await _productRepository.GetById(productId);
            if (product is not null && product.HasStock(quantity))
            {
                product.DebitStock(quantity);
                if (product.StockQuantity.Value < 10)
                {
                    await _mediatRHandler.PublishEvent(ProductBelowStockEvent<ProductId>.Create(product.Id, product.StockQuantity));
                }
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
