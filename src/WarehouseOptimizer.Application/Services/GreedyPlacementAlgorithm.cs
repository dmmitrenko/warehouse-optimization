using WarehouseOptimizer.Domain.Models;
using WarehouseOptimizer.Infrastructure;

namespace WarehouseOptimizer.Application.Services;

public class GreedyPlacementAlgorithm : IPlacementAlgorithm
{
    public Algorithm Algorithm => Algorithm.Greedy;

    public List<PlacementResult> Optimize(List<SkuRecord> skus, List<WarehouseCell> cells)
    {
        var sortedSkus = skus.OrderByDescending(s => s.Weight).ToList();
        var remainingCapacity = cells.ToDictionary(c => c, c => (Weight: c.MaxWeight, Volume: c.VolumeCapacity));
        var results = new List<PlacementResult>();

        foreach (var sku in sortedSkus)
        {
            var cell = remainingCapacity
                .Where(kv => kv.Value.Weight >= sku.Weight && kv.Value.Volume >= sku.Volume)
                .OrderBy(kv => DistanceToOrigin(kv.Key))
                .Select(kv => kv.Key)
                .FirstOrDefault();

            if (cell != null)
            {
                results.Add(new PlacementResult(sku, cell));
                remainingCapacity[cell] = (
                    remainingCapacity[cell].Weight - sku.Weight,
                    remainingCapacity[cell].Volume - sku.Volume);
            }
        }

        return results;
    }

    private double DistanceToOrigin(WarehouseCell cell)
    {
        return Math.Sqrt(cell.X * cell.X + cell.Y * cell.Y + cell.Z * cell.Z);
    }
}