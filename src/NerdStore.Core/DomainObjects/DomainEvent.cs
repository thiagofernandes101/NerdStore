using NerdStore.Core.Messages;

namespace NerdStore.Core.DomainObjects
{
    public class DomainEvent<TId> : Event<TId> where TId : notnull
    {
        public DomainEvent(TId aggregateId) : base(aggregateId)
        {
        }
    }
}
