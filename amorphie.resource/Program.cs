using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Logging.ClearProviders();
builder.Logging.AddJsonConsole();

builder.Services.AddDaprClient();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ResourceDBContext>
    (options => options.UseNpgsql("Host=localhost:5432;Database=resources;Username=postgres;Password=postgres"));

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

try
{
    app.Logger.LogInformation("Starting application...");
    app.Run();
}
catch (Exception ex)
{
    app.Logger.LogCritical(ex, "Aplication is terminated unexpectedly ");
}


