using System.Reflection;
using amorphie.core.Extension;
using amorphie.core.Identity;
using amorphie.core.Repository;
using amorphie.core.security;
using amorphie.core.security.Extensions;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

await builder.Configuration.AddVaultSecrets("amorphie-secretstore", new string[] { "amorphie-secretstore" });
var postgreSql = builder.Configuration["PostgreSql"];
// var postgreSql = "Host=localhost:5432;Database=resources;Username=postgres;Password=postgres";

builder.Logging.ClearProviders();
builder.Logging.AddJsonConsole();

builder.Services.AddDaprClient();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IBBTIdentity, FakeIdentity>();
builder.Services.AddScoped(typeof(IBBTRepository<,>), typeof(BBTRepository<,>));

var assemblies = new Assembly[]
                {
                     typeof(ResourceValidator).Assembly, typeof(ResourceMapper).Assembly
                };

builder.Services.AddValidatorsFromAssemblies(assemblies);
builder.Services.AddAutoMapper(assemblies);

builder.Services.AddDbContext<ResourceDBContext>
    (options => options.UseNpgsql(postgreSql, b => b.MigrationsAssembly("amorphie.resource.data")));

var app = builder.Build();
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

using var scope = app.Services.CreateScope();
var db = scope.ServiceProvider.GetRequiredService<ResourceDBContext>();
db.Database.Migrate();

app.UseCloudEvents();
app.UseRouting();
app.MapSubscribeHandler();

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


