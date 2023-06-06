using Chronicle;
using Confab.Modules.Sagas.Messages;
using Confab.Shared.Abstractions.Events;

namespace Confab.Modules.Sagas
{
    internal class Handler : IEventHandler<SpeakerCreated>, IEventHandler<SignedUp>, IEventHandler<SignedIn>
    {
        private readonly ISagaCoordinator _coordinator;

        public Handler(ISagaCoordinator coordinator)
            => _coordinator = coordinator;

        public Task HandleAsync(SpeakerCreated @event) => _coordinator.ProcessAsync(@event, SagaContext.Empty);

        public Task HandleAsync(SignedUp @event) => _coordinator.ProcessAsync(@event, SagaContext.Empty);

        public Task HandleAsync(SignedIn @event) => _coordinator.ProcessAsync(@event, SagaContext.Empty);
    }
}

