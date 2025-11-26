using Confluent.Kafka;
using Hangfire;
using Microsoft.OpenApi.Models;
using NLog.Extensions.Logging;
using Telemetry.Api.Background;
using Telemetry.Api.Background.Implementation;
using Telemetry.Api.Business;
using Telemetry.Api.Business.Implementation;
using Telemetry.Api.Components.Clients;
using Telemetry.Api.Components.Health;
using Telemetry.Api.Components.MessageBroker;
using Telemetry.Api.Components.MessageBroker.Implementation;
using Telemetry.Api.Components.Store;
using Telemetry.Api.Components.Store.Implementation;
using Telemetry.Api.Models.Settings;


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

builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("Application"));
AppSettings appSettings = builder.Configuration.GetSection("Application").Get<AppSettings>()!;
builder.Services.AddOptions<AppSettings>()
    .ValidateDataAnnotations()
    .ValidateOnStart();

builder.Services.Configure<KafkaSettings>(builder.Configuration.GetSection("Kafka"));
KafkaSettings kafkaSettings = builder.Configuration.GetSection("Kafka").Get<KafkaSettings>()!;
builder.Services.AddOptions<KafkaSettings>()
    .ValidateDataAnnotations()
    .ValidateOnStart();

builder.Services.AddHealthChecks()
    .AddCheck<SmartHomeClientHealthCheck>(
                        "smart-home-check")
    .AddKafka(new ProducerConfig
    {
        BootstrapServers = kafkaSettings!.BootstrapServers,
        SocketTimeoutMs = 10000
    });

builder.Services.AddControllers();

builder.Services.AddHangfire(config =>
    config
        .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
        .UseSimpleAssemblyNameTypeSerializer()
        .UseDefaultTypeSerializer()
        .UseInMemoryStorage()
);
builder.Services.AddHangfireServer();

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


builder.Services.AddSingleton<IUnitOfWork, TelemetryUoW>();

builder.Services
                .AddSingleton<ITelemetryStore, TelemetryStore>()
                .AddTransient<ITelemetryManager, TelemetryManager>()
                .AddTransient<ISmartHomeClient, SmartHomeClient>()
                .AddTransient<IDeviceMessagePublisher, DeviceMessagePublisher>();

builder.Services.AddHttpClient<SmartHomeClient>()
    .ConfigureHttpClient(hc => hc.BaseAddress = new Uri(appSettings.SmartHomeUrl!));

var app = builder.Build();


app.UseSwagger();
app.UseHangfireDashboard("/hangfire");
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Device mvp API v1"));

app.UseHealthChecks("/health");

app.UseHttpLogging();
app.UseHttpsRedirection();

app.MapControllers();



ILogger logger = app.Services.GetRequiredService<ILogger<Program>>();
try
{
    IEnumerable<IUnitOfWork> uowStore = app.Services.GetServices<IUnitOfWork>();
    IRecurringJobManager recurringJobManager = app.Services.GetRequiredService<IRecurringJobManager>();

    foreach (IUnitOfWork uow in uowStore)
    {
        string uowId = uow.GetType().Name!.ToLower();

        recurringJobManager.AddOrUpdate(uowId, () => uow.InvokeAsync(), "* * * * * *");
        logger.LogInformation($"Loader {uowId} was initialized");
    }

}
catch (Exception e)
{
    logger.LogError(e, "Loaders init was failed");
    throw;
}

app.Run();
