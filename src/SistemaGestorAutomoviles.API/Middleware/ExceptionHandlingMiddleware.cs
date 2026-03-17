using System.Text.Json;
using SistemaGestorAutomoviles.Application.Exceptions;

namespace SistemaGestorAutomoviles.API.Middleware;

public class ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (BusinessValidationException ex)
        {
            logger.LogWarning(ex, "Business validation error");
            await WriteErrorAsync(context, StatusCodes.Status400BadRequest, ex.Message);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unhandled exception");
            await WriteErrorAsync(context, StatusCodes.Status500InternalServerError, "Internal server error.");
        }
    }

    private static async Task WriteErrorAsync(HttpContext context, int statusCode, string message)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = statusCode;

        var payload = JsonSerializer.Serialize(new
        {
            statusCode,
            message
        });

        await context.Response.WriteAsync(payload);
    }
}
