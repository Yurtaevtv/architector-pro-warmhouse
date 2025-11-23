using Confluent.Kafka;
using DeviceService.Business;
using DeviceService.Business.Implementation;
using DeviceService.Components.MessageBroker;
using DeviceService.Components.MessageBroker.Implementation;
using DeviceService.DAL.Context;
using DeviceService.DAL.Repostory;
using DeviceService.DAL.Repostory.Implementation;
using DeviceService.Models.Settings;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.OpenApi.Models;
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

builder.Services.Configure<KafkaSettings>(builder.Configuration.GetSection("Kafka"));
KafkaSettings kafkaSettings = builder.Configuration.GetSection("Kafka").Get<KafkaSettings>()!;
builder.Services.AddOptions<KafkaSettings>()
    .ValidateDataAnnotations()
    .ValidateOnStart();

builder.Services.AddHealthChecks()
    .AddNpgSql(
        connectionStringFactory: (sp) => sp.GetRequiredService<IConfiguration>().GetConnectionString(AppSettings.ConnectionStringKey)!,
        name: "DeviceDb",
        failureStatus: HealthStatus.Unhealthy,
        tags: ["device-service", "db", "postgre"],
        timeout: TimeSpan.FromMinutes(1)
    )
    .AddKafka(new ProducerConfig
    {
        BootstrapServers = kafkaSettings!.BootstrapServers
    });

builder.Services.AddControllers();

builder.Services.AddAutoMapper(_ => { }, AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddHttpLogging(_ => { });

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Device mvp service API",
        Description = "MVP API для работы с устройств"
    });
});


builder.Services.AddScoped<IDeviceManager, DeviceManager>()
                .AddSingleton<IDeviceRepository, DeviceRepository>()
                .AddTransient<IDeviceEventPublisher, DeviceEventPublisher>();

builder.Services.AddDbContextFactory<DeviceContext>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Device mvp API v1"));
app.UseHealthChecks("/health");

app.UseHttpLogging();
app.UseHttpsRedirection();

app.MapControllers();
app.Run();
