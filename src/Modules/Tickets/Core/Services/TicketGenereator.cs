﻿using Confab.Modules.Tickets.Core.Entities;
using Confab.Shared.Abstractions.Time;

namespace Confab.Modules.Tickets.Core.Services
{
    internal class TicketGenereator : ITicketGenereator
    {
        private readonly IClock _clock;

        public TicketGenereator(IClock clock)
        {
            _clock = clock;
        }

        public Ticket Generate(Guid conferenceId, Guid ticketSaleId, decimal? price)
            => new()
            {
                Id = Guid.NewGuid(),
                TicketSaleId = ticketSaleId,
                ConferenceId = conferenceId,
                Code = Guid.NewGuid().ToString("N"),
                Price = price,
                CreatedAt = _clock.CurrentDate()
            };
    }
}

