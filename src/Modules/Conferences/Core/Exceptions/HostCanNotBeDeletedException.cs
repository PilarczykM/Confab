using Confab.Shared.Abstractions.Exceptions;

namespace Confab.Modules.Conferences.Core.Exceptions
{
    internal class HostCanNotBeDeletedException : ConfabException
    {
        public HostCanNotBeDeletedException(Guid id)
            : base($"Host with ID: '{id}' cannot be deleted.") { }
    }
}
