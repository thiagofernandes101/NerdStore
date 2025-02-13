namespace NerdStore.Core.Messages
{
    public record MessageType(string Value);

    public abstract class Message<TId> where TId : notnull
    {
        public MessageType MessageType { get; set; }
        public TId AggregateId { get; set; }

        protected Message(TId aggregateId)
        {
            AggregateId = aggregateId;
        }
    }
}
