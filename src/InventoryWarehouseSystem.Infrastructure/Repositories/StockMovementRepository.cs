using InventoryWarehouseSystem.Domain.Entities;
using InventoryWarehouseSystem.Domain.Repositories;
using InventoryWarehouseSystem.Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace InventoryWarehouseSystem.Infrastructure.Repositories;

public class StockMovementRepository : GenericRepository<StockMovement>, IStockMovementRepository
{
    public StockMovementRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IReadOnlyList<StockMovement>> GetHistoryAsync(int? productId, int? warehouseId, DateTime? from, DateTime? to, CancellationToken cancellationToken = default)
    {
        var query = DbSet.AsQueryable();

        if (productId.HasValue)
        {
            query = query.Where(x => x.ProductId == productId.Value);
        }

        if (warehouseId.HasValue)
        {
            query = query.Where(x => x.WarehouseId == warehouseId.Value);
        }

        if (from.HasValue)
        {
            query = query.Where(x => x.MovedAt >= from.Value);
        }

        if (to.HasValue)
        {
            query = query.Where(x => x.MovedAt <= to.Value);
        }

        return await query.OrderByDescending(x => x.MovedAt).ToListAsync(cancellationToken);
    }
}
