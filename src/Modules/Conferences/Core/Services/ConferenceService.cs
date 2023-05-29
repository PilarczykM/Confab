using Confab.Modules.Conferences.Core.DTO;
using Confab.Modules.Conferences.Core.Entities;
using Confab.Modules.Conferences.Core.Events;
using Confab.Modules.Conferences.Core.Exceptions;
using Confab.Modules.Conferences.Core.Policies;
using Confab.Modules.Conferences.Core.Repositories;
using Confab.Shared.Abstractions.Messaging;

namespace Confab.Modules.Conferences.Core.Services
{
    internal class ConferenceService : IConferenceService
    {
        private readonly IConferenceRepository _conferenceRepository;
        private readonly IHostRepository _hostRepository;
        private readonly IConferenceDelitionPolicy _conferenceDelitionPolicy;
        private readonly IMessageBroker _messageBroker;

        public ConferenceService(
            IConferenceRepository conferenceRepository,
            IHostRepository hostRepository,
            IConferenceDelitionPolicy conferenceDelitionPolicy,
            IMessageBroker messageBroker
        )
        {
            _conferenceRepository = conferenceRepository;
            _hostRepository = hostRepository;
            _conferenceDelitionPolicy = conferenceDelitionPolicy;
            _messageBroker = messageBroker;
        }

        public async Task AddAsync(ConferenceDetailsDto dto)
        {
            if (await _hostRepository.GetAsync(dto.HostId) is null)
            {
                throw new HostNotFoundException(dto.HostId);
            }

            dto.Id = Guid.NewGuid();
            var conference = new Conference()
            {
                Id = dto.Id,
                HostId = dto.HostId,
                Location = dto.Location,
                Description = dto.Description,
                Name = dto.Name,
                LogoUrl = dto.LogoUrl,
                ParticipantsLimit = dto.ParticipantsLimit,
                From = dto.From,
                To = dto.To
            };
            await _conferenceRepository.AddAsync(conference);

            await _messageBroker.PublishAsync(
                new ConferenceCreated(
                    conference.Id,
                    conference.Name,
                    conference.ParticipantsLimit,
                    conference.From,
                    conference.To
                )
            );
        }

        public async Task DeleteAsync(Guid id)
        {
            var conference =
                await _conferenceRepository.GetAsync(id)
                ?? throw new ConferenceNotFoundException(id);

            if (await _conferenceDelitionPolicy.CanDeleteAsync(conference) is false)
            {
                throw new ConferenceCanNotBeDeletedException(id);
            }

            await _conferenceRepository.DeleteAsync(conference);
        }

        public async Task<IReadOnlyList<ConferenceDto>> GetAllAsync()
        {
            var conferences = await _conferenceRepository.GetAllAsync();

            return conferences.Select(x => Map<ConferenceDto>(x)).ToList();
        }

        public async Task<ConferenceDetailsDto> GetAsync(Guid id)
        {
            var conference =
                await _conferenceRepository.GetAsync(id)
                ?? throw new ConferenceNotFoundException(id);

            var dto = Map<ConferenceDetailsDto>(conference);
            dto.Description = conference.Description;
            return dto;
        }

        public async Task UpdateAsync(ConferenceDetailsDto dto)
        {
            var conference =
                await _conferenceRepository.GetAsync(dto.Id)
                ?? throw new ConferenceNotFoundException(dto.Id);

            conference.Name = dto.Name;
            conference.Description = dto.Description;
            conference.Location = dto.Location;
            conference.LogoUrl = dto.LogoUrl;
            conference.From = dto.From;
            conference.To = dto.To;
            conference.ParticipantsLimit = dto.ParticipantsLimit;

            await _conferenceRepository.UpdateAsync(conference);
        }

        private static T Map<T>(Conference conference)
            where T : ConferenceDto, new() =>
            new()
            {
                Id = conference.Id,
                Name = conference.Name,
                HostId = conference.HostId,
                HostName = conference.Host?.Name,
                Location = conference.Location,
                ParticipantsLimit = conference.ParticipantsLimit,
                From = conference.From,
                LogoUrl = conference.LogoUrl,
                To = conference.To
            };
    }
}
