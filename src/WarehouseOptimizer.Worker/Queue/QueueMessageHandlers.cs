using System.Text.Json;
using WarehouseOptimizer.Contracts.Commands;

namespace WarehouseOptimizer.Worker.Queue;

public class QueueMessageHandlers
{
    public Dictionary<QueueNames, Func<string, Task>> Handlers { get; }

    public QueueMessageHandlers(ILogger<QueueMessageHandlers> logger)
    {
        Handlers = new Dictionary<QueueNames, Func<string, Task>>()
        {
            {
                QueueNames.RegisterSku, async messageJson =>
                {
                    var msg = JsonSerializer.Deserialize<RegisterSkuCommand>(messageJson);
                    logger.LogInformation("[{queueName}] Message received: {message}.", nameof(QueueNames.RegisterSku),
                        messageJson);
                }
            },
            {
                QueueNames.RegisterWarehouseCell, async messageJson =>
                {
                    var msg = JsonSerializer.Deserialize<RegisterWarehouseCellCommand>(messageJson);
                    logger.LogInformation("[{queueName}] Message received: {message}.", nameof(QueueNames.RegisterWarehouseCell),
                        messageJson);
                }
            },
            {
                QueueNames.UpdateSku, async messageJson =>
                {
                    var msg = JsonSerializer.Deserialize<UpdateSkuCommand>(messageJson);
                    logger.LogInformation("[{queueName}] Message received: {message}.", nameof(QueueNames.UpdateSku),
                        messageJson);
                }
            },
            {
                QueueNames.UpdateWarehouseCell, async messageJson =>
                {
                    var msg = JsonSerializer.Deserialize<UpdateWarehouseCellCommand>(messageJson);
                    logger.LogInformation("[{queueName}] Message received: {message}.", nameof(QueueNames.UpdateWarehouseCell),
                        messageJson);
                }
            },
            {
                QueueNames.CalculatePlacement, async messageJson =>
                {
                    var msg = JsonSerializer.Deserialize<CalculatePlacementCommand>(messageJson);
                    logger.LogInformation("[{queueName}] Message received: {message}.", nameof(QueueNames.CalculatePlacement),
                        messageJson);
                }
            },
        };
    }
}