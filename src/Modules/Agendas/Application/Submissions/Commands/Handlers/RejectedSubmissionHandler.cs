using Confab.Modules.Agendas.Application.Services;
using Confab.Modules.Agendas.Application.Submissions.Exceptions;
using Confab.Modules.Agendas.Domain.Submissions.Repositories;
using Confab.Shared.Abstractions.Commands;
using Confab.Shared.Abstractions.Kernel;
using Confab.Shared.Abstractions.Messaging;

namespace Confab.Modules.Agendas.Application.Submissions.Commands.Handlers
{
    public sealed class RejectedSubmissionHandler : ICommandHandler<RejectedSubmission>
    {
        private readonly ISubmissionRepository _submissionRepository;
        private readonly IDomainEventDispatcher _domainEventDispatcher;
        private readonly IEventMapper _eventMapper;
        private readonly IMessageBroker _messageBroker;

        public RejectedSubmissionHandler(
            ISubmissionRepository submissionRepository,
            IDomainEventDispatcher domainEventDispatcher
        )
        {
            _submissionRepository = submissionRepository;
            _domainEventDispatcher = domainEventDispatcher;
        }

        public async Task HandleAsync(RejectedSubmission command)
        {
            var submission =
                await _submissionRepository.GetAsync(command.Id)
                ?? throw new SubmissionNotFoundException(command.Id);

            submission.Rejected();
            await _submissionRepository.UpdateAsync(submission);
            await _domainEventDispatcher.DispatchAsync(submission.Events.ToArray());

            var integrationEvents = _eventMapper.MapAll(submission.Events);
            await _messageBroker.PublishAsync(integrationEvents.ToArray());
        }
    }
}
