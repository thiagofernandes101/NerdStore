namespace NerdStore.Core
{
    public abstract class Entity
    {
        public Guid Id { get; set; }

        protected Entity()
        {
            Id = Guid.NewGuid();
        }

        public override bool Equals(object? obj) =>
            ReferenceEquals(this, obj) || (obj is Entity compareTo && Id.Equals(compareTo.Id));

        public static bool operator ==(Entity a, Entity b) =>
            ReferenceEquals(a, b) || (a is not null && b is not null && a.Equals(b));

        public static bool operator !=(Entity a, Entity b) =>
            !(a == b);

        public override int GetHashCode() =>
            (GetType().GetHashCode() * 907) + Id.GetHashCode();
    }
}
