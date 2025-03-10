﻿using FluentValidation.Results;
using NerdStore.Catalog.Domain.Validations;
using NerdStore.Catalog.Domain.ValueObjects;
using NerdStore.Core.DomainObjects;

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
        public Stock Stock { get; private set; }
        public Category Category { get; private set; }
        public Dimension Dimension { get; private set; }

        [Obsolete("Parameterless constructor is for EF Core and mapping only.")]
        private Product() { }

        private Product(ProductId id, Name name, Description description, bool active, Price price, Stock stock, Category category, RegisterDate registerDate, Image image, Dimension dimension) : base(id)
        {
            Name = name;
            Description = description;
            Active = active;
            Price = price;
            Stock = stock;
            CategoryId = category?.Id ?? Category.Empty.Id;
            Category = category ?? Category.Empty;
            RegisterDate = registerDate;
            Image = image;
            Dimension = dimension;
        }

        public static Product CreateProductWithoutCategory(string name, string description, bool active, decimal price, int stockQuantity, string image, int height, int width, int depth) =>
            new(ProductId.NewId, Name.Create(name), Description.Create(description), active, Price.Create(price), Stock.Create(stockQuantity), Category.None, RegisterDate.Create(DateTime.Now), Image.CreateFromHash(image), Dimension.Create(height, width, depth));

        public static Product CreateProduct(string name, string description, bool active, decimal price, int stockQuantity, Category category, string image, int height, int width, int depth) =>
            new(ProductId.NewId, Name.Create(name), Description.Create(description), active, Price.Create(price), Stock.Create(stockQuantity), category, RegisterDate.Create(DateTime.Now), Image.CreateFromHash(image), Dimension.Create(height, width, depth));

        public void Activate() => Active = true;

        public void Deactivate() => Active = false;

        public void ChangeCategory(Category category)
        {
            Category = category;
            CategoryId = category.Id;
        }

        public void ChagneDescription(string description) => Description = Description.Create(description);

        public Result<Product> DebitStock(int quantity)
        {
            var debitQuantity = Math.Abs(quantity);

            if (!HasStock(debitQuantity))
                return Result<Product>.Failure("Insufficient stock.");

            Stock = Stock.Debit(Stock, debitQuantity);
            return Result<Product>.Success(this);
        }

        public bool HasStock(int quantity) => Stock.Amount >= quantity;

        public Result<Product> ReplenishStock(int quantity)
        {
            Stock = Stock.Replenish(Stock, quantity);
            return Result<Product>.Success(this);
        }

        private static readonly ProductValidator _validator = new();
        public ValidationResult IsValid() => _validator.Validate(this);
    }
}