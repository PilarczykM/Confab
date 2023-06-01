using Confab.Shared.Abstractions.Kernel;
using Microsoft.Extensions.DependencyInjection;

namespace Confab.Shared.Infrastructure.Kernel
{
    public class DomainEventDispatcher : IDomainEventDispatcher
    {
        private readonly IServiceProvider _service;

        public DomainEventDispatcher(IServiceProvider service)
        {
            _service = service;
        }

        public async Task DispatchAsync(params IDomainEvent[] events)
        {
            if (events is null || !events.Any())
            {
                return;
            }

            using var scope = _service.CreateScope();

            foreach (var @event in events)
            {
                var handlerType = typeof(IDomainEventHandler<>).MakeGenericType(@event.GetType());
                var handlers = scope.ServiceProvider.GetServices(handlerType);

                var tasks = handlers.Select(
                    handler =>
                        (Task)
                            handlerType
                                .GetMethod(nameof(IDomainEventHandler<IDomainEvent>.HandleAsync))
                                ?.Invoke(handler, new[] { @event })
                );

                await Task.WhenAll(tasks);
            }
        }
    }
}
