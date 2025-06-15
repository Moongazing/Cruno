namespace Moongazing.Cruno.Infrastructure.Messaging;

public interface IRabbitMqPublisher
{
    Task PublishAsync(string exchange, string routingKey, object message);
}
