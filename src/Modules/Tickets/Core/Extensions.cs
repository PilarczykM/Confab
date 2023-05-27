using Confab.Modules.Tickets.Core.DAL;
using Confab.Modules.Tickets.Core.DAL.Repositories;
using Confab.Modules.Tickets.Core.Repositories;
using Confab.Modules.Tickets.Core.Services;
using Confab.Shared.Infrastructure.Postgres;
using Microsoft.Extensions.DependencyInjection;

namespace Confab.Modules.Tickets.Core
{
    public static class Extensions
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            services.AddScoped<ITicketService, TicketService>();
            services.AddScoped<ITicketSaleService, TicketSaleService>();
            services.AddScoped<IConferenceRepository, ConferenceRepository>();
            services.AddScoped<ITicketRepository, TicketRepository>();
            services.AddScoped<ITicketSaleRepository, TicketSaleRepository>();
            services.AddSingleton<ITicketGenereator, TicketGenereator>();
            services.AddPostgres<TicketsDbContext>();

            return services;
        }
    }
}
