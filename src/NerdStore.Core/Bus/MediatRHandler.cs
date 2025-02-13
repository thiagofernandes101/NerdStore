using MediatR;

namespace NerdStore.Core.Bus
{
    public interface IMediatRHandler
    {
        Task PublishEvent<TEvent>(TEvent domainEvent) where TEvent : INotification;
    }

    public class MediatRHandler : IMediatRHandler
    {
        private readonly IMediator _mediator;

        public MediatRHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task PublishEvent<TEvent>(TEvent domainEvent) where TEvent : INotification
        {
            await _mediator.Publish(domainEvent);
        }
    }
}
