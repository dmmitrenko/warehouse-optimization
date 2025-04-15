using System.Text;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using WarehouseOptimizer.Worker.Queue;

namespace WarehouseOptimizer.Worker;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly QueueMessageHandlers _handlers;
    private IConnection _connection;
    private readonly List<IModel> _channels = new();

    private readonly QueueNames[] _queues =
    [
        QueueNames.RegisterSku,
        QueueNames.UpdateSku,
        QueueNames.UpdateWarehouseCell,
        QueueNames.CalculatePlacement,
        QueueNames.PlacementResult
    ];

    public Worker(
        ILogger<Worker> logger,
        QueueMessageHandlers handlers,
        IOptions<QueueConnectionSettings> connectionSettings)
    {
        _logger = logger;
        _handlers = handlers;

        var factory = new ConnectionFactory
        {
            HostName = connectionSettings.Value.HostName,
            UserName = connectionSettings.Value.UserName,
            Password = connectionSettings.Value.Password,
            VirtualHost = connectionSettings.Value.VirtualHost,
        };

        _connection = factory.CreateConnection();
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {

        foreach (var queue in _queues)
        {
            var channel = _connection.CreateModel();
            _channels.Add(channel);

            channel.QueueDeclare(queue.ToString(), durable: true, exclusive: false, autoDelete: false, arguments: null);

            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += async (model, ea) =>
            {
                var json = Encoding.UTF8.GetString(ea.Body.ToArray());

                if (_handlers.Handlers.TryGetValue(queue, out var handler))
                {
                    try
                    {
                        await handler(json);
                        channel.BasicAck(ea.DeliveryTag, false);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Message processing error for queue {Queue}", queue);
                        channel.BasicNack(ea.DeliveryTag, false, requeue: true);
                    }
                }
                else
                {
                    _logger.LogWarning("No handler for the queue {Queue}", queue);
                    channel.BasicNack(ea.DeliveryTag, false, requeue: false);
                }
            };

            channel.BasicConsume(queue.ToString(), autoAck: false, consumer);
            _logger.LogInformation("Queue consumer {Queue} has started.", queue);
        }

        return Task.CompletedTask;
    }

    public override void Dispose()
    {
        foreach (var channel in _channels)
        {
            channel.Close();
            channel.Dispose();
        }

        _connection.Close();
        _connection.Dispose();

        base.Dispose();
    }
}
