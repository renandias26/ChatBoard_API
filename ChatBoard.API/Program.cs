using ChatBoard.API;
using ChatBoard.API.Hubs;
using ChatBoard.API.HubsConnections;
using ChatBoard.DataBase.Injection;
using ChatBoard.Services.Injection;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);
builder.Services.SetDatabase();

var isMigrationMode = builder.Configuration.GetValue<bool>("MIGRATION_MODE", false);
if (isMigrationMode)
{
    MigrationExtension.ApplyMigrations(builder);
    return;

}

builder.Services.AddSignalR();
builder.Services.AddControllers();
builder.Services.AddApplicationServices();

builder.Services.AddHealthChecks()
    .AddDatabaseHealthCheck()
    .AddCheck(
        "signalr_health", 
        () => HealthCheckResult.Healthy("SignalR está disponível"),
        tags: ["signalr"]);


// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(opt =>
{
    opt.AddPolicy("AngularApp", CorsBuilder =>
    {
        string url = builder.Configuration.GetValue<string>("Front_URL") ?? "";

        CorsBuilder.WithOrigins(url)
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials();
    });
});

builder.Services.AddSingleton<ChatConnection>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapHub<ChatHub>("/Chat");

app.UseCors("AngularApp");

app.MapHealthChecks("/health");

app.MapHealthChecks("/health/database", new HealthCheckOptions
{
    Predicate = (check) => check.Tags.Contains(DatabaseInjectionExtensions.HealthCheckTag)
});

app.MapHealthChecks("/health/signalr", new HealthCheckOptions
{
    Predicate = (check) => check.Tags.Contains("signalr")
});

app.Run();