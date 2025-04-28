using WarehouseOptimizer.Domain.Models;

namespace WarehouseOptimizer.Infrastructure;

public interface IPlacementAlgorithmFactory
{
    IPlacementAlgorithm Create(Algorithm algorithmType);
}