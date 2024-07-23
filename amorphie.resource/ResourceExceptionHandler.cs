
using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Diagnostics;

namespace amorphie.resource;

public class ResourceExceptionHandler : IExceptionHandler
{
    internal const string ErrorFormat = "_AmorphieErrorFormat";
    
    private readonly ILogger<ResourceExceptionHandler> _logger;
    public ResourceExceptionHandler(ILogger<ResourceExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        await HandleAndWrapException(httpContext, exception, cancellationToken);

        return true;
    }
    
    private async Task HandleAndWrapException(HttpContext httpContext, Exception exception,
        CancellationToken cancellationToken)
    {
        _logger.LogError(exception, exception.Message);
        httpContext.Response.Clear();
        httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        httpContext.Response.Headers.Append(ErrorFormat, "true");
        httpContext.Response.Headers.Append("Content-Type", "application/json");

        await httpContext.Response.WriteAsync(
            JsonSerializer.Serialize(
                new ProblemDetails()
                {
                    Title = "Application failed",
                    Detail = exception.Message
                }, new JsonSerializerOptions() {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    WriteIndented = true
                }
            ), cancellationToken: cancellationToken);
    }
}
