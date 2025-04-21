using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyEcommerce.User.Application.Interfaces.Repository;
using MyEcommerce.User.Persistence.Contexts;
using MyEcommerce.User.Persistence.Repositories;

namespace MyEcommerce.User.Persistence;

public static class ServiceRegistration
{
    public static void AddPersistenceServices(this IServiceCollection serviceCollection,
        IConfiguration configuration = null)
    {
        serviceCollection.AddDbContext<UserDbContext>(options =>
            options.UseNpgsql(configuration?.GetConnectionString("Default")));

        serviceCollection.AddTransient<IUserRepository, UserRepository>();
        // serviceCollection.AddTransient<IUserService, UserService>();
    }
}