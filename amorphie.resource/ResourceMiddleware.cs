namespace amorphie.resource;

public class ResourceMiddleware : IMiddleware
{
    private string? SetTraceIdentifier(HttpContext context)
    {
        if (context.Request.Headers.TryGetValue("X-Request-Id", out var appCorrelationId)
            || context.Request.Headers.TryGetValue("x-request-id", out appCorrelationId))
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
