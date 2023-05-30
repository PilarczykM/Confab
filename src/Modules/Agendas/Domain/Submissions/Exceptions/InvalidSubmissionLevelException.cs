using Confab.Shared.Abstractions.Exceptions;

namespace Confab.Modules.Agendas.Domain.Submissions.Exceptions
{
    public class InvalidSubmissionLevelException : ConfabException
    {
        public InvalidSubmissionLevelException(Guid id)
            : base($"Submission with ID: '{id}' defines invalid status")
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}
