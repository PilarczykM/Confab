using Confab.Shared.Abstractions.Events;

namespace Confab.Modules.Speakers.Core.Events
{
    public record SpeakerCreated(Guid id, string FullName) : IEvent;
}

