using System.Runtime.CompilerServices;

namespace amorphie.resource;

public class ResourceExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger _logger;

    public ResourceExceptionHandlerMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
    {
        this._next = next;
        this._logger = (ILogger)loggerFactory.CreateLogger<ResourceExceptionHandlerMiddleware>();
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            context.Request.EnableBuffering();
            
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

            await this._next(context);
        }
        catch (Exception ex)
        {
            await this.HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
    {
        string text = "An error occured and logged with \"" + httpContext.TraceIdentifier + "\" trace identifier id";
        httpContext.Response.ContentType = "application/json";
        httpContext.Response.StatusCode =
            !(ex is BadHttpRequestException requestException) ? 500 : requestException.StatusCode;
        ILogger logger = this._logger;
        // DefaultInterpolatedStringHandler interpolatedStringHandler = new DefaultInterpolatedStringHandler(31, 2);
        // interpolatedStringHandler.AppendLiteral("TraceIdentifier : ");
        // interpolatedStringHandler.AppendFormatted(httpContext.TraceIdentifier);
        // interpolatedStringHandler.AppendLiteral(". Exception: ");
        // interpolatedStringHandler.AppendFormatted<Exception>(ex);
        // string stringAndClear = interpolatedStringHandler.ToStringAndClear();
        object[] objArray = Array.Empty<object>();
        // logger.LogError(stringAndClear, objArray);
        await httpContext.Response.WriteAsync(text);
    }
}
