using WarehouseOptimizer.Domain.Models;

namespace WarehouseOptimizer.Infrastructure;

public interface IPlacementAlgorithm
{
    public Algorithm Algorithm {get;}
    List<PlacementResult> Optimize(List<SkuRecord> skus, List<WarehouseCell> cells);
}