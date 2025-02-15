﻿using NerdStore.Core.DomainObjects;

namespace NerdStore.Catalog.Domain.Entities
{
    public readonly record struct CategoryId(Guid Value)
    {
        public static CategoryId Empty => new(Guid.Empty);
        public static CategoryId NewId => new(Guid.NewGuid());
        public static CategoryId From(Guid value) => new(value);
        public override string ToString() => Value.ToString();
    }

    public record CategoryName
    {
        public string Value { get; }

        private CategoryName(string value) => Value = value;

        public static CategoryName Create(string value) => new(value);

        public override string ToString() => Value;
    }

    public record CategoryCode
    {
        public int Value { get; }

        private CategoryCode(int value) => Value = value;

        public static CategoryCode Create(int value) => new(value);

        public override string ToString() => Value.ToString();
    }

    public class Category : Entity<CategoryId>
    {
        public CategoryName Name { get; private set; }
        public CategoryCode Code { get; private set; }

        [Obsolete("EF Core relation only.")]
        public List<Product> Products { get; set; } = [];

        [Obsolete("Parameterless constructor is for EF Core and mapping only.")]
        public Category() { }

        private Category(
            CategoryId id,
            CategoryName name,
            CategoryCode code) : base(id)
        {
            Name = name;
            Code = code;
        }

        public static Category Create(string name, int code) =>
            new(CategoryId.NewId, CategoryName.Create(name), CategoryCode.Create(code));

        public static Category None =>
            new Category(CategoryId.NewId, CategoryName.Create("None"), CategoryCode.Create(0));
        public override string ToString() => $"{Name} - {Code}";
    }
}
