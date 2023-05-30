using Confab.Shared.Abstractions.Exceptions;

namespace Confab.Modules.Agendas.Domain.Submissions.Exceptions
{
    public class EmptySubmissionDescriptionException : ConfabException
    {
        public EmptySubmissionDescriptionException(Guid id)
            : base($"Submission with ID: '{id}' defines empty description")
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}
