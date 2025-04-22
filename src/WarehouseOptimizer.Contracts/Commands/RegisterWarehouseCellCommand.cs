using MediatR;

namespace WarehouseOptimizer.Contracts.Commands;

/// <summary>
/// Command to register a new warehouse cell.
/// </summary>
public class RegisterWarehouseCellCommand : IRequest
{
    /// <summary>
    /// Gets or sets the unique code of the warehouse cell (e.g., "Z1-A1-3-2-1").
    /// </summary>
    public string CellCode { get; set; } = null!;

    /// <summary>
    /// Gets or sets the maximum weight capacity of the cell in kilograms.
    /// </summary>
    public decimal MaxWeight { get; set; }

    /// <summary>
    /// Gets or sets the maximum volume capacity of the cell in cubic meters.
    /// </summary>
    public decimal VolumeCapacity { get; set; }

    /// <summary>
    /// Gets or sets the horizontal position of the cell (e.g., slot index in a row).
    /// </summary>
    public int Position { get; set; }

    /// <summary>
    /// Gets or sets the aisle index of the cell (e.g., vertical corridor).
    /// </summary>
    public int Aisle { get; set; }

    /// <summary>
    /// Gets or sets the vertical level of the cell.
    /// </summary>
    public int Level { get; set; }
}