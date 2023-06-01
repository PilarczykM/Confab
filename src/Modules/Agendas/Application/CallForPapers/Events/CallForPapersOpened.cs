using Confab.Shared.Abstractions.Events;
using Confab.Shared.Abstractions.Kernel.Types;

namespace Confab.Modules.Agendas.Application.CallForPapers.Events
{
    internal record CallForPapersOpened(ConferenceId ConferenceId, DateTime From, DateTime To)
        : IEvent;
}
