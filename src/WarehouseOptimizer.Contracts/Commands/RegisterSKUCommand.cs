using MediatR;

namespace WarehouseOptimizer.Contracts.Commands;

public class RegisterSkuCommand : IRequest
{
    public string SkuCode { get; set; }
    public decimal Weight { get; set; }
    public decimal Height { get; set; }
    public decimal Width { get; set; }
    public decimal Length { get; set; }
}