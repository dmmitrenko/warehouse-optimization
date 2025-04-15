using MediatR;
using WarehouseOptimizer.Contracts.Commands;

namespace WarehouseOptimizer.Application.Handlers;

public class UpdateWarehouseCellCommandHandler : IRequestHandler<UpdateWarehouseCellCommand>
{
    public Task Handle(UpdateWarehouseCellCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}