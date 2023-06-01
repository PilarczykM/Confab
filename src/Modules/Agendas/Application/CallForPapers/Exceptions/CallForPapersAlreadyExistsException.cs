using Confab.Shared.Abstractions.Exceptions;

namespace Confab.Modules.Agendas.Application.CallForPapers.Exceptions
{
    public class CallForPapersAlreadyExistsException : ConfabException
    {
        public CallForPapersAlreadyExistsException(Guid conferenceId)
            : base($"Conference with ID: '{conferenceId}' already defined CFP.")
        {
            ConferenceId = conferenceId;
        }

        public Guid ConferenceId { get; }
    }
}
