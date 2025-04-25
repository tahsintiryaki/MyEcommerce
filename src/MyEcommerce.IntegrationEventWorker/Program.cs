using MyEcommerce.IntegrationEventWorker;
using MyEcommerce.IntegrationEventWorker.Configuration;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();
builder.Configuration
    .AddJsonFile("Configuration/Settings/appsettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile($"Configuration/Settings/appsettings.{builder.Environment.EnvironmentName}.json",
        optional: true,
        reloadOnChange: true)
    .AddEnvironmentVariables();
var mess = builder.Configuration["MessagingQueue:Uri"];
builder.Services.UseIntegrationHandler(builder.Configuration);
var host = builder.Build();
host.Run();