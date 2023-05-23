using Confab.Modules.Conferences.Core.Entities;
using Confab.Modules.Conferences.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Confab.Modules.Conferences.Core.DAL.Repositories
{
    internal class ConferenceRepository : IConferenceRepository
    {
        private readonly ConferencesDbContext _dbContext;
        private readonly DbSet<Conference> _conference;
        public ConferenceRepository(ConferencesDbContext dbContext)
        {
            _dbContext = dbContext;
            _conference = _dbContext.Conferences;
        }

        public async Task AddAsync(Conference conference)
        {
            await _conference.AddAsync(conference);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Conference conference)
        {
            _conference.Remove(conference);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<Conference>> GetAllAsync()
            => await _conference.ToListAsync();

        public async Task<Conference> GetAsync(Guid id)
            => await _conference
            .Include(x => x.Host)
            .SingleOrDefaultAsync(x => x.Id == id);

        public async Task UpdateAsync(Conference conference)
        {
            _conference.Update(conference);
            await _dbContext.SaveChangesAsync();
        }
    }
}

