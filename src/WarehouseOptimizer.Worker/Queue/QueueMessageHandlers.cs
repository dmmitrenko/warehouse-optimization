using System.Text.Json;
using MediatR;
using WarehouseOptimizer.Contracts.Commands;

namespace WarehouseOptimizer.Worker.Queue;

public class QueueMessageHandlers
{
    public Dictionary<QueueNames, Func<string, Task>> Handlers { get; }
    private readonly ILogger<QueueMessageHandlers> _logger;
    public IServiceProvider _serviceProvider { get; }

    public QueueMessageHandlers(ILogger<QueueMessageHandlers> logger, IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
        Handlers = new Dictionary<QueueNames, Func<string, Task>>()
        {
            {
                QueueNames.RegisterSku, async messageJson =>
                {
                    var msg = JsonSerializer.Deserialize<RegisterSkuCommand>(messageJson);
                    _logger.LogInformation("[{queueName}] Message received: {message}.", nameof(QueueNames.RegisterSku), messageJson);
                    await ProcessMessage(msg);
                }
            },
            {
                QueueNames.RegisterWarehouseCell, async messageJson =>
                {
                    var msg = JsonSerializer.Deserialize<RegisterWarehouseCellCommand>(messageJson);
                    logger.LogInformation("[{queueName}] Message received: {message}.", nameof(QueueNames.RegisterWarehouseCell), messageJson);
                    await ProcessMessage(msg);
                }
            },
            {
                QueueNames.UpdateSku, async messageJson =>
                {
                    var msg = JsonSerializer.Deserialize<UpdateSkuCommand>(messageJson);
                    logger.LogInformation("[{queueName}] Message received: {message}.", nameof(QueueNames.UpdateSku), messageJson);
                    await ProcessMessage(msg);
                }
            },
            {
                QueueNames.UpdateWarehouseCell, async messageJson =>
                {
                    var msg = JsonSerializer.Deserialize<UpdateWarehouseCellCommand>(messageJson);
                    logger.LogInformation("[{queueName}] Message received: {message}.", nameof(QueueNames.UpdateWarehouseCell), messageJson);

                    await ProcessMessage(msg);
                }
            },
            {
                QueueNames.CalculatePlacement, async messageJson =>
                {
                    var msg = JsonSerializer.Deserialize<CalculatePlacementCommand>(messageJson);
                    logger.LogInformation("[{queueName}] Message received: {message}.", nameof(QueueNames.CalculatePlacement), messageJson);

                    await ProcessMessage(msg);
                }
            },
        };
    }

    private async Task ProcessMessage(IRequest? request, CancellationToken cancellationToken = default)
    {
        if (request == null)
        {
            _logger.LogError("Error during deserialization.");
            return;
        }

        using var scope = _serviceProvider.CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

        await mediator.Send(request, cancellationToken);
    }
}