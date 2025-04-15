namespace WarehouseOptimizer.Domain.Models;

public class WarehouseCell
{
    /// <summary>
    /// Gets or sets the unique identifier for the warehouse cell record.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the unique code of the warehouse cell (e.g., "Z1-A1-3-2-1").
    /// The code typically contains information about the zone, aisle, depth, and level.
    /// </summary>
    public string CellCode { get; set; }

    /// <summary>
    /// Gets or sets the maximum weight capacity of the cell in kilograms.
    /// </summary>
    public decimal MaxWeight { get; set; }

    /// <summary>
    /// Gets or sets the maximum volume capacity of the cell in cubic meters.
    /// </summary>
    public decimal VolumeCapacity { get; set; }

    /// <summary>
    /// Gets or sets the X coordinate (e.g., depth or position) of the cell.
    /// </summary>
    public int X { get; set; }

    /// <summary>
    /// Gets or sets the Y coordinate (e.g., aisle number) of the cell.
    /// </summary>
    public int Y { get; set; }

    /// <summary>
    /// Gets or sets the Z coordinate (e.g., level or height) of the cell.
    /// </summary>
    public int Z { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the warehouse cell is outdated.
    /// Outdated cells are not considered for new placements.
    /// </summary>
    public bool IsOutdated { get; set; }
}