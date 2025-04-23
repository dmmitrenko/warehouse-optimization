using StackExchange.Redis;
using WarehouseOptimizer.Infrastructure;

namespace WarehouseOptimizer.API.Extensions;

public static class ServiceExtensions
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        services.AddSingleton<IConnectionMultiplexer>(sp =>
        {
            var redisConnectionString = config.GetConnectionString("Redis") ?? "localhost";
            return ConnectionMultiplexer.Connect(redisConnectionString);
        });

        services.AddScoped(typeof(IRepository<>), typeof(RedisRepository<>));
    }
}