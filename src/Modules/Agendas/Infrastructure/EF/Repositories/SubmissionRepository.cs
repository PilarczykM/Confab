﻿using Confab.Modules.Agendas.Domain.Submissions.Entities;
using Confab.Modules.Agendas.Domain.Submissions.Repositories;
using Confab.Shared.Abstractions.Kernel.Types;
using Microsoft.EntityFrameworkCore;

namespace Confab.Modules.Agendas.Infrastructure.EF.Repositories
{
    internal class SubmissionRepository : ISubmissionRepository
    {
        private readonly AgendasDbContext _context;
        private readonly DbSet<Submission> _submissions;

        public SubmissionRepository(AgendasDbContext context)
        {
            _context = context;
            _submissions = _context.Submissions;
        }

        public async Task AddAsync(Submission submission)
        {
            await _submissions.AddAsync(submission);
            await _context.SaveChangesAsync();
        }

        public Task<Submission> GetAsync(AggregateId id)
            => _submissions.Include(x => x.Speakers).SingleOrDefaultAsync(x => x.Id.Equals(id));

        public async Task UpdateAsync(Submission submission)
        {
            _submissions.Update(submission);
            await _context.SaveChangesAsync();
        }
    }
}

