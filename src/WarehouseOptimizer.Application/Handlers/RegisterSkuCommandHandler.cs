using MediatR;
using WarehouseOptimizer.Application.Mapper;
using WarehouseOptimizer.Contracts.Commands;
using WarehouseOptimizer.Infrastructure;

namespace WarehouseOptimizer.Application.Handlers;

class RegisterSkuCommandHandler(IRepository<SkuRecord> repository) : IRequestHandler<RegisterSkuCommand>
{
    public async Task Handle(RegisterSkuCommand request, CancellationToken cancellationToken)
    {
        var skuRecord = request.Map();
        await repository.AddAsync(skuRecord);
    }
}