using Confab.Modules.Conferences.Core.DTO;
using Confab.Modules.Conferences.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace Confab.Modules.Conferences.Api.Controllers
{
    internal class ConferenceController : BaseController
    {
        private readonly IConferenceService _conferenceService;

        public ConferenceController(IConferenceService hostService)
        {
            this._conferenceService = hostService;
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ConferenceDetailsDto>> GetAsync(Guid id) =>
            OkOrNotFound(await _conferenceService.GetAsync(id));

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ConferenceDto>>> GetAllAsync() =>
            OkOrNotFound(await _conferenceService.GetAllAsync());

        [HttpPost]
        public async Task<ActionResult> AddAsync([FromBody] ConferenceDetailsDto dto)
        {
            await _conferenceService.AddAsync(dto);
            return CreatedAtAction(nameof(GetAsync), new { id = dto.Id }, null);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult> UpdateAsync(Guid id, ConferenceDetailsDto dto)
        {
            dto.Id = id;
            await _conferenceService.UpdateAsync(dto);

            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> DeleteAsync(Guid id)
        {
            await _conferenceService.DeleteAsync(id);

            return NoContent();
        }
    }
}
