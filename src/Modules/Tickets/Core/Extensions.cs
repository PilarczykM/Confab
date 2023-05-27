using Confab.Modules.Tickets.Core.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Confab.Modules.Tickets.Core
{
    public static class Extensions
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            services.AddScoped<ITicketService, TicketService>();
            services.AddScoped<ITicketSaleService, TicketSaleService>();

            services.AddSingleton<ITicketGenereator, TicketGenereator>();
            return services;
        }
    }
}

