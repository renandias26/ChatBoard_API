using ChatBoard.API.Hubs;
using ChatBoard.API.HubsConnections;
using ChatBoard.DataBase.Injection;
using ChatBoard.Services.Injection;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR();
builder.Services.AddControllers();
builder.Services.AddApplicationServices();

builder.Services.SetDatabase(builder.Configuration);

builder.Services.AddHealthChecks()
    .AddNpgSql(
        builder.Configuration.GetConnectionString("DefaultConnection") ?? "",
        name: "database",
        failureStatus: HealthStatus.Unhealthy,
        tags: ["db", "postgresql"])
    .AddCheck("signalr_health", () => HealthCheckResult.Healthy("SignalR está disponível"),
        tags: ["signalr"]);


// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(opt =>
{
    opt.AddPolicy("AngularApp", builder =>
    {
        //Adjust Origin ULR
        builder.WithOrigins("http://localhost:4200")
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
    Predicate = (check) => check.Tags.Contains("db")
});

app.MapHealthChecks("/health/signalr", new HealthCheckOptions
{
    Predicate = (check) => check.Tags.Contains("signalr")
});

app.Run();
