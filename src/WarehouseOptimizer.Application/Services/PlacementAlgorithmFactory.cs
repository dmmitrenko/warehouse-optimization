using WarehouseOptimizer.Domain.Models;
using WarehouseOptimizer.Infrastructure;

namespace WarehouseOptimizer.Application.Services;

public class PlacementAlgorithmFactory : IPlacementAlgorithmFactory
{
    private readonly IServiceProvider _serviceProvider;

    public PlacementAlgorithmFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public IPlacementAlgorithm Create(Algorithm algorithmType)
    {
        return algorithmType switch
        {
            Algorithm.Greedy => (IPlacementAlgorithm)_serviceProvider.GetService(typeof(GreedyPlacementAlgorithm)),
            Algorithm.Genetic => (IPlacementAlgorithm)_serviceProvider.GetService(typeof(GeneticPlacementAlgorithm)),
            Algorithm.ParticleSwarm => (IPlacementAlgorithm)_serviceProvider.GetService(typeof(ParticleSwarmPlacementAlgorithm)),
            Algorithm.SimulatedAnnealing => (IPlacementAlgorithm)_serviceProvider.GetService(typeof(SimulatedAnnealingPlacementAlgorithm)),
            _ => throw new ArgumentException($"Unknown algorithm type: {algorithmType}")
        };
    }
}
