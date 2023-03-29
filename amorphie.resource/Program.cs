using amorphie.core.security;
using amorphie.core.security.Extensions;

var builder = WebApplication.CreateBuilder(args);

// await builder.Configuration.AddVaultSecrets("amorphie-secretstore", new string[] { "amorphie-secretstore" });
// var postgreSql = builder.Configuration["PostgreSql"];
var postgreSql = "Host=localhost:5432;Database=resources;Username=postgres;Password=postgres";

builder.Logging.ClearProviders();
builder.Logging.AddJsonConsole();

builder.Services.AddDaprClient();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ResourceDBContext>
    (options => options.UseNpgsql(postgreSql, b => b.MigrationsAssembly("amorphie.resource.data")));

var app = builder.Build();

using var scope = app.Services.CreateScope();
var db = scope.ServiceProvider.GetRequiredService<ResourceDBContext>();
db.Database.Migrate();

app.UseCloudEvents();
app.UseRouting();
app.MapSubscribeHandler();

app.UseSwagger();
app.UseSwaggerUI();

app.MapResourceEndpoints();
app.MapRoleEndpoints();
app.MapRoleGroupEndpoints();
app.MapRoleGroupRoleEndpoints();
app.MapResourceRoleEndpoints();
app.MapPrivilegeEndpoints();
app.MapResourceRateLimitEndpoints();
app.MapScopeEndpoints();

try
{
    app.Logger.LogInformation("Starting application...");
    app.Run();
}
catch (Exception ex)
{
    app.Logger.LogCritical(ex, "Aplication is terminated unexpectedly ");
}


