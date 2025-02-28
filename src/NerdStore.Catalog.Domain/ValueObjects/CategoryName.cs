using System.Text.Json.Serialization;

namespace NerdStore.Catalog.Domain.ValueObjects
{
    public record CategoryName
    {
        public string Value { get; }

        private CategoryName(string value) => Value = value;

        public static CategoryName Create(string value) => new(value);

        public override string ToString() => Value;
    }
}
