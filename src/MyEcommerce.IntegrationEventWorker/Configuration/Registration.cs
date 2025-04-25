using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MassTransit;
using MyEcommerce.IntegrationEventWorker.Consumers;

namespace MyEcommerce.IntegrationEventWorker.Configuration;

public static class Registration
{
    public static IServiceCollection UseIntegrationHandler(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.UseMessagingQueue(configuration);
        return services;
    }
     private static IServiceCollection UseMessagingQueue(this IServiceCollection services,
        IConfiguration configuration)
    {
        var mqOptions = configuration.GetSection("MessagingQueue").Get<MessagingQueue>();

        if (mqOptions == null)
        {
            throw new InvalidOperationException(
                "Messaging Queue settings must be properly configured in the application configuration file.");
        }

        services.AddMassTransit(x =>
        {
            x.SetKebabCaseEndpointNameFormatter();
            x.AddConsumer<OrderCreatedEventHandler>();
        

            x.UsingRabbitMq((rabbitContext, cfg) =>
            {
                cfg.Host(new Uri(mqOptions.Uri), configurator =>
                {
                    configurator.Username(mqOptions.Username);
                    configurator.Password(mqOptions.Password);
                });

                // cfg.ReceiveEndpoint("journey-abuse-mail-event-handler", e =>
                // {
                //     e.UseMessageRetry(r => { r.Interval(5, TimeSpan.FromMinutes(30)); });
                //     e.ConfigureConsumer<JourneyAbuseMailEventHandler>(rabbitContext);
                // });

                cfg.ConfigureEndpoints(rabbitContext);
            });
        });

        return services;
    }


    public record MessagingQueue
    {
        public string Uri { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}