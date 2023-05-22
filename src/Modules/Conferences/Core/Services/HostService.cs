using Confab.Modules.Conferences.Core.DTO;
using Confab.Modules.Conferences.Core.Entities;
using Confab.Modules.Conferences.Core.Exceptions;
using Confab.Modules.Conferences.Core.Policies;
using Confab.Modules.Conferences.Core.Repositories;

namespace Confab.Modules.Conferences.Core.Services
{
    internal class HostService : IHostService
    {
        private readonly IHostRepository _repository;
        private readonly IHostDeletionPolicy _hostDeletionPolicy;

        public HostService(IHostRepository repository, IHostDeletionPolicy hostDeletionPolicy)
        {
            _repository = repository;
            _hostDeletionPolicy = hostDeletionPolicy;
        }

        public async Task AddAsync(HostDto dto)
        {
            dto.Id = Guid.NewGuid();
            await _repository.AddAsync(
                new()
                {
                    Id = dto.Id,
                    Name = dto.Name,
                    Description = dto.Description,
                }
            );
        }

        public async Task DeleteAsync(Guid id)
        {
            var host = await _repository.GetAsync(id) ?? throw new HostNotFoundException(id);
            if (await _hostDeletionPolicy.CanDeleteAsync(host) is false)
            {
                throw new HostCanNotBeDeletedException(host.Id);
            }

            await _repository.DeleteAsync(host);
        }

        public async Task<IReadOnlyList<HostDto>> GetAllAsync()
        {
            var hosts = await _repository.GetAllAsync();

            return hosts.Select(Map<HostDto>).ToList();
        }

        public async Task<HostDetailsDto> GetAsync(Guid id)
        {
            var host = await _repository.GetAsync(id);

            if (host is null)
            {
                return null;
            }
            var dto = Map<HostDetailsDto>(host);
            dto.Conferences = host.Conferences
                ?.Select(
                    x =>
                        new ConferenceDto()
                        {
                            Id = x.Id,
                            HostId = x.HostId,
                            HostName = x.Host.Name,
                            Name = x.Name,
                            From = x.From,
                            To = x.To,
                            Location = x.Location,
                            LogoUrl = x.LogoUrl,
                            ParticipantsLimit = x.ParticipantsLimit
                        }
                )
                .ToList();

            return dto;
        }

        public async Task UpdateAsync(HostDetailsDto dto)
        {
            var host = await _repository.GetAsync(dto.Id);

            if (host is null)
            {
                throw new HostNotFoundException(host.Id);
            }

            host.Name = dto.Name;
            host.Description = dto.Description;
            await _repository.UpdateAsync(host);
        }

        private static T Map<T>(Host host)
            where T : HostDto, new() =>
            new()
            {
                Id = host.Id,
                Name = host.Name,
                Description = host.Description,
            };
    }
}
