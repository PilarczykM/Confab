using Confab.Modules.Attendances.Application.Clients.Agendas;
using Confab.Modules.Attendances.Application.Clients.Agendas.DTO;
using Confab.Modules.Attendances.Infrastructure.Clients.Requests;
using Confab.Shared.Abstractions.Module;

namespace Confab.Modules.Attendances.Infrastructure.Clients
{
    public class AgendasApiClients : IAgendasApiClient
    {
        private readonly IModuleClient _moduleClient;

        public AgendasApiClients(IModuleClient moduleClient)
        {
            _moduleClient = moduleClient;
        }

        public Task<IEnumerable<AgendaTrackDto>> GetAgendaAsync(Guid conferenceId)
        {
            throw new NotImplementedException();
        }

        public Task<RegularAgendaSlotDto> GetRegularAgendaSlotAsync(Guid id) =>
            _moduleClient.SendAsync<RegularAgendaSlotDto>(
                "/agendas/slots/reqular/get",
                new GetRegularAgendaSlot { AgendaItemId = id }
            );
    }
}
