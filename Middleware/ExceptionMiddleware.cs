using System;
using System.Net;
using System.Text.Json;
using DatingApp.Errors;

namespace DatingApp.Middleware;

public class ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment environment)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception e)
        {
            logger.LogError(e, e.Message);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            var response = environment.IsDevelopment() ? new ApiExceptions(context.Response.StatusCode, e.Message, e.StackTrace) :
            new ApiExceptions(context.Response.StatusCode, e.Message, "Internal Server Error");
            var option = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            var jsonString = JsonSerializer.Serialize(response, option);
            await context.Response.WriteAsync(jsonString);
        }
    }
}
