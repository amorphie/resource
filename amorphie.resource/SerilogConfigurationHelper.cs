using amorphie.core.Extension;
using Microsoft.AspNetCore.HttpLogging;
using Serilog;
using Serilog.Formatting.Compact;

namespace amorphie.resource;

public static class SerilogConfigurationHelper
{
    static readonly List<string> DefaultHeadersToBeLogged =
    [
        "Content-Type",
        "Host",
        "X-Zeebe-Job-Key",
        "xdeviceid",
        "X-Device-Id",
        "xtokenid",
        "X-Token-Id",
        "Transfer-Encoding",
        "X-Forwarded-Host",
        "X-Forwarded-For",
        "X-Request-Id",
        "xrequestid"
    ];
    
    public static WebApplicationBuilder SerilogConfigure(this WebApplicationBuilder builder)
    {
        builder.Logging.ClearProviders();
        builder.Services.AddSingleton<HttpRequestAndCorrelationContextEnricher>();
        builder.Services.AddHttpLogging((Action<HttpLoggingOptions>) (logging =>
        {
            bool flag = builder.Environment.IsProd() || builder.Environment.IsProduction();
            logging.LoggingFields = flag ? HttpLoggingFields.RequestScheme : HttpLoggingFields.RequestScheme | HttpLoggingFields.RequestBody | HttpLoggingFields.ResponseBody;
            DefaultHeadersToBeLogged.ForEach((Action<string>) (p => logging.RequestHeaders.Add(p)));
            logging.MediaTypeOptions.AddText("application/javascript");
            logging.RequestBodyLogLimit = 4096;
            logging.ResponseBodyLogLimit = 4096;
            logging.CombineLogs = true;
        }));
        
        builder.Host.UseSerilog((_, serviceProvider, loggerConfiguration) =>
        {
            var enricher = serviceProvider.GetRequiredService<HttpRequestAndCorrelationContextEnricher>();
            loggerConfiguration
                .Enrich.FromLogContext()
                .Enrich.WithEnvironmentName()
                .Enrich.WithMachineName()
                .Enrich.With(enricher)
                .WriteTo.Console()
                .WriteTo.File(new CompactJsonFormatter(), "logs/amorphie-resource-log.json",
                    rollingInterval: RollingInterval.Day)
                .ReadFrom.Configuration(builder.Configuration);
        });
        
        return builder;
    }
}
