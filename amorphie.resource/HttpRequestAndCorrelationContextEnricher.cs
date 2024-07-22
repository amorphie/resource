using Serilog.Core;
using Serilog.Events;

namespace amorphie.resource;

public class HttpRequestAndCorrelationContextEnricher(IHttpContextAccessor httpContextAccessor) : ILogEventEnricher

{
    public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
    {
        var
            httpContext = httpContextAccessor.HttpContext;
        if (httpContext != null)
        {
            logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty(
                "RequestMethod"
                , httpContext.Request.Method));

            logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty(
                "RequestPath"
                , httpContext.Request.Path));

            logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty(
                "UserAgent"
                , httpContext.Request.Headers[
                    "User-Agent"
                ]));

            if (httpContext.Request.Headers.TryGetValue("X-Request-Id", out var appCorrelationId)
                || httpContext.Request.Headers.TryGetValue("xrequestid", out appCorrelationId))
            {
                logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty(
                    "X-Request-Id"
                    , appCorrelationId));
            }
        }
    }
}
