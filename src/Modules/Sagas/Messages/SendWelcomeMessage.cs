using Confab.Shared.Abstractions.Commands;

namespace Confab.Modules.Sagas.Messages
{
    internal record SendWelcomeMessage(string Email, string FullName) : ICommand;
}
