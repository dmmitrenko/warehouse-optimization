using MediatR;
using WarehouseOptimizer.Contracts.Commands;
using WarehouseOptimizer.Infrastructure;

namespace WarehouseOptimizer.Application.Handlers;

public class UpdateSkuCommandHandler(IRepository<SkuRecord> repository) : IRequestHandler<UpdateSkuCommand>
{
    public async Task Handle(UpdateSkuCommand request, CancellationToken cancellationToken)
    {
        var skuRecordToUpdate = await repository.FindOneAsync(x => x.SKU == request.SkuCode);
        if (skuRecordToUpdate is null)
        {
            throw new ArgumentException($"Sku with code: {request.SkuCode} not found");
            
        }

        skuRecordToUpdate.IsOutdated = request.IsOutdated ?? skuRecordToUpdate.IsOutdated;
        skuRecordToUpdate.Weight = request.Weight ?? skuRecordToUpdate.Weight;
        skuRecordToUpdate.Height = request.Height ?? skuRecordToUpdate.Height;
        skuRecordToUpdate.Length = request.Length ?? skuRecordToUpdate.Length;
        skuRecordToUpdate.Width = request.Width ?? skuRecordToUpdate.Width;

        await repository.UpdateAsync(skuRecordToUpdate);
    }
}