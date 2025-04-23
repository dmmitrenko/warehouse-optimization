using System.Text.Json;
using MediatR;
using WarehouseOptimizer.Contracts.Commands;

namespace WarehouseOptimizer.Worker.Queue;

public class QueueMessageHandlers
{
    public Dictionary<QueueNames, Func<string, Task>> Handlers { get; }

    public QueueMessageHandlers(ILogger<QueueMessageHandlers> logger, IServiceProvider serviceProvider)
    {
        Handlers = new Dictionary<QueueNames, Func<string, Task>>()
        {
            {
                QueueNames.RegisterSku, async messageJson =>
                {
                    var msg = JsonSerializer.Deserialize<RegisterSkuCommand>(messageJson);
                    logger.LogInformation("[{queueName}] Message received: {message}.", nameof(QueueNames.RegisterSku), messageJson);
                    if (msg == null){
                        return;
                    }
                    
                    using var scope = serviceProvider.CreateScope();
                    var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

                    await mediator.Send(msg);
                }
            },
            {
                QueueNames.RegisterWarehouseCell, messageJson =>
                {
                    var msg = JsonSerializer.Deserialize<RegisterWarehouseCellCommand>(messageJson);
                    logger.LogInformation("[{queueName}] Message received: {message}.", nameof(QueueNames.RegisterWarehouseCell),
                        messageJson);
                    
                    return Task.CompletedTask;
                }
            },
            {
                QueueNames.UpdateSku, messageJson =>
                {
                    var msg = JsonSerializer.Deserialize<UpdateSkuCommand>(messageJson);
                    logger.LogInformation("[{queueName}] Message received: {message}.", nameof(QueueNames.UpdateSku),
                        messageJson);
                    
                    return Task.CompletedTask;
                }
            },
            {
                QueueNames.UpdateWarehouseCell, messageJson =>
                {
                    var msg = JsonSerializer.Deserialize<UpdateWarehouseCellCommand>(messageJson);
                    logger.LogInformation("[{queueName}] Message received: {message}.", nameof(QueueNames.UpdateWarehouseCell),
                        messageJson);
                    
                    return Task.CompletedTask;
                }
            },
            {
                QueueNames.CalculatePlacement, messageJson =>
                {
                    var msg = JsonSerializer.Deserialize<CalculatePlacementCommand>(messageJson);
                    logger.LogInformation("[{queueName}] Message received: {message}.", nameof(QueueNames.CalculatePlacement),
                        messageJson);

                    return Task.CompletedTask;
                }
            },
        };
    }
}