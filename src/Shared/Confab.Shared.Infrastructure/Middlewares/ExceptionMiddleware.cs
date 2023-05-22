using System.Net;
using Confab.Shared.Abstractions.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Confab.Shared.Infrastructure.Middlewares
{
    internal class ExceptionMiddleware : IMiddleware
    {
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IExceptionToResponseMapper _exceptionToResponseMapper;

        public ExceptionMiddleware(ILogger<ExceptionMiddleware> logger, IExceptionToResponseMapper exceptionToResponseMapper)
        {
            _logger = logger;
            _exceptionToResponseMapper = exceptionToResponseMapper;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception.Message);
                await HandleErrorAsync(context, exception);
            }
        }

        private async Task HandleErrorAsync(HttpContext context, Exception exception)
        {
            var errorResponse = _exceptionToResponseMapper.Map(exception);
            context.Response.StatusCode = (int)(errorResponse?.StatusCode ?? HttpStatusCode.InternalServerError);

            if (errorResponse?.Response is null)
            {
                return;
            }

            await context.Response.WriteAsJsonAsync(errorResponse);
        }
    }
}

