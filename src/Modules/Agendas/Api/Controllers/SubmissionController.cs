﻿using Confab.Modules.Agendas.Application.Submissions.Commands;
using Confab.Shared.Abstractions.Commands;
using Microsoft.AspNetCore.Mvc;

namespace Confab.Modules.Agendas.Api.Controllers
{
    internal class SubmissionController : BaseController
    {
        private readonly ICommandDispatcher _commandDispatcher;

        public SubmissionController(ICommandDispatcher commandDispatcher)
        {
            _commandDispatcher = commandDispatcher;
        }

        [HttpPost]
        public async Task<ActionResult> CreateAsync(CreateSubmission command)
        {
            await _commandDispatcher.SendAsync(command);
            return CreatedAtAction("GET", new { id = command.Id }, null);
        }

        [HttpPut("{id:guid}/approve")]
        public async Task<ActionResult> ApproveAsync(Guid id)
        {
            await _commandDispatcher.SendAsync(new ApprovedSubmission(id));
            return NoContent();
        }

        [HttpPut("{id:guid}/reject")]
        public async Task<ActionResult> RejectAsync(Guid id)
        {
            await _commandDispatcher.SendAsync(new RejectedSubmission(id));
            return NoContent();
        }
    }
}
