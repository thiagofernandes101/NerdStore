using MediatR;

namespace NerdStore.Core.Messages
{
    public record EventTime(DateTime Timestamp) 
    {
        public static EventTime Now => new(DateTime.Now);
    };

    public abstract class Event<TId> : Message<TId>, INotification where TId : notnull
    {
        public EventTime EventTime { get; set; }

        protected Event(TId aggregateId) : base(aggregateId)
        {
            EventTime = EventTime.Now;
        }
    }
}
