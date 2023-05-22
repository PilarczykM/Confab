using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Confab.Shared.Infrastructure.Middlewares
{
    internal static class Extensions
    {
        public static IServiceCollection AddMiddlewares(this IServiceCollection services)
        {
            services.AddSingleton<ExceptionMiddleware>();

            return services;
        }

        public static IApplicationBuilder UseMiddlewares(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();

            return app;
        }
    }
}

