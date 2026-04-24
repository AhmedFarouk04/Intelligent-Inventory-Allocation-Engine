using FluentAssertions;
using InventoryWarehouseSystem.Domain.Entities;

namespace InventoryWarehouseSystem.UnitTests.Domain;

public class ProductTests
{
    [Fact]
    public void Create_ValidInput_ReturnsProduct()
    {
        var product = Product.Create("SKU-001", "Test Product", 1, 10);

        product.Should().NotBeNull();
        product.Sku.Value.Should().Be("SKU-001");
        product.Name.Should().Be("Test Product");
    }

    [Fact]
    public void Create_EmptySku_ThrowsException()
    {
        var action = () => Product.Create(string.Empty, "Name", 1, 10);
        action.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void UpdateReorderLevel_ValidInput_UpdatesLevel()
    {
        var product = Product.Create("SKU-001", "Name", 1, 10);
        product.UpdateReorderLevel(20);
        product.ReorderLevel.Should().Be(20);
    }
}
