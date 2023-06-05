using Confab.Modules.Agendas.Application.Submissions.DTO;
using Confab.Modules.Agendas.Domain.Submissions.Entities;

namespace Confab.Modules.Agendas.Infrastructure.EF.Mappings
{
    internal static class SubmissionExtension
    {
        public static SubmissionDto AsDto(this Submission submission)
        => new()
        {
            Id = submission.Id,
            Title = submission.Title,
            Description = submission.Description,
            ConferenceId = submission.ConferenceId,
            Level = submission.Level,
            Status = submission.Status,
            Tags = submission.Tags,
            Speakers = submission.Speakers.Select(s => new SpeakerDto
            {
                FullName = s.FullName,
                Id = s.Id
            })
        };
    }
}

