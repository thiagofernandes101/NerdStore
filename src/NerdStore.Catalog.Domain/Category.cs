using NerdStore.Core;

namespace NerdStore.Catalog.Domain
{
    public readonly record struct CategoryId(Guid Value)
    {
        public CategoryId() : this(Guid.NewGuid()) { }
        public override string ToString() => Value.ToString();
    }

    public record CategoryName
    {
        public string Value { get; }

        public CategoryName(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Category name cannot be empty.", nameof(value));

            if (value.Length > 100)
                throw new ArgumentException("Category name cannot exceed 100 characters.", nameof(value));

            Value = value;
        }

        public override string ToString() => Value;
    }

    public record CategoryCode
    {
        public int Value { get; }

        public CategoryCode(int value)
        {
            if (value <= 0)
                throw new ArgumentException("Category code must be greater than zero.", nameof(value));

            Value = value;
        }

        public override string ToString() => Value.ToString();
    }

    public class Category : Entity<CategoryId>
    {
        public CategoryName Name { get; private set; }
        public CategoryCode Code { get; private set; }

        public Category(CategoryId id, CategoryName name, CategoryCode code) : base(id)
        {
            Name = name;
            Code = code;
        }

        public override string ToString() => $"{Name} - {Code}";
    }
}
