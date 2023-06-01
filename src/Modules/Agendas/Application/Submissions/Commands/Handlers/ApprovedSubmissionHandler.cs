using Confab.Modules.Agendas.Application.Services;
using Confab.Modules.Agendas.Application.Submissions.Exceptions;
using Confab.Modules.Agendas.Domain.Submissions.Repositories;
using Confab.Shared.Abstractions.Commands;
using Confab.Shared.Abstractions.Kernel;
using Confab.Shared.Abstractions.Messaging;

namespace Confab.Modules.Agendas.Application.Submissions.Commands.Handlers
{
    public sealed class ApprovedSubmissionHandler : ICommandHandler<ApprovedSubmission>
    {
        private readonly ISubmissionRepository _submissionRepository;
        private readonly IDomainEventDispatcher _domainEventDispatcher;
        private readonly IEventMapper _eventMapper;
        private readonly IMessageBroker _messageBroker;

        public ApprovedSubmissionHandler(
            ISubmissionRepository submissionRepository,
            IDomainEventDispatcher domainEventDispatcher,
            IEventMapper eventMapper,
            IMessageBroker messageBroker
        )
        {
            _submissionRepository = submissionRepository;
            _domainEventDispatcher = domainEventDispatcher;
            _eventMapper = eventMapper;
            _messageBroker = messageBroker;
        }

        public async Task HandleAsync(ApprovedSubmission command)
        {
            var submission =
                await _submissionRepository.GetAsync(command.Id)
                ?? throw new SubmissionNotFoundException(command.Id);

            submission.Approved();

            await _submissionRepository.UpdateAsync(submission);
            await _domainEventDispatcher.DispatchAsync(submission.Events.ToArray());

            var integrationEvents = _eventMapper.MapAll(submission.Events);
            await _messageBroker.PublishAsync(integrationEvents.ToArray());
        }
    }
}
