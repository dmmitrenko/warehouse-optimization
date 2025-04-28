using WarehouseOptimizer.Application.Helpers;
using WarehouseOptimizer.Domain.Models;
using WarehouseOptimizer.Infrastructure;

namespace WarehouseOptimizer.Application.Services;

public class SimulatedAnnealingPlacementAlgorithm : IPlacementAlgorithm
{
    public Algorithm Algorithm => Algorithm.SimulatedAnnealing;

    private static readonly Random _rand = new();
    private const double T0 = 1000.0;
    private const double TMin = 1e-3;
    private const double CoolingRate = 0.995;
    private const int IterPerTemp = 50;

    public List<PlacementResult> Optimize(List<SkuRecord> skus, List<WarehouseCell> cells)
    {
        int N = skus.Count;
        int M = cells.Count;
        var baseIdx = Enumerable.Range(0, M).ToList();

        var current = baseIdx.OrderBy(_ => _rand.Next()).Take(N).ToArray();
        double currentScore = AlgorithmHelpers.Evaluate(current, skus, cells);
        var best = current.ToArray();
        double bestScore = currentScore;
        double temp = T0;

        while (temp > TMin)
        {
            for (int it = 0; it < IterPerTemp; it++)
            {
                var neighbor = GetNeighbor(current);
                double score = AlgorithmHelpers.Evaluate(neighbor, skus, cells);
                double delta = score - currentScore;
                if (delta < 0 || _rand.NextDouble() < Math.Exp(-delta / temp))
                {
                    current = neighbor;
                    currentScore = score;
                    if (score < bestScore)
                    {
                        best = neighbor;
                        bestScore = score;
                    }
                }
            }
            temp *= CoolingRate;
        }

        return AlgorithmHelpers.BuildResults(best, skus, cells);
    }

    private int[] GetNeighbor(int[] sol)
    {
        var nb = sol.ToArray();
        if (_rand.NextDouble() < 0.5)
        {
            int a = _rand.Next(nb.Length), b = _rand.Next(nb.Length);
            (nb[a], nb[b]) = (nb[b], nb[a]);
        }
        else
        {
            for (int k = 0; k < 2; k++)
            {
                int a = _rand.Next(nb.Length), b = _rand.Next(nb.Length);
                (nb[a], nb[b]) = (nb[b], nb[a]);
            }
        }
        return nb;
    }
}
