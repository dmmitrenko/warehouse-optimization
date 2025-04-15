using WarehouseOptimizer.Domain.Models;
using WarehouseOptimizer.Infrastructure;

namespace WarehouseOptimizer.DataContext.Repository.Repository;

public class UnitOfWork(ApplicationDbContext dbContext) : IUnitOfWork
{
    public IRepository<WarehouseCell> WarehouseCellRepository => new Repository<WarehouseCell>(dbContext);

    public IRepository<SkuRecord> SkuRepository => new Repository<SkuRecord>(dbContext);

    public async Task SaveChangesAsync(CancellationToken stoppingToken = default)
    {
        await dbContext.SaveChangesAsync(stoppingToken);
    }
}