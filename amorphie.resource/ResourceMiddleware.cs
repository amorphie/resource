namespace amorphie.resource;

public class ResourceMiddleware : IMiddleware
{
    private readonly ILogger<ResourceMiddleware> _logger;

    public ResourceMiddleware(ILogger<ResourceMiddleware> logger)
    {
        _logger = logger;
    }

    private string? SetTraceIdentifier(HttpContext context)
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

        return context.TraceIdentifier;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        SetTraceIdentifier(context);
        await next(context);
    }
}
