﻿using Confab.Shared.Infrastructure.Api;
using Microsoft.AspNetCore.Mvc;

namespace Confab.Modules.Speakers.Api.Controllers
{
    [ApiController]
    [Route(SpeakersModule.BasePath + "/[controller]")]
    [ProducesDefaultContentType]
    internal abstract class BaseController : ControllerBase
    {
        protected ActionResult<T> OkOrNotFound<T>(T model)
        {
            if (model is not null)
            {
                return Ok(model);
            }

            return NotFound();
        }
    }
}
