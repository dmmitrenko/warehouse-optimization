using MediatR;
using WarehouseOptimizer.Contracts.Commands;

namespace WarehouseOptimizer.Application.Handlers;

class RegisterWarehouseCellCommandHandler : IRequestHandler<RegisterWarehouseCellCommand>
{
    public Task Handle(RegisterWarehouseCellCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}