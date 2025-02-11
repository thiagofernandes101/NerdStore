using FluentValidation.Results;
using NerdStore.Catalog.Domain.Validations;
using NerdStore.Catalog.Domain.ValueObjects;
using NerdStore.Core;

namespace NerdStore.Catalog.Domain.Entities
{
    public readonly record struct ProductId(Guid Value)
    {
        public static ProductId NewId => new(Guid.NewGuid());
        public static ProductId Empty => new(Guid.Empty);

        public override string ToString() => Value.ToString();
    }

    public record ProductName(string Value)
    {
        public static ProductName NewProductName(string value) => new(value);
        public override string ToString() => Value;
    }

    public record Description(string Value)
    {
        public static Description NewDescription(string value) => new(value);
        public override string ToString() => Value;
    }

    public record Price(decimal Value)
    {
        public static Price NewPrice(decimal value) => new(value);
        public override string ToString() => Value.ToString("C");
    }

    public record ImageHash(string Value)
    {
        public static ImageHash NewImageHash(string value) => new(value);
        public override string ToString() => Value;
    }

    public record Stock(int Value)
    {
        public static Stock NewStock(int value) => new(value);
        public override string ToString() => Value.ToString();
    }

    public record RegisterDate(DateTime Value)
    {
        public static RegisterDate NewRegisterDate(DateTime value) => new(value);
        public static RegisterDate Now => new(DateTime.Now);
        public override string ToString() => Value.ToString("yyyy-MM-dd");
    }

    public class Product : Entity<ProductId>, IAggregateRoot
    {
        public CategoryId CategoryId { get; set; }
        public ProductName Name { get; private set; }
        public Description Description { get; private set; }
        public bool Active { get; private set; }
        public Price Price { get; private set; }
        public RegisterDate RegisterDate { get; private set; }
        public ImageHash Image { get; private set; }
        public Stock StockQuantity { get; private set; }
        public Category Category { get; set; }
        public Dimension Dimension { get; set; }

        private static readonly ProductValidator _validator = new();

        private Product(ProductId id, ProductName name, Description description, bool active, Price price, Stock stockQuantity, CategoryId categoryId, RegisterDate registerDate, ImageHash image, Dimension dimension) : base(id)
        {
            Name = name;
            Description = description;
            Active = active;
            Price = price;
            StockQuantity = stockQuantity;
            CategoryId = categoryId;
            RegisterDate = registerDate;
            Image = image;
            Dimension = dimension;
        }

        public static Product NewProduct(ProductName name, Description description, bool active, Price price, Stock stockQuantity, CategoryId categoryId, RegisterDate registerDate, ImageHash image, Dimension dimension) =>
            new(ProductId.NewId, name, description, active, price, stockQuantity, categoryId, registerDate, image, dimension);

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

        public ValidationResult IsValid() =>
            _validator.Validate(this);
    }
}