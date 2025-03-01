using FluentValidation.Results;
using NerdStore.Catalog.Domain.Validations;
using NerdStore.Catalog.Domain.ValueObjects;
using NerdStore.Core.DomainObjects;
using NerdStore.Core.Exceptions;
using System.Text.Json.Serialization;

namespace NerdStore.Catalog.Domain.Entities
{
    public readonly record struct ProductId(Guid Value)
    {
        public static ProductId NewId => new(Guid.NewGuid());
        public static ProductId Empty => new(Guid.Empty);
        public static ProductId CreateFrom(Guid id) => new(id);

        public override string ToString() => Value.ToString();
    }

    public class Product : Entity<ProductId>, IAggregateRoot
    {
        public CategoryId CategoryId { get; private set; }
        public Name Name { get; private set; }
        public Description Description { get; private set; }
        public bool Active { get; private set; }
        public Price Price { get; private set; }
        public RegisterDate RegisterDate { get; private set; }
        public Image Image { get; private set; }
        public StockQuantity StockQuantity { get; private set; }
        public Category Category { get; private set; }
        public Dimension Dimension { get; private set; }

        private static readonly ProductValidator _validator = new();

        [Obsolete("Parameterless constructor is for EF Core and mapping only.")]
        public Product() { }

        private Product(
            ProductId id,
            Name name,
            Description description,
            bool active,
            Price price,
            StockQuantity stockQuantity,
            Category category,
            RegisterDate registerDate,
            Image image,
            Dimension dimension) : base(id)
        {
            Name = name;
            Description = description;
            Active = active;
            Price = price;
            StockQuantity = stockQuantity;
            CategoryId = category?.Id ?? Category.Empty.Id;
            Category = category ?? Category.Empty;
            RegisterDate = registerDate;
            Image = image;
            Dimension = dimension;
        }

        public static Product CreateProductWithoutCategory(
            string name,
            string description,
            bool active,
            decimal price,
            int stockQuantity,
            string image,
            int height,
            int width,
            int depth) =>
            new(
                ProductId.NewId,
                Name.Create(name),
                Description.Create(description),
                active,
                Price.Create(price),
                StockQuantity.Create(stockQuantity),
                Category.None,
                RegisterDate.Create(DateTime.Now),
                Image.CreateFromHash(image),
                Dimension.Create(height, width, depth));

        public static Product CreateProduct(
            string name,
            string description,
            bool active,
            decimal price,
            int stockQuantity,
            Category category,
            string image,
            int height,
            int width,
            int depth) =>
            new(
                ProductId.NewId,
                Name.Create(name),
                Description.Create(description),
                active,
                Price.Create(price),
                StockQuantity.Create(stockQuantity),
                category,
                RegisterDate.Create(DateTime.Now),
                Image.CreateFromHash(image),
                Dimension.Create(height, width, depth));

        public static Product CreateProduct(
            Guid id,
            string name,
            string description,
            bool active,
            decimal price,
            int stockQuantity,
            Category category,
            string image,
            int height,
            int width,
            int depth) =>
            new(
                id == Guid.Empty ? throw new ArgumentException("Id is mandatory") : ProductId.CreateFrom(id),
                Name.Create(name),
                Description.Create(description),
                active,
                Price.Create(price),
                StockQuantity.Create(stockQuantity),
                category,
                RegisterDate.Create(DateTime.Now),
                Image.CreateFromHash(image),
                Dimension.Create(height, width, depth));

        public static Product Default =>
            new(
                ProductId.Empty,
                Name.Create(string.Empty),
                Description.Create(string.Empty),
                false,
                Price.Create(0),
                StockQuantity.Create(0),
                Category.None,
                RegisterDate.Create(DateTime.MinValue),
                Image.CreateFromHash(string.Empty),
                Dimension.Create(0, 0, 0));

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