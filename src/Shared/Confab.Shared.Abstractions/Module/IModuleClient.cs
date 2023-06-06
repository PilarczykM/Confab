namespace Confab.Shared.Abstractions.Module
{
    public interface IModuleClient
    {
        Task SendAsync(string path, object request);
        Task PublishAsync(object message);
        Task<TResult> SendAsync<TResult>(string path, object request)
            where TResult : class;
    }
}
