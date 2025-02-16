namespace NerdStore.Catalog.Domain.ValueObjects
{
    public record ProductRegisterDate
    {
        public DateTime Value { get; }
        private ProductRegisterDate(DateTime value) => Value = value;
        public static ProductRegisterDate Create(DateTime value) => new(value);
        public static ProductRegisterDate Now => new(DateTime.Now);
        public override string ToString() => Value.ToString("yyyy-MM-dd");
    }
}