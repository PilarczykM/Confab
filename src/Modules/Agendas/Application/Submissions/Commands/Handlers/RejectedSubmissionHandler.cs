using Confab.Modules.Agendas.Application.Submissions.Exceptions;
using Confab.Modules.Agendas.Domain.Submissions.Repositories;
using Confab.Shared.Abstractions.Commands;

namespace Confab.Modules.Agendas.Application.Submissions.Commands.Handlers
{
    public sealed class RejectedSubmissionHandler : ICommandHandler<RejectedSubmission>
    {
        private readonly ISubmissionRepository _submissionRepository;

        public RejectedSubmissionHandler(ISubmissionRepository submissionRepository)
        {
            _submissionRepository = submissionRepository;
        }

        public async Task HandleAsync(RejectedSubmission command)
        {
            var submission =
                await _submissionRepository.GetAsync(command.Id)
                ?? throw new SubmissionNotFoundException(command.Id);

            submission.Rejected();
            await _submissionRepository.UpdateAsync(submission);
        }
    }
}
