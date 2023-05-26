using Confab.Shared.Infrastructure.Api;
using Microsoft.AspNetCore.Mvc;

namespace Confab.Modules.Users.Api.Controllers
{
    [ApiController]
    [Route(UsersModule.BasePath + "/[controller]")]
    [ProducesDefaultContentType]
    public class BaseController : ControllerBase
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

