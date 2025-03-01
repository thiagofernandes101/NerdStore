namespace NerdStore.Catalog.Domain.ValueObjects
{
    public record Stock
    {
        public int Amount { get; }
        private Stock(int amount) => Amount = amount;
        public static Stock Create(int amount) => new(amount);
        public override string ToString() => Amount.ToString();
    }
}