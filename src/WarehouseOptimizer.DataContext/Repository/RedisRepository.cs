using StackExchange.Redis;
using System.Text.Json;
using System.Linq.Expressions;
using WarehouseOptimizer.Infrastructure;

public class RedisRepository<T> : IRepository<T> where T : class
{
    private readonly IDatabase _database;
    private readonly string _prefix;

    public RedisRepository(IConnectionMultiplexer redis)
    {
        _database = redis.GetDatabase();
        _prefix = typeof(T).Name + ":";
    }

    private static Guid GetIdFromEntity(T entity)
    {
        var idProperty = typeof(T).GetProperty("Id");
        if (idProperty == null)
            throw new InvalidOperationException("Entity must have an Id property");

        var id = idProperty.GetValue(entity);
        return id is Guid guid ? guid : throw new InvalidOperationException("Id must be of type Guid");
    }

    public async Task<T?> GetByIdAsync(Guid id)
    {
        var redisKey = _prefix + id;
        var json = await _database.StringGetAsync(redisKey);
        return json.HasValue ? JsonSerializer.Deserialize<T>(json!) : null;
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        var server = _database.Multiplexer.GetServer(_database.Multiplexer.GetEndPoints().First());
        var keys = server.Keys(pattern: _prefix + "*").ToArray();

        var values = await _database.StringGetAsync(keys.Select(k => (RedisKey)k).ToArray());

        return values
            .Where(v => v.HasValue)
            .Select(v => JsonSerializer.Deserialize<T>(v!)!)
            .ToList();
    }

    public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
    {
        var all = await GetAllAsync();
        return all.AsQueryable().Where(predicate).ToList();
    }

    public async Task<T?> FindOneAsync(Expression<Func<T, bool>> predicate)
    {
        var all = await GetAllAsync();
        return all.AsQueryable().SingleOrDefault(predicate);
    }

    public async Task AddAsync(T entity)
    {
        var id = GetIdFromEntity(entity);
        var key = _prefix + id;
        var json = JsonSerializer.Serialize(entity);
        await _database.StringSetAsync(key, json);
    }

    public async Task UpdateAsync(T entity)
    {
        var id = GetIdFromEntity(entity);
        var key = _prefix + id;
        var json = JsonSerializer.Serialize(entity);
        await _database.StringSetAsync(key, json);
    }
}
