using Confab.Shared.Abstractions.Exceptions;

namespace Confab.Modules.Agendas.Domain.Submissions.Exceptions
{
    public class EmptySubmissionTitleException : ConfabException
    {
        public EmptySubmissionTitleException(Guid id)
            : base($"Submission with ID: '{id}' defines empty title")
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}
