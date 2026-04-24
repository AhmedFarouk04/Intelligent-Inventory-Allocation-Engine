using InventoryWarehouseSystem.Domain.Entities;
using InventoryWarehouseSystem.SharedKernel.Interfaces;

namespace InventoryWarehouseSystem.Domain.Repositories;

public interface IProductRepository : IRepository<Product>
{
    Task<Product?> GetBySkuAsync(string sku, CancellationToken cancellationToken = default);
}
