using Confab.Shared.Abstractions.Module;

namespace Confab.Shared.Infrastructure.Modules
{
    internal class ModuleClient : IModuleClient
    {
        private readonly IModuleRegistry _moduleRegistry;
        private readonly IModuleSerializer _moduleSerializer;

        public ModuleClient(IModuleRegistry moduleRegistry, IModuleSerializer moduleSerializer)
        {
            _moduleRegistry = moduleRegistry;
            _moduleSerializer = moduleSerializer;
        }

        public async Task PublishAsync(object message)
        {
            var key = message.GetType().Name;

            var registrations = _moduleRegistry.GetBroadcastRegistrations(key);

            var tasks = new List<Task>();

            foreach (var registry in registrations)
            {
                var recieverMessage = TranslateType(message, registry.ReciverType);
                tasks.Add(registry.Action(recieverMessage));
            }

            await Task.WhenAll(tasks);
        }

        private object TranslateType(object value, Type type) =>
            _moduleSerializer.Deserialize(_moduleSerializer.Serialize(value), type);
    }
}
