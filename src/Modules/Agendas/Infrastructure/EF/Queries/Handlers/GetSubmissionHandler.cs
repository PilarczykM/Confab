using Confab.Modules.Agendas.Application.Submissions.DTO;
using Confab.Modules.Agendas.Application.Submissions.Queries;
using Confab.Modules.Agendas.Domain.Submissions.Entities;
using Confab.Shared.Abstractions.Queries;
using Microsoft.EntityFrameworkCore;

namespace Confab.Modules.Agendas.Infrastructure.EF.Queries.Handlers
{
    internal class GetSubmissionHandler : IQueryHandler<GetSubmission, SubmissionDto>
    {
        private DbSet<Submission> _submission;

        public GetSubmissionHandler(AgendasDbContext context)
            => _submission = context.Submissions;

        public async Task<SubmissionDto> HandleAsync(GetSubmission query)
            => await _submission
            .AsNoTracking()
            .Where(x => x.Id.Equals(query.Id))
            .Include(x => x.Speakers)
            .Select(x => new SubmissionDto
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                ConferenceId = x.ConferenceId,
                Level = x.Level,
                Status = x.Status,
                Tags = x.Tags,
                Speakers = x.Speakers.Select(s => new SpeakerDto
                {
                    FullName = s.FullName,
                    Id = s.Id
                })
            }).SingleOrDefaultAsync();

    }
}

