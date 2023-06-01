using Confab.Modules.Agendas.Application.CallForPapers.Events;
using Confab.Modules.Agendas.Application.CallForPapers.Exceptions;
using Confab.Modules.Agendas.Domain.CallForPapers.Repositories;
using Confab.Shared.Abstractions.Commands;
using Confab.Shared.Abstractions.Messaging;

namespace Confab.Modules.Agendas.Application.CallForPapers.Commands.Handlers
{
    public class CreateCallForPapersHandler : ICommandHandler<CreateCallForPapers>
    {
        private readonly ICallForPapersRepository _callForPapersRepository;
        private readonly IMessageBroker _messageBroker;

        public CreateCallForPapersHandler(
            ICallForPapersRepository callForPapersRepository,
            IMessageBroker messageBroker
        )
        {
            _callForPapersRepository = callForPapersRepository;
            _messageBroker = messageBroker;
        }

        public async Task HandleAsync(CreateCallForPapers command)
        {
            if (await _callForPapersRepository.ExistsAsync(command.ConferenceId))
            {
                throw new CallForPapersAlreadyExistsException(command.ConferenceId);
            }

            var callForPapers = Domain.CallForPapers.Entities.CallForPapers.Create(
                command.Id,
                command.ConferenceId,
                command.From,
                command.To
            );

            await _callForPapersRepository.AddAsync(callForPapers);
            await _messageBroker.PublishAsync(new CallForPapersCreated(callForPapers.ConferenceId));
        }
    }
}
