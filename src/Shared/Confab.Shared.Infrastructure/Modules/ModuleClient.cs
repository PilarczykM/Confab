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

        public Task SendAsync(string path, object request) => SendAsync<object>(path, request);

        public async Task<TResult> SendAsync<TResult>(string path, object request)
            where TResult : class
        {
            var registration =
                _moduleRegistry.GetRequestRegistration(path)
                ?? throw new InvalidOperationException(
                    $"No action has been defined for path: '{path}'"
                );

            var reciverRequest = TranslateType(request, registration.RequestType);
            var result = await registration.Action(reciverRequest);

            return result is null ? null : TranslateType<TResult>(result);
        }

        private T TranslateType<T>(object value) =>
            _moduleSerializer.Deserialize<T>(_moduleSerializer.Serialize(value));

        private object TranslateType(object value, Type type) =>
            _moduleSerializer.Deserialize(_moduleSerializer.Serialize(value), type);
    }
}
