using System.Text.Json.Serialization;

namespace NerdStore.Catalog.Domain.ValueObjects
{
    public record StockQuantity
    {
        public int Value { get; }
        private StockQuantity(int value) => Value = value;
        public static StockQuantity Create(int value) => new(value);
        public override string ToString() => Value.ToString();
    }
}