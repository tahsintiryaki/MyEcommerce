using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyEcommerce.Order.Application.Interfaces.Repositories;
using MyEcommerce.Order.Persistence.Contexts;
using MyEcommerce.Order.Persistence.Repositories;

namespace MyEcommerce.Order.Persistence;

public static class ServiceRegistration
{
    public static void AddPersistenceService(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddDbContext<OrderDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("Default")));

        serviceCollection.AddScoped<IOrderRepository, OrderRepository>();
     
    }
}