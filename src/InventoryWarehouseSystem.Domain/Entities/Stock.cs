using InventoryWarehouseSystem.Domain.Events;
using InventoryWarehouseSystem.SharedKernel.Base;

namespace InventoryWarehouseSystem.Domain.Entities;

public class Stock : AggregateRoot
{
    public int ProductId { get; private set; }
    public int WarehouseId { get; private set; }
    public int CurrentQuantity { get; private set; }
    public int ReservedQuantity { get; private set; }
    public DateTime? LastMovedAt { get; private set; }

    public Product Product { get; set; } = null!;
    public Warehouse Warehouse { get; set; } = null!;

    protected Stock()
    {
    }

    public Stock(int productId, int warehouseId, int currentQuantity = 0)
    {
        if (currentQuantity < 0)
        {
            throw new ArgumentException("Current quantity cannot be negative.");
        }

        ProductId = productId;
        WarehouseId = warehouseId;
        CurrentQuantity = currentQuantity;
        ReservedQuantity = 0;
    }

    public void Allocate(int quantity)
    {
        if (quantity <= 0)
        {
            throw new ArgumentException("Allocation quantity must be greater than zero.");
        }

        if (CurrentQuantity < quantity)
        {
            throw new InvalidOperationException("Insufficient stock to allocate.");
        }

        CurrentQuantity -= quantity;
        ReservedQuantity += quantity;
        LastMovedAt = DateTime.UtcNow;

        RaiseDomainEvent(new StockMovedEvent
        {
            ProductId = ProductId,
            WarehouseId = WarehouseId,
            Quantity = quantity,
            MovementType = "Allocate"
        });
    }

    public void Add(int quantity)
    {
        if (quantity <= 0)
        {
            throw new ArgumentException("Quantity must be greater than zero.");
        }

        CurrentQuantity += quantity;
        LastMovedAt = DateTime.UtcNow;
    }

    public void RaiseLowStockAlertIfNeeded(int reorderLevel)
    {
        if (reorderLevel < 0)
        {
            throw new ArgumentException("Reorder level cannot be negative.");
        }

        if (CurrentQuantity <= reorderLevel)
        {
            RaiseDomainEvent(new LowStockAlertEvent
            {
                ProductId = ProductId,
                WarehouseId = WarehouseId,
                CurrentQuantity = CurrentQuantity,
                ReorderLevel = reorderLevel
            });
        }
    }
}
