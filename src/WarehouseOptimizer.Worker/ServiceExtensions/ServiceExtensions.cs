using WarehouseOptimizer.DataContext.Repository.Repository;
using WarehouseOptimizer.Infrastructure;
using WarehouseOptimizer.Worker.Queue;

namespace WarehouseOptimizer.Worker.ServiceExtensions;

public static class ServiceExtensions
{
    public static void AddServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddSingleton<QueueMessageHandlers>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddHostedService<Worker>();

        services.AddOptions();

        services.Configure<QueueConnectionSettings>(config.GetSection("QueueConnection"));
    }
}