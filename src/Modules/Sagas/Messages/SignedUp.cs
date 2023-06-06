using Confab.Shared.Abstractions.Events;

namespace Confab.Modules.Sagas.Messages;

internal record SignedUp(Guid UserId, string Email) : IEvent;
