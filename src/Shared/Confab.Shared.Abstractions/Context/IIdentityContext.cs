namespace Confab.Shared.Abstractions.Context
{
    public interface IIdentityContext
    {
        bool IsAuthenticated { get; }
        public Guid Id { get; }
        string Role { get; }
        IDictionary<string, IEnumerable<string>> Claims { get; }
    }
}

