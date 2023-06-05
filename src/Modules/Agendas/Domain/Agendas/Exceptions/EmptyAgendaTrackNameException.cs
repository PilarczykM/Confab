using Confab.Shared.Abstractions.Exceptions;

namespace Confab.Modules.Agendas.Domain.Agendas.Exceptions
{
    public class EmptyAgendaTrackNameException : ConfabException
    {
        public EmptyAgendaTrackNameException(Guid id)
            : base($"Agenda track with ID '{id}' defines empty name.")
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}
