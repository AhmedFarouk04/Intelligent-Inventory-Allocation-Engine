using InventoryWarehouseSystem.Domain.Entities;
using InventoryWarehouseSystem.Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace InventoryWarehouseSystem.Infrastructure.Seeders;

public static class InitialDataSeeder
{
    public static async Task SeedAsync(ApplicationDbContext context, CancellationToken cancellationToken = default)
    {
        await context.Database.MigrateAsync(cancellationToken);

        if (!await context.Categories.AnyAsync(cancellationToken))
        {
            context.Categories.AddRange(new Category("Electronics"), new Category("Office"));
            await context.SaveChangesAsync(cancellationToken);
        }

        if (!await context.Warehouses.AnyAsync(cancellationToken))
        {
            context.Warehouses.AddRange(
                new Warehouse("Main Warehouse", 10000, "Cairo"),
                new Warehouse("Secondary Warehouse", 5000, "Alexandria"));
            await context.SaveChangesAsync(cancellationToken);
        }

        if (!await context.Products.AnyAsync(cancellationToken))
        {
            var categoryId = await context.Categories.Select(x => x.Id).FirstAsync(cancellationToken);
            var p1 = Product.Create("SKU-001", "Wireless Mouse", categoryId, 20);
            var p2 = Product.Create("SKU-002", "Mechanical Keyboard", categoryId, 15);
            context.Products.AddRange(p1, p2);
            await context.SaveChangesAsync(cancellationToken);

            var warehouseId = await context.Warehouses.Select(x => x.Id).FirstAsync(cancellationToken);
            context.Stocks.AddRange(
                new Stock(p1.Id, warehouseId, 100),
                new Stock(p2.Id, warehouseId, 80));
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
