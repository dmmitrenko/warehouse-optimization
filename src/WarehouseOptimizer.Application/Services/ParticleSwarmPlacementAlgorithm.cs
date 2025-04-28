using WarehouseOptimizer.Application.Helpers;
using WarehouseOptimizer.Domain.Models;
using WarehouseOptimizer.Infrastructure;

namespace WarehouseOptimizer.Application.Services;

public class ParticleSwarmPlacementAlgorithm : IPlacementAlgorithm
{
    public Algorithm Algorithm => Algorithm.ParticleSwarm;

    private static readonly Random _rand = new();
    private const int SwarmSize = 100;
    private const int Iterations = 200;
    private const double C1 = 1.0;
    private const double C2 = 1.0;
    private const double MutationRate = 0.1;

    public List<PlacementResult> Optimize(List<SkuRecord> skus, List<WarehouseCell> cells)
    {
        int N = skus.Count;
        int M = cells.Count;
        var baseIdx = Enumerable.Range(0, M).ToList();

        var swarm = new List<int[]>();
        for (int i = 0; i < SwarmSize; i++)
            swarm.Add(baseIdx.OrderBy(_ => _rand.Next()).Take(N).ToArray());

        var pBest = swarm.Select(p => (p: (int[])p.Clone(), score: Evaluate(p, skus, cells))).ToList();
        var gBest = pBest.OrderBy(x => x.score).First();

        for (int iter = 0; iter < Iterations; iter++)
        {
            for (int i = 0; i < SwarmSize; i++)
            {
                var particle = swarm[i];
                var newParticle = ApplySwaps(particle, GetSwaps(particle, pBest[i].p), C1);
                newParticle = ApplySwaps(newParticle, GetSwaps(newParticle, gBest.p), C2);
                if (_rand.NextDouble() < MutationRate)
                    SwapMutation(newParticle);

                double score = Evaluate(newParticle, skus, cells);
                if (score < pBest[i].score)
                    pBest[i] = (newParticle, score);
                if (score < gBest.score)
                    gBest = (newParticle, score);

                swarm[i] = newParticle;
            }
        }

        return AlgorithmHelpers.BuildResults(gBest.p, skus, cells);
    }

    private double Evaluate(int[] ind, List<SkuRecord> skus, List<WarehouseCell> cells) => AlgorithmHelpers.Evaluate(ind, skus, cells);

    private List<(int, int)> GetSwaps(int[] current, int[] target)
    {
        var seq = new List<(int, int)>();
        var curr = current.ToArray();
        var pos = curr.Select((v, i) => (v, i)).ToDictionary(x => x.v, x => x.i);
        for (int i = 0; i < curr.Length; i++)
        {
            if (curr[i] != target[i])
            {
                int j = pos[target[i]];
                seq.Add((i, j));
                (curr[i], curr[j]) = (curr[j], curr[i]);
                pos[curr[i]] = i;
                pos[curr[j]] = j;
            }
        }
        return seq;
    }

    private int[] ApplySwaps(int[] particle, List<(int i, int j)> swaps, double coeff)
    {
        var copy = particle.ToArray();
        foreach (var (i, j) in swaps)
        {
            if (_rand.NextDouble() < coeff)
                (copy[i], copy[j]) = (copy[j], copy[i]);
        }
        return copy;
    }

    private void SwapMutation(int[] ind)
    {
        int a = _rand.Next(ind.Length), b = _rand.Next(ind.Length);
        (ind[a], ind[b]) = (ind[b], ind[a]);
    }
}
