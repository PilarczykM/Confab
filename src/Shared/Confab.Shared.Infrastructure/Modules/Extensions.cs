using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Confab.Shared.Infrastructure.Modules
{
    public static class Extensions
    {
        public static IHostBuilder ConfigureModules(this IHostBuilder hostBuilder) =>
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
