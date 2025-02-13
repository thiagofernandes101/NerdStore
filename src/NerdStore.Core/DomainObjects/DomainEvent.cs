using NerdStore.Core.Messages;

namespace NerdStore.Core.DomainObjects
{
    public abstract class DomainEvent<TId> : Event<TId> where TId : notnull
    {
        protected DomainEvent(TId aggregateId) : base(aggregateId)
        {
        }
    }
}
