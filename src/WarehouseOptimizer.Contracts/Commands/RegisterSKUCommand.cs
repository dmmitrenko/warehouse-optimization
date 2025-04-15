namespace WarehouseOptimizer.Contracts.Commands;

public class RegisterSKUCommand
{
    public string SkuCode { get; set; }
    public decimal Weight { get; set; }
    public decimal Height { get; set; }
    public decimal Width { get; set; }
    public decimal Length { get; set; }
}