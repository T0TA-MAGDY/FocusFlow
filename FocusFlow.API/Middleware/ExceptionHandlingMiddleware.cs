using FocusFlow.Application.Common.Exceptions;
using System.Text.Json;

namespace FocusFlow.API.Middleware;

public sealed class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next; private readonly ILogger<ExceptionHandlingMiddleware> _logger;
    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger) { _next = next; _logger = logger; }
    public async Task Invoke(HttpContext context)
    {
        try { await _next(context); }
        catch (AppException ex) { _logger.LogWarning(ex, "Application error occurred."); await WriteErrorAsync(context, ex.StatusCode, ex.Message); }
        catch (UnauthorizedAccessException ex) { _logger.LogWarning(ex, "Unauthorized access."); await WriteErrorAsync(context, StatusCodes.Status401Unauthorized, "Unauthorized."); }
        catch (Exception ex) { _logger.LogError(ex, "Unhandled exception occurred."); await WriteErrorAsync(context, StatusCodes.Status500InternalServerError, "An unexpected error occurred."); }
    }
    private static async Task WriteErrorAsync(HttpContext context, int statusCode, string message)
    {
        context.Response.StatusCode = statusCode; context.Response.ContentType = "application/json";
        await context.Response.WriteAsync(JsonSerializer.Serialize(new { error = message }));
    }
}
