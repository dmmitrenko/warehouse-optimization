namespace WarehouseOptimizer.Contracts.Commands;

using MediatR;

/// <summary>
/// Command to update an existing SKU record.
/// </summary>
public class UpdateSkuCommand : IRequest
{
    /// <summary>
    /// Gets or sets the Stock Keeping Unit (SKU) code.
    /// </summary>
    public string SkuCode { get; set; } = null!;

    /// <summary>
    /// Gets or sets the weight of the product in kilograms.
    /// </summary>
    public decimal? Weight { get; set; }

    /// <summary>
    /// Gets or sets the length of the product in meters.
    /// </summary>
    public decimal? Length { get; set; }

    /// <summary>
    /// Gets or sets the width of the product in meters.
    /// </summary>
    public decimal? Width { get; set; }

    /// <summary>
    /// Gets or sets the height of the product in meters.
    /// </summary>
    public decimal? Height { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the SKU record is outdated.
    /// </summary>
    public bool? IsOutdated { get; set; }
}