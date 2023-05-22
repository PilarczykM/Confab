using System.Runtime.CompilerServices;
using Microsoft.Extensions.DependencyInjection;

[assembly: InternalsVisibleTo("Confab.Bootstrapper")]
namespace Confab.Modules.Conferences.Core
{
    internal static class Extensions
    {
        public static IServiceCollection AddConferences(this IServiceCollection services)
        {
            services.AddCore();

            return services;
        }
    }
}

