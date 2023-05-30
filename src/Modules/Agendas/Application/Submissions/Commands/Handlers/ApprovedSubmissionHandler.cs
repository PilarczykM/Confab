using Confab.Modules.Agendas.Application.Submissions.Exceptions;
using Confab.Modules.Agendas.Domain.Submissions.Repositories;
using Confab.Shared.Abstractions.Commands;

namespace Confab.Modules.Agendas.Application.Submissions.Commands.Handlers
{
    public sealed class ApprovedSubmissionHandler : ICommandHandler<ApprovedSubmission>
    {
        private readonly ISubmissionRepository _submissionRepository;

        public ApprovedSubmissionHandler(ISubmissionRepository submissionRepository)
        {
            _submissionRepository = submissionRepository;
        }

        public async Task HandleAsync(ApprovedSubmission command)
        {
            var submission =
                await _submissionRepository.GetAsync(command.Id)
                ?? throw new SubmissionNotFoundException(command.Id);

            submission.Approved();
            await _submissionRepository.UpdateAsync(submission);
        }
    }
}
