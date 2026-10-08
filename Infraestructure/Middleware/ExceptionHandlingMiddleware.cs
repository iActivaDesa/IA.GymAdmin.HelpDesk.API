
using IA.GymAdmin.HelpDesk.API.Domain.Exceptions;
using IA.GymAdmin.HelpDesk.API.Utils;
using System.Text.Json;

namespace IA.GymAdmin.HelpDesk.API.Infraestructure.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // No interceptar Swagger/OpenAPI
            if (context.Request.Path.StartsWithSegments("/swagger"))
            {
                await _next(context);
                return;
            }

            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error no controlado en {Path}", context.Request.Path);
                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            var (statusCode, message) = exception switch
            {
                DomainException domainException => (domainException.StatusCode, domainException.Message),
                _ => (StatusCodes.Status500InternalServerError, "Ocurrio un error interno en el servidor.")
            };

            context.Response.StatusCode = statusCode;

            var response = ResponseModel.Custom(statusCode, message, null);
            var json = JsonSerializer.Serialize(response);

            await context.Response.WriteAsync(json);
        }
    }
}
