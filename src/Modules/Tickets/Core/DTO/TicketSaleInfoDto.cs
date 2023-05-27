namespace Confab.Modules.Tickets.Core.DTO
{
    public record TicketSaleInfoDto(string Name, ConferenceDto Conference, decimal? TicketPrice, int? TotalTickets,
        int? AvaiableTickets, DateTime SaleFrom, DateTime SaleTo)
    {
        public bool IsFree => !TicketPrice.HasValue;
        public bool UnlimitedTickets = !AvaiableTickets.HasValue;
        public bool IsAvaiable => SaleFrom <= DateTime.UtcNow && SaleTo >= DateTime.UtcNow &&
            (UnlimitedTickets || AvaiableTickets > 0);
    }
}

