namespace Confab.Shared.Abstractions.Context
{
    public interface IContext
    {
        string RequestID { get; }
        string TraceId { get; }
        IIdentityContext Identity { get; }
    }
}

