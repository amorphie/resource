using System.Reflection;
using amorphie.core.Extension;
using amorphie.core.Identity;
using amorphie.core.Swagger;
using amorphie.resource.data;
using FluentValidation;
using Elastic.Apm.NetCoreAll;
using amorphie.resource;


var builder = WebApplication.CreateBuilder(args);
await builder.Configuration.AddVaultSecrets("amorphie-secretstore", new string[] { "amorphie-secretstore" });
var postgreSql = builder.Configuration["PostgreSql"];
// var postgreSql = "Host=localhost:5432;Database=resources;Username=postgres;Password=postgres";

builder.Services.AddHttpContextAccessor();
builder.SerilogConfigure();
builder.Services.AddDaprClient();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options => { options.OperationFilter<AddSwaggerParameterFilter>(); });

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
if (!app.Environment.IsDevelopment())
{
    app.UseAllElasticApm(app.Configuration);
}

// app.UseMiddleware<HttpMiddleware>();
app.UseMiddleware<ResourceExceptionHandlerMiddleware>();
app.UseHttpLogging();
// app.UseLoggingHandlerMiddlewares();


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
