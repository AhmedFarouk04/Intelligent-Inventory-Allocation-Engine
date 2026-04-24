using InventoryWarehouseSystem.Domain.Entities;
using InventoryWarehouseSystem.SharedKernel.Specifications;

namespace InventoryWarehouseSystem.Application.Specifications;

public class ProductSpecifications : Specification<Product>
{
    public ProductSpecifications(string? search)
    {
        if (!string.IsNullOrWhiteSpace(search))
        {
            Criteria = x => x.Name.Contains(search) || x.Sku.Value.Contains(search);
        }

        AddOrderBy(x => x.Name);
    }
}
