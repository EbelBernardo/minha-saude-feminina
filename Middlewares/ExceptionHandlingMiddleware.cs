using Microsoft.Identity.Client;
using MinhaSaudeFeminina.DTOs.Responses;
using MinhaSaudeFeminina.Exceptions;

namespace MinhaSaudeFeminina.Middlewares
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
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocorreu um erro inesperado.");
                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";

            var errorResponse = new ErrorResponse();


            if(ex is ValidationException vEx)
            {
                errorResponse.StatusCode = StatusCodes.Status400BadRequest;
                errorResponse.Message = vEx.Message;
                errorResponse.Errors = vEx.Errors;
            }
            else if (ex is BusinessException bEx)
            {
                errorResponse.StatusCode = StatusCodes.Status400BadRequest;
                errorResponse.Message = bEx.Message;
                errorResponse.Errors = bEx.Errors;
            }
            else if (ex is IdentityException iEx)
            {
                errorResponse.StatusCode = StatusCodes.Status400BadRequest;
                errorResponse.Message = iEx.Message;
                errorResponse.Errors = iEx.Errors;
            }
            else
            {
                errorResponse.StatusCode = StatusCodes.Status500InternalServerError;
                errorResponse.Message = "Ocorreu um erro inesperado no servidor.";
                errorResponse.Details = ex.Message;
            }

            context.Response.StatusCode = errorResponse.StatusCode;

            await context.Response.WriteAsJsonAsync(errorResponse);
        }
    }
}
