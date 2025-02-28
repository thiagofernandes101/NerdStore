using System.Text.Json.Serialization;

namespace NerdStore.Catalog.Domain.ValueObjects
{
    public record ProductStockQuantity
    {
        public int Value { get; }
        private ProductStockQuantity(int value) => Value = value;
        public static ProductStockQuantity Create(int value) => new(value);
        public override string ToString() => Value.ToString();
    }
}