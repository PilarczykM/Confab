using Confab.Shared.Abstractions.Exceptions;

namespace Confab.Modules.Agendas.Domain.Agendas.Exceptions
{
    public class AgendaSlotNotFoundException : ConfabException
    {
        public AgendaSlotNotFoundException(Guid id)
            : base($"Agenda slot with ID: '{id}' was not found.")
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}
