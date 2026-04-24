using FluentAssertions;
using InventoryWarehouseSystem.Domain.Entities;

namespace InventoryWarehouseSystem.UnitTests.Domain;

public class WarehouseTests
{
    [Fact]
    public void Ctor_ValidInput_CreatesWarehouse()
    {
        var warehouse = new Warehouse("Main", 1000, "Cairo");
        warehouse.Name.Should().Be("Main");
        warehouse.Capacity.Should().Be(1000);
    }
}
