using Confab.Shared.Abstractions.Module;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Confab.Modules.Saga
{
    public class SagaModule : IModule
    {
        public const string BasePath = "saga-module";
        public string Name => "Saga";

        public string Path => BasePath;

        public void Register(IServiceCollection services)
        {
        }

        public void Use(IApplicationBuilder app)
        {
        }
    }
}

