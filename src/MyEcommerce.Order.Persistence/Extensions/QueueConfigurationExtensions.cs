
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MyEcommerce.Order.Persistence.Extensions;

public static class QueueConfigurationExtensions
{
    public static IServiceCollection AddMessagingQueue(this IServiceCollection services,
        IConfiguration configuration)
    {
        var mqOptions = configuration.GetSection("MessagingQueue").Get<MessagingQueueSettings>();
        if (mqOptions == null)
        {
            throw new InvalidOperationException("Messaging Queue settings must be properly configured in the application configuration file.");
        }

        services.AddMassTransit(x =>
        {
            x.SetKebabCaseEndpointNameFormatter();
            x.UsingRabbitMq((rabbitContext, cfg) =>
            {
                cfg.Host(new Uri(mqOptions.Uri), configurator =>
                {
                    configurator.Username(mqOptions.Username);
                    configurator.Password(mqOptions.Password);
                });
                cfg.ConfigureEndpoints(rabbitContext);
            });
        });


        return services;
    }
}