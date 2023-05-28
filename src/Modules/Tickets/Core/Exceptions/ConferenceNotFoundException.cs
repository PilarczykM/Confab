using Confab.Shared.Abstractions.Exceptions;

namespace Confab.Modules.Tickets.Core.Exceptions
{
    internal class ConferenceNotFoundException : ConfabException
    {
        public ConferenceNotFoundException(Guid id)
            : base($"Conference wih ID: '{id}' was not found.")
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}
