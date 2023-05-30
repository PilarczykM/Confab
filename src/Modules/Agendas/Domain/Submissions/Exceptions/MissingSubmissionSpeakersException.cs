using Confab.Shared.Abstractions.Exceptions;

namespace Confab.Modules.Agendas.Domain.Submissions.Exceptions
{
    public class MissingSubmissionSpeakersException : ConfabException
    {
        public MissingSubmissionSpeakersException(Guid id)
            : base($"Submission with ID: '{id}' has missing")
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}
