using InventoryWarehouseSystem.Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace InventoryWarehouseSystem.IntegrationTests;

public sealed class DatabaseFixture : IDisposable
{
    public ApplicationDbContext Context { get; }

    public DatabaseFixture()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase($"inventory-integration-{Guid.NewGuid()}")
            .Options;

        Context = new ApplicationDbContext(options);
    }

    public void Dispose() => Context.Dispose();
}
