using DeviceService.Business;
using DeviceService.Business.Implementation;
using DeviceService.Components.MessageBroker;
using DeviceService.Components.MessageBroker.Implementation;
using DeviceService.DAL.Repostory;
using DeviceService.DAL.Repostory.Implementation;
using DeviceService.Models.Settings;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using NLog.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddLogging(logBuilder =>
        {
            logBuilder.ClearProviders();
            logBuilder.SetMinimumLevel(LogLevel.Trace);
            logBuilder.AddConsole();
            logBuilder.AddNLog("NLog.config");
        }
    );

builder.Services.AddHealthChecks()
    .AddNpgSql(
        connectionStringFactory: (sp) => sp.GetRequiredService<IConfiguration>().GetConnectionString(AppSettings.ConnectionStringKey)!,
        name: "DeviceDb",
        failureStatus: HealthStatus.Unhealthy,
        tags: ["device-service", "db", "postgre"],
        timeout: TimeSpan.FromMinutes(1)
    );

builder.Services.AddControllers();

builder.Services.AddHttpLogging(option => { });

builder.Services.AddScoped<IDeviceManager, DeviceManager>()
                .AddSingleton<IDeviceRepository, DeviceRepository>()
                .AddTransient<IDeviceEventPublisher, DeviceEventPublisher>();

var app = builder.Build();

app.UseHttpLogging();
app.UseHttpsRedirection();
app.UseHealthChecks("health");

app.UseAuthorization();

app.MapControllers();
app.Run();
