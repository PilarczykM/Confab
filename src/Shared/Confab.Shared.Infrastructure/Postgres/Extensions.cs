using Microsoft.EntityFrameworkCore;
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

        public static IServiceCollection AddPostgres<T>(this IServiceCollection services) where T : DbContext
        {
            var options = services.GetOptions<PostgresOptions>(PostgresOptions.Postgres);
            services.AddDbContext<T>(x => x.UseNpgsql(options.ConnectionStrings));

            return services;
        }
    }
}

