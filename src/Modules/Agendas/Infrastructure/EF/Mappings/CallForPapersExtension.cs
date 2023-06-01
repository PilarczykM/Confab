using Confab.Modules.Agendas.Application.CallForPapers.DTO;
using Confab.Modules.Agendas.Domain.CallForPapers.Entities;

namespace Confab.Modules.Agendas.Infrastructure.EF.Mappings
{
    internal static class CallForPapersExtension
    {
        public static CallForPapersDto AsDto(this CallForPapers callForPapers)
            => new()
            {
                Id = callForPapers.Id,
                ConferenceId = callForPapers.ConferenceId,
                From = callForPapers.From,
                To = callForPapers.To,
                IsOpened = callForPapers.IsOpened
            };
    }
}

