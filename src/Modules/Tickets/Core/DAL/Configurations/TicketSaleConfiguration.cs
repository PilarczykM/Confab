using Confab.Modules.Tickets.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Confab.Modules.Tickets.Core.DAL.Configurations
{
    public class TicketSaleConfiguration : IEntityTypeConfiguration<TicketSale>
    {
        public void Configure(EntityTypeBuilder<TicketSale> builder) { }
    }
}
