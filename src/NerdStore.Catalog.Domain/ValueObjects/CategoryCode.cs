using System.Text.Json.Serialization;

namespace NerdStore.Catalog.Domain.ValueObjects
{
    public record CategoryCode
    {
        public int Value { get; }

        private CategoryCode(int value) => Value = value;

        public static CategoryCode Create(int value) => new(value);

        public override string ToString() => Value.ToString();
    }
}
