using Microsoft.Extensions.DependencyInjection;
using MyEcommerce.User.Application.Interfaces.Services;
using MyEcommerce.User.Infrastructure.Services;

namespace MyEcommerce.User.Infrastructure;

public static class ServiceRegistration
{
    public static void AddInfrastructureServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<IUserService, UserService>();
    }
}