using MediatR;

namespace WarehouseOptimizer.Contracts.Commands;

public class CalculatePlacementCommand : IRequest
{
    public string AlgotithmType { get; set; }
}