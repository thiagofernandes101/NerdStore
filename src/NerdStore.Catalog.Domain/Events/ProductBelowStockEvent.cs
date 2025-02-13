using NerdStore.Catalog.Domain.Entities;
using NerdStore.Core.DomainObjects;

namespace NerdStore.Catalog.Domain.Events
{
    public class ProductBelowStockEvent<TId> : DomainEvent<TId> where TId : notnull
    {
        public StockQuantity StockQuantity { get; private set; }

        public ProductBelowStockEvent(TId entityId, StockQuantity stockQuantity) : base(entityId)
        {
            StockQuantity = stockQuantity;
        }

        public static ProductBelowStockEvent<TId> Create(TId entityId, StockQuantity stockQuantity)
        {
            return new ProductBelowStockEvent<TId>(entityId, stockQuantity);
        }
    }
}