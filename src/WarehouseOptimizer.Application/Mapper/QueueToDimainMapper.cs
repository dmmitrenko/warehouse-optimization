using WarehouseOptimizer.Contracts.Commands;
using WarehouseOptimizer.Domain.Models;

namespace WarehouseOptimizer.Application.Mapper;
public static class QueueToDimainMapper
{
    public static WarehouseCell Map(this RegisterWarehouseCellCommand command)
    {
        var warehouseCell = new WarehouseCell
        {
            Id = Guid.NewGuid(),
            CellCode = command.CellCode,
            VolumeCapacity = command.VolumeCapacity,
            IsOutdated = false,
            MaxWeight = command.MaxWeight,
            X = command.Position,
            Y = command.Aisle,
            Z = command.Level,
        };

        return warehouseCell;
    }

    public static SkuRecord Map(this RegisterSkuCommand command)
    {
        var skuRecord = new SkuRecord
        {
            Id = Guid.NewGuid(),
            Weight = command.Weight,
            Width = command.Width,
            SKU = command.SkuCode,
            CreationTimestamp = DateTime.Now,
            Height = command.Height,
            IsOutdated = false,
            Length = command.Length,
        };

        return skuRecord;
    }
}