using System.Text;

namespace amorphie.resource;

public class ResourceMiddleware : IMiddleware
{
    private readonly ILogger<ResourceMiddleware> _logger;
    private readonly HashSet<string> _bypassHeaders;
    public ResourceMiddleware(ILogger<ResourceMiddleware> logger)
    {
        _logger = logger;
        _bypassHeaders = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
           "Authorization"
        };
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

    private async Task<string> RequestAsTextAsync(HttpContext httpContext)
    {
        string rawRequestBody = await GetRawBodyAsync(httpContext.Request);

        IEnumerable<string> headerLine = httpContext.Request.Headers.Where(h => !_bypassHeaders.Contains(h.Key))
            .Select(pair => $"{pair.Key} => {string.Join("|", pair.Value.ToList())}");
        string headerText = string.Join(Environment.NewLine, headerLine);

        string message =
            $"Request: {httpContext.Request.Scheme}://{httpContext.Request.Host}{httpContext.Request.Path}{httpContext.Request.QueryString}{Environment.NewLine}" +
            $"Headers: {Environment.NewLine}{headerText}{Environment.NewLine}" +
            $"Content : {Environment.NewLine}{rawRequestBody}";

        return message;
    }

    private async Task<string> GetRawBodyAsync(HttpRequest request, Encoding? encoding = null)
    {
        request.EnableBuffering();
        using var reader = new StreamReader(request.Body, encoding ?? Encoding.UTF8, leaveOpen: true);
        string body = await reader.ReadToEndAsync();
        request.Body.Position = 0;

        return body;
    }

    private async Task LogRequestAsync(HttpContext context)
    {
        var request = context.Request;
        var requestLog = new StringBuilder();
        requestLog.AppendLine("Incoming Request:");
        requestLog.AppendLine($"HTTP {request.Method} {request.Path}");
        requestLog.AppendLine($"Host: {request.Host}");
        requestLog.AppendLine(await RequestAsTextAsync(context));
        requestLog.AppendLine($"Content-Type: {request.ContentType}");
        requestLog.AppendLine($"Content-Length: {request.ContentLength}");
        _logger.LogInformation(requestLog.ToString());
    }

    private Task LogResponseAsync(HttpContext context)
    {
        var response = context.Response;
        var responseLog = new StringBuilder();
        responseLog.AppendLine("Outgoing Response:");
        responseLog.AppendLine($"HTTP {response.StatusCode}");
        responseLog.AppendLine($"Content-Type: {response.ContentType}");
        responseLog.AppendLine($"Content-Length: {response.ContentLength}");
        _logger.LogInformation(responseLog.ToString());
        return Task.CompletedTask;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        SetTraceIdentifier(context);
        await LogRequestAsync(context);
        await next(context);
        await LogResponseAsync(context);
    }
}
