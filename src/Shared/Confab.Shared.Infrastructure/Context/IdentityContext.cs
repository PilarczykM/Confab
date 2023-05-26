using System.Security.Claims;
using Confab.Shared.Abstractions.Context;

namespace Confab.Shared.Infrastructure.Context
{
    internal class IdentityContext : IIdentityContext
    {
        public Guid Id { get; }

        public string Role { get; }

        public IDictionary<string, IEnumerable<string>> Claims { get; }

        public bool IsAuthenticated { get; }

        public IdentityContext(ClaimsPrincipal principal)
        {
            IsAuthenticated = principal.Identity?.IsAuthenticated is true;
            Id = IsAuthenticated ? Guid.Parse(principal.Identity.Name) : Guid.Empty;
            Role = principal.Claims.SingleOrDefault(x => x.Type == ClaimTypes.Role)?.Value;
            Claims = principal.Claims
                .GroupBy(x => x.Type)
                .ToDictionary(x => x.Key, x => x.Select(c => c.Value.ToString()));
        }
    }
}
