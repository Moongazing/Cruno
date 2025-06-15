using Moongazing.Cruno.Worker.Messaging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace Moongazing.Cruno.Worker.Services;


public class JobExecutionConsumer : BackgroundService
{
    private readonly ILogger<JobExecutionConsumer> _logger;
    private readonly IJobExecutor _executor;
    private IModel? _channel;
    private IConnection? _connection;

    public JobExecutionConsumer(
        ILogger<JobExecutionConsumer> logger,
        IJobExecutor executor)
    {
        _logger = logger;
        _executor = executor;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var factory = new ConnectionFactory() { HostName = "localhost" }; 
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();

        _channel.QueueDeclare(queue: "cruno.job.queue",
                             durable: true,
                             exclusive: false,
                             autoDelete: false,
                             arguments: null);

        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += async (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var json = Encoding.UTF8.GetString(body);
            var message = JsonSerializer.Deserialize<JobExecutionMessage>(json);

            if (message is not null)
                await _executor.ExecuteAsync(message);
        };

        _channel.BasicConsume(queue: "cruno.job.queue", autoAck: true, consumer: consumer);

        _logger.LogInformation("WorkerService is listening to cruno.job.queue...");
        return Task.CompletedTask;
    }

    public override void Dispose()
    {
        _channel?.Dispose();
        _connection?.Dispose();
        base.Dispose();
    }
}
