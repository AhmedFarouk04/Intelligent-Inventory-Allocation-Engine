using FluentAssertions;
using InventoryWarehouseSystem.Domain.Entities;
using InventoryWarehouseSystem.Domain.Events;

namespace InventoryWarehouseSystem.UnitTests.Domain;

public class StockTests
{
    [Fact]
    public void RaiseLowStockAlertIfNeeded_WhenBelowOrEqual_ReorderLevel_RaisesEvent()
    {
        var stock = new Stock(productId: 1, warehouseId: 1, currentQuantity: 5);

        stock.RaiseLowStockAlertIfNeeded(reorderLevel: 5);

        stock.DomainEvents.Should().ContainSingle(e => e is LowStockAlertEvent);
    }
}

