using Confluent.Kafka;
using NLog.Extensions.Logging;
using WebSocketService.Components.Background;
using WebSocketService.Components.Kafka;
using WebSocketService.Components.Kafka.implementation;
using WebSocketService.Components.Manager;
using WebSocketService.Components.Manager.Implementation;
using WebSocketService.Models;

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
    .AddKafka(new ProducerConfig
    {
        BootstrapServers = kafkaSettings!.BootstrapServers
    });


builder.Services.AddSingleton<IWebSocketManager, WSSocketManager>();
builder.Services.AddSingleton<IConnectionManager, ConnectionManager>();
builder.Services.AddSingleton<IWSConsumer, DeviceEventConsumer>();

builder.Services.AddControllers();

builder.Services.AddHostedService<ConsumerBackgroundService>();

var app = builder.Build();

// Configure the HTTP request pipeline.


app.UseHealthChecks("/health");

app.UseHttpsRedirection();


app.UseWebSockets();

app.MapControllers();

app.Run();
