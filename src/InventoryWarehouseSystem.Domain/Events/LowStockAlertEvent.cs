namespace InventoryWarehouseSystem.Domain.Events;

public sealed class LowStockAlertEvent : DomainEvent
{
    public int ProductId { get; init; }
    public int WarehouseId { get; init; }
    public int CurrentQuantity { get; init; }
    public int ReorderLevel { get; init; }
}
