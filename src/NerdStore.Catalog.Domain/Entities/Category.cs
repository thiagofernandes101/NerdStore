using NerdStore.Catalog.Domain.ValueObjects;
using NerdStore.Core.DomainObjects;

namespace NerdStore.Catalog.Domain.Entities
{
    public readonly record struct CategoryId(Guid Value)
    {
        public static CategoryId Empty => new(Guid.Empty);
        public static CategoryId NewId => new(Guid.NewGuid());
        public static CategoryId CreateFrom(Guid id) => new(id);
        public override string ToString() => Value.ToString();
    }

    public class Category : Entity<CategoryId>
    {
        public Name Name { get; private set; }
        public CategoryCode Code { get; private set; }

        [Obsolete("EF Core relation only.")]
        public List<Product> Products { get; set; } = [];

        [Obsolete("Parameterless constructor is for EF Core and mapping only.")]
        private Category() { }

        private Category(
            CategoryId id,
            Name name,
            CategoryCode code) : base(id)
        {
            Name = name;
            Code = code;
        }

        public static Category Create(string name, int code) =>
            new(CategoryId.NewId, Name.Create(name), CategoryCode.Create(code));

        public static Category None =>
            new Category(CategoryId.NewId, Name.Create("None"), CategoryCode.Create(0));

        public static Category Empty =>
            new Category(CategoryId.Empty, Name.Create(string.Empty), CategoryCode.Create(0));

        public override string ToString() => $"{Name} - {Code}";
    }
}
