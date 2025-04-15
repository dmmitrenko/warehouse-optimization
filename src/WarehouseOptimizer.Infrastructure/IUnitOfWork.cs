using WarehouseOptimizer.Domain.Models;

namespace WarehouseOptimizer.Infrastructure;

public interface IUnitOfWork
{
    IRepository<WarehouseCell> WarehouseCellRepository { get; }
    IRepository<SkuRecord> SkuRepository { get; }

    Task SaveChangesAsync(CancellationToken stoppingToken = default);
}