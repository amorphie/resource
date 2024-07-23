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

            if (httpContext.Request.Headers.TryGetValue("X-Request-Id", out var requestId)
                || httpContext.Request.Headers.TryGetValue("xrequestid", out requestId))
            {
                logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty(
                    "X-Request-Id"
                    , requestId));
            }
            
            if (httpContext.Request.Headers.TryGetValue("X-Device-Id", out var deviceId)
                || httpContext.Request.Headers.TryGetValue("xdeviceid", out deviceId))
            {
                logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty(
                    "X-Device-Id"
                    , deviceId));
            }
            
            if (httpContext.Request.Headers.TryGetValue("X-Token-Id", out var tokenId)
                || httpContext.Request.Headers.TryGetValue("xtokenid", out tokenId))
            {
                logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty(
                    "X-Token-Id"
                    , tokenId));
            }
            
            if (httpContext.Request.Headers.TryGetValue("user_reference", out var reference))
            {
                logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty(
                    "user_reference"
                    , reference));
            }
        }
    }
}
