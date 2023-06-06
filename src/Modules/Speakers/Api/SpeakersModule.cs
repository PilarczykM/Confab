using Confab.Modules.Speakers.Core;
using Confab.Modules.Speakers.Core.DTO;
using Confab.Modules.Speakers.Core.Services;
using Confab.Shared.Abstractions.Module;
using Confab.Shared.Infrastructure.Modules;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Confab.Modules.Speakers.Api
{
    internal class SpeakersModule : IModule
    {
        public const string BasePath = "speakers-module";

        public string Name => "Speakers";

        public string Path => BasePath;

        public IEnumerable<string> Policies { get; } = new[] { "speakers" };

        public void Register(IServiceCollection services)
        {
            services.AddCore();
        }

        public void Use(IApplicationBuilder app)
        {
            app.UseModuleRequest()
                .Subscribe<SpeakerDto, object>(
                    "speakers/create",
                    async (dto, sp) =>
                    {
                        var service = sp.GetRequiredService<ISpeakersService>();
                        await service.CreateAsync(dto);
                        return null;
                    }
                );
        }
    }
}
