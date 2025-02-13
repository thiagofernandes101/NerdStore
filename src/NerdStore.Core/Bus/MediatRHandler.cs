using MediatR;
using NerdStore.Core.Messages;

namespace NerdStore.Core.Bus
{
    public interface IMediatRHandler
    {
        Task PublishEvent<TEvent, TId>(TEvent domainEvent) where TEvent : Event<TId> where TId : notnull;
    }

    public class MediatRHandler : IMediatRHandler
    {
        private readonly IMediator _mediator;

        public MediatRHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task PublishEvent<TEvent, TId>(TEvent domainEvent) where TEvent : Event<TId> where TId : notnull
        {
            await _mediator.Publish(domainEvent);
        }
    }
}
