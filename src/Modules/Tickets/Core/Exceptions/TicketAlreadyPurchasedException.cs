using Confab.Shared.Abstractions.Exceptions;

namespace Confab.Modules.Tickets.Core.Exceptions
{
    internal class TicketAlreadyPurchasedException : ConfabException
    {
        private Guid conferenceId;
        private readonly Guid userId;
        private Guid value;

        public TicketAlreadyPurchasedException(Guid conferenceId, Guid userId) : base($"")
        {
            this.conferenceId = conferenceId;
            this.userId = userId;
        }

    }
}

