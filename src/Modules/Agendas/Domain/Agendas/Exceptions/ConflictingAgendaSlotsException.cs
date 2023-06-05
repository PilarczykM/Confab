using Confab.Shared.Abstractions.Exceptions;

namespace Confab.Modules.Agendas.Domain.Agendas.Exceptions
{
    public class ConflictingAgendaSlotsException : ConfabException
    {
        public ConflictingAgendaSlotsException(DateTime from, DateTime to)
            : base($"There is slot clonflicting with date range: {from} | {to}")
        {
            From = from;
            To = to;
        }

        public DateTime From { get; }
        public DateTime To { get; }
    }
}
