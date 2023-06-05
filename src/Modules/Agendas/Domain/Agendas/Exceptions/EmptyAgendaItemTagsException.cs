using Confab.Shared.Abstractions.Exceptions;

namespace Confab.Modules.Agendas.Domain.Agendas.Exceptions
{
    public class EmptyAgendaItemTagsException : ConfabException
    {
        public EmptyAgendaItemTagsException(Guid agendaItemId) : base($"Agenda item with ID: '{agendaItemId}' defines empty tags.")
        {
            AgendaItemId = agendaItemId;
        }

        public Guid AgendaItemId { get; }
    }
}

