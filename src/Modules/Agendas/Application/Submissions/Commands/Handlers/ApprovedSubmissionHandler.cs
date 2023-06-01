using Confab.Modules.Agendas.Application.Submissions.Exceptions;
using Confab.Modules.Agendas.Domain.Submissions.Repositories;
using Confab.Shared.Abstractions.Commands;
using Confab.Shared.Abstractions.Kernel;

namespace Confab.Modules.Agendas.Application.Submissions.Commands.Handlers
{
    public sealed class ApprovedSubmissionHandler : ICommandHandler<ApprovedSubmission>
    {
        private readonly ISubmissionRepository _submissionRepository;
        private readonly IDomainEventDispatcher _domainEventDispatcher;

        public ApprovedSubmissionHandler(
            ISubmissionRepository submissionRepository,
            IDomainEventDispatcher domainEventDispatcher
        )
        {
            _submissionRepository = submissionRepository;
            _domainEventDispatcher = domainEventDispatcher;
        }

        public async Task HandleAsync(ApprovedSubmission command)
        {
            var submission =
                await _submissionRepository.GetAsync(command.Id)
                ?? throw new SubmissionNotFoundException(command.Id);

            submission.Approved();
            await _submissionRepository.UpdateAsync(submission);
            await _domainEventDispatcher.DispatchAsync(submission.Events.ToArray());
        }
    }
}
