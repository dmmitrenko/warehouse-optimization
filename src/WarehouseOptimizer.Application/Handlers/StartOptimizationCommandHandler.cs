using MediatR;
using WarehouseOptimizer.Contracts.Commands;
using WarehouseOptimizer.Infrastructure;

namespace WarehouseOptimizer.Application.Handlers;

class StartOptimizationCommandHandler : IRequestHandler<CalculatePlacementCommand>
{
    public Task Handle(CalculatePlacementCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}