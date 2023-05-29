using Confab.Shared.Abstractions.Module;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Confab.Shared.Infrastructure.Messaging.Dispatchers
{
    internal class BackgroundDispatcher : BackgroundService
    {
        private readonly IMessageChannel _messageChannel;
        private readonly IModuleClient _moduleClient;
        private readonly ILogger _logger;

        public BackgroundDispatcher(
            IMessageChannel messageChannel,
            IModuleClient moduleClient,
            ILogger<BackgroundDispatcher> logger
        )
        {
            _messageChannel = messageChannel;
            _moduleClient = moduleClient;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Running the background dispatcher");

            await foreach (var message in _messageChannel.Reader.ReadAllAsync(stoppingToken))
            {
                try
                {
                    await _moduleClient.PublishAsync(message);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, ex.Message);
                }
            }

            _logger.LogInformation("Finished running the background dispatcher.");
        }
    }
}
