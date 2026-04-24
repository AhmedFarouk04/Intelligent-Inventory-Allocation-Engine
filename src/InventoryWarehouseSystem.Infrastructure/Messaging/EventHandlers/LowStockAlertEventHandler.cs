using InventoryWarehouseSystem.Domain.Events;
using InventoryWarehouseSystem.SharedKernel.Events;
using Microsoft.Extensions.Logging;

namespace InventoryWarehouseSystem.Infrastructure.Messaging.EventHandlers;

public class LowStockAlertEventHandler : IDomainEventHandler<LowStockAlertEvent>
{
    private readonly ILogger<LowStockAlertEventHandler> _logger;

    public LowStockAlertEventHandler(ILogger<LowStockAlertEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(LowStockAlertEvent domainEvent, CancellationToken cancellationToken = default)
    {
        _logger.LogWarning(
            "Low stock alert: Product {ProductId} in Warehouse {WarehouseId}. Current={CurrentQuantity}, Reorder={ReorderLevel}",
            domainEvent.ProductId,
            domainEvent.WarehouseId,
            domainEvent.CurrentQuantity,
            domainEvent.ReorderLevel);
        return Task.CompletedTask;
    }
}
