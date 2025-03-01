using System.Text.Json.Serialization;

namespace NerdStore.Catalog.Domain.ValueObjects
{
    public record RegisterDate
    {
        public DateTime Value { get; }
        private RegisterDate(DateTime value) => Value = value;
        public static RegisterDate Create(DateTime value) => new(value);
        public static RegisterDate Now => new(DateTime.Now);
        public override string ToString() => Value.ToString("yyyy-MM-dd");
    }
}