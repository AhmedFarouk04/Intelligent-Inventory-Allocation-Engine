namespace InventoryWarehouseSystem.Domain.Events;

public sealed class StockMovedEvent : DomainEvent
{
    public int ProductId { get; init; }
    public int WarehouseId { get; init; }
    public int Quantity { get; init; }
    public string MovementType { get; init; } = string.Empty;
}
