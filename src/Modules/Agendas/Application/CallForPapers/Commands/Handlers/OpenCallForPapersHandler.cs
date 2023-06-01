using Confab.Modules.Agendas.Application.CallForPapers.Events;
using Confab.Modules.Agendas.Application.CallForPapers.Exceptions;
using Confab.Modules.Agendas.Domain.CallForPapers.Repositories;
using Confab.Shared.Abstractions.Commands;
using Confab.Shared.Abstractions.Messaging;

namespace Confab.Modules.Agendas.Application.CallForPapers.Commands.Handlers
{
    public class OpenCallForPapersHandler : ICommandHandler<OpenCallForPapers>
    {
        private readonly ICallForPapersRepository _callForPapersRepository;
        private readonly IMessageBroker _messageBroker;

        public OpenCallForPapersHandler(
            ICallForPapersRepository callForPapersRepository,
            IMessageBroker messageBroker
        )
        {
            _callForPapersRepository = callForPapersRepository;
            _messageBroker = messageBroker;
        }

        public async Task HandleAsync(OpenCallForPapers command)
        {
            var callForPaper =
                await _callForPapersRepository.GetAsync(command.ConferenceId)
                ?? throw new CallForPapersNotFoundException(command.ConferenceId);
            
            callForPaper.Open();
            await _callForPapersRepository.UpdateAsync(callForPaper);
            await _messageBroker.PublishAsync(
                new CallForPapersOpened(
                    callForPaper.ConferenceId,
                    callForPaper.From,
                    callForPaper.To
                )
            );
        }
    }
}
