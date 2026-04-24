using FluentAssertions;
using InventoryWarehouseSystem.Domain.Entities;
using InventoryWarehouseSystem.Domain.Enums;

namespace InventoryWarehouseSystem.UnitTests.Domain;

public class StockMovementTests
{
    [Fact]
    public void Ctor_QuantityZero_ThrowsException()
    {
        var act = () => new StockMovement(1, 1, MovementTypeEnum.Out, 0, null, null, null);
        act.Should().Throw<ArgumentException>();
    }
}
