using Microsoft.Extensions.DependencyInjection;
using MyEcommerce.Product.Persistence.Services;
using MyEcommerce.Product.Application.Interfaces.Services;

namespace MyEcommerce.Product.Infrastructure;

public static class ServiceRegistration
{
    public static void AddInfrastructureServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<IProductService,ProductService>();
    }
    
}