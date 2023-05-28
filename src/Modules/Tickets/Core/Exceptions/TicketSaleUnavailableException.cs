﻿using Confab.Shared.Abstractions.Exceptions;

namespace Confab.Modules.Tickets.Core.Exceptions
{
    public class TicketSaleUnavailableException : ConfabException
    {
        public TicketSaleUnavailableException(Guid conferenceId)
            : base($"Ticket sale for the conference is unavaiable.")
        {
            ConferenceId = conferenceId;
        }

        public Guid ConferenceId { get; }
    }
}
