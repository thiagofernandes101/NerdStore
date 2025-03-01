using System.Text.Json.Serialization;

namespace NerdStore.Catalog.Domain.ValueObjects
{
    public record Name
    {
        public string Value { get; }

        private Name(string value) => Value = value;
        public static Name Create(string value) => new(value);
        public override string ToString() => Value;
    }
}