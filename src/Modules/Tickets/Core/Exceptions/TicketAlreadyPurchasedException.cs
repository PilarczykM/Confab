﻿using Confab.Shared.Abstractions.Exceptions;

namespace Confab.Modules.Tickets.Core.Exceptions
{
    internal class TicketAlreadyPurchasedException : ConfabException
    {
        public TicketAlreadyPurchasedException(Guid conferenceId, Guid userId)
            : base($"Ticket for the conference has already been purchased.")
        {
            ConferenceId = conferenceId;
            UserId = userId;
        }

        public Guid ConferenceId { get; }
        public Guid UserId { get; }
    }
}
