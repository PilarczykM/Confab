using Confab.Modules.Attendances.Application.Clients.Agendas;
using Confab.Modules.Attendances.Infrastructure.Clients;
using Microsoft.Extensions.DependencyInjection;

namespace Confab.Modules.Attendances.Infrastructure
{
    public static class Extensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddSingleton<IAgendasApiClient, AgendasApiClients>();

            return services;
        }
    }
}
