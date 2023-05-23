using Confab.Modules.Speakers.Core.DAL;
using Confab.Modules.Speakers.Core.DAL.Repositories;
using Confab.Modules.Speakers.Core.Services;
using Confab.Shared.Infrastructure.Postgres;
using Microsoft.Extensions.DependencyInjection;

namespace Confab.Modules.Speakers.Core
{
    public static class Extensions
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            services.AddScoped<ISpeakersService, SpeakersService>();
            services.AddScoped<ISpeakersRepository, SpeakersRepository>();
            services.AddPostgres<SpeakerDbContext>();

            return services;
        }
    }
}

