using GbAviationTicketApi.ExceptionMiddleware;
using GbAviationTicketApi.Models;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace GbAviationTicketApi.Extentions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if(contextFeature != null)
                    {
                        await context.Response.WriteAsync(new ApiResponse
                        {
                            StatusCode = HttpStatusCode.InternalServerError,
                            IsSuccess = false,
                            ErrorMesseges = new() { "Internal Server Error" },
                            Result = null

                        }.ToString());
                    }
                });
            });
        }
        
        public static void ConfigureExceptionMiddleware(this WebApplication app)
        {
            app.UseMiddleware<ExceptionMiddleware.ExceptionMiddleware>();
        }
    }
}
