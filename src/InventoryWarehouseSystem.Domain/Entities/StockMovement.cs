using InventoryWarehouseSystem.Domain.Enums;
using InventoryWarehouseSystem.SharedKernel.Base;

namespace InventoryWarehouseSystem.Domain.Entities;

public class StockMovement : Entity
{
    public int ProductId { get; private set; }
    public int WarehouseId { get; private set; }
    public MovementTypeEnum MovementType { get; private set; }
    public int Quantity { get; private set; }
    public string? Notes { get; private set; }
    public string? ReferenceNumber { get; private set; }
    public string? MovedBy { get; private set; }
    public DateTime MovedAt { get; private set; }

    public Product Product { get; set; } = null!;
    public Warehouse Warehouse { get; set; } = null!;

    protected StockMovement()
    {
    }

    public StockMovement(int productId, int warehouseId, MovementTypeEnum movementType, int quantity, string? notes, string? referenceNumber, string? movedBy)
    {
        if (quantity <= 0)
        {
            throw new ArgumentException("Quantity must be greater than zero.");
        }

        ProductId = productId;
        WarehouseId = warehouseId;
        MovementType = movementType;
        Quantity = quantity;
        Notes = notes;
        ReferenceNumber = referenceNumber;
        MovedBy = movedBy;
        MovedAt = DateTime.UtcNow;
    }
}
