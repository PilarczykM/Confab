using Chronicle;
using Microsoft.Extensions.DependencyInjection;

namespace Confab.Modules.Sagas
{
    public static class Extensions
    {
        public static IServiceCollection AddSaga(this IServiceCollection services)
        {
            services.AddChronicle();
            return services;
        }
    }
}
