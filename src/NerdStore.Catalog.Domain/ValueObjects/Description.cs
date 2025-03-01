namespace NerdStore.Catalog.Domain.ValueObjects
{
    public record Description
    {
        public string Value { get; }
        private Description(string value) => Value = value;
        public static Description Create(string value) => new(value);
        public override string ToString() => Value;
    }
}