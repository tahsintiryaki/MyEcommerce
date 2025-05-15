using MLE.Product.Persistence;
using MyEcommerce.Cache;
using MyEcommerce.Product.Application;
using MyEcommerce.Product.Infrastructure;
using MyEcommerce.Product.Infrastructure.GRPC;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureMongoDb(builder.Configuration);
builder.Services.AddPersistenceServices();
builder.Services.AddInfrastructureServices();
builder.Services.AddApplicationServices();
builder.Services.CacheConfigurator(builder.Configuration);
builder.Services.AddGrpc();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}
app.MapGrpcService<ProductCheckerService>();
app.UseHttpsRedirection();
app.MapControllers();
app.Run();
