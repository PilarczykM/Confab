using Confab.Shared.Abstractions.Exceptions;

namespace Confab.Modules.Agendas.Application.CallForPapers.Exceptions
{
    public class CallForPapersNotFoundException : ConfabException
    {
        public CallForPapersNotFoundException(Guid conferenceId) : base($"Conference with ID: '{conferenceId}' has no CFP.")
        {
            ConferenceId = conferenceId;
        }

        public Guid ConferenceId { get; }
    }
}
