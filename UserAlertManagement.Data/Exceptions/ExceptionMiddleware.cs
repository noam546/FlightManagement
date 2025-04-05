using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace UserAlertManagement.Data.Exceptions;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
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
        catch (BaseException ex)
        {
            _logger.LogError($"Custom error: {ex.Message}");
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = ex.StatusCode;
            await httpContext.Response.WriteAsync(new
            {
                StatusCode = ex.StatusCode,
                Message = ex.Message
            }.ToString()); // Consider serializing this properly
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unhandled error: {ex.Message}");
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            await httpContext.Response.WriteAsync(new
            {
                StatusCode = 500,
                Message = "Internal Server Error"
            }.ToString()); // Consider serializing this properly
        }
    }
}