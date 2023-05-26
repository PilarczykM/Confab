﻿using Confab.Shared.Abstractions.Module;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Confab.Shared.Infrastructure.Modules
{
    public static class Extensions
    {
        internal static IServiceCollection AddModuleInfo(
            this IServiceCollection services,
            IList<IModule> modules
        )
        {
            var moduleInfoProvider = new ModuleInfoProvider();
            var moduleInfo =
                modules?.Select(
                    x => new ModuleInfo(x.Name, x.Path, x.Policies ?? Enumerable.Empty<string>())
                ) ?? Enumerable.Empty<ModuleInfo>();

            moduleInfoProvider.Modules.AddRange(moduleInfo);

            services.AddSingleton(moduleInfoProvider);

            return services;
        }

        internal static void MapModuleInfo(this IEndpointRouteBuilder endpoint)
        {
            endpoint.MapGet(
                "modules",
                context =>
                {
                    var moduleInfoProvider =
                        context.RequestServices.GetRequiredService<ModuleInfoProvider>();

                    return context.Response.WriteAsJsonAsync(moduleInfoProvider.Modules);
                }
            );
        }

        internal static IHostBuilder ConfigureModules(this IHostBuilder hostBuilder) =>
            hostBuilder.ConfigureAppConfiguration(
                (ctx, cfg) =>
                {
                    foreach (var setting in GetSettings("*"))
                    {
                        cfg.AddJsonFile(setting);
                    }

                    foreach (
                        var setting in GetSettings($"*.{ctx.HostingEnvironment.EnvironmentName}")
                    )
                    {
                        cfg.AddJsonFile(setting);
                    }

                    IEnumerable<string> GetSettings(string pattern) =>
                        Directory.EnumerateFiles(
                            ctx.HostingEnvironment.ContentRootPath,
                            $"module.{pattern}.json",
                            SearchOption.AllDirectories
                        );
                }
            );
    }
}
