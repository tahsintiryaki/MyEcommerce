using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace MyEcommerce.Order.Application;

public static class ServiceRegistration
{
    public static void AddApplicationServices(this IServiceCollection serviceCollection)
    {
        var assm = Assembly.GetExecutingAssembly();

        serviceCollection.AddAutoMapper(assm);
    }
}