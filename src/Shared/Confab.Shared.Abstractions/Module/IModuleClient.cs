namespace Confab.Shared.Abstractions.Module
{
    public interface IModuleClient
    {
        Task PublishAsync(object message);
    }
}
