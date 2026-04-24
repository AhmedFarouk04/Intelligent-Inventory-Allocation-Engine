using FluentAssertions;
using InventoryWarehouseSystem.Domain.Entities;
using InventoryWarehouseSystem.Infrastructure.Repositories;

namespace InventoryWarehouseSystem.IntegrationTests.Persistence;

public class ProductRepositoryTests : IClassFixture<DatabaseFixture>
{
    private readonly DatabaseFixture _fixture;

    public ProductRepositoryTests(DatabaseFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task AddAndGetBySku_Works()
    {
        var category = new Category("Electronics");
        _fixture.Context.Categories.Add(category);
        await _fixture.Context.SaveChangesAsync();

        var repo = new ProductRepository(_fixture.Context);
        var product = Product.Create("SKU-XYZ", "Keyboard", category.Id, 5);
        await repo.AddAsync(product);
        await _fixture.Context.SaveChangesAsync();

        var found = await repo.GetBySkuAsync("SKU-XYZ");
        found.Should().NotBeNull();
        found!.Name.Should().Be("Keyboard");
    }
}
