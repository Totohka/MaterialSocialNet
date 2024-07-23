using FluentValidation;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Net;
using ValidationException = FluentValidation.ValidationException;

namespace Goods.System.Social.Network.Microservice.Users.Middleware
{
    public class ValidationExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ValidationExceptionHandlingMiddleware> _logger;

        public ValidationExceptionHandlingMiddleware(RequestDelegate next, ILogger<ValidationExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            switch (exception)
            {
                case ValidationException validationException:
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    await context.Response.WriteAsJsonAsync(validationException.Errors);
                    break;
                default:
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;
            }
        }           
    }
}
