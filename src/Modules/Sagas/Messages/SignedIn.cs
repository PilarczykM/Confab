using Confab.Shared.Abstractions.Events;

namespace Confab.Modules.Sagas.Messages;

internal record SignedIn(Guid UserId) : IEvent;

