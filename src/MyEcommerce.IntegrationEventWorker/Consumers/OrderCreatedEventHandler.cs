using MassTransit;
using MyEcommerce.SharedLibrary.IntegrationEvents.Order;


namespace MyEcommerce.IntegrationEventWorker.Consumers;

public class OrderCreatedEventHandler : IConsumer<OrderCreatedEvent>
{
    public Task Consume(ConsumeContext<OrderCreatedEvent> context)
    {
        Console.WriteLine($"{context.Message.Id} nolu order was created.");
        return Task.CompletedTask;
    }
}