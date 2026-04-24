using InventoryWarehouseSystem.Domain.Events;
using InventoryWarehouseSystem.SharedKernel.Events;
using Microsoft.Extensions.Logging;

namespace InventoryWarehouseSystem.Infrastructure.Messaging.EventHandlers;

public class StockMovedEventHandler : IDomainEventHandler<StockMovedEvent>
{
    private readonly ILogger<StockMovedEventHandler> _logger;

    public StockMovedEventHandler(ILogger<StockMovedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(StockMovedEvent domainEvent, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation(
            "Stock moved: Product {ProductId}, Warehouse {WarehouseId}, Qty {Quantity}, Type {MovementType}",
            domainEvent.ProductId,
            domainEvent.WarehouseId,
            domainEvent.Quantity,
            domainEvent.MovementType);
        return Task.CompletedTask;
    }
}
