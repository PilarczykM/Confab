using Confab.Shared.Abstractions.Exceptions;

namespace Confab.Modules.Agendas.Application.CallForPapers.Exceptions
{
    public class CallForPapersClosedException : ConfabException
    {
        public CallForPapersClosedException(Guid conferenceId) : base($"Conference with ID: '{conferenceId}' has closed CFP.")
        {
            ConferenceId = conferenceId;
        }

        public Guid ConferenceId { get; }
    }
}

