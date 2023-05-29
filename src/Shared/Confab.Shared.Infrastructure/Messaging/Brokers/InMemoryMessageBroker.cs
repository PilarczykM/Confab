using Confab.Shared.Abstractions.Messaging;
using Confab.Shared.Abstractions.Module;

namespace Confab.Shared.Infrastructure.Messaging.Brokers
{
    internal sealed class InMemoryMessageBroker : IMessageBroker
    {
        private readonly IModuleClient _moduleClient;

        public InMemoryMessageBroker(IModuleClient moduleClient)
        {
            _moduleClient = moduleClient;
        }

        public async Task PublishAsync(params IMessage[] messages)
        {
            if (messages is null)
            {
                return;
            }

            var tasks = new List<Task>();
            foreach (var message in messages)
            {
                if (message is null)
                {
                    continue;
                }

                tasks.Add(_moduleClient.PublishAsync(message));
            }

            await Task.WhenAll(tasks);
        }
    }
}
