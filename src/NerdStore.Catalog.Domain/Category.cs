using NerdStore.Core;

namespace NerdStore.Catalog.Domain
{
    public readonly record struct CategoryId(Guid Value)
    {
        public CategoryId() : this(Guid.NewGuid()) { }
        public override string ToString() => Value.ToString();
    }

    public class Category : Entity<CategoryId>
    {
        public Category(CategoryId id) : base(id)
        {
        }
    }
}
