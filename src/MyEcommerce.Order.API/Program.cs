using MyEcommerce.Cache;
using MyEcommerce.Order.Application;
using MyEcommerce.Order.Infrastructure;
using MyEcommerce.Order.Persistence;
using MyEcommerce.Order.Persistence.Contexts;
using MyEcommerce.Order.Persistence.Extensions;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddPersistenceService(builder.Configuration);
builder.Services.AddInfrastructureService();
builder.Services.AddApplicationServices();
builder.Services.AddMessagingQueue(builder.Configuration);
builder.Services.CacheConfigurator(builder.Configuration);

 // builder.Configuration
 //        .AddJsonFile("Configuration/Settings/appsettings.json", optional: true, reloadOnChange: true)
 //        .AddJsonFile($"Configuration/Settings/appsettings.{builder.Environment.EnvironmentName}.json",
 //            optional: true,
 //            reloadOnChange: true)
 //        .AddEnvironmentVariables();
 //    var mess = builder.Configuration["MessagingQueue:Uri"];
// builder.Services.UseIntegrationHandler(builder.Configuration);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    await scope.ServiceProvider
        .GetRequiredService<OrderDbContext>()
        .DatabaseMigrator();
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();