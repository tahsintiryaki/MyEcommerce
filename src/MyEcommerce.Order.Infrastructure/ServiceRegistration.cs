using Microsoft.Extensions.DependencyInjection;
using MyEcommerce.Order.Application.Interfaces.Services;
using MyEcommerce.Order.Infrastructure.Services;

namespace MyEcommerce.Order.Infrastructure;

public static class ServiceRegistration
{
    public static void AddInfrastructureService(this IServiceCollection serviceCollection)
    {
        
        serviceCollection.AddScoped<IOrderService, OrderService>();
    }
}