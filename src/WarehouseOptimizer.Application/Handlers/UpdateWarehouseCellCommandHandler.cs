using MediatR;
using WarehouseOptimizer.Contracts.Commands;
using WarehouseOptimizer.Domain.Models;
using WarehouseOptimizer.Infrastructure;

namespace WarehouseOptimizer.Application.Handlers;

public class UpdateWarehouseCellCommandHandler(IRepository<WarehouseCell> repository) : IRequestHandler<UpdateWarehouseCellCommand>
{
    public async Task Handle(UpdateWarehouseCellCommand request, CancellationToken cancellationToken)
    {
        var warehouseCell = await repository.FindOneAsync(x => x.CellCode == request.CellCode);
        if (warehouseCell is null)
        {
            throw new ArgumentException($"Cell with code: {request.CellCode} not found");
        }

        warehouseCell.VolumeCapacity = request.VolumeCapacity ?? warehouseCell.VolumeCapacity;
        warehouseCell.IsOutdated = request.IsOutdated ?? warehouseCell.IsOutdated;
        warehouseCell.MaxWeight = request.MaxWeight ?? warehouseCell.MaxWeight;
        warehouseCell.X = request.Position ?? warehouseCell.X;
        warehouseCell.Y = request.Aisle ?? warehouseCell.Y;
        warehouseCell.Z = request.Level ?? warehouseCell.Z;

        await repository.UpdateAsync(warehouseCell);
    }
}