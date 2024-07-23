using System.Text;
using Microsoft.IO;

namespace amorphie.resource;

public class ResourceExceptionHandlerMiddleware : IMiddleware
{
    private readonly ILogger<ResourceExceptionHandlerMiddleware> _logger;

    public ResourceExceptionHandlerMiddleware(ILogger<ResourceExceptionHandlerMiddleware> logger)
    {
        _logger = logger;
    }
    
    private void SetTraceIdentifier(HttpContext context)
    {
        if (context.Request.Headers.TryGetValue("X-Request-Id", out var appCorrelationId)
            || context.Request.Headers.TryGetValue("xrequestid", out appCorrelationId))
        {
            if (appCorrelationId.Any())
            {
                context.TraceIdentifier = appCorrelationId;
            }
            else
            {
                context.TraceIdentifier = Guid.NewGuid().ToString();
            }
        }
    }
    
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        SetTraceIdentifier(context);
        await next(context);
    }
}
