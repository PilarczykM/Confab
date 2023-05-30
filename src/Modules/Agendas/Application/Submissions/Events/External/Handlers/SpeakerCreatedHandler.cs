using Confab.Modules.Agendas.Domain.Submissions.Entities;
using Confab.Modules.Agendas.Domain.Submissions.Repositories;
using Confab.Shared.Abstractions.Events;

namespace Confab.Modules.Agendas.Application.Submissions.Events.External.Handlers
{
    public sealed class SpeakerCreatedHandler : IEventHandler<SpeakerCreated>
    {
        private readonly ISpeakerRepository _speakerRepository;

        public SpeakerCreatedHandler(ISpeakerRepository speakerRepository)
        {
            _speakerRepository = speakerRepository;
        }

        async Task IEventHandler<SpeakerCreated>.HandleAsync(SpeakerCreated @event)
        {
            if (await _speakerRepository.ExistsAsync(@event.id))
            {
                return;
            }

            var speaker = Speaker.Create(@event.id, @event.FullName);
            await _speakerRepository.CreateAsync(speaker);
        }
    }
}

