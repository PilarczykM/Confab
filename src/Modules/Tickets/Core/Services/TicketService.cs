using Confab.Modules.Tickets.Core.DTO;
using Confab.Modules.Tickets.Core.Entities;
using Confab.Modules.Tickets.Core.Events;
using Confab.Modules.Tickets.Core.Exceptions;
using Confab.Modules.Tickets.Core.Repositories;
using Confab.Shared.Abstractions.Messaging;
using Confab.Shared.Abstractions.Time;
using Microsoft.Extensions.Logging;

namespace Confab.Modules.Tickets.Core.Services
{
    internal class TicketService : ITicketService
    {
        private readonly IConferenceRepository _conferenceRepository;
        private readonly ITicketSaleRepository _ticketSaleRepository;
        private readonly ITicketRepository _ticketRepository;
        private readonly ITicketGenereator _ticketGenereator;
        private readonly IClock _clock;
        private readonly ILogger<TicketService> _logger;
        private readonly IMessageBroker _messageBroker;

        public TicketService(
            IConferenceRepository conferenceRepository,
            ITicketSaleRepository ticketSaleRepository,
            ITicketRepository ticketRepository,
            ITicketGenereator ticketGenereator,
            IClock clock,
            ILogger<TicketService> logger
,
            IMessageBroker messageBroker)
        {
            _conferenceRepository = conferenceRepository;
            _ticketSaleRepository = ticketSaleRepository;
            _ticketRepository = ticketRepository;
            _ticketGenereator = ticketGenereator;
            _clock = clock;
            _logger = logger;
            _messageBroker = messageBroker;
        }

        public async Task<IEnumerable<TicketDto>> GetForUserAsync(Guid userId)
        {
            var tickets = await _ticketRepository.GetForUserAsync(userId);

            return tickets
                .Select(
                    x =>
                        new TicketDto(
                            x.Code,
                            x.Price,
                            x.PurchasedAt.Value,
                            new ConferenceDto(x.ConferenceId, x.Conference.Name)
                        )
                )
                .OrderBy(x => x.PurchasedAt);
        }

        public async Task PurchaseAsync(Guid conferenceId, Guid userId)
        {
            var conference = await _conferenceRepository.GetAsync(conferenceId);
            if (conference is null)
            {
                throw new ConferenceNotFoundException(conferenceId);
            }

            var ticket = await _ticketRepository.GetAsync(conferenceId, userId);
            if (ticket is not null)
            {
                throw new TicketAlreadyPurchasedException(conferenceId, userId);
            }

            var now = _clock.CurrentDate();
            var ticketSale = await _ticketSaleRepository.GetCurrentForConferenceAsync(
                conferenceId,
                now
            );
            if (ticketSale is null)
            {
                throw new TicketSaleUnavailableException(conferenceId);
            }

            if (ticketSale.Amount.HasValue)
            {
                await PurchaseAvailableAsync(ticketSale, userId, ticketSale.Price);
                return;
            }

            ticket = _ticketGenereator.Generate(conferenceId, ticketSale.Id, ticketSale.Price);
            ticket.Purchase(userId, _clock.CurrentDate(), ticketSale.Price);
            await _ticketRepository.AddAsync(ticket);
            _logger.LogInformation(
                $"Ticket with ID: '{ticket.Id}' was generated for the conference: "
                    + $"'{conferenceId}' by user: '{userId}'."
            );
            await _messageBroker.PublishAsync(new TicketPurchased(ticket.Id, conferenceId, userId));
        }

        private async Task PurchaseAvailableAsync(
            TicketSale ticketSale,
            Guid userId,
            decimal? price
        )
        {
            var conferenceId = ticketSale.ConferenceId;
            var ticket = ticketSale.Tickets
                .Where(x => x.UserId is null)
                .OrderBy(_ => Guid.NewGuid())
                .FirstOrDefault();
            if (ticket is null)
            {
                throw new TicketsUnavailableException(conferenceId);
            }

            ticket.Purchase(userId, _clock.CurrentDate(), price);
            await _ticketRepository.UpdateAsync(ticket);
            _logger.LogInformation(
                $"Ticket with ID: '{ticket.Id}' was purchased for the conference: "
                    + $"'{conferenceId}' by user: '{userId}'."
            );
            await _messageBroker.PublishAsync(new TicketPurchased(ticket.Id, conferenceId, userId));
        }
    }
}
