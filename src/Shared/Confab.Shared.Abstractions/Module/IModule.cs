using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Confab.Shared.Abstractions.Module
{
    public interface IModule
    {
        string Name { get; }
        string Path { get; }
        void Register(IServiceCollection services);
        void Use(IApplicationBuilder app);
    }
}
