using InventoryWarehouseSystem.Domain.Entities;
using InventoryWarehouseSystem.Domain.Repositories;
using InventoryWarehouseSystem.Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace InventoryWarehouseSystem.Infrastructure.Repositories;

public class ProductRepository : GenericRepository<Product>, IProductRepository
{
    public ProductRepository(ApplicationDbContext context) : base(context)
    {
    }

    public Task<Product?> GetBySkuAsync(string sku, CancellationToken cancellationToken = default)
        => DbSet.FirstOrDefaultAsync(x => x.Sku.Value == sku.ToUpper(), cancellationToken);
}
