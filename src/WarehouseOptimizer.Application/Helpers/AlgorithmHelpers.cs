using WarehouseOptimizer.Domain.Models;

namespace WarehouseOptimizer.Application.Helpers;

public static class AlgorithmHelpers
{
    public static List<PlacementResult> BuildResults(int[] individual, List<SkuRecord> skus, List<WarehouseCell> cells)
    {
        var res = new List<PlacementResult>();
        for (int i = 0; i < individual.Length; i++)
            res.Add(new PlacementResult(skus[i], cells[individual[i]]));
        return res;
    }

    public static double Evaluate(int[] individual, List<SkuRecord> skus, List<WarehouseCell> cells)
    {
        double score = 0;
        for (int i = 0; i < individual.Length; i++)
        {
            var cell = cells[individual[i]];
            score += skus[i].Weight * Math.Sqrt(cell.X * cell.X + cell.Y * cell.Y + cell.Z * cell.Z);
        }
        return score;
    }
}