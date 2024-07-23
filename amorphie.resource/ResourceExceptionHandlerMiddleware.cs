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

    private void RequestEnableBuffering(HttpContext context)
    {
        context.Request.EnableBuffering();
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

    private async Task<string> ReadStreamInChunks(HttpContext context)
    {
        using (StreamReader reader = new StreamReader(context.Request.Body, Encoding.UTF8))
        {
            return await reader.ReadToEndAsync();
        }
    }

    private string ReadHeaders(HttpContext context)
    {
        var headerText = new StringBuilder();
        var headers = context.Request.Headers;
        foreach (var header in headers)
        {
            if (header.Key == "Authorization")
            {
                context.Request.Headers.Remove(header);
            }

            if (header.Key == "Cookie")
            {
                context.Request.Headers.Remove(header);
            }

            if (header.Key == "x-access-token")
            {
                context.Request.Headers.Remove(header);
            }

            if (header.Key == "psu-fraud-check")
            {
                context.Request.Headers.Remove(header);
            }

            if (header.Key == "x-jws-signature" || header.Key == "x-userinfo")
            {
                context.Request.Headers.Remove(header);
            }

            headerText.AppendLine($"header.{header.Key}: {header.Value}");
        }

        return headerText.ToString();
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        RequestEnableBuffering(context);
        string body = "";
        if (context.Request.ContentLength is > 0)
        {
            using (var reader = new StreamReader(context.Request.Body, Encoding.UTF8, true, 1024, true))
            {
                body = await reader.ReadToEndAsync();
            }

            context.Request.Body.Position = 0;
        }

        var header = ReadHeaders(context);
        _logger.LogInformation($"{nameof(ResourceExceptionHandlerMiddleware)}. \n Body: {body} \n Headers: {header}");

        SetTraceIdentifier(context);

        await next(context);
    }
}
