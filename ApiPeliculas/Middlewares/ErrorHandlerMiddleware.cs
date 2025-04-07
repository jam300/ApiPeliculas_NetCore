using ApiPeliculas.Exceptions;
using System.Net;
using System.Text.Json;

namespace ApiPeliculas.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlerMiddleware> _logger;

        public ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger)
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
                _logger.LogError(ex, $"there is a error {context.Request.Path}");

                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            int statusCode = (int)HttpStatusCode.InternalServerError;
            int errorCode = 0;
            string message = exception.Message;

            if(exception is BaseException customException)
            {
                statusCode = customException.StatusCode;
                errorCode = customException.ErrorCode;
                message = customException.Message;
            }

            var response = new
            {
                StatusCode = statusCode,
                Message = message,
                ErrorCode = errorCode,
                Path = context.Request.Path
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;

            var jsonResponse = JsonSerializer.Serialize(response);
            return context.Response.WriteAsync(jsonResponse);

        }


    }
}
