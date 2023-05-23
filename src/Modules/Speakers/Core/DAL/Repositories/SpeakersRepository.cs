using Confab.Modules.Speakers.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Confab.Modules.Speakers.Core.DAL.Repositories
{
    internal class SpeakersRepository : ISpeakersRepository
    {
        private readonly DbSet<Speaker> _speakers;
        private readonly SpeakerDbContext _context;

        public SpeakersRepository(SpeakerDbContext context)
        {
            _context = context;
            _speakers = context.Speakers;
        }

        public async Task AddAsync(Speaker speaker)
        {
            await _speakers.AddAsync(speaker);
            await _context.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<Speaker>> BrowseAsync()
            => await _speakers.ToListAsync();

        public async Task<bool> ExistsAsync(Guid id)
            => await _speakers.AnyAsync(x => x.Id == id);

        public async Task<Speaker> GetAsync(Guid id)
            => await _speakers.SingleOrDefaultAsync(x => x.Id == id);

        public async Task Update(Speaker speaker)
        {
            _speakers.Update(speaker);
            await _context.SaveChangesAsync();
        }
    }
}

