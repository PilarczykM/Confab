using Confab.Modules.Speakers.Core.DAL.Repositories;
using Confab.Modules.Speakers.Core.DTO;
using Confab.Modules.Speakers.Core.Exceptions;
using Confab.Modules.Speakers.Core.Mapping;

namespace Confab.Modules.Speakers.Core.Services
{
    internal sealed class SpeakersService : ISpeakersService
    {
        private readonly ISpeakersRepository _speakersRepository;

        public SpeakersService(ISpeakersRepository speakersRepository)
        {
            _speakersRepository = speakersRepository;
        }

        public async Task<IEnumerable<SpeakerDto>> BrowseAsync()
        {
            var entities = await _speakersRepository.BrowseAsync();
            return entities?.Select(x => x.AsDto());
        }

        public async Task CreateAsync(SpeakerDto speaker)
        {
            var alreadyExists = await _speakersRepository.ExistsAsync(speaker.Id);

            if (alreadyExists)
            {
                throw new SpeakerAlreadyExistsException(speaker.Id);
            }

            await _speakersRepository.AddAsync(speaker.AsEntity());
        }

        public async Task<SpeakerDto> GetAsync(Guid id)
        {
            var entity = await _speakersRepository.GetAsync(id);
            return entity?.AsDto();
        }

        public async Task UpdateAsync(SpeakerDto speaker)
        {
            var alreadyExists = await _speakersRepository.ExistsAsync(speaker.Id);

            if (alreadyExists)
            {
                throw new SpeakerNotFoundException(speaker.Id);
            }

            await _speakersRepository.Update(speaker.AsEntity());
        }
    }
}

