using Confab.Shared.Abstractions.Exceptions;

namespace Confab.Modules.Agendas.Domain.Submissions.Exceptions
{
    public class InvalidSubmissionStatusException : ConfabException
    {
        public InvalidSubmissionStatusException(Guid id, string desiredStatus, string currentStatus)
            : base(
                $"Cannot change status of submission with ID: '{id}' from {currentStatus} to {desiredStatus}"
            )
        {
            Id = id;
            DesiredStatus = desiredStatus;
            CurrentStatus = currentStatus;
        }

        public Guid Id { get; }
        public string DesiredStatus { get; }
        public string CurrentStatus { get; }
    }
}
