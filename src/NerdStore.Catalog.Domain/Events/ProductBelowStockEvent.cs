using NerdStore.Catalog.Domain.Entities;
using NerdStore.Core.DomainObjects;

namespace NerdStore.Catalog.Domain.Events
{
    public class ProductBelowStockEvent<TId> : DomainEvent<TId> where TId : notnull
    {
        public ProductStockQuantity StockQuantity { get; private set; }

        public ProductBelowStockEvent(TId entityId, ProductStockQuantity stockQuantity) : base(entityId)
        {
            StockQuantity = stockQuantity;
        }

        public static ProductBelowStockEvent<TId> Create(TId entityId, ProductStockQuantity stockQuantity)
        {
            return new ProductBelowStockEvent<TId>(entityId, stockQuantity);
        }
    }
}