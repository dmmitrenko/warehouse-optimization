namespace WarehouseOptimizer.Domain.Models;

/// <summary>
/// Represents a record for a specific Stock Keeping Unit (SKU) in the system.
/// </summary>
public class SKURecord
{
    /// <summary>
    /// Gets or sets the unique identifier for the SKU record.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the Stock Keeping Unit (SKU) code for the product.
    /// This is the unique code used to identify the product.
    /// </summary>
    public string SKU { get; set; }

    /// <summary>
    /// Gets or sets the weight of the product in kilograms.
    /// </summary>
    public decimal Weight { get; set; }

    /// <summary>
    /// Gets or sets the volume of the product in cubic meters.
    /// </summary>
    public decimal Volume { get; set; }

    /// <summary>
    /// Gets or sets the timestamp indicating when the SKU record was created.
    /// </summary>
    public DateTime CreationTimestamp { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the SKU record is outdated.
    /// If set to true, the SKU is no longer considered valid.
    /// </summary>
    public bool IsOutdated { get; set; }
}
