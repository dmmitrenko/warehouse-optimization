using StackExchange.Redis;
using WarehouseOptimizer.Application;
using WarehouseOptimizer.Infrastructure;
using WarehouseOptimizer.Worker.Queue;

namespace WarehouseOptimizer.Worker.ServiceExtensions;

public static class ServiceExtensions
{
    public static void AddServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddSingleton<QueueMessageHandlers>();
        services.AddHostedService<Worker>();

        services.AddOptions();

        services.Configure<QueueConnectionSettings>(config.GetSection("QueueConnection"));
    }

    public static void AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        services.AddSingleton<IConnectionMultiplexer>(sp =>
        {
            var redisConnectionString = config.GetConnectionString("Redis") ?? "localhost";
            return ConnectionMultiplexer.Connect(redisConnectionString);
        });

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AssemblyReference.Assembly));

        services.AddScoped(typeof(IRepository<>), typeof(RedisRepository<>));
    }
}