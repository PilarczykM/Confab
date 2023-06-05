using Confab.Shared.Abstractions.Exceptions;

namespace Confab.Modules.Agendas.Domain.CallForPapers.Exceptions
{
    public class InvalidCallForPapersDatesException : ConfabException
    {
        public InvalidCallForPapersDatesException(DateTime from, DateTime to) : base($"FCP has invalid dates, from: '{from:d}' > to: '{to:d}'.")
        {
            From = from;
            To = to;
        }

        public DateTime From { get; }
        public DateTime To { get; }
    }
}

