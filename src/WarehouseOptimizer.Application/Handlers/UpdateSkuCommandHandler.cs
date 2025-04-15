using MediatR;
using WarehouseOptimizer.Contracts.Commands;

namespace WarehouseOptimizer.Application.Handlers;

public class UpdateSkuCommandHandler : IRequestHandler<UpdateSkuCommand>
{
    public Task Handle(UpdateSkuCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}