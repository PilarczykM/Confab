using System.Runtime.CompilerServices;
using Confab.Shared.Abstractions.Time;
using Confab.Shared.Infrastructure.Api;
using Confab.Shared.Infrastructure.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

[assembly: InternalsVisibleTo("Confab.Bootstrapper")]

namespace Confab.Shared.Infrastructure
{
    internal static class Extensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddSingleton<IClock, UtcClock>();
            services.AddErrorHandling();
            services
                .AddControllers()
                .ConfigureApplicationPartManager(manager =>
                {
                    manager.FeatureProviders.Add(new InternalControllerFeatureProvider());
                });

            return services;
        }

        public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app)
        {
            app.UseErrorHandling();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapGet("/", context => context.Response.WriteAsync("Confab API!"));
            });

            return app;
        }
    }
}
