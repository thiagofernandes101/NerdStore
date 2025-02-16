namespace NerdStore.Core.DomainObjects
{
    public abstract class Entity<TId> where TId : notnull
    {
        // Protected parameterless constructor for EF Core
        protected Entity() { }

        protected Entity(TId id)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
        }

        public TId Id { get; private set; }

        public override bool Equals(object? obj) =>
            ReferenceEquals(this, obj) || obj is Entity<TId> other && Id.Equals(other.Id);

        public override int GetHashCode() => Id.GetHashCode();
        public static bool operator ==(Entity<TId>? a, Entity<TId>? b) =>
            ReferenceEquals(a, b) || a is not null && b is not null && a.Equals(b);

        public static bool operator !=(Entity<TId>? a, Entity<TId>? b) => !(a == b);
    }
}
