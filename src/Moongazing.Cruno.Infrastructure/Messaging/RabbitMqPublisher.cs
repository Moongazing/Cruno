using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace Moongazing.Cruno.Infrastructure.Messaging;

public class RabbitMqPublisher : IRabbitMqPublisher
{
    private readonly IConnection _connection;
    private readonly RabbitMQ.Client.IModel _channel;

    public RabbitMqPublisher()
    {
        var factory = new ConnectionFactory() { HostName = "localhost" };
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
    }

    public Task PublishAsync(string exchange, string routingKey, object message)
    {
        var json = JsonSerializer.Serialize(message);
        var body = Encoding.UTF8.GetBytes(json);

        _channel.BasicPublish(
            exchange: exchange,
            routingKey: routingKey,
            basicProperties: null,
            body: body);

        return Task.CompletedTask;
    }
}
