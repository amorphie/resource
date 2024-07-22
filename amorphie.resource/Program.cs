using System.Reflection;
using amorphie.core.Extension;
using amorphie.core.Identity;
using amorphie.core.Swagger;
using amorphie.resource.data;
using FluentValidation;
using Elastic.Apm.NetCoreAll;
using Microsoft.AspNetCore.HttpLogging;
using amorphie.resource;
using amorphie.core.Middleware.Logging;
using Serilog;
using Serilog.Core;
using Serilog.Formatting.Compact;


var builder = WebApplication.CreateBuilder(args);
await builder.Configuration.AddVaultSecrets("amorphie-secretstore", new string[] { "amorphie-secretstore" });
var postgreSql = builder.Configuration["PostgreSql"];
// var postgreSql = "Host=localhost:5432;Database=resources;Username=postgres;Password=postgres";

builder.Logging.ClearProviders();
Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .Enrich.WithEnvironmentName()
    .Enrich.WithMachineName()
    .WriteTo.Console()
    .WriteTo.File(new CompactJsonFormatter(), "logs/amorphie-resource-log.json", rollingInterval: RollingInterval.Day)
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();
List<string> defaultHeadersToBeLogged = new List<string>()
{
    "Content-Type",
    "Host",
    "X-Zeebe-Job-Key",
    "xdeviceid",
    "X-Device-Id",
    "xtokenid",
    "X-Token-Id",
    "Transfer-Encoding",
    "X-Forwarded-Host",
    "X-Forwarded-For"
};
builder.Services.AddHttpLogging((Action<HttpLoggingOptions>) (logging =>
{
    bool flag = builder.Environment.IsProd() || builder.Environment.IsProduction();
    logging.LoggingFields = flag ? HttpLoggingFields.RequestScheme : HttpLoggingFields.RequestScheme | HttpLoggingFields.RequestBody | HttpLoggingFields.ResponseBody;
    defaultHeadersToBeLogged.ForEach((Action<string>) (p => logging.RequestHeaders.Add(p)));
    logging.MediaTypeOptions.AddText("application/javascript");
    logging.RequestBodyLogLimit = 4096;
    logging.ResponseBodyLogLimit = 4096;
    logging.CombineLogs = true;
}));
builder.Services.AddHttpContextAccessor();

builder.Host.UseSerilog(Log.Logger, true);
builder.Services.AddDaprClient();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.OperationFilter<AddSwaggerParameterFilter>();
});

builder.Services.AddScoped<IBBTIdentity, FakeIdentity>();
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

var assemblies = new Assembly[]
                {
                     typeof(ResourceValidator).Assembly, typeof(ResourceMapper).Assembly
                };

builder.Services.AddValidatorsFromAssemblies(assemblies);
builder.Services.AddAutoMapper(assemblies);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            builder.WithOrigins("*")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

builder.Services.AddDbContext<ResourceDBContext>
    (options => options.UseNpgsql(postgreSql, b => b.MigrationsAssembly("amorphie.resource.data")));

builder.Services.AddHealthChecks();

var app = builder.Build();

app.UseMiddleware<HttpMiddleware>();

if (!app.Environment.IsDevelopment())
{
    app.UseAllElasticApm(app.Configuration);
}

app.UseLoggingHandlerMiddlewares();

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

using var scope = app.Services.CreateScope();
var db = scope.ServiceProvider.GetRequiredService<ResourceDBContext>();
db.Database.Migrate();
DbInitializer.Initialize(db);
app.MapHealthChecks("/health");
app.UseRouting();
app.MapSubscribeHandler();
app.UseCors();
app.UseSwagger();
app.UseSwaggerUI();

try
{
    app.Logger.LogInformation("Starting application...");
    app.AddRoutes();
    app.Run();
}
catch (Exception ex)
{
    app.Logger.LogCritical(ex, "Aplication is terminated unexpectedly ");
}
