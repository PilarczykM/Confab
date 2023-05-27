using Confab.Shared.Abstractions.Exceptions;

namespace Confab.Modules.Tickets.Core.Exceptions
{
    internal class ConferenceNotFoundException : ConfabException
    {
        public ConferenceNotFoundException(Guid conferenceId) : base($"")
        {
            ConferenceId = conferenceId;
        }

        public Guid ConferenceId { get; }
    }
}

