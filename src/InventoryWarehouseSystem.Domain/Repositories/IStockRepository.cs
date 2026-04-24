using InventoryWarehouseSystem.Domain.Entities;
using InventoryWarehouseSystem.SharedKernel.Interfaces;

namespace InventoryWarehouseSystem.Domain.Repositories;

public interface IStockRepository : IRepository<Stock>
{
    Task<Stock?> GetByProductAndWarehouseAsync(int productId, int warehouseId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Stock>> GetByProductAsync(int productId, CancellationToken cancellationToken = default);
}
