using Confab.Shared.Abstractions.Events;

namespace Confab.Modules.Sagas.Messages;

public record SpeakerCreated(Guid Id, string FullName) : IEvent;
