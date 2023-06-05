﻿using Confab.Modules.Attendances.Application.Clients.Agendas;
using Confab.Modules.Attendances.Domain.Repositories;
using Confab.Modules.Attendances.Infrastructure.Clients;
using Confab.Modules.Attendances.Infrastructure.EF;
using Confab.Modules.Attendances.Infrastructure.EF.Repositories;
using Confab.Shared.Infrastructure.Postgres;
using Microsoft.Extensions.DependencyInjection;

namespace Confab.Modules.Attendances.Infrastructure
{
    public static class Extensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddSingleton<IAgendasApiClient, AgendasApiClients>();
            services.AddScoped<IAttendableEventsRepository, AttendableEventsRepository>();
            services.AddScoped<IParticipantsRepository, ParticipantsRepository>();
            services.AddPostgres<AttendancesDbContext>();

            return services;
        }
    }
}
