using Confab.Shared.Abstractions.Exceptions;

namespace Confab.Modules.Users.Core.Exceptions
{
    public class EmailInUseException : ConfabException
    {
        public EmailInUseException() : base("Email is already in use.")
        {
        }
    }
}
