using Confab.Modules.Speakers.Core.DTO;

namespace Confab.Modules.Speakers.Core.Services
{
    public interface ISpeakersService
    {
        Task<IEnumerable<SpeakerDto>> BrowseAsync();
        Task<SpeakerDto> GetAsync(Guid id);
        Task CreateAsync(SpeakerDto speaker);
        Task UpdateAsync(SpeakerDto speaker);
    }
}
