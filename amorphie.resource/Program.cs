using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);


builder.Logging.ClearProviders();
builder.Logging.AddJsonConsole();

builder.Services.AddDaprClient();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCloudEvents();
app.UseRouting();
app.MapSubscribeHandler();

app.UseSwagger();
app.UseSwaggerUI();



app.MapGet("/resource", ([FromQuery] string? name) => { })
    .WithOpenApi(operation =>
        {
            operation.Summary = "Returns queried resources.";
            operation.Parameters[0].Description = "Full or partial name of resource name to be queried.";
            return operation;
        })
    .Produces<GetResourceResponse[]>(StatusCodes.Status200OK)
    .Produces(StatusCodes.Status204NoContent)
;

try
{
    app.Logger.LogInformation("Starting application...");
    app.Run();
}
catch (Exception ex)
{
    app.Logger.LogCritical(ex, "Aplication is terminated unexpectedly ");
}


