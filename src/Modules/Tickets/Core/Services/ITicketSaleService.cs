using Confab.Modules.Tickets.Core.DTO;

namespace Confab.Modules.Tickets.Core.Services
{
    public interface ITicketSaleService
    {
        Task AddAsync(TicketSaleDto dto);
        Task<IEnumerable<TicketSaleDto>> GetAllAsync(Guid conferenceId);
        Task<TicketSaleDto> GetAsync(Guid conferenceId);
    }
}
