using InventoryWarehouseSystem.Domain.Entities;
using InventoryWarehouseSystem.SharedKernel.Interfaces;

namespace InventoryWarehouseSystem.Domain.Repositories;

public interface IStockMovementRepository : IRepository<StockMovement>
{
    Task<IReadOnlyList<StockMovement>> GetHistoryAsync(int? productId, int? warehouseId, DateTime? from, DateTime? to, CancellationToken cancellationToken = default);
}
