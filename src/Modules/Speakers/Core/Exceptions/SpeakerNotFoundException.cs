using Confab.Shared.Abstractions.Exceptions;

namespace Confab.Modules.Speakers.Core.Exceptions
{
    public sealed class SpeakerNotFoundException : ConfabException
    {
        public SpeakerNotFoundException(Guid id) : base($"Speaker with id: '{id}' was not found.")
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}
