using Confab.Shared.Abstractions.Exceptions;

namespace Confab.Modules.Users.Core.Exceptions
{
    public class UserNotActiveException : ConfabException
    {
        public Guid Id { get; }

        public UserNotActiveException(Guid id) : base($"User with ID: '{id}' is no active.")
        {
            Id = id;
        }
    }
}
