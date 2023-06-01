﻿using Confab.Modules.Agendas.Application.CallForPapers.Commands;
using Confab.Modules.Agendas.Application.CallForPapers.DTO;
using Confab.Modules.Agendas.Application.CallForPapers.Queries;
using Confab.Shared.Abstractions.Commands;
using Confab.Shared.Abstractions.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Confab.Modules.Agendas.Api.Controllers
{
    [Route(AgendasModule.BasePath + "/conferences/{conferenceId:guid}/cfp")]
    internal class CallForPapersController : BaseController
    {
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IQueryDispatcher _queryDispatcher;

        public CallForPapersController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
        {
            _commandDispatcher = commandDispatcher;
            _queryDispatcher = queryDispatcher;
        }

        [HttpGet]
        public async Task<ActionResult<CallForPapersDto>> GetAsync(Guid conferenceId)
            => OkOrNotFound(await _queryDispatcher.QueryAsync(new GetCallForPepers { ConferenceId = conferenceId }));

        [HttpPost]
        public async Task<ActionResult> CreateAsync(Guid conferenceId, CreateCallForPapers command)
        {
            command = command with { ConferenceId = conferenceId };
            await _commandDispatcher.SendAsync(command);
            return CreatedAtAction(nameof(GetAsync), new { conferenceId = command.ConferenceId }, null);
        }

        [HttpPut("open")]
        public async Task<ActionResult> OpenAsync(Guid conferenceId)
        {
            await _commandDispatcher.SendAsync(new OpenCallForPapers(conferenceId));
            return NoContent();
        }


        [HttpPut("close")]
        public async Task<ActionResult> CaloseAsync(Guid conferenceId)
        {
            await _commandDispatcher.SendAsync(new CloseCallForPapers(conferenceId));
            return NoContent();
        }
    }
}

