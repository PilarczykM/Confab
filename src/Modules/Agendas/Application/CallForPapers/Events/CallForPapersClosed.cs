using Confab.Shared.Abstractions.Events;
using Confab.Shared.Abstractions.Kernel.Types;

namespace Confab.Modules.Agendas.Application.CallForPapers.Events
{
    internal record CallForPapersClosed(ConferenceId ConferenceId) : IEvent;
}
