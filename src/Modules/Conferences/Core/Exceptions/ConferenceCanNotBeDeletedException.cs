using Confab.Shared.Abstractions.Exceptions;

namespace Confab.Modules.Conferences.Core.Exceptions
{
    internal class ConferenceCanNotBeDeletedException : ConfabException
    {
        public ConferenceCanNotBeDeletedException(Guid id)
            : base($"Conference with ID: '{id}' cannot be deleted.") { }
    }
}
