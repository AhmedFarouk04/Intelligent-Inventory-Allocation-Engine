using InventoryWarehouseSystem.Domain.Entities;
using InventoryWarehouseSystem.SharedKernel.Specifications;

namespace InventoryWarehouseSystem.Application.Specifications;

public class StockMovementSpecifications : Specification<StockMovement>
{
    public StockMovementSpecifications(int? productId, int? warehouseId, DateTime? from, DateTime? to)
    {
        Criteria = x =>
            (!productId.HasValue || x.ProductId == productId.Value) &&
            (!warehouseId.HasValue || x.WarehouseId == warehouseId.Value) &&
            (!from.HasValue || x.MovedAt >= from.Value) &&
            (!to.HasValue || x.MovedAt <= to.Value);

        AddOrderByDescending(x => x.MovedAt);
    }
}
