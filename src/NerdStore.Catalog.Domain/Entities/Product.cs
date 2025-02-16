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
        public static ProductId CreateFrom(Guid id) => new(id);

        public override string ToString() => Value.ToString();
    }

    public class Product : Entity<ProductId>, IAggregateRoot
    {
        public CategoryId CategoryId { get; private set; }
        public ProductName Name { get; private set; }
        public ProductDescription Description { get; private set; }
        public bool Active { get; private set; }
        public ProductPrice Price { get; private set; }
        public ProductRegisterDate RegisterDate { get; private set; }
        public ProductImageHash Image { get; private set; }
        public ProductStockQuantity StockQuantity { get; private set; }
        public Category Category { get; private set; }
        public ProductDimension Dimension { get; private set; }

        private static readonly ProductValidator _validator = new();

        [Obsolete("Parameterless constructor is for EF Core and mapping only.")]
        public Product() { }

        private Product(
            ProductId id,
            ProductName name,
            ProductDescription description,
            bool active,
            ProductPrice price,
            ProductStockQuantity stockQuantity,
            Category category,
            ProductRegisterDate registerDate,
            ProductImageHash image,
            ProductDimension dimension) : base(id)
        {
            Name = name;
            Description = description;
            Active = active;
            Price = price;
            StockQuantity = stockQuantity;
            CategoryId = category.Id;
            Category = category;
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
                ProductName.Create(name),
                ProductDescription.Create(description),
                active,
                ProductPrice.Create(price),
                ProductStockQuantity.Create(stockQuantity),
                Category.None,
                ProductRegisterDate.Create(DateTime.Now),
                ProductImageHash.Create(image),
                ProductDimension.Create(height, width, depth));

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
                ProductName.Create(name),
                ProductDescription.Create(description),
                active,
                ProductPrice.Create(price),
                ProductStockQuantity.Create(stockQuantity),
                category,
                ProductRegisterDate.Create(DateTime.Now),
                ProductImageHash.Create(image),
                ProductDimension.Create(height, width, depth));

        public static Product Default =>
            new(
                ProductId.Empty,
                ProductName.Create(string.Empty),
                ProductDescription.Create(string.Empty),
                false,
                ProductPrice.Create(0),
                ProductStockQuantity.Create(0),
                Category.None,
                ProductRegisterDate.Create(DateTime.MinValue),
                ProductImageHash.Create(string.Empty),
                ProductDimension.Create(0, 0, 0));

        public void Activate() => Active = true;

        public void Deactivate() => Active = false;

        public void ChangeCategory(Category category)
        {
            Category = category;
            CategoryId = category.Id;
        }

        public void ChagneDescription(string description) =>
            Description = ProductDescription.Create(description);

        public void DebitStock(int quantity)
        {
            if (quantity < 0)
                quantity *= -1;

            if (!HasStock(quantity))
                throw new DomainException("Insufficient stock.");

            StockQuantity = ProductStockQuantity.Create(StockQuantity.Value - quantity);
        }

        public bool HasStock(int quantity) =>
            StockQuantity.Value >= quantity;

        public void ReplenishStock(int quantity) =>
            StockQuantity = ProductStockQuantity.Create(StockQuantity.Value + quantity);

        public ValidationResult IsValid() =>
            _validator.Validate(this);
    }
}