
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MyEcommerce.Product.Application.Interfaces.Repositories;
using MyEcommerce.Product.Persistence.Contexts;
using MyEcommerce.Product.Persistence.Repositories;

namespace MLE.Product.Persistence;

public static class ServiceRegistration
{
    public static IServiceCollection ConfigureMongoDb(this IServiceCollection services, IConfiguration configuration)
    {
        // appsettings.json içindeki MongoDbSettings bölümünü otomatik olarak bağlar.
        services.Configure<MongoDbSettings>(options =>
            configuration.GetSection("MongoDbSettings").Bind(options));


        // Singleton olarak MongoDbSettings enjekte edilir.
        services.AddSingleton<IMongoDbSettings>(sp =>
            sp.GetRequiredService<IOptions<MongoDbSettings>>().Value);

        // MongoDB Client'ı ve Database bağlamını oluşturur.
        services.AddSingleton<IMongoClient>(sp =>
        {
            var settings = sp.GetRequiredService<IMongoDbSettings>();
            return new MongoClient(settings.ConnectionString);
        });

        services.AddSingleton<IMongoDatabase>(sp =>
        {
            var settings = sp.GetRequiredService<IMongoDbSettings>();
            var client = sp.GetRequiredService<IMongoClient>();
            return client.GetDatabase(settings.DatabaseName);
        });

        return services;
    }

    public static void AddPersistenceServices(this IServiceCollection services)
    {
        services.AddTransient<IProductRepository, ProductRepository>();
    }
  
}