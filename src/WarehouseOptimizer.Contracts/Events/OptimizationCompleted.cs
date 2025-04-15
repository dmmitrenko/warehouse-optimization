using WarehouseOptimizer.Contracts.Models;

namespace WarehouseOptimizer.Contracts.Events;

class OptimizationCompleted
{
    public string FileName { get; set; }
    public DateTime CompletedAt { get; set; }
    public Algorithms AlgorithmType { get; set; }
}