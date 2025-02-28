using System.Text.Json.Serialization;

namespace NerdStore.Catalog.Domain.ValueObjects
{
    public record ProductName
    {
        public string Value { get; }

        private ProductName(string value) => Value = value;
        public static ProductName Create(string value) => new(value);
        public override string ToString() => Value;
    }
}