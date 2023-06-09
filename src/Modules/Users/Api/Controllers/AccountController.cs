﻿using Confab.Modules.Users.Core.DTO;
using Confab.Modules.Users.Core.Services;
using Confab.Shared.Abstractions.Auth;
using Confab.Shared.Abstractions.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Confab.Modules.Users.Api.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IIdentityService _identityService;
        private readonly IContext _context;

        public AccountController(IIdentityService identityService, IContext context)
        {
            _identityService = identityService;
            _context = context;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<AccountDto>> Get()
            => OkOrNotFound(await _identityService.GetAsync(_context.Identity.Id));

        [HttpPost("sign-up")]
        public async Task<ActionResult> SignUp(SignUpDto dto)
        {
            await _identityService.SignUpAsync(dto);
            return NoContent();
        }

        [HttpPost("sign-in")]
        public async Task<ActionResult<JsonWebToken>> SignIn(SignInDto dto)
            => Ok(await _identityService.SignInAsync(dto));
    }
}

