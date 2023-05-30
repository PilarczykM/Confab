using Confab.Shared.Abstractions.Commands;
using Microsoft.Extensions.DependencyInjection;

namespace Confab.Shared.Infrastructure.Commands
{
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly IServiceProvider _serviceProvider;

        public CommandDispatcher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        async Task ICommandDispatcher.SendAsync<TCommand>(TCommand command)
        {
            if (command is null)
            {
                return;
            }

            using var scope = _serviceProvider.CreateScope();

            var handler = scope.ServiceProvider.GetRequiredService<ICommandHandler<TCommand>>();
            await handler.HandleAsync(command);
        }
    }
}
