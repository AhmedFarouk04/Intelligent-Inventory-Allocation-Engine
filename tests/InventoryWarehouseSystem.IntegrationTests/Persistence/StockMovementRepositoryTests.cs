using FluentAssertions;
using InventoryWarehouseSystem.Domain.Entities;
using InventoryWarehouseSystem.Domain.Enums;
using InventoryWarehouseSystem.Infrastructure.Repositories;

namespace InventoryWarehouseSystem.IntegrationTests.Persistence;

public class StockMovementRepositoryTests : IClassFixture<DatabaseFixture>
{
    private readonly DatabaseFixture _fixture;

    public StockMovementRepositoryTests(DatabaseFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task GetHistoryAsync_ReturnsMovements()
    {
        var category = new Category("Office");
        var warehouse = new Warehouse("Main", 1000, "Cairo");
        _fixture.Context.Categories.Add(category);
        _fixture.Context.Warehouses.Add(warehouse);
        await _fixture.Context.SaveChangesAsync();

        var product = Product.Create("SKU-H1", "Chair", category.Id, 10);
        _fixture.Context.Products.Add(product);
        await _fixture.Context.SaveChangesAsync();

        _fixture.Context.StockMovements.Add(new StockMovement(product.Id, warehouse.Id, MovementTypeEnum.In, 10, null, null, "tester"));
        await _fixture.Context.SaveChangesAsync();

        var repo = new StockMovementRepository(_fixture.Context);
        var items = await repo.GetHistoryAsync(product.Id, warehouse.Id, null, null);

        items.Should().HaveCountGreaterThan(0);
    }
}
