using NerdStore.Core;

namespace NerdStore.Catalog.Domain
{
    public readonly record struct ProductId(Guid Value)
    {
        public ProductId() : this(Guid.NewGuid()) { }

        public override string ToString() => Value.ToString();
    }

    public record ProductName
    {
        public string Value { get; }

        public ProductName(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Product name cannot be empty.", nameof(value));
            if (value.Length > 100)
                throw new ArgumentException("Product name cannot exceed 100 characters.", nameof(value));

            Value = value;
        }

        public override string ToString() => Value;
    }

    public record Description
    {
        public string Value { get; }

        public Description(string value)
        {
            if (value.Length > 500)
                throw new ArgumentException("Description cannot exceed 500 characters.", nameof(value));

            Value = value;
        }

        public override string ToString() => Value;
    }

    public record Price
    {
        public decimal Value { get; }

        public Price(decimal value)
        {
            if (value < 0)
                throw new ArgumentException("Price cannot be negative.", nameof(value));

            Value = value;
        }

        public override string ToString() => Value.ToString("C");
    }

    public record ImageHash
    {
        public string Value { get; }

        public ImageHash(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Image URL cannot be empty.", nameof(value));
            if (!Uri.TryCreate(value, UriKind.Absolute, out _))
                throw new ArgumentException("Invalid URL format.", nameof(value));

            Value = value;
        }

        public override string ToString() => Value;
    }

    public record Stock
    {
        public int Value { get; }

        public Stock(int value)
        {
            if (value < 0)
                throw new ArgumentException("Stock quantity cannot be negative.", nameof(value));

            Value = value;
        }

        public override string ToString() => Value.ToString();
    }

    public class Product : Entity<ProductId>, IAggregateRoot
    {
        public Product(ProductId id, ProductName name, Description description, bool active, Price value, DateTime registerDate, ImageHash image, Stock stockQuantity) : base(id)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Description = description ?? throw new ArgumentNullException(nameof(description));
            Active = active;
            Value = value ?? throw new ArgumentNullException(nameof(value));
            RegisterDate = registerDate;
            Image = image ?? throw new ArgumentNullException(nameof(image));
            StockQuantity = stockQuantity ?? throw new ArgumentNullException(nameof(stockQuantity));
        }

        public CategoryId CategoryId { get; set; }
        public ProductName Name { get; private set; }
        public Description Description { get; private set; }
        public bool Active { get; private set; }
        public Price Value { get; private set; }
        public DateTime RegisterDate { get; private set; }
        public ImageHash Image { get; private set; }
        public Stock StockQuantity { get; private set; }
        public Category Category { get; set; }
    }
}