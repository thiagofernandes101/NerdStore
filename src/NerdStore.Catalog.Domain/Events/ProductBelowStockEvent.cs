using NerdStore.Catalog.Domain.ValueObjects;
using NerdStore.Core.DomainObjects;

namespace NerdStore.Catalog.Domain.Events
{
    public class ProductBelowStockEvent<TId> : DomainEvent<TId> where TId : notnull
    {
        public Stock StockQuantity { get; private set; }

        public ProductBelowStockEvent(TId entityId, Stock stockQuantity) : base(entityId)
        {
            StockQuantity = stockQuantity;
        }

        public static ProductBelowStockEvent<TId> Create(TId entityId, Stock stockQuantity)
        {
            return new ProductBelowStockEvent<TId>(entityId, stockQuantity);
        }
    }
}