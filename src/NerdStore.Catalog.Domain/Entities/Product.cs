using FluentValidation.Results;
using NerdStore.Catalog.Domain.Validations;
using NerdStore.Catalog.Domain.ValueObjects;
using NerdStore.Core.DomainObjects;
using NerdStore.Core.Exceptions;

namespace NerdStore.Catalog.Domain.Entities
{
    public readonly record struct ProductId(Guid Value)
    {
        public static ProductId NewId => new(Guid.NewGuid());
        public static ProductId Empty => new(Guid.Empty);

        public override string ToString() => Value.ToString();
    }

    public record Name
    {
        public string Value { get; }
        private Name(string value) => Value = value;
        public static Name Create(string value) => new(value);
        public override string ToString() => Value;
    }

    public record Description
    {
        public string Value { get; }
        private Description(string value) => Value = value;
        public static Description Create(string value) => new(value);
        public override string ToString() => Value;
    }

    public record Price
    {
        public decimal Value { get; }
        private Price(decimal value) => Value = value;
        public static Price Create(decimal value) => new(value);
        public override string ToString() => Value.ToString("C");
    }

    public record ImageHash
    {
        public string Value { get; }
        private ImageHash(string value) => Value = value;
        public static ImageHash Create(string value) => new(value);
        public override string ToString() => Value;
    }

    public record StockQuantity
    {
        public int Value { get; }
        private StockQuantity(int value) => Value = value;
        public static StockQuantity Create(int value) => new(value);
        public override string ToString() => Value.ToString();
    }

    public record RegisterDate
    {
        public DateTime Value { get; }
        private RegisterDate(DateTime value) => Value = value;
        public static RegisterDate Create(DateTime value) => new(value);
        public static RegisterDate Now => new(DateTime.Now);
        public override string ToString() => Value.ToString("yyyy-MM-dd");
    }

    public class Product : Entity<ProductId>, IAggregateRoot
    {
        public CategoryId CategoryId { get; private set; }
        public Name Name { get; private set; }
        public Description Description { get; private set; }
        public bool Active { get; private set; }
        public Price Price { get; private set; }
        public RegisterDate RegisterDate { get; private set; }
        public ImageHash Image { get; private set; }
        public StockQuantity StockQuantity { get; private set; }
        public Category Category { get; private set; }
        public Dimension Dimension { get; private set; }

        private static readonly ProductValidator _validator = new();

        [Obsolete("Parameterless constructor is for EF Core and mapping only.")]
        public Product() { }

        private Product(ProductId id, Name name, Description description, bool active, Price price, StockQuantity stockQuantity, CategoryId categoryId, RegisterDate registerDate, ImageHash image, Dimension dimension) : base(id)
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

        public static Product Create(string name, string description, bool active, decimal price, int stockQuantity, 
            CategoryId categoryId, string image, int height, int width, int depth) =>
            new(
                ProductId.NewId, 
                Name.Create(name),
                Description.Create(description), 
                active, 
                Price.Create(price), 
                StockQuantity.Create(stockQuantity), 
                categoryId,
                RegisterDate.Create(DateTime.Now), 
                ImageHash.Create(image),
                Dimension.Create(height, width, depth));

        public static Product Default => 
            new(ProductId.Empty, Name.Create(string.Empty), Description.Create(string.Empty), false, Price.Create(0), StockQuantity.Create(0), CategoryId.Empty, RegisterDate.Create(DateTime.MinValue), ImageHash.Create(string.Empty), Dimension.Create(0, 0, 0));

        public void Activate() => Active = true;

        public void Deactivate() => Active = false;

        public void ChangeCategory(Category category)
        {
            Category = category;
            CategoryId = category.Id;
        }

        public void ChagneDescription(string description) => 
            Description = Description.Create(description);

        public void DebitStock(int quantity)
        {
            if (quantity < 0)
                quantity *= -1;

            if (!HasStock(quantity))
                throw new DomainException("Insufficient stock.");

            StockQuantity = StockQuantity.Create(StockQuantity.Value - quantity);
        }

        public bool HasStock(int quantity) =>
            StockQuantity.Value >= quantity;

        public void ReplenishStock(int quantity) =>
            StockQuantity = StockQuantity.Create(StockQuantity.Value + quantity);

        public ValidationResult IsValid() =>
            _validator.Validate(this);
    }
}