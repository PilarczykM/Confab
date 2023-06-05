using Confab.Modules.Agendas.Application.Submissions.DTO;
using Confab.Modules.Agendas.Application.Submissions.Queries;
using Confab.Modules.Agendas.Domain.Submissions.Entities;
using Confab.Modules.Agendas.Infrastructure.EF.Mappings;
using Confab.Shared.Abstractions.Queries;
using Microsoft.EntityFrameworkCore;

namespace Confab.Modules.Agendas.Infrastructure.EF.Queries.Handlers
{
    internal class GetSubmissionHandler : IQueryHandler<GetSubmission, SubmissionDto>
    {
        private readonly DbSet<Submission> _submission;

        public GetSubmissionHandler(AgendasDbContext context) => _submission = context.Submissions;

        public async Task<SubmissionDto> HandleAsync(GetSubmission query) =>
            await _submission
                .AsNoTracking()
                .Where(x => x.Id.Equals(query.Id))
                .Include(x => x.Speakers)
                .Select(x => x.AsDto())
                .SingleOrDefaultAsync();
    }
}
