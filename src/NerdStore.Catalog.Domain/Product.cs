using NerdStore.Catalog.Domain.Validations;
using NerdStore.Core;

namespace NerdStore.Catalog.Domain
{
    public readonly record struct ProductId(Guid Value)
    {
        public ProductId() : this(Guid.NewGuid()) { }

        public override string ToString() => Value.ToString();
    }

    public record ProductName(string Value)
    {
        public override string ToString() => Value;
    }

    public record Description(string Value)
    {
        public override string ToString() => Value;
    }

    public record Price(decimal Value)
    {
        public override string ToString() => Value.ToString("C");
    }

    public record ImageHash(string Value)
    {
        public override string ToString() => Value;
    }

    public record Stock(int Value)
    {
        public override string ToString() => Value.ToString();
    }

    public class Product : Entity<ProductId>, IAggregateRoot
    {
        public CategoryId CategoryId { get; set; }
        public ProductName Name { get; private set; }
        public Description Description { get; private set; }
        public bool Active { get; private set; }
        public Price Price { get; private set; }
        public DateTime RegisterDate { get; private set; }
        public ImageHash Image { get; private set; }
        public Stock StockQuantity { get; private set; }
        public Category Category { get; set; }
        public Dimension Dimension { get; set; }

        private static readonly ProductValidator _validator = new();

        public Product(ProductId id, ProductName name, Description description, bool active, Price price, CategoryId categoryId, DateTime registerDate, ImageHash image, Stock stockQuantity, Dimension dimension) : base(id)
        {
            Name = name;
            Description = description;
            Active = active;
            Price = price;
            CategoryId = categoryId;
            RegisterDate = registerDate;
            Image = image;
            Dimension = dimension;
        }

        public void Activate() => Active = true;

        public void Deactivate() => Active = false;

        public void ChangeCategory(Category category)
        {
            Category = category;
            CategoryId = category.Id;
        }

        public void ChagneDescription(string description) => Description = new Description(description);

        public void DebitStock(int quantity)
        {
            if (quantity < 0)
                quantity *= -1;

            if (!HasStock(quantity))
                throw new DomainException("Insufficient stock.");

            StockQuantity = new Stock(StockQuantity.Value - quantity);
        }

        public bool HasStock(int quantity) => 
            StockQuantity.Value >= quantity;

        public void ReplenishStock(int quantity) => 
            StockQuantity = new Stock(StockQuantity.Value + quantity);

        public bool IsValid() =>
            _validator.Validate(this).IsValid;
    }
}