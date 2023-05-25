using Confab.Modules.Conferences.Core.DTO;
using Confab.Modules.Conferences.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Confab.Modules.Conferences.Api.Controllers
{
    [Authorize(Policy = Policy)]
    internal class ConferenceController : BaseController
    {
        private const string Policy = "conferences";
        private readonly IConferenceService _conferenceService;

        public ConferenceController(IConferenceService hostService)
        {
            this._conferenceService = hostService;
        }

        [HttpGet("{id:guid}")]
        [AllowAnonymous]
        public async Task<ActionResult<ConferenceDetailsDto>> Get(Guid id) =>
            OkOrNotFound(await _conferenceService.GetAsync(id));

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IReadOnlyList<ConferenceDto>>> GetAll() =>
            OkOrNotFound(await _conferenceService.GetAllAsync());

        [HttpPost]
        public async Task<ActionResult> Add([FromBody] ConferenceDetailsDto dto)
        {
            await _conferenceService.AddAsync(dto);
            return CreatedAtAction(nameof(Get), new { id = dto.Id }, null);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult> Update(Guid id, ConferenceDetailsDto dto)
        {
            dto.Id = id;
            await _conferenceService.UpdateAsync(dto);

            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            await _conferenceService.DeleteAsync(id);

            return NoContent();
        }
    }
}
