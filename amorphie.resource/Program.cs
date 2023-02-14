using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecretExtensions;

var builder = WebApplication.CreateBuilder(args);

await builder.Configuration.AddVaultSecrets("amorphie-secretstore", "amorphie-secretstore");
var postgreSql = builder.Configuration["PostgreSql"];



builder.Logging.ClearProviders();
builder.Logging.AddJsonConsole();

builder.Services.AddDaprClient();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ResourceDBContext>
    (options => options.UseNpgsql(postgreSql));

var app = builder.Build();

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
// app.MapResourceLanguageEndpoints();

try
{
    app.Logger.LogInformation("Starting application...");
    app.Run();
}
catch (Exception ex)
{
    app.Logger.LogCritical(ex, "Aplication is terminated unexpectedly ");
}


