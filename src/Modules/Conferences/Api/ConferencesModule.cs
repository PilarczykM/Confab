using Confab.Modules.Conferences.Core;
using Confab.Shared.Abstractions.Module;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Confab.Modules.Conferences.Api
{
    public class ConferencesModule : IModule
    {
        public const string BasePath = "conferences-module";

        public string Name => "Conferences";

        public string Path => BasePath;

        public IEnumerable<string> Policies { get; } = new[] { "conferences", "host" };

        public void Register(IServiceCollection services)
        {
            services.AddCore();
        }

        public void Use(IApplicationBuilder app) { }
    }
}
