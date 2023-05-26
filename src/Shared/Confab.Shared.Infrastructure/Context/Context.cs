using Confab.Shared.Abstractions.Context;
using Microsoft.AspNetCore.Http;

namespace Confab.Shared.Infrastructure.Context
{
    internal class Context : IContext
    {
        public string RequestID => $"{Guid.NewGuid():N}";

        public string TraceId { get; }

        public IIdentityContext Identity { get; }
        public static Context Empty => new();

        internal Context() { }

        public Context(HttpContext context)
            : this(context.TraceIdentifier, new IdentityContext(context.User)) { }

        internal Context(string tractId, IIdentityContext identity)
        {
            TraceId = tractId;
            Identity = identity;
        }
    }
}
