using MediatR;
using NerdStore.Catalog.Domain.Entities;
using NerdStore.Catalog.Domain.Repositories;

namespace NerdStore.Catalog.Domain.Events
{
    public class ProductEventHandler : INotificationHandler<ProductBelowStockEvent<ProductId>>
    {
        private readonly IProductRepository _productRepository;

        public ProductEventHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task Handle(ProductBelowStockEvent<ProductId> notification, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetById(notification.AggregateId);
            // Handle the event (e.g., send a notification, log the event, etc.)
        }
    }
}
