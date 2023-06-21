using EntityFramework.Exceptions.Common;
using GbAviationTicketApi.Models;
using GbAviationTicketApi.Services.LoggerService;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace GbAviationTicketApi.ExceptionMiddleware
{
    public class ExceptionMiddleware
    {
        //TODO: implement logger
        private readonly RequestDelegate _next;
        private readonly ILoggerManager _logger;

        public ExceptionMiddleware(RequestDelegate next, ILoggerManager logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something wrong happened: {ex}");
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";
            var message = "Internal Server Error";

            if(ex.GetType() == typeof(DbUpdateException)
                && ex.InnerException != null
                && ex.InnerException.Message.Contains("Cannot insert duplicate key row"))
            {
                message = "Duplicated Row";
            }

            await context.Response.WriteAsync(new ApiResponse
            {
                StatusCode = message == "Internal Server Error" ? 
                    HttpStatusCode.InternalServerError : HttpStatusCode.BadRequest,
                IsSuccess = false,
                ErrorMesseges = new() { message }
            }.ToString());
            
        }
    }
}
