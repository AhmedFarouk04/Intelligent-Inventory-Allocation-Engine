using InventoryWarehouseSystem.Domain.Entities;
using InventoryWarehouseSystem.Domain.Repositories;
using InventoryWarehouseSystem.Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace InventoryWarehouseSystem.Infrastructure.Repositories;

public class StockRepository : GenericRepository<Stock>, IStockRepository
{
    public StockRepository(ApplicationDbContext context) : base(context)
    {
    }

    public Task<Stock?> GetByProductAndWarehouseAsync(int productId, int warehouseId, CancellationToken cancellationToken = default)
        => DbSet.FirstOrDefaultAsync(x => x.ProductId == productId && x.WarehouseId == warehouseId, cancellationToken);

    public async Task<IReadOnlyList<Stock>> GetByProductAsync(int productId, CancellationToken cancellationToken = default)
        => await DbSet.Where(x => x.ProductId == productId).ToListAsync(cancellationToken);
}
