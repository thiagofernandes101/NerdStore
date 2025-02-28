using System.Text.Json.Serialization;

namespace NerdStore.Catalog.Domain.ValueObjects
{
    public record ProductPrice
    {
        public decimal Value { get; }
        private ProductPrice(decimal value) => Value = value;
        public static ProductPrice Create(decimal value) => new(value);
        public override string ToString() => Value.ToString("C");
    }
}