using CsvHelper;
using CsvHelper.Configuration;
using MediatR;
using WarehouseOptimizer.Contracts.Commands;
using WarehouseOptimizer.Domain.Models;
using WarehouseOptimizer.Infrastructure;

namespace WarehouseOptimizer.Application.Handlers;

class StartOptimizationCommandHandler : IRequestHandler<CalculatePlacementCommand>
{
    private readonly IRepository<WarehouseCell> _cellRepository;
    private readonly IPlacementAlgorithmFactory _factory;
    private readonly IRepository<SkuRecord> _skuRepository;

    public StartOptimizationCommandHandler(
        IRepository<SkuRecord> skuRepository, 
        IRepository<WarehouseCell> cellRepository,
        IPlacementAlgorithmFactory factory)
    {
        _skuRepository = skuRepository;
        _cellRepository = cellRepository;
        _factory = factory;
    }

    public async Task Handle(CalculatePlacementCommand request, CancellationToken cancellationToken)
    {
        if (!Enum.TryParse<Algorithm>(request.AlgotithmType, out var algorithmType))
        {
            throw new ArgumentException("Failed to parse algorithm type", nameof(request.AlgotithmType));
        }

        var skus = (await _skuRepository.GetAllAsync()).Where(s => !s.IsOutdated).ToList();
        var cells = (await _cellRepository.GetAllAsync()).Where(c => !c.IsOutdated).ToList();

        var algorithm = _factory.Create(algorithmType);

        var placement = algorithm.Optimize(skus, cells);

        using var writer = new StreamWriter("placement_results.csv");
        using var csv = new CsvWriter(writer, new CsvConfiguration(System.Globalization.CultureInfo.InvariantCulture));
        csv.WriteField("SkuId");
        csv.WriteField("CellId");
        csv.NextRecord();

        foreach (var assignment in placement)
        {
            csv.WriteField(assignment.Sku.Id);
            csv.WriteField(assignment.Cell.Id);
            csv.NextRecord();
        }
    }
}