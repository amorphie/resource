using System.Text;

namespace amorphie.resource;

public class ResourceExceptionHandlerMiddleware : IMiddleware
{
    private readonly ILogger<ResourceExceptionHandlerMiddleware> _logger;

    public ResourceExceptionHandlerMiddleware(ILogger<ResourceExceptionHandlerMiddleware> logger)
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
        var traceId = SetTraceIdentifier(context);
        try
        {
            var body = await LogBodyAsync(context);
            var header =  ReadHeaders(context);
            _logger.LogInformation($"{nameof(ResourceExceptionHandlerMiddleware)}. RequestId: {traceId} | Headers: {header} | Body: {body}");
        }
        catch (Exception e)
        {
           _logger.LogError(e, $"{nameof(ResourceExceptionHandlerMiddleware)}. RequestId: {traceId}");
        }
        
        await next(context);
    }

    private string ReadHeaders(HttpContext context)
    {
        var headerText = new StringBuilder();
        foreach (var header in context.Request.Headers)
        {
            headerText.AppendLine($"header.{header.Key}: {header.Value}");
        }

        return headerText.ToString();
    }

    private async Task<string> LogBodyAsync(HttpContext context)
    {
        context.Request.EnableBuffering();
        string body = "";
        if (context.Request.ContentLength is > 0)
        {
            using (var reader = new StreamReader(context.Request.Body, Encoding.UTF8, true, 1024, true))
            {
                body = await reader.ReadToEndAsync();
            }

            context.Request.Body.Position = 0;
        }

        return body;
    }
}
