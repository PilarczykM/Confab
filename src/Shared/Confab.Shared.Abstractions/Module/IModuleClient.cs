namespace Confab.Shared.Abstractions.Module
{
    public interface IModuleClient
    {
        Task PublishAsync(object message);
        Task<TResult> SendAsync<TResult>(string path, object request)
            where TResult : class;
    }
}
