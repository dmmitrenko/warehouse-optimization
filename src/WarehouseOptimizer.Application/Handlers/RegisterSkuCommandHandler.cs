using MediatR;
using WarehouseOptimizer.Contracts.Commands;
using WarehouseOptimizer.Infrastructure;

namespace WarehouseOptimizer.Application.Handlers;

class RegisterSkuCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<RegisterSkuCommand>
{
    public async Task Handle(RegisterSkuCommand request, CancellationToken cancellationToken)
    {
        var skuRecord = new SkuRecord()
        {
            CreationTimestamp = DateTime.UtcNow,
            SKU = request.SkuCode,
            Weight = request.Weight,
            Width = request.Width,
            Height = request.Height,
            Length = request.Length,
        };

        await unitOfWork.SkuRepository.AddAsync(skuRecord);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}