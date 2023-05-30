using Confab.Shared.Abstractions.Commands;

namespace Confab.Modules.Agendas.Application.Submissions.Commands
{
    public record RejectedSubmission(Guid Id) : ICommand;
}
