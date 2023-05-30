using Confab.Shared.Abstractions.Exceptions;

namespace Confab.Modules.Agendas.Application.Submissions.Exceptions
{
    public class SubmissionNotFoundException : ConfabException
    {
        public SubmissionNotFoundException(Guid id)
            : base($"Submission with ID: '{id}' was not found.")
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}
