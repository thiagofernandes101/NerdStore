namespace NerdStore.Catalog.Domain.ValueObjects
{
    public record Image
    {
        public string Value { get; }
        private Image(string value) => Value = value;
        public static Image CreateFromHash(string value) => new(value);
        public override string ToString() => Value;
    }
}