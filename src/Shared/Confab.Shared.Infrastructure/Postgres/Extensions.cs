using Microsoft.Extensions.DependencyInjection;

namespace Confab.Shared.Infrastructure.Postgres
{
    public static class Extensions
    {
        internal static IServiceCollection AddPostgres(this IServiceCollection services)
        {
            var options = services.GetOptions<PostgresOptions>(PostgresOptions.Postgres);
            services.AddSingleton(options);

            return services;
        }
    }
}

