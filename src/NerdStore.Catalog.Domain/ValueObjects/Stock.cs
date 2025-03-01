namespace NerdStore.Catalog.Domain.ValueObjects
{
    public record Stock
    {
        public int Amount { get; }
        private Stock(int amount) => Amount = amount;
        public static Stock Create(int amount) => new(amount);
        public static Stock Replenish(Stock stock, int quantity) => new(stock.Amount + quantity);
        public static Stock Debit(Stock stock, int quantity) => new(stock.Amount - quantity);
        public override string ToString() => Amount.ToString();
    }
}