using Confab.Modules.Tickets.Core.Entities;

namespace Confab.Modules.Tickets.Core.Services
{
    public interface ITicketGenereator
    {
        Ticket Generate(Guid conferenceId, Guid ticketSaleId, decimal? price);
    }
}

