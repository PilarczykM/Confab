using Confab.Modules.Conferences.Core.Entities;

namespace Confab.Modules.Conferences.Core.Repositories
{
    internal class InMemoryConferenceRepository : IConferenceRepository
    {
        // Not thread safe. Use concurrent collections.
        private readonly List<Conference> _conferences = new();

        public Task AddAsync(Conference conference)
        {
            _conferences.Add(conference);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(Conference conference)
        {
            _conferences.Remove(conference);
            return Task.CompletedTask;
        }

        public async Task<IReadOnlyList<Conference>> GetAllAsync()
        {
            await Task.CompletedTask;
            return _conferences;
        }

        public Task<Conference> GetAsync(Guid id) =>
            Task.FromResult(_conferences.SingleOrDefault(x => x.Id == id));

        public Task UpdateAsync(Conference conference)
        {
            return Task.CompletedTask;
        }
    }
}

