using System.Reflection;
using Confab.Shared.Abstractions.Module;
using Confab.Shared.Infrastructure;
using Confab.Shared.Infrastructure.Modules;

namespace Confab.Bootstrapper
{
    public class Startup
    {
        private readonly IList<Assembly> _assemblies;
        private readonly IList<IModule> _modules;

        public Startup(IConfiguration configuration)
        {
            _assemblies = ModuleLoader.LoadAssemblies(configuration);
            _modules = ModuleLoader.LoadModules(_assemblies);
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSharedInfrastructure(_assemblies, _modules);

            foreach (var module in _modules)
            {
                module.Register(services);
            }
        }

        public void Configure(IApplicationBuilder app, ILogger<Startup> logger)
        {
            app.UseSharedInfrastructure();

            foreach (var module in _modules)
            {
                module.Use(app);
            }

            logger.LogInformation($"Modules: '{string.Join(", ", _modules.Select(x => x.Name))}'");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapGet("/", context => context.Response.WriteAsync("Confab API!"));
                endpoints.MapModuleInfo();
            });

            _assemblies.Clear();
            _modules.Clear();
        }
    }
}
