using WarehouseOptimizer.Application.Helpers;
using WarehouseOptimizer.Domain.Models;
using WarehouseOptimizer.Infrastructure;

namespace WarehouseOptimizer.Application.Services;

public class GeneticPlacementAlgorithm : IPlacementAlgorithm
{
    public Algorithm Algorithm => Algorithm.Genetic;

    private static readonly Random _rand = new();
    private const int PopSize = 100;
    private const int Generations = 200;
    private const double CrossoverRate = 0.8;
    private const double MutationRate = 0.2;
    private const int TournamentSize = 5;

    public List<PlacementResult> Optimize(List<SkuRecord> skus, List<WarehouseCell> cells)
    {
        int N = skus.Count;
        int M = cells.Count;
        var baseIndices = Enumerable.Range(0, M).ToList();

        var population = new List<int[]>();
        for (int i = 0; i < PopSize; i++)
        {
            var individual = baseIndices.OrderBy(_ => _rand.Next()).Take(N).ToArray();
            population.Add(individual);
        }

        int[] best = null;
        double bestScore = double.MaxValue;

        for (int gen = 0; gen < Generations; gen++)
        {
            var scores = population.Select(ind => AlgorithmHelpers.Evaluate(ind, skus, cells)).ToArray();
            for (int i = 0; i < PopSize; i++)
            {
                if (scores[i] < bestScore)
                {
                    bestScore = scores[i];
                    best = (int[])population[i].Clone();
                }
            }

            var newPop = new List<int[]>();
            while (newPop.Count < PopSize)
            {
                var p1 = TournamentSelect(population, scores);
                var p2 = TournamentSelect(population, scores);
                int[] child = _rand.NextDouble() < CrossoverRate
                    ? OrderCrossover(p1, p2)
                    : (int[])p1.Clone();

                if (_rand.NextDouble() < MutationRate)
                    SwapMutation(child);

                newPop.Add(child);
            }
            population = newPop;
        }

        return AlgorithmHelpers.BuildResults(best, skus, cells);
    }

    private int[] TournamentSelect(List<int[]> pop, double[] scores)
    {
        var idx = Enumerable.Range(0, PopSize).OrderBy(_ => _rand.Next()).Take(TournamentSize);
        return idx.Select(i => (i, scores[i])).OrderBy(t => t.Item2).First().i == idx.First()
            ? pop[idx.First()]
            : pop[idx.First()];
    }

    private int[] OrderCrossover(int[] p1, int[] p2)
    {
        int len = p1.Length;
        int a = _rand.Next(len), b = _rand.Next(len);
        if (a > b) (a, b) = (b, a);
        var child = new int[len];
        Array.Fill(child, -1);
        for (int i = a; i <= b; i++) child[i] = p1[i];
        int idx = (b + 1) % len;
        for (int i = 0; i < len; i++)
        {
            int gene = p2[(b + 1 + i) % len];
            if (!child.Contains(gene))
            {
                child[idx] = gene;
                idx = (idx + 1) % len;
            }
        }
        return child;
    }

    private void SwapMutation(int[] ind)
    {
        int a = _rand.Next(ind.Length), b = _rand.Next(ind.Length);
        (ind[a], ind[b]) = (ind[b], ind[a]);
    }
}