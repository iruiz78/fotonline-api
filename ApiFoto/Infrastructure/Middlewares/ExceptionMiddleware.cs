using ApiFoto.Infrastructure.Communication.Exceptions;
using ApiFoto.Infrastructure.Communication;
using System.Text.Json;

namespace ApiFoto.Infrastructure.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                context.Response.ContentType = "text/plain";
                if (ex is AppException exception)
                {
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    await context.Response.WriteAsync(JsonSerializer.Serialize(new GenericResponse<object>(exception.StatusCode, ex?.Message, exception.Args != null ? exception.Args : "")));
                }
                else
                {
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    await context.Response.WriteAsync("Ocurrió un error interno.");
                }
            }
        }
    }
}
