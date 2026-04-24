namespace InventoryWarehouseSystem.Domain.Events;

public sealed class ProductCreatedEvent : DomainEvent
{
    public int ProductId { get; init; }
    public string Sku { get; init; } = string.Empty;
}
