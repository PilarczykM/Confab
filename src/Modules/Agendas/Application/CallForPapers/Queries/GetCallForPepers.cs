using Confab.Modules.Agendas.Application.CallForPapers.DTO;
using Confab.Shared.Abstractions.Queries;

namespace Confab.Modules.Agendas.Application.CallForPapers.Queries
{
    public class GetCallForPepers : IQuery<CallForPapersDto>
    {
        public Guid ConferenceId { get; set; }
    }
}

