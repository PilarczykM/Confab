using Confab.Modules.Speakers.Core.DAL.Repositories;
using Confab.Modules.Speakers.Core.DTO;
using Confab.Modules.Speakers.Core.Events;
using Confab.Modules.Speakers.Core.Exceptions;
using Confab.Modules.Speakers.Core.Mapping;
using Confab.Shared.Abstractions.Messaging;

namespace Confab.Modules.Speakers.Core.Services
{
    internal sealed class SpeakersService : ISpeakersService
    {
        private readonly ISpeakersRepository _speakersRepository;
        private readonly IMessageBroker _messageBroker;

        public SpeakersService(ISpeakersRepository speakersRepository, IMessageBroker messageBroker)
        {
            _speakersRepository = speakersRepository;
            _messageBroker = messageBroker;
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
            await _messageBroker.PublishAsync(new SpeakerCreated(speaker.Id, speaker.FullName));
        }

        public async Task<SpeakerDto> GetAsync(Guid id)
        {
            var entity = await _speakersRepository.GetAsync(id);
            return entity?.AsDto();
        }

        public async Task UpdateAsync(SpeakerDto speaker)
        {
            var speakerExists = await _speakersRepository.ExistsAsync(speaker.Id);

            if (!speakerExists)
            {
                throw new SpeakerNotFoundException(speaker.Id);
            }

            await _speakersRepository.Update(speaker.AsEntity());
        }
    }
}
