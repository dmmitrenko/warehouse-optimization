using MediatR;
using WarehouseOptimizer.Application.Mapper;
using WarehouseOptimizer.Contracts.Commands;
using WarehouseOptimizer.Domain.Models;
using WarehouseOptimizer.Infrastructure;

namespace WarehouseOptimizer.Application.Handlers;

class RegisterWarehouseCellCommandHandler(IRepository<WarehouseCell> repository) : IRequestHandler<RegisterWarehouseCellCommand>
{
    public async Task Handle(RegisterWarehouseCellCommand request, CancellationToken cancellationToken)
    {
        var warehouseCell = request.Map();
        await repository.AddAsync(warehouseCell);
    }
}